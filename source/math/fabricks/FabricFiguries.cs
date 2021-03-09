using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;
/*
 * Я не очень понимаю зачем нам нужны три фабрики для каждого типа фигуры
 * как я понимаю с каждой фигурой мы будем работать одинаково 
 * складывать их в лист фигур для отрисовки и собственно создавать их.
 * Так что я создал свой класс для фабрики (Я ХУДОЖНИК Я ТАК ВИЖУ!!!11!!1)
 * (А учитывая что пишу я это в час ночи вижу я всякое :Р)
 */
namespace Sloths.source.math
{
    public static class FabricFiguries
    {
        static FabricFiguries()
        {
            ListOfFigures = new List<IFigure>();
        }
        public static List<IFigure> ListOfFigures { get; }
        //Если по простому то метод этого класса создает экземпляр класса по его названию
        //Из этого следует что: КНОПКИ ДОЛЖНЫ НАЗЫВАТЬСЯ ТАК ЖЕ КАК И КЛАССЫ!!!!
        //(это не работает)
        public static IFigure Create(string name)
        {
            //потом сделаю свич
            return new Line();
            //object obj = Activator.CreateInstance("Sloths", "Sloths.source.math." + name);
            //var t = obj.GetType();
            //obj = Activator.CreateInstance(t);
            //return obj as IFigure;
        }
        public static void AddFigureToFabric(IFigure newfig)
        {

            ListOfFigures.Add(newfig);
        }
        public static void DrawAll(model.IPaint screen)
        {
            if(ListOfFigures != null) //костыль
                foreach (IFigure figure in ListOfFigures) figure.Draw(screen);
        }
    }
}
