using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloths.source.math
{
    class RightTriangle: IFigure
    {
        public NormPoint BeginCoord { get; set; }
        public NormPoint EndCoord { get; set; }
        private NormPoint Node3 { get; set; }
        private NormPoint Node4 { get; set; }
        public float LineThick { get; set; }
        public Color BorderColor { get; set; }

        public RightTriangle()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
            Node3 = new NormPoint();
            Node4 = new NormPoint();
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

        public void SelectLineThick(float p)
        {
            LineThick = p;
        }

        public void SelectBorderColor(byte p1, byte p2, byte p3, byte p4)
        {
            BorderColor = Color.FromRGBA(p1, p2, p3, p4);
        }

        private double Scal(NormPoint a, NormPoint b, NormPoint c)
        {
            return a.X * b.Y - b.X * a.Y - a.X * c.Y + c.X * a.Y + b.X * c.Y - c.X * b.Y;
        }

        public bool IsIn(NormPoint p)
        {
            double abp = Scal(BeginCoord, Node3, p);
            double bcp = Scal(Node3, EndCoord, p);
            double acp = Scal(EndCoord, BeginCoord, p);
            if (abp > 0 && bcp > 0 && acp > 0 || abp < 0 && bcp < 0 && acp < 0)
                return true;
            else return false;
        }

        public IFigure Scale(double koeff)
        {
            RightTriangle tmp = new RightTriangle();
            double CX = (BeginCoord.X + EndCoord.X) / 2;
            double CY = (BeginCoord.Y + EndCoord.Y) / 2;
            tmp.BeginCoord.UpdateCoord(CX + (BeginCoord.X - CX) * koeff, CY + (BeginCoord.Y - CY) * koeff);
            tmp.EndCoord.UpdateCoord(CX + (EndCoord.X - CX) * koeff, CY + (EndCoord.Y - CY) * koeff);
            tmp.Node3.UpdateCoord(CX + (Node3.X - CX) * koeff, CY + (Node3.Y - CY) * koeff);
            tmp.Node4.UpdateCoord(CX + (Node4.X - CX) * koeff, CY + (Node4.Y - CY) * koeff);
            //tmp.Init(tmp.BeginCoord, tmp.EndCoord, BorderColor, LineThick);
            return tmp;
        }

        public IFigure MoveByVector(float x, float y)
        {
            RightTriangle tmp = new RightTriangle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X + x, this.BeginCoord.Y + y);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X + x, this.EndCoord.Y + y);
            tmp.Node3.UpdateCoord(this.Node3.X + x, this.Node3.Y + y);
            tmp.Node4.UpdateCoord(this.Node4.X + x, this.Node4.Y + y);
            //tmp.Init(tmp.BeginCoord, tmp.EndCoord, BorderColor, LineThick);
            return tmp;
        }

        public IFigure Rotate(double Phi)
        {
            RightTriangle tmp = new RightTriangle();
            tmp = this;
            Single PI = (Single)(Math.PI);
            NormPoint C = new NormPoint();
            C.UpdateCoord((BeginCoord.X + EndCoord.X) / 2, (BeginCoord.Y + EndCoord.Y) / 2);
            tmp.BeginCoord.UpdateCoord(C.X + (BeginCoord.X - C.X) * Math.Cos(Phi * PI / 180) - (BeginCoord.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y + (BeginCoord.X - C.X) * Math.Sin(Phi * PI / 180) + (BeginCoord.Y - C.Y) * Math.Cos(Phi * PI / 180));
            tmp.EndCoord.UpdateCoord(C.X + (EndCoord.X - C.X) * Math.Cos(Phi * PI / 180) - (EndCoord.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y + (EndCoord.X - C.X) * Math.Sin(Phi * PI / 180) + (EndCoord.Y - C.Y) * Math.Cos(Phi * PI / 180));
            tmp.Node3.UpdateCoord(C.X + (Node3.X - C.X) * Math.Cos(Phi * PI / 180) - (Node3.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y + (Node3.X - C.X) * Math.Sin(Phi * PI / 180) + (Node3.Y - C.Y) * Math.Cos(Phi * PI / 180));
            tmp.Node4.UpdateCoord(C.X + (Node4.X - C.X) * Math.Cos(Phi * PI / 180) - (Node4.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y + (Node4.X - C.X) * Math.Sin(Phi * PI / 180) + (Node4.Y - C.Y) * Math.Cos(Phi * PI / 180));

            //tmp.Init(tmp.BeginCoord, tmp.EndCoord, BorderColor, LineThick);
            return tmp;
        }


        public void Highlight(IPaint screen)
        {
            NormPoint a = new NormPoint();
            NormPoint b = new NormPoint();
            NormPoint c = new NormPoint();
            NormPoint d = new NormPoint();
            a.UpdateCoord(BeginCoord.X, BeginCoord.Y);
            b.UpdateCoord(Node3.X, Node3.Y);
            c.UpdateCoord(EndCoord.X, EndCoord.Y);
            d.UpdateCoord(Node4.X, Node4.Y);
            screen.drawhighlight(new List<NormPoint> { a, b, b, c, c, d, d, a });
        }


        public void Draw(IPaint screen)
        {
            screen.drawline(new List<NormPoint> { BeginCoord, Node3, BeginCoord, EndCoord, EndCoord, Node3 }, BorderColor, LineThick);
        }

    }
}
