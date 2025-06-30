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
            var teams = (await _teamService.GetAll()).ToList();
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
    }
}
