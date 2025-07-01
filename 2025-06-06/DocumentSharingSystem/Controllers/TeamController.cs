using System.Security.Claims;
using AutoMapper;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;
using DocumentSharingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentSharingSystem.Controllers
{
    [Route("api/v1/teams")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private TeamService _teamService;
        private IMapper _mapper;
        private CustomResponseGeneration _res;
        public TeamController(TeamService teamService, CustomResponseGeneration customResponseGeneration, IMapper mapper)
        {
            _res = customResponseGeneration;
            _teamService = teamService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResponseDTO<List<TeamResponseDTO>>>> GetAll()
        {
            List<Team> teams;
            if (User.FindFirstValue(ClaimTypes.Role) == "Admin")
            {
                teams = (await _teamService.GetAll_Admin()).ToList();
            }
            else
            {
                teams = (await _teamService.GetAll()).ToList();
            }

            if (teams == null)
                return NotFound(
                    new CustomResponseDTO<string>
                    {
                        Success = false,
                        Data = null,
                        Message = "No teams found",
                        ResultsCount = 0,
                        Errors = new ErrorDTO { type = "Not found", message = "No teams found" }
                    }
                );
            var teamsDTO = teams.Select(t => _mapper.Map<Team, TeamResponseDTO>(t)).ToList();
            var res = _res.Generate<List<TeamResponseDTO>>(teamsDTO, "Succesfully fetched Teams");
            return Ok(res);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("filter")]
        public async Task<ActionResult<CustomResponseDTO<List<TeamResponseDTO>>>> GetFilter(string? searchQuery)
        {
            List<Team> teams = await _teamService.GetFilter_Admin(searchQuery);

            if (teams == null)
                return NotFound(
                    new CustomResponseDTO<string>
                    {
                        Success = false,
                        Data = null,
                        Message = "No teams found",
                        ResultsCount = 0,
                        Errors = new ErrorDTO { type = "Not found", message = "No teams found" }
                    }
                );
            var teamsDTO = teams.Select(t => _mapper.Map<Team, TeamResponseDTO>(t)).ToList();
            var res = _res.Generate<List<TeamResponseDTO>>(teamsDTO, "Succesfully fetched Teams");
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CustomResponseDTO<TeamResponseDTO>>> Add(string name)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var team = await _teamService.AddTeam(name, Guid.Parse(userId!));
            var teamDTO = _mapper.Map<Team, TeamResponseDTO>(team);
            var res = _res.Generate<TeamResponseDTO>(teamDTO, "Team added Succesfully");
            return Ok(res);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomResponseDTO<TeamResponseDTO>>> Update(long id, string name)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var team = await _teamService.UpdateTeam(id,name, Guid.Parse(userId!));
            var teamDTO = _mapper.Map<Team, TeamResponseDTO>(team);
            var res = _res.Generate<TeamResponseDTO>(teamDTO, "Team updated Succesfully");
            return Ok(res);

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResponseDTO<TeamResponseDTO>>> Delete(long id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var team = await _teamService.DeleteTeam(id, Guid.Parse(userId!));
            var teamDTO = _mapper.Map<Team, TeamResponseDTO>(team);
            var res = _res.Generate<TeamResponseDTO>(teamDTO, "Team deleted Succesfully");
            return Ok(res);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("restore/{id}")]
        public async Task<ActionResult<CustomResponseDTO<TeamResponseDTO>>> Restore(long id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var team = await _teamService.RestoreTeam(id, Guid.Parse(userId!));
            var teamDTO = _mapper.Map<Team, TeamResponseDTO>(team);
            var res = _res.Generate<TeamResponseDTO>(teamDTO, "Team restored Succesfully");
            return Ok(res);

        }
    }
}
