using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloths.source.math
{
    class IsoscelesTriangle: IFigure
    {
        public NormPoint BeginCoord { get; set; }
        public NormPoint EndCoord { get; set; }
        private NormPoint Node3 { get; set; }
        private NormPoint Node4 { get; set; }
        public float LineThick { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color BorderColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IsoscelesTriangle()
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
            Node4.UpdateCoord((EndCoord.X+BeginCoord.X)/2, BeginCoord.Y);
        }


        private double Scal(NormPoint a, NormPoint b, NormPoint c)
        {
            return a.X * b.Y - b.X * a.Y - a.X * c.Y + c.X * a.Y + b.X * c.Y - c.X * b.Y;
        }

        public bool IsIn(NormPoint p)
        {
            double abp = Scal(Node3, Node4, p);
            double bcp = Scal(Node4, EndCoord, p);
            double acp = Scal(EndCoord, Node3, p);

            if (abp > 0 && bcp > 0 && acp > 0 || abp < 0 && bcp < 0 && acp < 0)
                return true;
            else return false;
        }

        public IFigure Scale(double koeff)
        {
            IsoscelesTriangle tmp = new IsoscelesTriangle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X * koeff, this.BeginCoord.Y * koeff);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X * koeff, this.EndCoord.Y * koeff);
            Node3.UpdateCoord(Node3.X * koeff, Node3.Y * koeff);
            Node4.UpdateCoord(Node4.X * koeff, Node4.Y * koeff);
            return tmp;
        }

        public IFigure MoveByVector(float x, float y)
        {
            IsoscelesTriangle tmp = new IsoscelesTriangle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X + x, this.BeginCoord.Y + y);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X + x, this.EndCoord.Y + y);
            tmp.Node3.UpdateCoord(this.Node3.X + x, this.Node3.Y + y);
            tmp.Node4.UpdateCoord(this.Node4.X + x, this.Node4.Y + y);
            return tmp;
        }

        public IFigure Rotate(NormPoint center, double Phi)
        {
            IsoscelesTriangle tmp = new IsoscelesTriangle();
            tmp = this;
            tmp.BeginCoord.UpdateCoord(center.X + BeginCoord.X * Math.Sin(Phi) + BeginCoord.Y * Math.Cos(Phi), center.Y + BeginCoord.X * Math.Cos(Phi) - BeginCoord.Y * Math.Sin(Phi));
            tmp.EndCoord.UpdateCoord(center.X + EndCoord.X * Math.Sin(Phi) + EndCoord.Y * Math.Cos(Phi), center.Y + EndCoord.X * Math.Cos(Phi) - EndCoord.Y * Math.Sin(Phi));
            tmp.Node3.UpdateCoord(center.X + Node3.X * Math.Sin(Phi) + Node3.Y * Math.Cos(Phi), center.Y + Node3.X * Math.Cos(Phi) - Node3.Y * Math.Sin(Phi));
            tmp.Node4.UpdateCoord(center.X + Node4.X * Math.Sin(Phi) + Node4.Y * Math.Cos(Phi), center.Y + Node4.X * Math.Cos(Phi) - Node4.Y * Math.Sin(Phi));
            return tmp;
        }

        public IFigure Rotate(double Phi)
        {
            IsoscelesTriangle tmp = new IsoscelesTriangle();
            tmp = this;

            Single PI = (Single)(Math.PI);
            NormPoint C = new NormPoint();
            C.UpdateCoord((BeginCoord.X + EndCoord.X) / 2, (BeginCoord.Y + EndCoord.Y) / 2);
            tmp.BeginCoord.UpdateCoord(C.X + (BeginCoord.X - C.X) * Math.Cos(Phi * PI / 180) - (BeginCoord.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y + (BeginCoord.X - C.X) * Math.Sin(Phi * PI / 180) + (BeginCoord.Y - C.Y) * Math.Cos(Phi * PI / 180));
            tmp.EndCoord.UpdateCoord(C.X + (EndCoord.X - C.X) * Math.Cos(Phi * PI / 180) - (EndCoord.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y + (EndCoord.X - C.X) * Math.Sin(Phi * PI / 180) + (EndCoord.Y - C.Y) * Math.Cos(Phi * PI / 180));
            tmp.Node3.UpdateCoord(C.X + (Node3.X - C.X) * Math.Cos(Phi * PI / 180) - (Node3.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y + (Node3.X - C.X) * Math.Sin(Phi * PI / 180) + (Node3.Y - C.Y) * Math.Cos(Phi * PI / 180));
            tmp.Node4.UpdateCoord(C.X + (Node4.X - C.X) * Math.Cos(Phi * PI / 180) - (Node4.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y + (Node4.X - C.X) * Math.Sin(Phi * PI / 180) + (Node4.Y - C.Y) * Math.Cos(Phi * PI / 180));

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
            a.UpdateCoord(BeginCoord.X, BeginCoord.Y);
            b.UpdateCoord(Node3.X, Node3.Y);
            c.UpdateCoord(EndCoord.X, EndCoord.Y);
            d.UpdateCoord(EndCoord.X, BeginCoord.Y);
            
            screen.drawhighlight(new List<NormPoint> { a, b, b, c, c, d, d, a });
        }

        public void Draw(IPaint screen)
        {
            screen.drawline(new List<NormPoint> { Node3, Node4, Node4, EndCoord, EndCoord, Node3 }, BorderColor, LineThick);
        }

        public void Init(NormPoint p1, NormPoint p2, Color BorderColor, float LineThick)
        {
            throw new NotImplementedException();
        }

        public void SelectLineThick(float p)
        {
            throw new NotImplementedException();
        }

        public void SelectBorderColor(byte p1, byte p2, byte p3, byte p4)
        {
            throw new NotImplementedException();
        }
    }
}
