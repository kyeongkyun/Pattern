# 8. MVP (Model-View-Presenter) 패턴
## 개념
MVC와 비슷하지만, UI 이벤트를 Presenter가 직접 제어한다는 게 핵심!

WinForms처럼 데이터 바인딩이 약한 플랫폼에 적합

View는 인터페이스로 추상화되고, 로직은 Presenter에 몰아넣음

## 역할 분담
구성요소	역할
Model	비즈니스 로직, 데이터
View	사용자에게 보여지는 UI (Form, Control)
Presenter	View와 Model을 연결하고 모든 로직을 처리

## 예제: 사용자 이름 입력 후 버튼 누르면 환영 메시지 표시
### 1. Model
```cs
public class UserModel
{
    public string Name { get; set; }
}
```
### 2. View 인터페이스
```cs
public interface IUserView
{
    string UserName { get; }
    event EventHandler SubmitClicked;
    void ShowMessage(string message);
}
```
### 3. 실제 View (WinForm)
```cs
public partial class MainForm : Form, IUserView
{
    public MainForm()
    {
        InitializeComponent();
        btnSubmit.Click += (s, e) => SubmitClicked?.Invoke(this, EventArgs.Empty);
    }

    public string UserName => txtName.Text;

    public event EventHandler SubmitClicked;

    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }
}
```
### 4. Presenter
```cs
public class UserPresenter
{
    private readonly IUserView _view;
    private readonly UserModel _model;

    public UserPresenter(IUserView view)
    {
        _view = view;
        _model = new UserModel();

        _view.SubmitClicked += OnSubmitClicked;
    }

    private void OnSubmitClicked(object sender, EventArgs e)
    {
        _model.Name = _view.UserName;
        _view.ShowMessage($"안녕하세요, {_model.Name}님!");
    }
}
```
### 5. Program.cs (Presenter 연결)
```cs
static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var view = new MainForm();
        var presenter = new UserPresenter(view);

        Application.Run(view);
    }
}
```
## 특징 요약
- 테스트 용이 : View를 인터페이스로 추상화했기 때문에 유닛 테스트 쉬움
- 관심사 분리 : 로직은 Presenter, UI는 View로 역할 명확
- WinForms 적합 : MVVM처럼 바인딩 신경 안 써도 됨
### WinForms에 MVP를 적용하는 경우
    - 복잡한 UI 입력 검증
    - 버튼마다 많은 분기 처리
    - Presenter만 Mock으로 테스트하고 싶을 때