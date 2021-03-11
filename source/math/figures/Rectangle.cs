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
        public NormPoint Node3 { get; set; }
        public NormPoint Node4 { get; set; }

        public Rectangle()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
        }

        public void InitNodes()
        {
            Node3.UpdateCoord(BeginCoord.X, EndCoord.Y);
            Node4.UpdateCoord(EndCoord.X, BeginCoord.Y);
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


        public void Draw(IPaint screen)
        {
            Node3.UpdateCoord(BeginCoord.X,EndCoord.Y);
            Node4.UpdateCoord(EndCoord.X, BeginCoord.Y);
            screen.drawline(new List<NormPoint> { BeginCoord, Node3 });
            screen.drawline(new List<NormPoint> { BeginCoord, Node4 });
            screen.drawline(new List<NormPoint> { Node4, EndCoord });
            screen.drawline(new List<NormPoint> { EndCoord, Node3 });
        }
    }
}
