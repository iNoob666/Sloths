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

        public double R { get; set; }
        public NormPoint C { get; set; }

        public Circle()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
        }

        public void InitRC()
        {
            bool tmp;
            int flag;
            if (BeginCoord.X - EndCoord.X < BeginCoord.Y - EndCoord.Y)
            {
                R = (BeginCoord.X - EndCoord.X) / 2;
                if (BeginCoord.Y > EndCoord.Y) flag = 1;
                else flag = -1;
                EndCoord.UpdateCoord(EndCoord.X, BeginCoord.Y - flag * (BeginCoord.X + EndCoord.X));
            }
            else
            { 
                R = (BeginCoord.Y - EndCoord.Y) / 2;
                if (BeginCoord.X > EndCoord.X) flag = 1;
                else flag = -1;
                EndCoord.UpdateCoord(BeginCoord.X - flag * (BeginCoord.Y - EndCoord.Y), EndCoord.Y);
            }
            C.UpdateCoord((BeginCoord.X + EndCoord.X) / 2, (BeginCoord.Y + EndCoord.Y) / 2);
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

        public IFigure MoveByVector(NormPoint v)
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


        public void Draw()
        {

        }

        public void Draw(IPaint screen)
        {
            throw new NotImplementedException();//передается точка центра и радиус
        }
    }
}
