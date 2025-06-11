namespace DocumentSharingSystem.Models.DTOs
{
    public class PaginationDataDTO<T>
    {
        public List<T>? Data { get; set; }
        public int TotalRecords{ get; set; }
    }
}