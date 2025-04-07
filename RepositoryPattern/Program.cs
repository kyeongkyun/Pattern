// See https://aka.ms/new-console-template for more information
using RepositoryPattern;

Console.WriteLine("Hello, World!");
Main main = new Main();


public class Main
{
     private readonly IUserRepository _userRepository;

    public Main()
    {
        _userRepository = new UserRepository();
        var users = _userRepository.GetAllUsers();
        foreach (var user in users)
        {
            Console.WriteLine($"Id: {user.Id}, Name: {user.Name}");
        }
    }
}