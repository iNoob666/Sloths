using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sloths.source.model
{
    public interface IFigure
    {
        //Начальная координа определяемая начало фигуры
        NormPoint BeginCoord { get; set; }
        //Коненая координата определяющая размер фигуры
        NormPoint EndCoord { get; set; }
        //void SetCoords(IEnumerable<NormPoint> xy);

        bool IsIn(NormPoint p);         //принадлежность точки фигуре

        IFigure Scale(double koeff);        //изменение размера фигуры процентами
        IFigure MoveByVector(NormPoint v);          //перемещение
        IFigure Rotate(NormPoint center, double Phi);         //поворот 
        IFigure Rotate(double Phi);         //поворот относительно центра
        IFigure Reflection(NormPoint a, NormPoint b);   //отражение

 
        void Draw(model.IPaint screen);
    }
}
