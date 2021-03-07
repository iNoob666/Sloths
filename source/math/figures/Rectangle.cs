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
        public NormPoint BeginCoord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public NormPoint EndCoord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Rectangle()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
        }

        public bool IsIn(NormPoint p)
        {
            if (p.X > BeginCoord.X  && p.X < EndCoord.X || p.X < BeginCoord.X && p.X > EndCoord.X)
                if (p.Y > BeginCoord.Y && p.Y < EndCoord.Y || p.Y < BeginCoord.Y && p.Y > EndCoord.Y)
                    return true;
            return false;
        }

        public IFigure Scale(double koeff)
        {
            Rectangle tmp = new Rectangle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X * koeff, this.BeginCoord.Y * koeff);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X * koeff, this.EndCoord.Y * koeff);
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


       /* public void Draw()
        {

        }*/

        public void Draw(IPaint screen)
        {
            NormPoint a,b;
            a = BeginCoord;
            b = BeginCoord;
            a.UpdateCoord(BeginCoord.X,EndCoord.Y);
            b.UpdateCoord(EndCoord.X, BeginCoord.Y);
            screen.drawline(new List<NormPoint> { BeginCoord, a });
            screen.drawline(new List<NormPoint> { BeginCoord, b });
            screen.drawline(new List<NormPoint> { b, EndCoord });
            screen.drawline(new List<NormPoint> { EndCoord, a });
        }
    }
}
