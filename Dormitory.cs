using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab8
{
    public class Dormitory : Entity, IEnumerable<Room>, ISearch
    {
        private List<Room> Rooms {  get; set; }
        public int FreeRooms { get; set; }
        public Dormitory(string id, int maxStudents) : base(id, maxStudents)
        {
            Rooms = new List<Room>();
            FreeRooms = Rooms.Count;
        }
        private void UpdateFreeRooms()
        {
            FreeRooms = 0;
            foreach (Room room in Rooms)
            {
                if (!room.IsFull())
                {
                    FreeRooms++;
                }
            }
        }
        private Room TakeRoomData()
        {
            Console.Write("Enter the room id: ");
            string id = Console.ReadLine();

            Console.Write("Enter the maximum number of residents: ");
            int roomMaxResidents = Convert.ToInt32(Console.ReadLine());

            return new Room(id, roomMaxResidents);
        }
        public override bool IsIdValid(string id)
        {
            string pattern = @"^\d{1,2}$";
            return Regex.IsMatch(id, pattern);
        }
        private int GetSumOfMaxRoomResidents()
        {
            int sum = 0;
            foreach(Room room in Rooms)
            {
                sum += room.MaxStudents;
            }
            return sum;
        }
        public override void EditData()
        {
            Console.WriteLine("1. Add room\n" +
                "2. Delete room\n" +
                "3. Edit maximum number of residents\n" +
                "4. Edit maximum number of room residents\n");
            int ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    try
                    {
                        Room newRoom = TakeRoomData();
                        if(GetSumOfMaxRoomResidents() + newRoom.MaxStudents > this.MaxStudents)
                        {
                            throw new ArgumentException("The dormitory is full");
                        }
                        Rooms.Add(newRoom);
                        UpdateFreeRooms();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case 2:
                    Console.Write("Enter the room id: ");
                    string deletionId = Console.ReadLine();
                    foreach (Room room in Rooms)
                    {
                        if (room.Id == deletionId)
                        {
                            Rooms.Remove(room);
                            UpdateFreeRooms();
                            this.StudentCount -= room.StudentCount;
                        }
                    }
                    break;
                case 3:
                    Console.Write("Enter new maximum number of residents: ");
                    int maxResidents = Convert.ToInt32(Console.ReadLine());
                    if (maxResidents > 0)
                    {
                        MaxStudents = maxResidents;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid number of residents");
                    }
                    break;
                case 4:
                    Console.Write("Enter room id: ");
                    string roomId = Console.ReadLine();
                    foreach (Room room in Rooms)
                    {
                        if (room.Id == roomId)
                        {
                            room.EditData();
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid choice!");
            }
        }
        public void AddStudentToRoom(string roomId, Student student)
        {
            Room targetRoom = Rooms.FirstOrDefault(room => room.Id == roomId);
            if(targetRoom == null)
            {
                throw new Exception("Room with this id is not found");
            } else
            {
                targetRoom.AddStudent(student);
                Students.Add(student);
                UpdateFreeRooms();
            }
        }
        public void DeleteStudentFromRoom(Student student)
        {
            foreach (Room room in Rooms)
            {
                if(room.Students.Contains(student)){
                    room.DeleteStudent(student);
                    Students.Remove(student);
                }
            }
            UpdateFreeRooms();
        }
        public override string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Dormitory {Id} {Rooms.Count}  rooms");
            sb.AppendLine($"{FreeRooms} are free");
            sb.AppendLine($"There are {StudentCount} in the dormitory");
            sb.AppendLine($"The maximun number of residents is {MaxStudents}");
            for(int i = 0; i < Rooms.Count; i++)
            {
                sb.AppendLine(Rooms[i].GetInfo());
            }
            return sb.ToString();
        }
        public IEnumerator<Room> GetEnumerator()
        {
            return Rooms.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

