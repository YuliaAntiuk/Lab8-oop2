using System;
using System.Text.RegularExpressions;

namespace Lab8
{
    public class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        private DateTime DateOfBirth { get; set; }
        private DateTime EntryDate { get; set; }
        public Student(string id, string name, string surname, string birthDate, string entryDate)
        {
            if (!IsIdValid(id))
            {
                throw new ArgumentException("Id is invalid");
            } else
            {
                StudentId = id;
            }
            if(IsNameOrSurnameValid(name) && IsNameOrSurnameValid(surname)) { 
                Name = name;
                Surname = surname;
            } else
            {
                throw new ArgumentException("Invalid name or surname");
            }
            if (!DateTime.TryParse(birthDate, out DateTime dateBirth) || !DateTime.TryParse(entryDate, out DateTime dateEntry))
            {
                throw new ArgumentException("Invalid date format for the date of birth");
            } else
            {
                DateOfBirth = dateBirth;
                EntryDate = dateEntry;
            }
        }
        private bool IsIdValid(string id)
        {
            string pattern = @"^[a-zA-Z]{2}\d{8}$";
            return Regex.IsMatch(id, pattern);
        }
        private bool IsNameOrSurnameValid(string nameOrSurname)
        {
            if (nameOrSurname.Length > 20)
                return false;

            foreach (char c in nameOrSurname)
            {
                if (!char.IsLetter(c))
                    return false;
            }

            return true;
        }
        public string GetStudentInfo()
        {
            return ($"{StudentId} - {Surname} {Name} - {DateOfBirth.ToString("yyyy-MM-dd")} - {EntryDate.ToString("yyyy-MM-dd")}");
        }
        public void EditStudentData()
        {
            Console.WriteLine("1. Change name\n" +
                "2. Change surname\n");
            int ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    Console.Write("Enter the new name: ");
                    string name = Console.ReadLine();
                    if (IsNameOrSurnameValid(name))
                    {
                        Name = name;
                    } else
                    {
                        throw new ArgumentException("Name is invalid");
                    }
                    break;
                case 2:
                    Console.Write("Enter the new surname: ");
                    string surname = Console.ReadLine();
                    if (IsNameOrSurnameValid(surname))
                    {
                        Surname = surname;
                    }
                    else
                    {
                        throw new ArgumentException("Surname is invalid");
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid choice!");
            }
        }
    }
}
