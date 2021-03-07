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
        private double k;
        private double b;
        private double x0;
        private double x1;

        private List<double> findY()
        {
            double y0 = this.k * this.x0 + this.b;
            double y1 = this.k * this.x1 + this.b;
            List<double> tmp = new List<double>();
            tmp.Add(y0);
            tmp.Add(y1);
            return tmp;
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
