namespace Lab8
{
    public static class Menu
    {
        private static DataBase dataBase = new DataBase();
        static void Main()
        {
            DisplayMainMenu();
        }
        static void DisplayMainMenu()
        {
            var mainMenuActions = new Dictionary<string, (Action, string)>
            {
                { "1", (DisplayUniversityWorkerMenu, "University worker") },
                { "2", (DisplayStudentMenu, "Student") },
                { "0", (() => { }, "Exit") }
            };

            DisplayMenu("Main Menu", mainMenuActions);
        }
        static void DisplayUniversityWorkerMenu()
        {
            var universityWorkerMenuActions = new Dictionary<string, (Action, string)>
            {
                { "1", (DisplayStudentManagementMenu, "Student Management") },
                { "2", (DisplayGroupManagementMenu, "Group Management") },
                { "3", (DisplayDormitoryManagementMenu, "Dormitory Management") },
                { "4", (DisplaySearchMenu, "Search") },
                { "0", (() => { }, "Back") }
            };

            DisplayMenu("University Worker Menu", universityWorkerMenuActions);
        }
        static void DisplayStudentManagementMenu()
        {
            var studentManagementMenuActions = new Dictionary<string, (Action, string)>
            {
                { "1", (() => dataBase.AddStudentsToGroup(), "Add students") },
                { "2", (() => dataBase.DeleteStudentFromGroup(), "Delete students") },
                { "3", (() => dataBase.EditStudent(), "Edit student data") },
                { "4", (() => dataBase.ChangeGroupForStudent(), "Change group for student") },
                { "5", (() => dataBase.ListStudents(), "View the list of students") },
                { "6", (() => dataBase.ViewStudentData(), "View student data") },
                { "0", (() => { }, "Back") }
            };

            DisplayMenu("Student Management Menu", studentManagementMenuActions);
        }
        static void DisplayGroupManagementMenu()
        {
            var groupManagementMenuActions = new Dictionary<string, (Action, string)>
            {
                { "1", (() => dataBase.AddGroup(), "Add group") },
                { "2", (() => dataBase.DeleteGroup(), "Delete group") },
                { "3", (() => dataBase.EditGroup(), "Edit group data") },
                { "4", (() => dataBase.ListGroups(), "View the list of groups") },
                { "5", (() => dataBase.ViewGroupData(), "View group data") },
                { "0", (() => { }, "Back") }
            };

            DisplayMenu("Group Management Menu", groupManagementMenuActions);
        }
        static void DisplayDormitoryManagementMenu()
        {
            var dormitoryManagementMenuActions = new Dictionary<string, (Action, string)>
            {
                { "1", (() => dataBase.AddDormitory(), "Add new dormitory") },
                { "2", (() => dataBase.DeleteDormitory(), "Delete dormitory") },
                { "3", (() => dataBase.EditDormitory(), "Edit dormitory data") }, 
                { "4", (() => dataBase.ListDormitories(), "View the list of dormitories") },
                { "5", (() => dataBase.ViewDormitoryData(), "View dormitory data") },
                { "6", (() => dataBase.AddStudentToDormitory(), "Add student to dormitory") },
                { "7", (() => dataBase.DeleteStudentFromDormitory(), "Delete student from dormitory") }, 
                { "0", (() => { }, "Back") }
            };

            DisplayMenu("Dormitory Management Menu", dormitoryManagementMenuActions);
        }
        static void DisplaySearchMenu()
        {
            const int surnameSearch = 1;
            const int idSearch = 2;
            var searchMenuActions = new Dictionary<string, (Action, string)>
            {
                { "1", (() => dataBase.StudentSearch(surnameSearch, dataBase), "Search for student by surname") }, 
                { "2", (() => dataBase.StudentSearch(idSearch, dataBase), "Search for student by id") }, 
                { "3", (() => dataBase.SearchInGroup(surnameSearch), "Search for student by surname in group") },
                { "4", (() => dataBase.SearchInGroup(idSearch), "Search for student by id in group") },
                { "5", (() => dataBase.SearchInDormitory(surnameSearch), "Search for student by surname in dormitory") },
                { "6", (() => dataBase.SearchInDormitory(idSearch), "Search for student by id in dormitory") },
                { "0", (() => { }, "Back") }
            };

            DisplayMenu("Search Menu", searchMenuActions);
        }
        static void DisplayStudentMenu()
        {
            var studentMenuActions = new Dictionary<string, (Action, string)>
            {
                { "1", (() => dataBase.ViewStudentData(), "View student data")},
                { "2", (() => dataBase.ViewGroupData(), "View group data") },
                { "3", (() => dataBase.ViewDormitoryData(), "View dormitory data")},
                { "0", (() => { }, "Back") }
            };

            DisplayMenu("Student Menu", studentMenuActions);
        }
        static void DisplayMenu(string menuTitle, Dictionary<string, (Action, string)> menuActions)
        {
            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine($"--- {menuTitle} ---\n");

                foreach (var kvp in menuActions)
                {
                    Console.WriteLine($"{kvp.Key}. {kvp.Value.Item2}");
                }

                choice = Console.ReadLine();

                if (menuActions.ContainsKey(choice))
                {
                    menuActions[choice].Item1.Invoke();
                }
                else if (choice != "0")
                {
                    DisplayError("Invalid choice");
                }

            } while (choice != "0");
        }
        static void DisplayError(string message)
        {
            Console.WriteLine($"Error: {message}\nPress any key to continue.");
            Console.ReadKey();
        }
    }
}