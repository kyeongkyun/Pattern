using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace MVVMPattern;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}

public class User
{
    public string Name { get; set; }
}

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