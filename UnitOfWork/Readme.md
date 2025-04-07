# 2. Unit of Work Pattern (작업 단위 패턴)
## 개념
여러 개의 Repository 작업을 하나의 트랜잭션 단위로 묶어서 관리하는 패턴.

흔히 SaveChanges() 하나로 DB 작업을 일괄 처리할 수 있도록 하는 구조.

주로 Entity Framework에서 사용

## 왜 쓰는가?
데이터 일관성 유지: 여러 개 객체를 수정했을 때, 중간에 오류가 나면 전체 롤백 가능

DB 호출을 최소화: 변경된 항목만 한 번에 반영 (트랜잭션 단위 처리)

## 구조 예시 (사용자 + 주문 정보를 같이 저장한다고 가정)
### 1. 모델 (User, Order)
```cs
public class User { public int Id; public string Name; }
public class Order { public int Id; public string ProductName; public int UserId; }
```
### 2. 각 리포지토리
```cs
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
```
### 3. UnitOfWork 클래스
```cs
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
```
### 4. WinForm에서 사용하는 예
```cs
private void btnSave_Click(object sender, EventArgs e)
{
    var unit = new UnitOfWork();

    unit.Users.Add(new User { Id = 1, Name = "Alice" });
    unit.Orders.Add(new Order { Id = 101, ProductName = "Laptop", UserId = 1 });

    unit.Commit();  // 여기서 모든 저장을 한 번에 처리
}
```
## 요약
Unit of Work는 "변경사항을 모아서 한 번에 처리"하는 구조

WinForms에서 직접 DB 호출할 일이 있을 때, Repository + UnitOfWork 조합으로 구성하면 트랜잭션 처리, 롤백, 테스트가 쉬워짐