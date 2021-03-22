using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
/*
 * У OpenGL своя "очень интересная" система координат: 
 * левому нижнему углу соотвествует координата (-1,-1)
 * а правому верхнему (1,1). Как вы понимаете ВПФ об этом понятия не имеет
 * посему приходится ковертить((
 */
namespace Sloths.source.math
{
    [DataContract]
    [KnownType(typeof(NormPoint))]
    public class NormPoint
    {
        [DataMember]
        private double mouse_x, mouse_y; // Координаты мыши
        //Размеры полотна по которому нормируем
        [DataMember]
        public static double Widht;
        [DataMember]
        public static double Height;
        [DataMember]
        public double X { get; private set; }
        [DataMember]
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

        public void UpdateCoord(double x, double y)
        {
            X = x;
            Y = y;
        }

    }
}
