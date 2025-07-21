public class TrainingVideoAddRequestDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? Video { get; set; }
}