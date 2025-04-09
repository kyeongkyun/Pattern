namespace MVPPattern
{
    public partial class Form1 : Form, IUserView
    {
        TextBox txtName = new TextBox();
        public Form1()
        {
            InitializeComponent();
            var btnSubmit = new Button();
            btnSubmit.Text = "Submit";
            this.Controls.Add(btnSubmit);
            btnSubmit.Location = new Point(100, 0);

            this.Controls.Add(txtName);

            btnSubmit.Click += (s, e) => SubmitClicked?.Invoke(this, EventArgs.Empty);
        }

        public string UserName => txtName.Text;

        public event EventHandler SubmitClicked;

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }

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
            _view.ShowMessage($"æ»≥Á«œººø‰, {_model.Name}¥‘!");
        }
    }

    public class UserModel
    {
        public string Name { get; set; }
    }

    public interface IUserView
    {
        string UserName { get; }
        event EventHandler SubmitClicked;
        void ShowMessage(string message);
    }
}
