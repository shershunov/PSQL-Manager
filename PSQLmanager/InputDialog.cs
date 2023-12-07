using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PSQL
{
    public partial class InputDialog : Form
    {
        public string Host { get; private set; }
        public string Port { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Database { get; private set; }


        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadCursorFromFile(string fileName);
        private void setCursor()
        {
            IntPtr customCursor = LoadCursorFromFile("C:\\Windows\\Cursors\\aero_link.cur");
            if (customCursor != IntPtr.Zero)
            {
                foreach (Control control in Controls)
                {
                    if (control is Button)
                    {
                        ((Button)control).Cursor = new Cursor(customCursor);
                    }
                }
            }
            else
            {
                foreach (Control control in Controls)
                {
                    if (control is Button)
                    {
                        ((Button)control).Cursor = Cursors.Hand;
                    }
                }
            }
        }

        public InputDialog()
        {
            InitializeComponent();
            setCursor(); 
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(bindKeys);
        }
        private void bindKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CancelButton_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                OKButton_Click(sender, e);
            }
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
