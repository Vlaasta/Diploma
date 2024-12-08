using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diplom
{
    public class Button : Control
    {
        private StringFormat SF = new StringFormat();
        private bool MouseEntered = false;
        private bool MousePressed = false;

        public int Radius { get; set; } = 10;

        public Button()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint,
                true
            );

            DoubleBuffered = true;

            //Size = new System.Drawing.Size(100, 30);
            //BackColor = Color.Magenta;
            //ForeColor = Color.White;

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            if (Radius <= 0) Radius = 1;
            GraphicsPath rectGp = RoundedRectangle(rect, Radius);

            graph.FillPath(new SolidBrush(BackColor), rectGp);

            graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);

            if (MouseEntered)
            {
                graph.FillPath(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), rectGp);
                graph.DrawPath(new Pen(Color.FromArgb(150, 0, 0, 0)), rectGp);
            }
            if (MousePressed)
            {
                graph.FillPath(new SolidBrush(Color.FromArgb(50, Color.AliceBlue)), rectGp);
                graph.DrawPath(new Pen(Color.FromArgb(150, Color.AliceBlue)), rectGp);
            }

            graph.DrawPath(new Pen(Color.FromArgb(255, 77, 77, 77)), rectGp);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            MouseEntered = true;

            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseEntered = false;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MousePressed = true;

            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MousePressed = false;

            Invalidate();
        }

        private GraphicsPath RoundedRectangle(Rectangle rect, int RoundSize)
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddArc(rect.X, rect.Y, RoundSize, RoundSize, 180, 90);
            gp.AddArc(rect.X + rect.Width - RoundSize, rect.Y, RoundSize, RoundSize, 270, 90);
            gp.AddArc(rect.X + rect.Width - RoundSize, rect.Y + rect.Height - RoundSize, RoundSize, RoundSize, 0, 90);
            gp.AddArc(rect.X, rect.Y + rect.Height - RoundSize, RoundSize, RoundSize, 90, 90);

            gp.CloseFigure();

            return gp;
        }
    }
}
