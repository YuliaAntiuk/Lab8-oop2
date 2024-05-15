namespace Lab8
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public int MaxStudents { get; set; }
        public int StudentCount { get; set; }
        public List<Student> Students { get; set; }
        public Entity(string id, int maxStudents) {
            if (!IsIdValid(id))
            {
                throw new ArgumentException("Invalid id!");
            } else
            {
                Id = id;
            }
            if (maxStudents <= 0)
            {
                throw new ArgumentException("The group can not have 0 or less maximum students");
            }
            MaxStudents = maxStudents;
            Students = new List<Student>();
            StudentCount = 0;
        }
        public bool IsFull()
        {
            return StudentCount >= MaxStudents;
        }
        public void AddStudent(Student newStudent)
        {
            if (StudentCount < MaxStudents)
            {
                Students.Add(newStudent);
                StudentCount++;
            }
            else
            {
                throw new Exception("The maximum number of students is reached");
            }
        }
        public void DeleteStudent(Student student)
        {
            Students.Remove(student);
            StudentCount--;
        }
        public abstract bool IsIdValid(string id);
        public abstract string GetInfo();
        public abstract void EditData();
    }
}
