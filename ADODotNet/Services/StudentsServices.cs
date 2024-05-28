using ADODotNet.Models;
using Microsoft.Data.SqlClient;

namespace ADODotNet.Services
{
    public class StudentsServices : IStudentsServices
    {
        private readonly IConfiguration configuration;
        private string ConnectionString { get; set; }
        public StudentsServices(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.ConnectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
    public List<Students> GetStudents()
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
                SqlCommand cmd = new SqlCommand("select * from students", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Students> studs = new List<Students>();
                while (reader.Read())
                {
                    Students student = new Students();
                    student.id = Convert.ToInt32(reader["id"]);
                    student.stud_name = reader["student_name"].ToString();
                    student.course = reader["course"].ToString();
                    studs.Add(student);
                }
                return studs;
        }
    }
        public Students GetbyId(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("find", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                Students student = new Students();
                if(reader.Read())
                {
                    student.id = (int)reader["id"];
                    student.stud_name = (string)reader["student_name"];
                    student.course = (string)reader["course"];
                }
                return student;
            }
        }
        public void AddStudents(Students student)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into students (id, student_name, course) values (@id, @student_name, @course)", con);
                cmd.Parameters.AddWithValue("@id", student.id);
                cmd.Parameters.AddWithValue("@student_name", student.stud_name);
                cmd.Parameters.AddWithValue("@course", student.course);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        } 
        public void UpdateStudents(int id, Students student)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand($"update students set student_name=@student_name, course=@course where id={id}", con);
                cmd.Parameters.AddWithValue("@student_name", student.stud_name);
                cmd.Parameters.AddWithValue("@course", student.course);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveStudents (int id)
        {
            using (SqlConnection con = new SqlConnection (ConnectionString))
            {
                SqlCommand cmd = new SqlCommand($"delete from students where id={id} ", con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }

}
