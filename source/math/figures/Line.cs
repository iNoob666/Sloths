using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sloths.source.math
{
    class Line : IFigure
    {
        //private double k;
        //private double b;
        //private double x0;
        //private double x1;
        //Начальная координа определяемая начало фигуры
        public NormPoint BeginCoord { get; set; }
        //Коненая координата определяющая размер фигуры
        public NormPoint EndCoord { get; set; }
        public Line()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
        }
        public void Draw(IPaint screen)
        {
            screen.drawline(new List<NormPoint> { BeginCoord, EndCoord });
        }

        public bool IsIn(NormPoint p)
        {
            double dX = EndCoord.X - BeginCoord.X;
            double dY = EndCoord.Y - BeginCoord.Y;
            dX= (p.X - BeginCoord.X)/dX;
            dY = dX * dY + BeginCoord.Y;
            if ((dY - p.Y) / dY < 0.001) return true;
            else return false;           
        }

        public IFigure Scale(double koeff)
        {
            Line tmp = new Line();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X * Math.Sqrt(koeff),this.BeginCoord.Y * Math.Sqrt(koeff));
            tmp.EndCoord.UpdateCoord(this.EndCoord.X * Math.Sqrt(koeff), this.EndCoord.Y * Math.Sqrt(koeff));
            return tmp;
        }

        public IFigure MoveByVector(NormPoint v) 
        {
            Line tmp = new Line();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X + v.X, this.BeginCoord.Y + v.Y);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X + v.X, this.EndCoord.Y + v.Y);
            return tmp;
        }

        public IFigure Rotate(NormPoint center, double Phi)
        {
            return this;
        }

        public IFigure Rotate(double Phi)
        {
            return this;
        }

        public IFigure Reflection(NormPoint a, NormPoint b)
        {
            return this;
        }
    }
}
