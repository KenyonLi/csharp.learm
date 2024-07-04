namespace Lifetime.Demo.IServices
{
    public interface IStudentService
    {
        public void Show();
    }

    public class StudentService : IStudentService
    {
        public void Show()
        {
            Console.WriteLine("UserService");
        }
    }
    public class StudentServiceEx : IStudentService
    {
        public void Show()
        {
            Console.WriteLine("StudentServiceEx");
        }
    }
}
