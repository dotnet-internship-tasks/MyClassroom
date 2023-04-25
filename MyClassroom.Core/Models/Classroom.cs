namespace MyClassroom.Core.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int OrganizationId { get; set; }
    }
}
