// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var unit = new UnitOfWork();

unit.Users.Add(new User { Id = 1, Name = "Alice" });
unit.Orders.Add(new Order { Id = 101, ProductName = "Laptop", UserId = 1 });

unit.Commit();  // 여기


public class User { public int Id; public string Name; }
public class Order { public int Id; public string ProductName; public int UserId; }

public interface IUserRepository { void Add(User user); }
public interface IOrderRepository { void Add(Order order); }

public class UserRepository : IUserRepository
{
    private readonly List<User> _context;
    public UserRepository(List<User> context) => _context = context;
    public void Add(User user) => _context.Add(user);
}

public class OrderRepository : IOrderRepository
{
    private readonly List<Order> _context;
    public OrderRepository(List<Order> context) => _context = context;
    public void Add(Order order) => _context.Add(order);
}

public class UnitOfWork
{
    private readonly List<User> _userContext = new();
    private readonly List<Order> _orderContext = new();

    public IUserRepository Users { get; }
    public IOrderRepository Orders { get; }

    public UnitOfWork()
    {
        Users = new UserRepository(_userContext);
        Orders = new OrderRepository(_orderContext);
    }

    public void Commit()
    {
        // 여기서 실제 DB 저장 로직이 들어감 (트랜잭션)
        Console.WriteLine("Users: " + _userContext.Count);
        Console.WriteLine("Orders: " + _orderContext.Count);
    }
}