namespace DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;

public class PaginationDTO
{
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}