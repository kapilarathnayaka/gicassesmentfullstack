namespace CafeEmployee.Dtos
{
    public class UpdateCafeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
