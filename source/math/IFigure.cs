using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloths.source.math
{
    public interface IFigure
    {
        bool IsIn(Point p);         //принадлежность точки фигуре
        IFigure Scale(double percentX, double percentY);        //изменение размера фигуры процентами
        IFigure Fit(double w, double h);        //изменение размера фигуры значениями
        IFigure MoveByVector(Point v);          //перемещение
        IFigure Rotate(Point center, double Phi);         //поворот 
        IFigure Rotate(double Phi);         //поворот относительно центра
        IFigure Reflection(Point a, Point b);   //отражение
        void Draw(IGraphic screen, IFigureGraphicParameters param); //рисование
    }
}
