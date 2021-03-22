using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;


using System.Runtime.Serialization;

namespace Sloths.source.math
{
    [DataContract]
    [KnownType(typeof(Circle))]
    class Circle : IFigure
    {
        [DataMember]
        public NormPoint BeginCoord { get; set; }
        [DataMember]
        public NormPoint EndCoord { get; set; }
        [DataMember]
        public float LineThick { get; set; }
        [DataMember]
        public Color BorderColor { get; set; }
        [DataMember]
        private double R { get; set; }
        [DataMember]
        private NormPoint C { get; set; }

        public Circle()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
            BorderColor = Color.FromRGBA(0,0,0,1);
            C = new NormPoint();
            LineThick = 1;
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

        public void Init(NormPoint p1, NormPoint p2, Color Color, float Thick)
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
            SelectBorderColor(Color.R, Color.G, Color.B, Color.A);
            SelectLineThick(Thick);

        }

        public void SelectLineThick(float p)
        {
            LineThick = p;
        }

        public void SelectBorderColor(byte p1, byte p2, byte p3, byte p4)
        {
            BorderColor = Color.FromRGBA(p1,p2,p3,p4);
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
            tmp.BeginCoord.UpdateCoord(C.X + (BeginCoord.X - C.X) * koeff, C.Y + (BeginCoord.Y - C.Y) * koeff);
            tmp.EndCoord.UpdateCoord(C.X + (EndCoord.X - C.X) * koeff, C.Y + (EndCoord.Y - C.Y) * koeff);
            tmp.Init(tmp.BeginCoord, tmp.EndCoord, BorderColor, LineThick);
            tmp.R = R * koeff;
            tmp.C = C;
            return tmp;
        }
        public IFigure MoveByVector(float x, float y)
        {
            Circle tmp = new Circle();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X + x, this.BeginCoord.Y + y);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X + x, this.EndCoord.Y + y);
            //tmp.C.UpdateCoord(C.X + x, C.Y + y);
            //tmp.R = R;
            tmp.Init(tmp.BeginCoord, tmp.EndCoord, BorderColor, LineThick);
            return tmp;
        }

        public IFigure Rotate(double Phi)
        {
            return this;
        }

        public void Highlight(IPaint screen)
        {
            NormPoint a = new NormPoint();
            NormPoint b = new NormPoint();
            a.UpdateCoord(EndCoord.X, BeginCoord.Y);
            b.UpdateCoord(BeginCoord.X, EndCoord.Y);
            screen.drawhighlight(new List<NormPoint> { BeginCoord, a,BeginCoord, b,b, EndCoord,EndCoord, a  });
        }


        public void Draw(IPaint screen)
        {
            screen.drawcircle(C, R, BorderColor,LineThick);
        }
    }
}
