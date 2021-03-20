using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sloths.source.math
{
    class Rectangle : IFigure
    {
        public NormPoint BeginCoord { get; set; }
        public NormPoint EndCoord { get; set; }
        private NormPoint Node3 { get; set; }
        private NormPoint Node4 { get; set; }
        public float LineThick { get; set; }
        public Color BorderColor { get; set; }

        public Rectangle()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
            Node3 = new NormPoint();
            Node4 = new NormPoint();
            BorderColor = Color.FromRGBA(0, 0, 0, 1);
            LineThick = 1;
        }
        public void SelectLineThick(float p)
        {
            LineThick = p;
        }

        public void SelectBorderColor(byte p1, byte p2, byte p3, byte p4)
        {
            BorderColor = Color.FromRGBA(p1, p2, p3, p4);
        }

        public void Init(NormPoint p1, NormPoint p2)
        {
            BeginCoord = p1;
            EndCoord = p2;
            Node3.UpdateCoord(BeginCoord.X, EndCoord.Y);
            Node4.UpdateCoord(EndCoord.X, BeginCoord.Y);
        }

        public void Init(NormPoint p1, NormPoint p2, Color Color, float Thick)
        {
            BeginCoord = p1;
            EndCoord = p2;
            Node3.UpdateCoord(BeginCoord.X, EndCoord.Y);
            Node4.UpdateCoord(EndCoord.X, BeginCoord.Y);
            SelectBorderColor(Color.R, Color.G, Color.B, Color.A);
            SelectLineThick(Thick);
        }


        private double Scal(NormPoint a, NormPoint b, NormPoint c)
        {
            return a.X * b.Y - b.X * a.Y - a.X * c.Y + c.X * a.Y + b.X * c.Y - c.X * b.Y;
        }

        public bool IsIn(NormPoint p)
        {
            double abp = Scal(BeginCoord, Node3, p);
            double bcp = Scal(Node3, EndCoord, p);
            double acp = Scal(EndCoord, BeginCoord , p);
            double adp = Scal(BeginCoord, Node4, p);
            double cdp = Scal(Node4, EndCoord, p);

            if (abp > 0 && bcp > 0 && acp > 0 || abp < 0 && bcp < 0 && acp < 0 || adp > 0 && cdp > 0 && acp > 0 || adp < 0 && cdp < 0 && acp < 0)
                return true;
            else return false;
        }

        public IFigure Scale(double koeff)
        {
            Rectangle tmp = new Rectangle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X * koeff, this.BeginCoord.Y * koeff);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X * koeff, this.EndCoord.Y * koeff);
            Node3.UpdateCoord(Node3.X*koeff, Node3.Y * koeff);
            Node4.UpdateCoord(Node4.X * koeff, Node4.Y * koeff);
            return tmp;
        }

        public IFigure MoveByVector(NormPoint v)
        {
            Rectangle tmp = new Rectangle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X + v.X, this.BeginCoord.Y + v.Y);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X + v.X, this.EndCoord.Y + v.Y);
            return tmp;
        }

        public IFigure Rotate(NormPoint center, double Phi)
        {
            Rectangle tmp = new Rectangle();
            tmp = this;
            tmp.BeginCoord.UpdateCoord(center.X + BeginCoord.X * Math.Sin(Phi) + BeginCoord.Y * Math.Cos(Phi), center.Y + BeginCoord.X * Math.Cos(Phi) - BeginCoord.Y * Math.Sin(Phi));
            tmp.EndCoord.UpdateCoord(center.X+EndCoord.X * Math.Sin(Phi) + EndCoord.Y * Math.Cos(Phi), center.Y + EndCoord.X * Math.Cos(Phi) - EndCoord.Y * Math.Sin(Phi));
            tmp.Node3.UpdateCoord(center.X+Node3.X * Math.Sin(Phi) + Node3.Y * Math.Cos(Phi), center.Y + Node3.X * Math.Cos(Phi) - Node3.Y * Math.Sin(Phi));
            tmp.Node4.UpdateCoord(center.X+Node4.X * Math.Sin(Phi) + Node4.Y * Math.Cos(Phi), center.Y + Node4.X * Math.Cos(Phi) - Node4.Y * Math.Sin(Phi));
            return tmp;
        }

        public IFigure Rotate(double Phi)
        {
            Rectangle tmp = new Rectangle();
            tmp = this;
            tmp.BeginCoord.UpdateCoord(BeginCoord.X * Math.Sin(Phi)+ BeginCoord.Y * Math.Cos(Phi), BeginCoord.X * Math.Cos(Phi) - BeginCoord.Y * Math.Sin(Phi));
            tmp.EndCoord.UpdateCoord(EndCoord.X * Math.Sin(Phi) + EndCoord.Y * Math.Cos(Phi), EndCoord.X * Math.Cos(Phi) - EndCoord.Y * Math.Sin(Phi));
            tmp.Node3.UpdateCoord(Node3.X * Math.Sin(Phi) + Node3.Y * Math.Cos(Phi), Node3.X * Math.Cos(Phi) - Node3.Y * Math.Sin(Phi));
            tmp.Node4.UpdateCoord(Node4.X * Math.Sin(Phi) + Node4.Y * Math.Cos(Phi), Node4.X * Math.Cos(Phi) - Node4.Y * Math.Sin(Phi));
            return tmp;
        }

        public IFigure Reflection(NormPoint a, NormPoint b)
        {
            return this;
        }

        public void Highlight(IPaint screen)
        {
            NormPoint a = new NormPoint();
            NormPoint b = new NormPoint();
            NormPoint c = new NormPoint();
            NormPoint d = new NormPoint();
            a.UpdateCoord(BeginCoord.X,BeginCoord.Y);
            b.UpdateCoord(Node3.X, Node3.Y);
            c.UpdateCoord(EndCoord.X, EndCoord.Y);
            d.UpdateCoord(Node4.X, Node4.Y);
            int hx,hy;
            if (BeginCoord.X < EndCoord.X) hx = 1;
            else hx = -1;
            a.UpdateCoord(BeginCoord.X - hx * 0.001, BeginCoord.Y);
            d.UpdateCoord(Node4.X - hx * 0.001, Node4.Y);
            c.UpdateCoord(EndCoord.X + hx * 0.001, EndCoord.Y);
            b.UpdateCoord(Node3.X + hx * 0.001, Node3.Y);

            if (BeginCoord.Y < EndCoord.Y) hy = 1;
            else hy = -1;
            a.UpdateCoord(BeginCoord.X , BeginCoord.Y- hy * 0.001);
            d.UpdateCoord(Node4.X , Node4.Y- hy * 0.001);
            c.UpdateCoord(EndCoord.X , EndCoord.Y+ hy * 0.001);
            b.UpdateCoord(Node3.X , Node3.Y+ hy * 0.001);

            screen.drawhighlight(new List<NormPoint> { a, b });
            screen.drawhighlight(new List<NormPoint> { b, c });
            screen.drawhighlight(new List<NormPoint> { c, d });
            screen.drawhighlight(new List<NormPoint> { d, a });
        }




        public void Draw(IPaint screen)
        {

            screen.drawline(new List<NormPoint> { BeginCoord, Node3, BeginCoord, Node4, Node4, EndCoord, EndCoord, Node3},BorderColor,LineThick);
        }
    }
}
