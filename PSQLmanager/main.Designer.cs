using System.Windows.Forms;

namespace PSQL
{
    partial class main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows
        
        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.connectDialog = new System.Windows.Forms.Button();
            this.tablesComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.tabaleNameLabel = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(18, 146);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.Size = new System.Drawing.Size(822, 375);
            this.dataGridView.TabIndex = 0;
            // 
            // connectDialog
            // 
            this.connectDialog.Location = new System.Drawing.Point(18, 18);
            this.connectDialog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connectDialog.Name = "connectDialog";
            this.connectDialog.Size = new System.Drawing.Size(190, 72);
            this.connectDialog.TabIndex = 1;
            this.connectDialog.Text = "Подключить БД";
            this.connectDialog.UseVisualStyleBackColor = true;
            this.connectDialog.Click += new System.EventHandler(this.connectDialog_Click);
            // 
            // tablesComboBox
            // 
            this.tablesComboBox.FormattingEnabled = true;
            this.tablesComboBox.Location = new System.Drawing.Point(302, 15);
            this.tablesComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tablesComboBox.Name = "tablesComboBox";
            this.tablesComboBox.Size = new System.Drawing.Size(199, 28);
            this.tablesComboBox.TabIndex = 2;
            this.tablesComboBox.SelectedIndexChanged += new System.EventHandler(this.displayData);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(462, 55);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(112, 35);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tabaleNameLabel
            // 
            this.tabaleNameLabel.AutoSize = true;
            this.tabaleNameLabel.Location = new System.Drawing.Point(218, 18);
            this.tabaleNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tabaleNameLabel.Name = "tabaleNameLabel";
            this.tabaleNameLabel.Size = new System.Drawing.Size(79, 20);
            this.tabaleNameLabel.TabIndex = 4;
            this.tabaleNameLabel.Text = "Таблицы:";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(342, 55);
            this.updateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(112, 35);
            this.updateButton.TabIndex = 5;
            this.updateButton.Text = "Обновить";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.displayData);
            // 
            // statusLabel
            // 
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(699, 9);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(147, 23);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "Нет подключения";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(222, 55);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(112, 35);
            this.disconnectButton.TabIndex = 7;
            this.disconnectButton.Text = "Отключить";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // main
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(858, 534);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.tabaleNameLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tablesComboBox);
            this.Controls.Add(this.connectDialog);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::psqlm.Properties.Resources.favicon;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(880, 590);
            this.MinimumSize = new System.Drawing.Size(880, 590);
            this.Name = "main";
            this.Text = "PSQL manager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button connectDialog;
        private System.Windows.Forms.ComboBox tablesComboBox;
        private Button saveButton;
        private Label tabaleNameLabel;
        private Button updateButton;
        private Label statusLabel;
        private Button disconnectButton;
    }
}

