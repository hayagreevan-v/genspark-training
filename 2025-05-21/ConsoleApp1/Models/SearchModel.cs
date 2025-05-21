namespace ConsoleApp1.Models
{
    public class SearchModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public Range<int>? Age { get; set; }
        public Range<double>? Salary { get; set; }
    }

    public class Range<T>
    {
        public T? MinVal { get; set; }
        public T? MaxVal { get; set; }
    }
}