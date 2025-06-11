using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using DocumentSharingSystem.Misc;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Services;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;
using DocumentSharingSystem.Models.DTOs;
using AutoMapper;

namespace DocumentSharingSystem.Controllers
{
    [Route("api/v1/documents")]
    [ApiController]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly DocumentService _documentService;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly CustomResponseGeneration _res;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public DocumentController(UserService userService,
                                    DocumentService documenService,
                                    IHubContext<NotificationHub> hubContext,
                                    CustomResponseGeneration customResponseGeneration,
                                    IMapper mapper,
                                    IConfiguration configuration)
        {
            _userService = userService;
            _documentService = documenService;
            _notificationHub = hubContext;
            _res = customResponseGeneration;
            _mapper = mapper;
            _config = configuration;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<CustomResponseDTO<DocumentReponseDTO>>> Upload(IFormFile formFile)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                if (User == null || email == null) return Unauthorized("User not Authenticated");
                var user = await _userService.GetUserByEmail(email);

                DateTime currentTime = DateTime.UtcNow;
                string ext = formFile.FileName.Split(".").LastOrDefault() ?? "txt";
                Document doc = new Document
                {
                    Id = Guid.NewGuid(),
                    StoredFileName = $"{user.Id}_{currentTime.Ticks}.{ext}",
                    OriginalFileName = formFile.FileName,
                    CreatedByUserId = user.Id,
                    CreatedAt = currentTime,
                    LastUpdatedByUserId = user.Id,
                    LastUpdatedAt = currentTime
                };
                doc = await _documentService.AddDocument(doc);

                string path = $"{_config["Directory"]}/{doc.StoredFileName}";
                using (var stream = System.IO.File.Create(path))
                {
                    await formFile.CopyToAsync(stream);
                }

                await _notificationHub.Clients.All.SendAsync("RecieveMessage", user.Name, $"Uploaded Document : {doc.Id}");
                DocumentReponseDTO docDTO = _mapper.Map<Document, DocumentReponseDTO>(doc);
                return Created("",_res.Generate<DocumentReponseDTO>(docDTO, "Document uploaded successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Request\n" + ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<CustomResponseDTO<List<DocumentReponseDTO>>>> ViewAll()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            ICollection<Document> docs;
            if (role == "Admin")
                docs = await _documentService.GetAll_Admin();
            else if (role == "User")
                docs = await _documentService.GetAll();
            else
                return Unauthorized("UnAuthorized Access");

            var docDTOs = docs.Select(d => _mapper.Map<Document, DocumentReponseDTO>(d));
            // return Ok(docs.ToList());
            return Ok(_res.Generate<List<DocumentReponseDTO>>(docDTOs.ToList(), "Documents fetched successfully"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDocument(Guid id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            Document? doc;
            if (role == "Admin")
            {
                doc = await _documentService.GetDocument_Admin(id);
            }
            else if (role == "User")
            {
                doc = await _documentService.GetDocument(id);
            }
            else
            {
                return Unauthorized("UnAuthorized Access");
            }
            if (doc == null) return NotFound("No data record found");

            var file = $"{_config["Directory"]}/{doc.StoredFileName}";
            if (file == null) throw new Exception("No document found");

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) throw new Exception("Unauthrized Access");


            var user = await _userService.GetUserByEmail(email);
            await _notificationHub.Clients.All.SendAsync("RecieveMessage", user.Name, $"Viewed Document - {doc.Id}");

            return File(new FileStream(file, FileMode.Open), "application/octet-stream", doc.OriginalFileName);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "SpecifiedOwnerOrAdmin")]
        public async Task<ActionResult<CustomResponseDTO<DocumentReponseDTO>>> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Unauthorized Access");
            Document doc = await _documentService.DeleteDocument(id, Guid.Parse(userId));
            var docDTO = _mapper.Map<Document, DocumentReponseDTO>(doc);
            return _res.Generate<DocumentReponseDTO>(docDTO, "User deleted successfully");
        }



        [HttpGet("page")]
        public async Task<ActionResult<CustomPaginationDTO<DocumentReponseDTO>>> GetWithPagination(int pageNo=1, int pageSize=10)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            try
            {
                if (role == "Admin")
                {
                    var documents_dto = await _documentService.DocumentsPagination_Admin(pageNo, pageSize);
                    if (documents_dto == null || documents_dto.Data == null ) throw new Exception("No records found");
                    var docDTOs = documents_dto.Data.Select(d => _mapper.Map<Document, DocumentReponseDTO>(d));
                    var docRes =_res.GeneratePagination_Document(docDTOs.ToList(), pageNo, pageSize, documents_dto.TotalRecords, "Succesfully fetched");
                    return Ok(docRes);
                }
                else if (role == "User")
                {
                    {
                    var documents_dto = await _documentService.DocumentsPagination(pageNo, pageSize);
                    if (documents_dto == null || documents_dto.Data == null ) throw new Exception("No records found");
                    var docDTOs = documents_dto.Data.Select(d => _mapper.Map<Document, DocumentReponseDTO>(d));

                    var docRes =_res.GeneratePagination_Document(docDTOs.ToList(), pageNo, pageSize, documents_dto.TotalRecords, "Succesfully fetched");

                    return Ok(docRes);
                    }
                }
                else
                    throw new Exception("UnAuthorized User");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("filter")]
        public async Task<ActionResult<CustomResponseDTO<List<DocumentReponseDTO>>>> Filter([FromBody] DocumentFilterModel filter)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            var docs = await _documentService.Filter(filter, role!);
            // return Ok(docs);
            var docDTOs = docs.Select(d => _mapper.Map<Document, DocumentReponseDTO>(d));
            // return Ok(docs.ToList());
            return Ok(_res.Generate<List<DocumentReponseDTO>>(docDTOs.ToList(), "Documents fetched successfully"));
        }
    }
}
