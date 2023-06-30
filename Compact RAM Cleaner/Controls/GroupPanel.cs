using System.Drawing;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public class GroupPanel : Panel
    {
        readonly int _indent = 10;
        readonly int _cornerRadius = 6;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var pen = new Pen(SystemColors.ControlDarkDark))
                e.Graphics.DrawRoundedRectangle(pen, new Rectangle(_indent, _indent, Width - (_indent * 2), Height - (_indent * 2)), _cornerRadius);
        }
    }
}
