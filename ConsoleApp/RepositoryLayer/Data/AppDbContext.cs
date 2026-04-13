

using DomainLayer.Entities;

namespace RepositoryLayer.Data
{
    public static class AppDbContext
    {
        public static List<CourseGroup> Groups {  get; set; }
        public static List<Student> Students {  get; set; }

        static AppDbContext()
        {
            Groups = new List<CourseGroup>();
            Students = new List<Student>();
        }
    }
}
