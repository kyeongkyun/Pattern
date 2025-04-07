namespace ObserverPattern
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TemperatureSensor _sensor;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _sensor = new TemperatureSensor();
            _sensor.TemperatureChanged += Sensor_TemperatureChanged;
        }

        private void Sensor_TemperatureChanged(int newTemp)
        {
             lblTemp.Text = $"���� �µ�: {newTemp}��C";
        }

        private void btnChangeTemp_Click(object sender, EventArgs e)
        {
            int newTemp = new Random().Next(-10, 40);
            _sensor.SetTemperature(newTemp);  // ���� �ٲ�� �ڵ����� �̺�Ʈ �ߵ�
        }
    }

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
                TemperatureChanged?.Invoke(_temperature);  // �̺�Ʈ �߻�!
            }
        }
    }
}
