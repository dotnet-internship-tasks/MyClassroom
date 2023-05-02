namespace MyClassroom.Core.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int TemplateId { get; set; }
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; } = null!;
    }
}
