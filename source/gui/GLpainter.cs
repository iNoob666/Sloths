using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloths.source.model;

using SharpGL;
using Sloths.source.math;
using System.Windows.Media;

namespace Sloths.source.gui
{
    class GLpainter : IPaint
    {
        private OpenGL openGL;
        public GLpainter(ref OpenGL gl)
        {
            openGL = gl;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            openGL.ClearColor(1f, 1f, 1f, 0.3f);
        }
        void IPaint.drawline(IEnumerable<NormPoint> xy) //Отрисовка линии
        {

            openGL.Begin(OpenGL.GL_LINES); //Начало рисования
            openGL.Color(0f, 0f, 0f); //Задаем цвет
            foreach(NormPoint p in xy) openGL.Vertex(p.X, p.Y); //Отрисовываем точки
            openGL.End();
        }
        void IPaint.drawcircle(NormPoint xy, double rad) //Отрисовка круга
        {
            
            Single twicePI = (Single)(2.0f * Math.PI); 
            int stop = 100; //количество линий в "многоугольнике"
            openGL.Begin(OpenGL.GL_LINE_LOOP); 
            openGL.Color(0f, 0f, 0f);
            //Single x = (float)(xy.X + rad);
            //Single y = (float) xy.Y;
            //openGL.Vertex(x, y);
            for (int i = 0; i <= stop; i++)
            {
                //считаем позиции x,y для следующей точки

                Single x = (float)(xy.X + rad*Math.Cos(i * twicePI / stop));
                Single y = (float)(xy.Y + rad*Math.Sin(i * twicePI / stop));
                //Single x1 = (float)(xy.X + x * Math.Sin(i * twicePI / stop) + y * Math.Cos(i * twicePI / stop));
                //Single y1 = (float)(xy.Y + x * Math.Cos(i * twicePI / stop) - y * Math.Sin(i * twicePI / stop)); 
                //рисуем точку
                openGL.Vertex(x, y); 
            }

            openGL.End();
        }
    }
}
