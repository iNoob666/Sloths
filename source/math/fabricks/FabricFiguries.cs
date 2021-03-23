using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using System.Windows.Input;
using Sloths.source.model;
using System.Runtime.Serialization;
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
            SelectedItem = -1;
        }

        public static List<IFigure> ListOfFigures { get; } //список всех нарисованых фигур
        private static int SelectedItem { get; set; }
        //ВРЕМЕННО 
        //Рома хочет сделать фабрики для каждого типа фигур
        //будем переделывать
        public static IFigure Create(string name) //Создание фигуры name - название фигуры 
        {
            SelectedItem = -1;
            switch (name)
            {
                case "Line":
                    return new Line();
                case "Circle":
                    return new Circle();
                case "Rectangle":
                    return new Rectangle();
                case "Triangle2":
                    return new IsoscelesTriangle();
                case "Triangle":
                    return new RightTriangle();
                default:
                    return new Line(); //что нибудь придумать для дефолта
            }  
        }

        public static void AddFigureToFabric(IFigure newfig) //Добавление фигуры в список newfig - фигура
        {
            ListOfFigures.Add(newfig);
        }
        public static void DeleteSelectedFigureFromFabric()
        {
            if (SelectedItem != -1)  ListOfFigures.Remove(ListOfFigures[SelectedItem]);
            SelectedItem = -1;
        }
        public static void DrawAll(IPaint screen) //Отрисовка всех фигур из списка screen - класс полотна на котором рисуем 
        {

            for (int i = 0; i < ListOfFigures.Count(); i++)
            {
                ListOfFigures[i].Draw(screen);
            }
            if(SelectedItem != -1) ListOfFigures[SelectedItem].Highlight(screen);
            screen._flush();
        }

        public static void Update() //Изменение координат фигур при изменении размеров полотна 
        {
            foreach(IFigure fig in ListOfFigures)
            {
                fig.BeginCoord.UpdateSize();
                fig.EndCoord.UpdateSize();
                fig.Init(fig.BeginCoord, fig.EndCoord);
            }
        }
        public static void SelectFigure(NormPoint point)
        {
            int i = 1;
            for (; i <= ListOfFigures.Count(); i++)
            {
                if (ListOfFigures[ListOfFigures.Count() - i].IsIn(point))
                {
                    SelectedItem = ListOfFigures.Count() - i;
                    break;
                };
            }
        }
        public static Color GetColor(NormPoint point)
        {
            foreach(IFigure elem in ListOfFigures)
            {
                if (elem.IsIn(point)) return elem.BorderColor;
            }
            return Color.WHITE;
                    
        }
        /// <summary>
        /// Ивенты для работы с фигурами
        /// </summary>
        public static void Undo()
        {
            if (ListOfFigures.Count() > 0)
            {
                ListOfFigures.RemoveAt(ListOfFigures.Count() - 1);
                SelectedItem = -1;
            }
        }
        public static void Redo()
        {
            if (ListOfFigures.Count() > 0)
                ListOfFigures.Add(ListOfFigures[ListOfFigures.Count() - 1].MoveByVector(0.05f, 0.05f));

        }
        public static void OpenEvent(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        //пкркмещение фигур в пространстве
        internal static void UpEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].MoveByVector(0,0.02f);
        }

        internal static void DownEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].MoveByVector(0,-0.02f);
        }

        internal static void RightEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].MoveByVector(0.02f, 0);
        }

        internal static void LeftEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].MoveByVector(-0.02f, 0);
        }
        internal static void СounterClockWiseEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Rotate(1);
        }
        internal static void ClockWiseEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Rotate(-1);
        }
        internal static void PlusSizeEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Scale(1.1);
        }
        internal static void MinusSizeEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Scale(0.9);
        }
        /* internal static void СounterClockWiseAroundCenterEvent()
         {
             ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Rotate(new NormPoint(0,0), 0.0001);
         }*/
        /*internal static void ClockWiseAroundCenterEvent()
        {
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Rotate(new NormPoint(0, 0), -0.0001);
        }*/
    }
}

