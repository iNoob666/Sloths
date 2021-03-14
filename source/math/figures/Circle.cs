using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sloths.source.math
{
    class Circle : IFigure
    {
        public NormPoint BeginCoord { get; set; }
        public NormPoint EndCoord { get; set; }

        private double R { get; set; }
        private NormPoint C { get; set; }

        public Circle()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
            C = new NormPoint();
        }

        public void Init(NormPoint p1, NormPoint p2)
        {
            BeginCoord = p1;
            EndCoord = p2;
            bool tmp;
            int flag;
            if (Math.Abs(p1.X - p2.X) < Math.Abs(p1.Y - p2.Y))
            {
                R = Math.Abs(p1.X - p2.X) / 2;
                if (p1.Y > p2.Y) flag = 1;
                else flag = -1;
                EndCoord.UpdateCoord(p2.X, p1.Y - flag * Math.Abs(p2.X - p1.X));
            }
            else
            { 
                R = Math.Abs(p1.Y - p2.Y) / 2;
                if (p1.X > p2.X) flag = 1;
                else flag = -1;
                EndCoord.UpdateCoord(p1.X - flag * Math.Abs(p1.Y - p2.Y), p2.Y);
            }
            C.UpdateCoord((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        public bool IsIn(NormPoint p)
        {
            if ((p.X - C.X) * (p.X - C.X) + (p.Y - C.Y) * (p.Y - C.Y) < R*R)
                return true;
            return false;
        }

        public IFigure Scale(double koeff)
        {
            Circle tmp = new Circle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X * koeff, this.BeginCoord.Y * koeff);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X * koeff, this.EndCoord.Y * koeff);
            tmp.R = R * koeff;
            tmp.C = C;
            return tmp;
        }

        public IFigure MoveByVector(NormPoint v)// ? фигня какая-то, надо изменить.
        {
            Circle tmp = new Circle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X + v.X, this.BeginCoord.Y + v.Y);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X + v.X, this.EndCoord.Y + v.Y);
            tmp.C.UpdateCoord(this.C.X + v.X, this.C.Y + v.Y);
            return tmp;
        }

        public IFigure Rotate(NormPoint center, double Phi)
        {
            Circle tmp = new Circle();
            tmp = this;
            tmp.BeginCoord.UpdateCoord(center.X + BeginCoord.X * Math.Sin(Phi) + BeginCoord.Y * Math.Cos(Phi), center.Y + BeginCoord.X * Math.Cos(Phi) - BeginCoord.Y * Math.Sin(Phi));
            tmp.EndCoord.UpdateCoord(center.X + EndCoord.X * Math.Sin(Phi) + EndCoord.Y * Math.Cos(Phi), center.Y + EndCoord.X * Math.Cos(Phi) - EndCoord.Y * Math.Sin(Phi));
            tmp.C.UpdateCoord(center.X + C.X * Math.Sin(Phi) + C.Y * Math.Cos(Phi), center.Y + C.X * Math.Cos(Phi) - C.Y * Math.Sin(Phi));
            return tmp;
        }

        public IFigure Rotate(double Phi)
        {
            return this;
        }

        public IFigure Reflection(NormPoint a, NormPoint b)
        {
            return this;
        }


        public void Draw(IPaint screen)
        {
            screen.drawcircle(C, R);
        }
    }
}
