using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;
using DocumentSharingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentSharingSystem.Controllers
{
    [Route("api/teams")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private TeamService _teamService;
        private CustomResponseGeneration _res;
        public TeamController(TeamService teamService, CustomResponseGeneration customResponseGeneration)
        {
            _res = customResponseGeneration;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResponseDTO<List<Team>>>> GetAll()
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
            var res = _res.Generate<List<Team>>(teams, "Succesfully fetched Teams");
            return Ok(res);
        }
    }
}
