namespace ShapeBuilder.Types.Models
{
    public class ShapeData
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public bool Match { get; set; }
        public string? Message { get; set; }
        public List<DataPoint>? DataPoints { get; set; }
    }
}
