// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Logger.Instance.Log("사용자가 버튼을 클릭했습니다.");

public class Logger
{
    private static Logger _instance;
    private static readonly object _lock = new();

    private Logger() { } // 외부에서 new 방지

    public static Logger Instance
    {
        get
        {
            if (_instance == null) // 1차 체크: 인스턴스가 이미 있으면 lock 안 걸고 바로 리턴
            {
                lock (_lock)
                {
                    if (_instance == null) // 2차 체크: 생성되었는지 다시 확인
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }
    }

    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
    }
}
