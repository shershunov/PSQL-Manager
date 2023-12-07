using System;
using System.Windows.Forms;

namespace BD
{
    public partial class InputDialog : Form
    {
        public string Host { get; private set; }
        public string Port { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Database { get; private set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Host = hostTextBox.Text;
            Port = portTextBox.Text;
            Username = usernameTextBox.Text;
            Password = passwordTextBox.Text;
            Database = databaseTextBox.Text;
            if (string.IsNullOrWhiteSpace(Host) || string.IsNullOrWhiteSpace(Port) ||
                string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Database))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
