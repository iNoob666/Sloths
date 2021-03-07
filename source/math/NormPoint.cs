using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * У OpenGL своя "очень интересная" система координат: 
 * левому нижнему углу соотвествует координата (-1,-1)
 * а правому верхнему (1,1). Как вы понимаете ВПФ об этом понятия не имеет
 * посему приходится ковертить((
 */
namespace Sloths.source.math
{
    public class NormPoint
    {
        private double w,h;
        public double X { get; private set; }
        public double Y { get; private set;  }
        public NormPoint(double x = 0, double y = 0, double w = 1, double h = 1)
        {
            this.w = w;
            this.h = h;
            X = 2 * x / w - 1;
            Y = 2 * (h - y) / h - 1;
        }
        public void UpdateSize(double w, double h)
        {
            this.w = w;
            this.h = h;
            X = 2 * X / w - 1;
            Y = 2 * (h - Y) / h - 1;
        }

        public void UpdateCoord(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
