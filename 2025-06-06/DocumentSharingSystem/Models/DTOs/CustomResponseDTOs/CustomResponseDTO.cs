namespace DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;

public class CustomResponseDTO<D>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public int ResultsCount { get; set; } = 1;
    public D? Data { get; set; }
    public ErrorDTO? Errors { get; set; }
}