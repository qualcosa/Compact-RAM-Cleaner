namespace Compact_RAM_Cleaner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.AppName = new System.Windows.Forms.Label();
            this.MinimizePanel = new System.Windows.Forms.Panel();
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.ClosePanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.PhysicalMemoryLabel = new System.Windows.Forms.Label();
            this.PhysicalMemoryData = new System.Windows.Forms.Label();
            this.PageFileLabel = new System.Windows.Forms.Label();
            this.PageFileData = new System.Windows.Forms.Label();
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ClearButton2 = new Compact_RAM_Cleaner.CustomRadioButton();
            this.ClearButton1 = new Compact_RAM_Cleaner.CustomRadioButton();
            this.ClearTypePanel = new System.Windows.Forms.Panel();
            this.ClearButton = new System.Windows.Forms.Panel();
            this.ExpandPanel = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.TitlePanel.SuspendLayout();
            this.ClearTypePanel.SuspendLayout();
            this.ClearButton.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.TitlePanel.Controls.Add(this.AppName);
            this.TitlePanel.Controls.Add(this.MinimizePanel);
            this.TitlePanel.Controls.Add(this.SettingsPanel);
            this.TitlePanel.Controls.Add(this.ClosePanel);
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(250, 25);
            this.TitlePanel.TabIndex = 0;
            // 
            // AppName
            // 
            this.AppName.AutoSize = true;
            this.AppName.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.AppName.Location = new System.Drawing.Point(3, 6);
            this.AppName.Name = "AppName";
            this.AppName.Size = new System.Drawing.Size(114, 13);
            this.AppName.TabIndex = 1;
            this.AppName.Text = "Compact RAM Cleaner";
            // 
            // MinimizePanel
            // 
            this.MinimizePanel.Location = new System.Drawing.Point(175, 0);
            this.MinimizePanel.Name = "MinimizePanel";
            this.MinimizePanel.Size = new System.Drawing.Size(25, 25);
            this.MinimizePanel.TabIndex = 3;
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.Location = new System.Drawing.Point(200, 0);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(25, 25);
            this.SettingsPanel.TabIndex = 2;
            // 
            // ClosePanel
            // 
            this.ClosePanel.Location = new System.Drawing.Point(225, 0);
            this.ClosePanel.Name = "ClosePanel";
            this.ClosePanel.Size = new System.Drawing.Size(25, 25);
            this.ClosePanel.TabIndex = 1;
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(82, 56);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(84, 84);
            this.MainPanel.TabIndex = 1;
            // 
            // PhysicalMemoryLabel
            // 
            this.PhysicalMemoryLabel.AutoSize = true;
            this.PhysicalMemoryLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.PhysicalMemoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.PhysicalMemoryLabel.Location = new System.Drawing.Point(16, 6);
            this.PhysicalMemoryLabel.Name = "PhysicalMemoryLabel";
            this.PhysicalMemoryLabel.Size = new System.Drawing.Size(86, 13);
            this.PhysicalMemoryLabel.TabIndex = 2;
            this.PhysicalMemoryLabel.Text = "Physical memory";
            // 
            // PhysicalMemoryData
            // 
            this.PhysicalMemoryData.AutoSize = true;
            this.PhysicalMemoryData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.PhysicalMemoryData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.PhysicalMemoryData.Location = new System.Drawing.Point(16, 22);
            this.PhysicalMemoryData.Name = "PhysicalMemoryData";
            this.PhysicalMemoryData.Size = new System.Drawing.Size(17, 13);
            this.PhysicalMemoryData.TabIndex = 3;
            this.PhysicalMemoryData.Text = "—";
            // 
            // PageFileLabel
            // 
            this.PageFileLabel.AutoSize = true;
            this.PageFileLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.PageFileLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.PageFileLabel.Location = new System.Drawing.Point(16, 6);
            this.PageFileLabel.Name = "PageFileLabel";
            this.PageFileLabel.Size = new System.Drawing.Size(48, 13);
            this.PageFileLabel.TabIndex = 4;
            this.PageFileLabel.Text = "Page file";
            // 
            // PageFileData
            // 
            this.PageFileData.AutoSize = true;
            this.PageFileData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.PageFileData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.PageFileData.Location = new System.Drawing.Point(16, 22);
            this.PageFileData.Name = "PageFileData";
            this.PageFileData.Size = new System.Drawing.Size(17, 13);
            this.PageFileData.TabIndex = 5;
            this.PageFileData.Text = "—";
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.Text = "Compact RAM Cleaner";
            this.NotifyIcon1.Visible = true;
            // 
            // ClearButton2
            // 
            this.ClearButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ClearButton2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ClearButton2.Location = new System.Drawing.Point(3, 21);
            this.ClearButton2.Name = "ClearButton2";
            this.ClearButton2.Size = new System.Drawing.Size(92, 18);
            this.ClearButton2.TabIndex = 10;
            this.ClearButton2.Text = "RAM + Cache";
            this.ClearButton2.UseVisualStyleBackColor = false;
            // 
            // ClearButton1
            // 
            this.ClearButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ClearButton1.Checked = true;
            this.ClearButton1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ClearButton1.Location = new System.Drawing.Point(3, 3);
            this.ClearButton1.Name = "ClearButton1";
            this.ClearButton1.Size = new System.Drawing.Size(92, 18);
            this.ClearButton1.TabIndex = 9;
            this.ClearButton1.TabStop = true;
            this.ClearButton1.Text = "RAM";
            this.ClearButton1.UseVisualStyleBackColor = false;
            // 
            // ClearTypePanel
            // 
            this.ClearTypePanel.Controls.Add(this.ClearButton2);
            this.ClearTypePanel.Controls.Add(this.ClearButton1);
            this.ClearTypePanel.Location = new System.Drawing.Point(132, 163);
            this.ClearTypePanel.Name = "ClearTypePanel";
            this.ClearTypePanel.Size = new System.Drawing.Size(107, 42);
            this.ClearTypePanel.TabIndex = 9;
            // 
            // ClearButton
            // 
            this.ClearButton.Controls.Add(this.ExpandPanel);
            this.ClearButton.Location = new System.Drawing.Point(96, 140);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(58, 24);
            this.ClearButton.TabIndex = 13;
            // 
            // ExpandPanel
            // 
            this.ExpandPanel.BackColor = System.Drawing.Color.Transparent;
            this.ExpandPanel.Location = new System.Drawing.Point(38, 3);
            this.ExpandPanel.Name = "ExpandPanel";
            this.ExpandPanel.Size = new System.Drawing.Size(18, 18);
            this.ExpandPanel.TabIndex = 14;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.PhysicalMemoryLabel);
            this.Panel1.Controls.Add(this.PhysicalMemoryData);
            this.Panel1.Location = new System.Drawing.Point(12, 211);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(226, 40);
            this.Panel1.TabIndex = 14;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.PageFileLabel);
            this.Panel2.Controls.Add(this.PageFileData);
            this.Panel2.Location = new System.Drawing.Point(12, 263);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(226, 40);
            this.Panel2.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(47)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(250, 320);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ClearTypePanel);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.TitlePanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compact RAM Cleaner";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            this.ClearTypePanel.ResumeLayout(false);
            this.ClearButton.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Panel ClosePanel;
        private System.Windows.Forms.Panel SettingsPanel;
        private System.Windows.Forms.Panel MinimizePanel;
        private System.Windows.Forms.Label AppName;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label PhysicalMemoryLabel;
        private System.Windows.Forms.Label PhysicalMemoryData;
        private System.Windows.Forms.Label PageFileLabel;
        private System.Windows.Forms.Label PageFileData;
        private System.Windows.Forms.NotifyIcon NotifyIcon1;
        private CustomRadioButton ClearButton1;
        private CustomRadioButton ClearButton2;
        private System.Windows.Forms.Panel ClearTypePanel;
        private System.Windows.Forms.Panel ClearButton;
        private System.Windows.Forms.Panel ExpandPanel;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel Panel2;
    }
}

