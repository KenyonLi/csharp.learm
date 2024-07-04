namespace Lifetime.Demo.IServices
{
    public interface IUserService
    {
        void Show();
    }
    public class UserService : IUserService
    {
        public void Show()
        {
            Console.WriteLine("UserService");
        }
    }

    public class UserServiceEx : IUserService
    {
        public void Show()
        {
            Console.WriteLine("UserServiceEx");
        }
    }
}
