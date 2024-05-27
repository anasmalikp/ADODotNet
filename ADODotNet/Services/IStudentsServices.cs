using ADODotNet.Models;

namespace ADODotNet.Services
{
    public interface IStudentsServices
    {
        public List<Students> GetStudents();
        public Students GetbyId(int id);
        public void AddStudents(Students student);
        public void UpdateStudents(int id, Students student);
        public void RemoveStudents(int id);
    }
}
