using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Misc;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;

namespace DocumentSharingSystem.Services;

public class CustomResponseGeneration
{
    private readonly UserService _userService;
    private readonly DocumentService _documentService;
    private readonly PaginationContextFns _paginationContextFns;
    public CustomResponseGeneration(UserService userSerivce,DocumentService documentService, PaginationContextFns paginationContextFns)
    {
        _userService = userSerivce;
        _documentService = documentService;
        _paginationContextFns = paginationContextFns;
    }
    public CustomResponseDTO<T> Generate<T>(T obj, string message)
    {
        int count = 1;
        if (obj is ICollection collection)
        {
            var countProperty = typeof(T).GetProperty("Count");
            if (countProperty != null)
            {
                var objCount = countProperty.GetValue(obj);
                count = Convert.ToInt32(objCount);  
            }
        }
        return new CustomResponseDTO<T>
        {
            Message = message,
            ResultsCount = count,
            Data = obj
        };
    }
    public CustomPaginationDTO<UserResponseDTO> GeneratePagination_User(ICollection<UserResponseDTO> obj, int pageNo, int pageSize, int totalRecords, string message)
    {
        int count = obj.Count();
        if (count != 0)
        {
            return new CustomPaginationDTO<UserResponseDTO>
            {
                Message = message,
                ResultsCount = count,
                Data = obj.ToList(),
                Pagination = new PaginationDTO
                {
                    TotalRecords = totalRecords,
                    Page = pageNo,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double) totalRecords / pageSize)
                }
            };
        }
        return new CustomPaginationDTO<UserResponseDTO>
        {
            Success= false,
            Message = "No records found",
            ResultsCount = 0,
            Data = null,
            Pagination = new PaginationDTO
                {
                    TotalRecords = totalRecords,
                    Page = pageNo,
                    PageSize = pageSize,
                    TotalPages = 0
                }
        };
    }
    public CustomPaginationDTO<DocumentReponseDTO> GeneratePagination_Document(ICollection<DocumentReponseDTO> obj, int pageNo, int pageSize, int totalRecords, string message)
    {
        int count = obj.Count();
        if (count != 0)
        {
            return new CustomPaginationDTO<DocumentReponseDTO>
            {
                Message = message,
                ResultsCount = count,
                Data = obj.ToList(),
                Pagination = new PaginationDTO
                {
                    TotalRecords = totalRecords,
                    Page = pageNo,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double) totalRecords / pageSize)
                }
            };
        }
        return new CustomPaginationDTO<DocumentReponseDTO>
        {
            Success= false,
            Message = "No records found",
            ResultsCount = 0,
            Data = null,
            Pagination = new PaginationDTO
                {
                    TotalRecords = totalRecords,
                    Page = pageNo,
                    PageSize = pageSize,
                    TotalPages = 0
                }
        };
    }
}