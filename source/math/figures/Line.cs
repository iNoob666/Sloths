using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;
using System.Runtime.Serialization;

namespace Sloths.source.math
{
    [DataContract]
    [KnownType(typeof(Line))]
    class Line : IFigure
    {
        //private double k;
        //private double b;
        //private double x0;
        //private double x1;
        //Начальная координа определяемая начало фигуры
        [DataMember]
        public NormPoint BeginCoord { get; set; }
        //Коненая координата определяющая размер фигуры
        [DataMember]
        public NormPoint EndCoord { get; set; }
        [DataMember]
        public float LineThick { get; set; }
        [DataMember]
        public System.Drawing.Color BorderColor { get; set; }
        public Line()
        {
            BeginCoord = new NormPoint();
            EndCoord = new NormPoint();
            BorderColor = System.Drawing.Color.FromArgb(0, 0, 0, 1);
            LineThick = 1;
        }

        public void Init(NormPoint p1, NormPoint p2)
        {
            BeginCoord = p1;
            EndCoord = p2;
        }

        public void Init(NormPoint p1, NormPoint p2, System.Drawing.Color Color, float Thick = 1)
        {
            BeginCoord = p1;
            EndCoord = p2;
            SelectBorderColor(Color.R, Color.G, Color.B, Color.A);
            SelectLineThick(Thick);
        }


        public void SelectLineThick(float p)
        {
            LineThick = p;
        }

        public void SelectBorderColor(byte p1, byte p2, byte p3, byte p4)
        {
            BorderColor = System.Drawing.Color.FromArgb(p4, p1, p2, p3);
        }

        public void Draw(IPaint screen)
        {
            screen.drawline(new List<NormPoint> { BeginCoord, EndCoord }, BorderColor, LineThick);
        }

        public bool IsIn(NormPoint p)
        {
            double dX = EndCoord.X - BeginCoord.X;
            double dY = EndCoord.Y - BeginCoord.Y;
            dX= (p.X - BeginCoord.X)/dX;
            dY = dX * dY + BeginCoord.Y;
            if (p.Y < dY + LineThick/100 && p.Y > dY - LineThick / 10) return true;
            else return false;           
        }

        public IFigure Scale(double koeff)
        {
            Line tmp = new Line();
            double CX = (BeginCoord.X + EndCoord.X) / 2;
            double CY = (BeginCoord.Y + EndCoord.Y) / 2;
            tmp.BeginCoord.UpdateCoord(CX + (BeginCoord.X-CX) * koeff, CY + (BeginCoord.Y-CY) * koeff);
            tmp.EndCoord.UpdateCoord(CX + (EndCoord.X - CX) * koeff, CY + (EndCoord.Y - CY) * koeff);
            tmp.Init(tmp.BeginCoord, tmp.EndCoord, BorderColor, LineThick);
            return tmp;
        }

        public IFigure MoveByVector(float x, float y)
        {
            Line tmp = new Line();
            tmp.BeginCoord.UpdateCoord(this.BeginCoord.X + x, this.BeginCoord.Y + y);
            tmp.EndCoord.UpdateCoord(this.EndCoord.X + x, this.EndCoord.Y + y);
            tmp.Init(tmp.BeginCoord, tmp.EndCoord, BorderColor, LineThick);
            return tmp;
        }

        public IFigure Rotate(double Phi)
        {
            Line tmp = new Line();
            tmp = this;
            Single PI = (Single)(Math.PI);
            NormPoint C = new NormPoint();
            C.UpdateCoord((BeginCoord.X + EndCoord.X) / 2, (BeginCoord.Y + EndCoord.Y) / 2);
            tmp.BeginCoord.UpdateCoord(C.X+(BeginCoord.X - C.X) * Math.Cos(Phi * PI / 180) - (BeginCoord.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y+(BeginCoord.X - C.X) * Math.Sin(Phi * PI / 180) + (BeginCoord.Y - C.Y) * Math.Cos(Phi * PI / 180));
            tmp.EndCoord.UpdateCoord(C.X+(EndCoord.X - C.X) * Math.Cos(Phi * PI / 180) - (EndCoord.Y - C.Y) * Math.Sin(Phi * PI / 180), C.Y+(EndCoord.X - C.X) * Math.Sin(Phi * PI / 180) + (EndCoord.Y - C.Y) * Math.Cos(Phi * PI / 180));
            return tmp;
        }

        public void Highlight(IPaint screen)
        {
            NormPoint a = new NormPoint();
            NormPoint b = new NormPoint();
            NormPoint c = new NormPoint();
            NormPoint d = new NormPoint();
            a.UpdateCoord(BeginCoord.X + 0.001 , BeginCoord.Y + 0.01 + LineThick / 200);
            b.UpdateCoord(EndCoord.X + 0.001, EndCoord.Y + 0.01 + LineThick / 200);
            c.UpdateCoord(BeginCoord.X - 0.01 , BeginCoord.Y - 0.001 + LineThick / 200);
            d.UpdateCoord(EndCoord.X - 0.01,  EndCoord.Y - 0.001 + LineThick / 200);
            screen.drawhighlight(new List<NormPoint> { a, b,c,d });

        }

    }
}
