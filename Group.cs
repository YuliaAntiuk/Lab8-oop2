using System.Text;
using System.Text.RegularExpressions;

namespace Lab8
{
    public class Group : Entity, ISearch
    {
        private string Specialty { get; set; }
        private string Faculty { get; set; }
        public int Year { get; set; }
        public Group(string id, string specialty, string faculty, int year, int maxStudents):base(id, maxStudents) {
            if (IsFacultyOrSpecialtyValid(faculty) && IsFacultyOrSpecialtyValid(specialty))
            {
                Specialty = specialty;
                Faculty = faculty;
            }
            else
            {
                throw new ArgumentException("Invalid faculty or specialty");
            }
            if(year > 0 && year < 6)
            {
                Year = year;
            } else
            {
                throw new ArgumentException("Invalid year");
            }
        }
        public override bool IsIdValid(string id)
        {
            string pattern = @"^[A-Za-z]{2}-\d{2}$";
            return Regex.IsMatch(id, pattern);
        }
        private bool IsFacultyOrSpecialtyValid(string facultyOrSpecialty)
        {
            foreach (char c in facultyOrSpecialty)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    return false;
            }
            return true;
        }
        public override void EditData()
        {
            Console.WriteLine("1. Change speciality\n" +
                "2. Change faculty\n" +
                "3. Change year\n");
            int ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    Console.Write("Enter the new speciality: ");
                    string specialty = Console.ReadLine();
                    if (IsFacultyOrSpecialtyValid(specialty))
                    {
                        Specialty = specialty;
                    }
                    else
                    {
                        throw new ArgumentException("Specialty is invalid");
                    }
                    break;
                case 2:
                    Console.Write("Enter the new faculty: ");
                    string faculty = Console.ReadLine();
                    if (IsFacultyOrSpecialtyValid(faculty))
                    {
                        Faculty = faculty;
                    }
                    else
                    {
                        throw new ArgumentException("Faculty is invalid");
                    }
                    break;
                case 3:
                    Console.Write("Enter the new year: ");
                    int year = Convert.ToInt32(Console.ReadLine());
                    if (year > 0 && year < 7)
                    {
                        Year = year;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid year");
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid choice!");
            }
        }
        public override string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Id} - {Faculty} faculty, {Specialty} speciality - year {Year} - {StudentCount} of {MaxStudents} students");
            if(Students.Count > 0)
            {
                sb.AppendLine("List of students in this group: ");
                for (int i = 0; i < StudentCount; i++)
                {
                    sb.AppendLine($"{i + 1}. {Students[i].Surname} {Students[i].Name}\t\tid: {Students[i].StudentId}");
                }
            } 
            return sb.ToString();
        }
        public List<Student> SearchBySurname(string surname)
        {
            return Students.Where(student => student.Surname == surname).ToList();
        }
        public Student SearchById(string id)
        {
            return Students.FirstOrDefault(student => student.StudentId == id);
        }
    }
}

