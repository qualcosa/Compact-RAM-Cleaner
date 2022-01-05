using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public partial class Notify : Shadows
    {
        public Notify()
        {
            InitializeComponent();
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width - 10, Screen.PrimaryScreen.WorkingArea.Height - Height - 10);
            NotifyText.Text = Popup.NotifyText;
        }
        void Notify_Paint(object sender, PaintEventArgs e) => e.Graphics.DrawLine(new Pen(Color.FromArgb(48, 49, 54), 2), 0, Height, Width, Height);
        async void Notify_Load(object sender, EventArgs e)
        {
            for (Opacity = 0; Opacity < 1; Opacity += .1) await Task.Delay(10);
            await Task.Delay(4000);
            for (Opacity = 1; Opacity > .0; Opacity -= .2) await Task.Delay(10); Close();
        }
    }
}
