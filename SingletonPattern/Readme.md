
# 3. Singleton Pattern (싱글톤 패턴)
## 개념
앱 전체에서 하나만 존재해야 하는 객체를 보장하는 패턴이에요.

주로 설정, 로깅, 상태 관리 객체 등에 사용.

WinForms에서는 전역 설정 객체, 공용 리소스, 로그 클래스 등을 싱글톤으로 만들어두면 유용.

## 왜 쓰는가?
중복 생성 방지
→ 예: Logger가 여기저기서 중복 생성되면 로그 순서 꼬일 수 있음

전역 접근 필요
→ 설정이나 상태처럼 어디서든 동일한 인스턴스를 참조해야 할 때

## 구조 예시 (로그 기록용 싱글톤 클래스)
### 1. Singleton Logger 클래스
```cs
public class Logger
{
    private static Logger _instance;
    private static readonly object _lock = new();

    private Logger() { } // 외부에서 new 방지

    public static Logger Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new Logger();
                return _instance;
            }
        }
    }

    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }
}
```
### 2. WinForm에서 사용하는 예
```cs
private void btnLog_Click(object sender, EventArgs e)
{
    Logger.Instance.Log("사용자가 버튼을 클릭했습니다.");
}
```
## 요약
Logger.Instance 처럼 어디서든 접근 가능

생성자는 private로 막고, static 속성을 통해 인스턴스 보장

WinForms에서는 아래 같은 것들에 자주 활용됨:

- 프로그램 설정 값 (AppConfigManager)
- 공용 상태 (Session, ThemeManager)
- 유일한 연결 객체 (예: SerialPortManager)







## Double-Check Locking

lock을 최소한으로 걸기 위해 2번 체크하는 기법.

### 현재 코드: 일반적인 lock 방식
```csharp
lock (_lock)
{
    if (_instance == null)
        _instance = new Logger();
    return _instance;
}
```
문제는, Instance를 호출할 때마다 lock을 항상 건다는 점.

멀티스레드 환경에서 thread-safe하긴 하지만, 매번 lock을 걸면 성능 저하가 생길 수 있음.

### 개선된 방식: Double-Check Locking
```cs
public static Logger Instance
{
    get
    {
        if (_instance == null) // 1차 체크: 인스턴스가 이미 있으면 lock 안 걸고 바로 리턴
        {
            lock (_lock)
            {
                if (_instance == null) //  2차 체크: 생성되었는지 다시 확인
                {
                    _instance = new Logger();
                }
            }
        }
        return _instance;
    }
}
```
왜 2번 확인하는가?
- 첫 번째 if로 성능 향상 (이미 만들어졌으면 lock 안 걸고 끝)
- 두 번째 if는 멀티스레드 동시 접근 시 중복 생성 방지
### 단, 주의점!
.NET Framework 4.0 이상에서는 아래처럼 Lazy<T> 또는 static 초기화 방식을 쓰면 더 안전하고 간단함.

```cs
public sealed class Logger
{
    private static readonly Logger _instance = new Logger();
    public static Logger Instance => _instance;
    private Logger() { }
}
```
JIT 컴파일 시점에서 thread-safe 보장

lock도 안 쓰고, 코드도 짧음.

## Lazy<T>를 활용한 싱글톤

스레드 안전 + 지연 초기화(처음 사용할 때까지 객체 생성 안 함)를 동시에 제공.

### Lazy<T>로 구현한 Singleton
```cs
public class Logger
{
    private static readonly Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());

    public static Logger Instance => _instance.Value;

    private Logger() { }

    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }
}
```
### 설명
Lazy<Logger>는 내부적으로 스레드 안전을 보장함 (LazyThreadSafetyMode.ExecutionAndPublication이 기본값). 
Instance를 처음 호출할 때만 new Logger()가 실행되고, 이후에는 캐시된 인스턴스를 재사용함.

WinForms에서는 Logger, Settings, SerialPortManager, AppStatusManager 등을 Lazy<T>로 관리하면 아주 깔끔하고 안전하게 쓸 수 있음.
