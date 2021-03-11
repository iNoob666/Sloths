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
        public static List<IFigure> ListOfFigures { get; } //список всех нарисованых фигур
        //ВРЕМЕННО 
        //Рома хочет сделать фабрики для каждого типа фигур
        //будем переделывать
        public static IFigure Create(string name) //Создание фигуры name - название фигуры 
        {

            switch (name)
            {
                case "Line":
                    return new Line();
                case "Circle":
                    return new Circle();
                case "Rectangle":
                    return new Rectangle();
                default:
                    return new Line(); //что нибудь придумать для дефолта

            }  
        }
        public static void AddFigureToFabric(IFigure newfig) //Добавление фигуры в список newfig - фигура
        {

            ListOfFigures.Add(newfig);
        }
        public static void DrawAll(model.IPaint screen) //Отрисовка всех фигур из списка screen - клас полотна на котором рисуем 
        {
            if(ListOfFigures != null) //костыль
                foreach (IFigure figure in ListOfFigures) figure.Draw(screen);
        }
        public static void Update() //Изменение координат фигур при изменении размеров полотна 
        {
            foreach(IFigure fig in ListOfFigures)
            {
                fig.BeginCoord.UpdateSize();
                fig.EndCoord.UpdateSize();
            }
        }
    }
}
