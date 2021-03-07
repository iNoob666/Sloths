using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;

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

        public void Draw(IPaint screen)
        {
            List<double> y = findY();
            List<double> xy = new List<double>();
            xy.Add(this.x0);
            xy.Add(y[0]);
            xy.Add(this.x1);
            xy.Add(y[1]);
            screen.drawline(xy);
        }
    }
}
