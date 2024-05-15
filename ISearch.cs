namespace Lab8
{
    public interface ISearch
    {
        public List<Student> SearchBySurname(string surname);
        public Student SearchById(string id);
    }
}
