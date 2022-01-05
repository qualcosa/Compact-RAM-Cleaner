
namespace Compact_RAM_Cleaner
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Context1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Button1 = new System.Windows.Forms.Button();
            this.CacheCheck = new System.Windows.Forms.CheckBox();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.SettingsPanel = new System.Windows.Forms.Panel();
            this.AppName = new System.Windows.Forms.Label();
            this.Minimize = new System.Windows.Forms.Panel();
            this.ClosePanel = new System.Windows.Forms.Panel();
            this.LabelMon1 = new System.Windows.Forms.Label();
            this.LabelMon2 = new System.Windows.Forms.Label();
            this.LabelRam = new System.Windows.Forms.Label();
            this.LabelMon3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LabelPageFile = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LabelMon6 = new System.Windows.Forms.Label();
            this.LabelMon5 = new System.Windows.Forms.Label();
            this.LabelMon4 = new System.Windows.Forms.Label();
            this.Context1.SuspendLayout();
            this.TitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.ContextMenuStrip = this.Context1;
            this.NotifyIcon1.Text = "Compact RAM Cleaner";
            this.NotifyIcon1.Visible = true;
            // 
            // Context1
            // 
            this.Context1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu1,
            this.Menu3,
            this.Menu2});
            this.Context1.Name = "Context1";
            this.Context1.Size = new System.Drawing.Size(190, 70);
            // 
            // Menu1
            // 
            this.Menu1.Name = "Menu1";
            this.Menu1.Size = new System.Drawing.Size(189, 22);
            this.Menu1.Text = "Очистить ОЗУ";
            // 
            // Menu3
            // 
            this.Menu3.Name = "Menu3";
            this.Menu3.Size = new System.Drawing.Size(189, 22);
            this.Menu3.Text = "Очистить ОЗУ + кэш";
            // 
            // Menu2
            // 
            this.Menu2.Name = "Menu2";
            this.Menu2.Size = new System.Drawing.Size(189, 22);
            this.Menu2.Text = "Выход";
            // 
            // Button1
            // 
            this.Button1.BackColor = System.Drawing.Color.Silver;
            this.Button1.FlatAppearance.BorderSize = 0;
            this.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button1.ForeColor = System.Drawing.Color.Black;
            this.Button1.Location = new System.Drawing.Point(17, 205);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(134, 20);
            this.Button1.TabIndex = 14;
            this.Button1.Text = "Очистить";
            this.Button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Button1.UseVisualStyleBackColor = false;
            // 
            // CacheCheck
            // 
            this.CacheCheck.AutoSize = true;
            this.CacheCheck.Location = new System.Drawing.Point(157, 208);
            this.CacheCheck.Name = "CacheCheck";
            this.CacheCheck.Size = new System.Drawing.Size(55, 17);
            this.CacheCheck.TabIndex = 26;
            this.CacheCheck.Text = "+ кэш";
            this.CacheCheck.UseVisualStyleBackColor = true;
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.TitlePanel.Controls.Add(this.SettingsPanel);
            this.TitlePanel.Controls.Add(this.AppName);
            this.TitlePanel.Controls.Add(this.Minimize);
            this.TitlePanel.Controls.Add(this.ClosePanel);
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(250, 20);
            this.TitlePanel.TabIndex = 25;
            // 
            // SettingsPanel
            // 
            this.SettingsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SettingsPanel.Location = new System.Drawing.Point(208, 0);
            this.SettingsPanel.Name = "SettingsPanel";
            this.SettingsPanel.Size = new System.Drawing.Size(18, 18);
            this.SettingsPanel.TabIndex = 34;
            this.SettingsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingsPanel_Paint);
            // 
            // AppName
            // 
            this.AppName.AutoSize = true;
            this.AppName.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.AppName.Location = new System.Drawing.Point(3, 4);
            this.AppName.Name = "AppName";
            this.AppName.Size = new System.Drawing.Size(115, 13);
            this.AppName.TabIndex = 33;
            this.AppName.Text = "Compact RAM Cleaner";
            // 
            // Minimize
            // 
            this.Minimize.Location = new System.Drawing.Point(187, 1);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(18, 18);
            this.Minimize.TabIndex = 32;
            this.Minimize.Paint += new System.Windows.Forms.PaintEventHandler(this.Minimize_Paint);
            // 
            // ClosePanel
            // 
            this.ClosePanel.Location = new System.Drawing.Point(229, 0);
            this.ClosePanel.Name = "ClosePanel";
            this.ClosePanel.Size = new System.Drawing.Size(18, 18);
            this.ClosePanel.TabIndex = 31;
            this.ClosePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ClosePanel_Paint);
            // 
            // LabelMon1
            // 
            this.LabelMon1.AutoSize = true;
            this.LabelMon1.Location = new System.Drawing.Point(122, 53);
            this.LabelMon1.Name = "LabelMon1";
            this.LabelMon1.Size = new System.Drawing.Size(13, 13);
            this.LabelMon1.TabIndex = 27;
            this.LabelMon1.Text = "—";
            // 
            // LabelMon2
            // 
            this.LabelMon2.AutoSize = true;
            this.LabelMon2.Location = new System.Drawing.Point(122, 72);
            this.LabelMon2.Name = "LabelMon2";
            this.LabelMon2.Size = new System.Drawing.Size(13, 13);
            this.LabelMon2.TabIndex = 28;
            this.LabelMon2.Text = "—";
            // 
            // LabelRam
            // 
            this.LabelRam.AutoSize = true;
            this.LabelRam.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelRam.Location = new System.Drawing.Point(54, 27);
            this.LabelRam.Name = "LabelRam";
            this.LabelRam.Size = new System.Drawing.Size(148, 17);
            this.LabelRam.TabIndex = 30;
            this.LabelRam.Text = "Оперативная память";
            // 
            // LabelMon3
            // 
            this.LabelMon3.AutoSize = true;
            this.LabelMon3.Location = new System.Drawing.Point(122, 92);
            this.LabelMon3.Name = "LabelMon3";
            this.LabelMon3.Size = new System.Drawing.Size(13, 13);
            this.LabelMon3.TabIndex = 29;
            this.LabelMon3.Text = "—";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Занято:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Всего памяти:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Свободной памяти:";
            // 
            // LabelPageFile
            // 
            this.LabelPageFile.AutoSize = true;
            this.LabelPageFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelPageFile.Location = new System.Drawing.Point(71, 116);
            this.LabelPageFile.Name = "LabelPageFile";
            this.LabelPageFile.Size = new System.Drawing.Size(111, 17);
            this.LabelPageFile.TabIndex = 38;
            this.LabelPageFile.Text = "Файл подкачки";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Свободной памяти:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Выделено памяти:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 142);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "Занято:";
            // 
            // LabelMon6
            // 
            this.LabelMon6.AutoSize = true;
            this.LabelMon6.Location = new System.Drawing.Point(124, 181);
            this.LabelMon6.Name = "LabelMon6";
            this.LabelMon6.Size = new System.Drawing.Size(13, 13);
            this.LabelMon6.TabIndex = 47;
            this.LabelMon6.Text = "—";
            // 
            // LabelMon5
            // 
            this.LabelMon5.AutoSize = true;
            this.LabelMon5.Location = new System.Drawing.Point(124, 161);
            this.LabelMon5.Name = "LabelMon5";
            this.LabelMon5.Size = new System.Drawing.Size(13, 13);
            this.LabelMon5.TabIndex = 46;
            this.LabelMon5.Text = "—";
            // 
            // LabelMon4
            // 
            this.LabelMon4.AutoSize = true;
            this.LabelMon4.Location = new System.Drawing.Point(124, 142);
            this.LabelMon4.Name = "LabelMon4";
            this.LabelMon4.Size = new System.Drawing.Size(13, 13);
            this.LabelMon4.TabIndex = 45;
            this.LabelMon4.Text = "—";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(248, 240);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.LabelMon6);
            this.Controls.Add(this.LabelMon5);
            this.Controls.Add(this.LabelMon4);
            this.Controls.Add(this.LabelPageFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LabelRam);
            this.Controls.Add(this.LabelMon3);
            this.Controls.Add(this.LabelMon2);
            this.Controls.Add(this.LabelMon1);
            this.Controls.Add(this.CacheCheck);
            this.Controls.Add(this.TitlePanel);
            this.Controls.Add(this.Button1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compact RAM Cleaner";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Context1.ResumeLayout(false);
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon NotifyIcon1;
        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Panel Minimize;
        private System.Windows.Forms.Panel ClosePanel;
        private System.Windows.Forms.Label AppName;
        private System.Windows.Forms.Panel SettingsPanel;
        public System.Windows.Forms.Button Button1;
        public System.Windows.Forms.Label LabelMon1;
        public System.Windows.Forms.Label LabelMon2;
        public System.Windows.Forms.Label LabelRam;
        public System.Windows.Forms.Label LabelMon3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label LabelPageFile;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label LabelMon6;
        public System.Windows.Forms.Label LabelMon5;
        public System.Windows.Forms.Label LabelMon4;
        public System.Windows.Forms.CheckBox CacheCheck;
        public System.Windows.Forms.ContextMenuStrip Context1;
        public System.Windows.Forms.ToolStripMenuItem Menu1;
        public System.Windows.Forms.ToolStripMenuItem Menu2;
        public System.Windows.Forms.ToolStripMenuItem Menu3;
    }
}

