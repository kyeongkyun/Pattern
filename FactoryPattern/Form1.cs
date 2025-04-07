namespace FactoryPattern
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // �˾� Ÿ���� �����ϴ� ����
            PopupType selectedPopupType = PopupType.Info; // ���÷� Info Ÿ�� ����
            ICustomPopup popup = PopupFactory.Create(selectedPopupType);
            popup.Show("Hello, World!"); // �˾� �޽���
        }
    }

    public interface ICustomPopup
    {
        void Show(string message);
    }

    public class InfoPopup : ICustomPopup
    {
        public void Show(string message) =>
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public class WarningPopup : ICustomPopup
    {
        public void Show(string message) =>
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public enum PopupType
    {
        Info,
        Warning
    }

    public class PopupFactory
    {
        public static ICustomPopup Create(PopupType type)
        {
            return type switch
            {
                PopupType.Info => new InfoPopup(),
                PopupType.Warning => new WarningPopup(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
