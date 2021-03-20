﻿using System;
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
        Color BorderColor { get; set; }

        bool IsIn(NormPoint p);         //принадлежность точки фигуре
        void Init(NormPoint p1, NormPoint p2);
        void Init(NormPoint p1, NormPoint p2, Color BorderColor, float LineThick);
        IFigure Scale(double koeff);        //изменение размера фигуры процентами
        IFigure MoveByVector(NormPoint v);          //перемещение
        IFigure Rotate(NormPoint center, double Phi);         //поворот 
        IFigure Rotate(double Phi);         //поворот относительно центра
        IFigure Reflection(NormPoint a, NormPoint b);   //отражение
        void Highlight(model.IPaint screen);    //выделение
        void SelectLineThick(float p); //выбор толщины границы
        void SelectBorderColor(byte p1, byte p2, byte p3, byte p4); //выбор цвета граинцы
        void Draw(model.IPaint screen);
    }
}
