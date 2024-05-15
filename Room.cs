using System.Text;
using System.Text.RegularExpressions;

namespace Lab8
{
    public class Room:Entity
    {
        public Room(string id, int maxStudents) : base(id, maxStudents) { }
        public override bool IsIdValid(string id)
        {
            string pattern = @"^\d{1,3}$";
            return Regex.IsMatch(id, pattern);
        }
        public override void EditData()
        {
            Console.Write($"Enter new maximum number of residents in room {Id}: ");
            int maxResidents = Convert.ToInt32(Console.ReadLine());
            if (maxResidents > 0)
            {
                MaxStudents = maxResidents;
            }
            else
            {
                throw new ArgumentException("Invalid number of residents");
            }
        }
        public override string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Room {Id} {StudentCount}/{MaxStudents} residents");
            if (Students.Count > 0)
            {
                sb.AppendLine("List of students in this room: ");
                foreach (Student student in this.Students)
                {
                    sb.AppendLine($"{student.Surname} {student.Name}\t\tid: {student.StudentId}");
                }
            }
            return sb.ToString();
        }
    }
}
