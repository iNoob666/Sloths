using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.math;

namespace Sloths.source.model
{
    public interface IFigure
    {
        //Начальная координа определяемая начало фигуры
        NormPoint BeginCoord { get; set; }
        //Коненая координата определяющая размер фигуры
        NormPoint EndCoord { get; set; }
        //void SetCoords(IEnumerable<NormPoint> xy);

        float LineThick { get; set; }
        System.Drawing.Color BorderColor { get; set; }

        bool IsIn(NormPoint p);         //принадлежность точки фигуре
        void Init(NormPoint p1, NormPoint p2);
        void Init(NormPoint p1, NormPoint p2, System.Drawing.Color BorderColor, float LineThick = 1);
        IFigure Scale(double koeff);        //изменение размера фигуры процентами
        IFigure MoveByVector(float x, float y);         //перемещение
        IFigure Rotate(double Phi);         //поворот относительно центра
        void Highlight(model.IPaint screen);    //выделение
        void SelectLineThick(float p); //выбор толщины границы
        void SelectBorderColor(byte p1, byte p2, byte p3, byte p4); //выбор цвета граинцы
        void Draw(model.IPaint screen);
    }
}
