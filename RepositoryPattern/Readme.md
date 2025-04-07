# 1. Repository Pattern (리포지토리 패턴)
## 개념
데이터 소스(예: DB, XML, 메모리 등)에 접근하는 로직을 캡슐화하는 패턴.

WinForms에서는 DB 연결 코드를 Form 안에 직접 넣기보다, Repository 클래스에서 분리해서 관리하는 데에 자주 사용됨.

## 왜 쓰는가?
관심사의 분리 (Separation of Concerns)
- UI와 DB 로직을 분리해서 코드 유지보수성을 높임

유닛 테스트 가능성 향상
- DB 없이도 가짜 Repository(Mock)로 테스트 가능

## 구조 예시 (간단한 사용자 정보 조회 예제)
### 1. 모델 (Entity)
```cs
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```
### 2. 인터페이스 정의
```cs
public interface IUserRepository
{
    List<User> GetAll();
    User GetById(int id);
}
```
### 3. 실제 구현
```cs
public class UserRepository : IUserRepository
{
    public List<User> GetAll()
    {
        // 예: DB에서 불러오는 코드 대신 샘플 데이터
        return new List<User>
        {
            new User { Id = 1, Name = "Alice" },
            new User { Id = 2, Name = "Bob" }
        };
    }

    public User GetById(int id)
    {
        return GetAll().FirstOrDefault(u => u.Id == id);
    }
}
```
### 4. WinForm에서 사용하는 예
```cs
public partial class MainForm : Form
{
    private readonly IUserRepository _userRepository;

    public MainForm()
    {
        InitializeComponent();
        _userRepository = new UserRepository();  // 의존성 주입 없이 단순 생성

        var users = _userRepository.GetAll();
        listBox1.DataSource = users;
        listBox1.DisplayMember = "Name";
    }
}
```
### 요약
Repository 패턴은 "DB 접근을 모듈화" 하고, Form에서 직접 DB 코드를 안 쓰게 해주는 역할

나중에 UserRepository만 수정하면 DB에서 → API로 바꾸더라도 Form은 건드릴 필요 없음