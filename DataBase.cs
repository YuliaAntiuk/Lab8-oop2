#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS8603
namespace Lab8
{
    public class DataBase:ISearch
    {
        private List<string> takenIds = new List<string>();
        private List<Student> Students { get; set; }
        private List<Group> Groups { get; set; }
        private List<Dormitory> Dormitories { get; set; }
        public DataBase()
        {
            Students = new List<Student>();
            Groups = new List<Group>();
            Dormitories = new List<Dormitory>();
        }
        private void PauseProgram(string message)
        {
            Console.WriteLine($"{message}\nPress any key to continue");
            Console.ReadLine();
        }
        private string ReadConsoleInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        private void CheckId(string id)
        {
            if (takenIds.Contains(id))
            {
                throw new Exception("This id is taken");
            } else
            {
                takenIds.Add(id);
            }
        }
        private Student TakeStudentData()
        {
            string studentId = ReadConsoleInput("Enter student id: ");
            CheckId(studentId);
            string name = ReadConsoleInput("Enter name: ");
            string surname = ReadConsoleInput("Enter surname: ");
            string birthDate = ReadConsoleInput("Enter date of birth (yyyy-mm-dd): ");
            string entryDate = ReadConsoleInput("Enter the entry date (yyyy-mm-dd): ");
            return new Student(studentId, name, surname, birthDate, entryDate);
        }
        private Group TakeGroupData()
        {
            string id = ReadConsoleInput("Enter id: ");
            CheckId(id);
            string speciality = ReadConsoleInput("Enter speciality: ");
            string faculty = ReadConsoleInput("Enter faculty: ");
            int year = Convert.ToInt32(ReadConsoleInput("Enter year: "));
            int maxNumOfStudents = Convert.ToInt32(ReadConsoleInput("Enter maximum number of students: "));
            return new Group(id, speciality, faculty, year, maxNumOfStudents);
        }
        private Dormitory TakeDormitoryData()
        {
            string id = ReadConsoleInput("Enter id: ");
            CheckId(id);
            int maxResidents = Convert.ToInt32(ReadConsoleInput("Enter the maximum number of residents: "));
            return new Dormitory(id, maxResidents);
        }
        private Group FindGroup(string id)
        {
            return Groups.FirstOrDefault(group => group.Id == id);
        }
        private Dormitory FindDormitory(string id)
        {
            return Dormitories.FirstOrDefault(dormitory => dormitory.Id == id);
        }
        public void AddStudentsToGroup()
        {
            if(!int.TryParse(ReadConsoleInput("Enter the number of students you want to add: "), out int numOfStudents) || numOfStudents < 1) {
                PauseProgram("Invalid number of students!");
                return;
            }
            string id = ReadConsoleInput("Enter the group id: ");
            Group group = FindGroup(id);
            if(group == null)
            {
                PauseProgram("The group with this id is not found");
                return;
            }
            for (int i = 0; i < numOfStudents; i++)
            {
                 try
                 {
                    Console.WriteLine($"--{i+1}--");
                    Student newStudent = TakeStudentData();
                    group.AddStudent(newStudent);
                    Students.Add(newStudent);
                    Console.WriteLine($"Student {i + 1} was successfully added");
                 } catch (Exception ex) { 
                    PauseProgram(ex.Message);
                    return;
                 }
            }
            PauseProgram($"{numOfStudents} students were added");
        }
        public void DeleteStudentFromGroup()
        {
            string studentId = ReadConsoleInput("Enter student id: ");
            Group studentGroup = FindGroupForStudent(studentId);
            Student student = SearchById(studentId);
            if (student == null || studentGroup == null)
            {
                PauseProgram("The group or student is not found");
                return;
            }
            studentGroup.DeleteStudent(student);
            takenIds.Remove(studentId);
            Students.Remove(student);
            PauseProgram($"Student with id {studentId} was deleted");
        }
        public void EditStudent()
        {
            string id = ReadConsoleInput("Enter student id: ");
            Student student = SearchById(id);
            if (student == null)
            {
                PauseProgram("The group or student is not found");
                return;
            }
            try
            {
                student.EditStudentData();
            } catch (Exception ex)
            {
                PauseProgram(ex.Message);
            }
            PauseProgram("Data was edited");
        }
        public void ChangeGroupForStudent()
        {
            string studentId = ReadConsoleInput("Enter student id: ");
            Student student = SearchById(studentId);
            if (student == null)
            {
                PauseProgram("This student is not found");
            }
            Group oldGroup = FindGroupForStudent(studentId);
            string newGroupId = ReadConsoleInput("Enter new group id: ");
            Group newGroup = FindGroup(newGroupId);
            if (newGroup == null)
            {
                PauseProgram("The group is not found");
                return;
            }
            oldGroup.DeleteStudent(student);
            newGroup.AddStudent(student);
            PauseProgram("The group was changed");
        }
        public void ListStudents()
        {
            List<Student> sortedStudents = Students.OrderBy(s => s.Surname).ToList();
            if(sortedStudents.Count == 0)
            {
                PauseProgram("There are no students");
            }
            else
            {
                for (int i = 0; i < Students.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Students[i].Surname} {Students[i].Name}\t\tid: {Students[i].StudentId}");
                }
                PauseProgram("\n");
            }
        }
        public void ViewStudentData()
        {
            string id = ReadConsoleInput("Enter student id: ");
            Student student = SearchById(id);
            if (student == null)
            {
                PauseProgram("The group or student is not found");
                return;
            }
            Console.WriteLine(student.GetStudentInfo());
            PauseProgram("\n");
        }
        public void AddGroup()
        {
            try
            {
                Group newGroup = TakeGroupData();
                Groups.Add(newGroup);
                PauseProgram("Group was added");
            }
            catch (Exception ex)
            {
                PauseProgram(ex.Message);
                return;
            }
        }
        public void DeleteGroup()
        {
            string groupId = ReadConsoleInput("Enter group id: ");
            Group group = FindGroup(groupId);
            if(group == null)
            {
                PauseProgram("This group does not exist");
            }
            takenIds.Remove(groupId);
            Groups.Remove(group);
            PauseProgram($"Group with id {groupId} was deleted");
        }
        public void EditGroup()
        {
            string id = ReadConsoleInput("Enter group id: ");
            Group group = FindGroup(id);
            if (group == null)
            {
                PauseProgram("This group does not exist");
                return;
            }
            try
            {
                  group.EditData();
            } catch (Exception ex)
            {
                  PauseProgram(ex.Message);
            }
            PauseProgram("Group data was edited\n");
        }
        public void ListGroups()
        {
            List<Group> sortedGroups = Groups.OrderBy(g => g.Year).ToList();
            if (sortedGroups.Count == 0)
            {
                PauseProgram("There are no groups");
            } else
            {
                foreach (Group group in Groups)
                {
                    Console.WriteLine($"Year {group.Year} {group.Id}");
                }
                PauseProgram("\n");
            }
        }
        public void ViewGroupData()
        {
            string id = ReadConsoleInput("Enter group id: ");
            Group group = FindGroup(id);
            if (group == null)
            {
                PauseProgram("This group does not exist");
                return;
            }
            Console.WriteLine(group.GetInfo());
            PauseProgram("\n");
        }
        public void AddDormitory()
        {
            try
            {
                Dormitory newDormitory = TakeDormitoryData();
                Dormitories.Add(newDormitory);
                PauseProgram("Dormitory was added");
            }
            catch (Exception ex)
            {
                PauseProgram(ex.Message);
                return;
            }
        }
        public void DeleteDormitory()
        {
            string dormitoryId = ReadConsoleInput("Enter dormitory id: ");
            Dormitory dormitory = FindDormitory(dormitoryId);
            if (dormitory == null)
            {
                PauseProgram("This dormitory does not exist");
                return;
            }
            takenIds.Remove(dormitoryId);
            Dormitories.Remove(dormitory);
            PauseProgram($"Dormitory with id {dormitoryId} was deleted");
        }
        public void EditDormitory()
        {
            string id = ReadConsoleInput("Enter dormitory id: ");
            Dormitory dormitory = FindDormitory(id);
            if (dormitory == null)
            {
                PauseProgram("This dormitory does not exist");
                return;
            }
            try
            {
                dormitory.EditData();
                PauseProgram("Data was edited");
            }
                catch (Exception ex)
            {
                PauseProgram(ex.Message);
            }
        }
        public void ListDormitories()
        {
            foreach (Dormitory dormitory in Dormitories)
            {
                Console.WriteLine($"{dormitory.Id} - {dormitory.StudentCount}/{dormitory.MaxStudents}, {dormitory.FreeRooms} rooms free");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        public void ViewDormitoryData()
        {
            string id = ReadConsoleInput("Enter dormitory id: ");
            Dormitory dormitory = FindDormitory(id);
            if (dormitory == null)
            {
                PauseProgram("This dormitory does not exist");
                return;
            }
            Console.WriteLine(dormitory.GetInfo());
            PauseProgram("\n");
        }
        public void AddStudentToDormitory()
        {
            if (!int.TryParse(ReadConsoleInput("Enter the number of students you want to add: "), out int numOfStudents) || numOfStudents < 1)
            {
                PauseProgram("Invalid number of students!");
                return;
            }
            string dormitoryId = ReadConsoleInput("Enter dormitory id: ");
            string roomId = ReadConsoleInput("Enter room id: ");
            Dormitory dormitory = FindDormitory(dormitoryId);
            if (dormitory == null)
            {
                PauseProgram("This dormitory does not exist");
                return;
            }
            for(int i = 0; i < numOfStudents; i++)
            {
                string studentId = ReadConsoleInput("Enter student id: ");
                Student newStudent = SearchById(studentId);
                try
                {
                    dormitory.AddStudentToRoom(roomId, newStudent);
                }
                catch (Exception ex)
                {
                    PauseProgram(ex.Message);
                }
            }
            PauseProgram($"{numOfStudents} students were added");
        }
        public void DeleteStudentFromDormitory()
        {
            string dormitoryId = ReadConsoleInput("Enter dormitory id: ");
            string studentId = ReadConsoleInput("Enter student id: ");

            Student student = SearchById(studentId);
            Dormitory dormitory = FindDormitory(dormitoryId);
            if (dormitory == null)
            {
                PauseProgram("This dormitory does not exist");
                return;
            }
            try
            {
                dormitory.DeleteStudentFromRoom(student);
            }
            catch (Exception ex)
            {
                PauseProgram(ex.Message);
            }
            PauseProgram("Student was deleted");
        }
        public void SearchInGroup(int option)
        {
            string id = ReadConsoleInput("Enter group id: ");
            Group group = FindGroup(id);
            if (group == null)
            {
                PauseProgram("This group does not exist");
                return;
            }
            StudentSearch(option, group);
        }
        public void SearchInDormitory(int option)
        {
            string id = ReadConsoleInput("Enter dormitory id: ");
            Dormitory dormitory = FindDormitory(id);
            if (dormitory == null)
            {
                PauseProgram("This dormitory does not exist");
                return;
            }
            StudentSearch(option, dormitory);
        }
        public void StudentSearch(int option, ISearch searchable)
        {
            switch (option)
            {
                case 1:
                    string surname = ReadConsoleInput("Enter student surname: ");
                    List<Student> foundStudents = searchable.SearchBySurname(surname);
                    if (foundStudents.Count == 0)
                    {
                        Console.WriteLine("There are no students found with this surname");
                    }
                    else
                    {
                        foreach (Student student in foundStudents)
                        {
                            Console.WriteLine(student.GetStudentInfo());
                        }
                    }
                    break;
                case 2:
                    string id = ReadConsoleInput("Enter student id: ");
                    Student studentFound = searchable.SearchById(id);
                    if (studentFound != null)
                    {
                        Console.WriteLine(studentFound.GetStudentInfo());
                    }
                    else
                    {
                        Console.WriteLine("Student is not found");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            PauseProgram("\n");
        }
        public Group FindGroupForStudent(string Id)
        {
            foreach(Group group in Groups)
            {
                foreach (Student student in group.Students)
                {
                    if(student.StudentId == Id)
                    {
                        return group;
                    }
                }
            }
            return null;
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
