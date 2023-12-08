using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Npgsql;

namespace PSQL
{
    public partial class main : Form
    {
        NpgsqlConnection connection;
        NpgsqlDataAdapter adapter;
        DataTable dataTable;
        string currentTable;


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

        public main()
        {
            InitializeComponent();
            setCursor();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(bindKeys);
        }

        private void connectedStatus()
        {
            statusLabel.Text = "Connected";
            statusLabel.ForeColor = Color.Green;
            disconnectButton.Enabled = true;
            updateButton.Enabled = true;
            saveButton.Enabled = true;
        }

        private void bindKeys(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveButton_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                displayData(sender, e);
            }
        }

        private void getTableNames()
        {
            try
            {
                DataTable schema = connection.GetSchema("Tables");
                foreach (DataRow row in schema.Rows)
                {
                    string tableName = row["table_name"].ToString();
                    tablesComboBox.Items.Add(tableName);
                }
                tablesComboBox.Enabled = true;
                tablesComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting table names: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                try {
                    DataTable schemaTable = new DataTable();
                    using (NpgsqlCommand schemaCommand = new NpgsqlCommand($"SELECT * FROM {currentTable} WHERE 1=0", connection))
                    {
                        using (NpgsqlDataAdapter schemaAdapter = new NpgsqlDataAdapter(schemaCommand))
                        {
                            schemaAdapter.FillSchema(schemaTable, SchemaType.Source);
                        }
                    }

                    foreach (DataGridViewRow dgvRow in dataGridView.Rows)
                    {
                        if (dgvRow.IsNewRow) continue;

                        DataRow row = ((DataRowView)dgvRow.DataBoundItem).Row;

                        NpgsqlCommand command = connection.CreateCommand();

                        string updateQuery = $"UPDATE {currentTable} SET ";
                        for (int i = 0; i < schemaTable.Columns.Count; i++)
                        {
                            string columnName = schemaTable.Columns[i].ColumnName;
                            updateQuery += $"{columnName} = @value{i}";
                            command.Parameters.AddWithValue($"@value{i}", row[columnName]);

                            if (i < schemaTable.Columns.Count - 1)
                            {
                                updateQuery += ", ";
                            }
                        }

                        updateQuery += $" WHERE id = @id";

                        command.CommandText = updateQuery;
                        command.Parameters.AddWithValue("@id", row["id"]);
                        command.ExecuteNonQuery();
                    }

                    foreach (DataGridViewRow dgvRow in dataGridView.Rows)
                    {
                        if (dgvRow.IsNewRow) continue;

                        DataRow row = ((DataRowView)dgvRow.DataBoundItem).Row;

                        NpgsqlCommand command = connection.CreateCommand();

                        string checkQuery = $"SELECT COUNT(*) FROM {currentTable} WHERE ";
                        for (int i = 0; i < schemaTable.Columns.Count; i++)
                        {
                            string columnName = schemaTable.Columns[i].ColumnName;
                            checkQuery += $"{columnName} = @value{i}";

                            command.Parameters.AddWithValue($"@value{i}", row[columnName]);

                            if (i < schemaTable.Columns.Count - 1)
                            {
                                checkQuery += " AND ";
                            }
                        }

                        command.CommandText = checkQuery;
                        int rowCount = Convert.ToInt32(command.ExecuteScalar());

                        if (rowCount == 0)
                        {
                            string insertQuery = $"INSERT INTO {currentTable} (";

                            for (int i = 0; i < schemaTable.Columns.Count; i++)
                            {
                                string columnName = schemaTable.Columns[i].ColumnName;
                                insertQuery += columnName;

                                if (i < schemaTable.Columns.Count - 1)
                                {
                                    insertQuery += ", ";
                                }
                            }

                            insertQuery += ") VALUES (";

                            for (int i = 0; i < schemaTable.Columns.Count; i++)
                            {
                                string paramName = $"@value{i}";
                                insertQuery += paramName;
                                string columnName = schemaTable.Columns[i].ColumnName;
                                command.Parameters.AddWithValue(paramName, row[columnName]);

                                if (i < schemaTable.Columns.Count - 1)
                                {
                                    insertQuery += ", ";
                                }
                            }

                            insertQuery += ")";

                            command.CommandText = insertQuery;
                            command.ExecuteNonQuery();
                        }
                    }

                    DataTable changesDataTable = ((DataTable)dataGridView.DataSource).GetChanges();
                    if (changesDataTable != null)
                    {
                        foreach (DataRow row in changesDataTable.Rows)
                        {
                            NpgsqlCommand command = connection.CreateCommand();

                            switch (row.RowState)
                            {
                                case DataRowState.Deleted:
                                    command.CommandText = $"DELETE FROM {currentTable} WHERE id = @id";
                                    command.Parameters.AddWithValue("@id", row["id", DataRowVersion.Original]);
                                    command.ExecuteNonQuery();
                                    break;
                            }
                        }
                    }

                ((DataTable)dataGridView.DataSource).AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Saving error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void connectBD(string host, string port, string username, string password, string database)
        {   
            try
            {
                string connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database};";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                getTableNames();
                connectedStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to the DB: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void displayData(object sender, EventArgs e)
        {
            if (connection != null)
            {
                try
                {
                    currentTable = tablesComboBox.SelectedItem.ToString();
                    string sql = $"SELECT * FROM {currentTable}";
                    adapter = new NpgsqlDataAdapter(sql, connection);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when displaying the table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void connectDialog_Click(object sender, EventArgs e)
        {
            using (InputDialog inputDialog = new InputDialog())
            {
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    string host = inputDialog.Host;
                    string port = inputDialog.Port;
                    string username = inputDialog.Username;
                    string password = inputDialog.Password;
                    string database = inputDialog.Database;
                    connectBD(host, port, username, password, database);
                }
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
                adapter = null;
                tablesComboBox.Items.Clear();
                tablesComboBox.Text = string.Empty;
                this.statusLabel.Text = "No connection";
                this.statusLabel.ForeColor = Color.Red;
                dataGridView.Columns.Clear();
                dataGridView.DataSource = null;
                disconnectButton.Enabled = false;
                updateButton.Enabled = false;
                saveButton.Enabled = false;
                tablesComboBox.Enabled = false;
            }
        }
    }
}
