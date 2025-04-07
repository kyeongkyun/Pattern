# 5. Observer Pattern (옵저버 패턴)
## 개념
어떤 객체의 상태 변화를 감지하고, 그 변화에 따라 자동으로 반응하는 객체들(옵저버)을 연결하는 패턴.

.NET에선 주로 event, delegate, 그리고 INotifyPropertyChanged를 통해 구현.

## 왜 쓰는가?
느슨한 결합: 관찰 대상(Subject)과 옵저버(Observer)가 직접 연결되지 않아도 동작

이벤트 기반 프로그래밍에 최적

WinForms에서 UI를 업데이트할 때 아주 자주 활용됨

## 예제 시나리오: 어떤 값이 바뀌면 UI에 자동 반영
### 1. 옵저버 대상 클래스 (Subject)
```cs
public class TemperatureSensor
{
    public delegate void TemperatureChangedHandler(int newTemp);
    public event TemperatureChangedHandler TemperatureChanged;

    private int _temperature;

    public void SetTemperature(int temp)
    {
        if (_temperature != temp)
        {
            _temperature = temp;
            TemperatureChanged?.Invoke(_temperature);  // 이벤트 발생!
        }
    }
}
```
### 2. WinForm에서 옵저버로 동작
```cs
private TemperatureSensor _sensor;

private void Form1_Load(object sender, EventArgs e)
{
    _sensor = new TemperatureSensor();
    _sensor.TemperatureChanged += Sensor_TemperatureChanged;
}

private void Sensor_TemperatureChanged(int newTemp)
{
    lblTemp.Text = $"현재 온도: {newTemp}°C";
}

private void btnChangeTemp_Click(object sender, EventArgs e)
{
    int newTemp = new Random().Next(-10, 40);
    _sensor.SetTemperature(newTemp);  // 값이 바뀌면 자동으로 이벤트 발동
}
```
## 요약
옵저버 패턴은 WinForms에서 가장 흔한 이벤트 처리 구조

Subject → 상태 변화 → Observer가 반응

WinForms 기본 컨트롤도 거의 다 이 패턴 기반: Button.Click, TextChanged, ValueChanged 등등

### 확장 예: INotifyPropertyChanged
WinForms에서는 잘 안 쓰지만, WPF나 MVVM에선 아래처럼도 씀:

```cs
public class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
        }
    }
}
```