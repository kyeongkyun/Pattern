# 6. MVVM Pattern (Model-View-ViewModel)
## 개념
MVVM은 3개의 구성요소로 이루어진 아키텍처

- Model: 순수 데이터, 비즈니스 로직 (예: DB 모델, 서비스)
- View:사용자에게 보이는 UI (XAML 화면)
- ViewModel:View와 Model 사이의 다리, 데이터 바인딩 + 커맨드 처리 담당

View는 ViewModel을 바인딩만 할 뿐, 직접 참조하지 않음


## 간단 예제: 이름을 입력하고 버튼 누르면 메시지를 표시하는 앱
### 1. Model
```cs
public class User
{
    public string Name { get; set; }
}
```
### 2. ViewModel (INotifyPropertyChanged + ICommand)
```cs
public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            if (_userName != value)
            {
                _userName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserName)));
                ShowCommand?.RaiseCanExecuteChanged();  // 텍스트 바뀌면 버튼 상태 갱신
            }
        }
    }

    public RelayCommand ShowCommand { get; }

    public MainViewModel()
    {
        ShowCommand = new RelayCommand(
            execute: ShowMessage,
            canExecute: () => !string.IsNullOrWhiteSpace(UserName)
        );
    }

    private void ShowMessage()
    {
        MessageBox.Show($"안녕하세요, {UserName}님!");
    }
}
```
> RelayCommand는 커맨드를 쉽게 정의해주는 도우미 클래스 (아래 참고)

### 3. View (MainWindow.xaml)
```xaml
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:WpfApp"
        Title="MVVM Example" Height="200" Width="300">
    <Window.DataContext>
        <local:MainViewModel/>
    </Window.DataContext>

    <StackPanel Margin="20">
        <TextBox Text="{Binding UserName, UpdateSourceTrigger=PropertyChanged}" />
        <Button Content="인사하기" Command="{Binding ShowCommand}" Margin="0,10,0,0"/>
    </StackPanel>
</Window>
```
### 4. RelayCommand 도우미
```cs
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public event EventHandler CanExecuteChanged;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

    public void Execute(object parameter) => _execute();

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
```
## MVVM의 장점
* XAML 바인딩 덕분에 View는 거의 코드 없이 구성 가능
* UI 로직과 비즈니스 로직이 완전히 분리되어 유지보수 편함
* 유닛 테스트 용이

## WPF가 버튼을 자동으로 비활성화하는 이유는?
WPF는 Button이 ICommandSource 인터페이스를 구현하고 있고,
거기서 Command 바인딩된 객체의 CanExecute()를 호출해서 true/false에 따라 자동으로 활성/비활성 상태를 적용하기 때문.
