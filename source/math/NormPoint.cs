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
        private double mouse_x, mouse_y; // Координаты мыши
        //Размеры полотна по которому нормируем
        public static double Widht; 
        public static double Height;
        public double X { get; private set; }
        public double Y { get; private set;  }
        public NormPoint(double x = 0, double y = 0)
        {
            //переводим позицию мыши в кооринаты OpenGL
            X = 2 * x / Widht - 1;
            Y = 2 * (Height - y) / Height - 1;
            mouse_x = x; mouse_y = y;
        }
        public void UpdateSize()
        {
            //переводим позицию мыши в кооринаты OpenGL
            X = 2 * mouse_x / Widht - 1;
            Y = 2 * (Height - mouse_y) / Height - 1;
        }
    }
}
