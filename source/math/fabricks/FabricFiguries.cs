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
            UndoStack = new Stack<IFigure>();
            currentFigure = new Line();
            SelectedItem = -1;
        }

        public static List<IFigure> ListOfFigures { get; } //список всех нарисованых фигур
        public static Stack<IFigure> UndoStack { get; }

        internal static void ClearFabric()
        {
            ListOfFigures.Clear();
        }

        private static int SelectedItem { get; set; }
        private static IFigure currentFigure;
        private static System.Drawing.Color currentColor;
        private static System.Drawing.Color CurrentColor
        {
            get
            {
                return currentColor;
            }
            set
            {
                currentColor = value;
                currentFigure.BorderColor = currentColor;
            }
         
        }


        private static float currentThickness;
        private static float CurrentThickness
        {
            get
            {
                return currentThickness;
            }
            set
            {
                currentThickness = value;
                currentFigure.LineThick = currentThickness;
            }
        }

        public static void Create(string name) //Создание фигуры name - название фигуры 
        {
            SelectedItem = -1;
            var type = Type.GetType("Sloths.source.math." + name);
            currentFigure = (IFigure)Activator.CreateInstance(type);
            //currentFigure.LineThick = CurrentThickness;
            //currentFigure.BorderColor = CurrentColor;
            //switch (name)
            //{
            //    case "Line":
            //        return new Line();
            //    case "Circle":
            //        return new Circle();
            //    case "Rectangle":
            //        return new Rectangle();
            //    case "Triangle2":
            //        return new IsoscelesTriangle();
            //    case "Triangle":
            //        return new RightTriangle();
            //    default:
            //        return new Line(); //что нибудь придумать для дефолта
            //}
        }
        public static void ReCreate()
        {
            var type = currentFigure.GetType();
            var Thickness = currentFigure.LineThick;
            var Color = currentFigure.BorderColor;
            currentFigure = (IFigure)Activator.CreateInstance(type);
            currentFigure.LineThick = Thickness;
            currentFigure.BorderColor = Color;

        }
        public static void AddCurrenFigtToList() => ListOfFigures.Add(currentFigure);

        public static void DrawCurrenFigure(IPaint screen)
        { 
            if(currentFigure != null) currentFigure.Draw(screen);
        }
        public static void DrawAll(IPaint screen) //Отрисовка всех фигур из списка screen - класс полотна на котором рисуем 
        {

            for (int i = 0; i < ListOfFigures.Count(); i++)
            {
                ListOfFigures[i].Draw(screen);
            }
            if (SelectedItem != -1) ListOfFigures[SelectedItem].Highlight(screen);
            screen._flush();
        }
        public static void SetBegin(NormPoint p) => currentFigure.BeginCoord = p;
        public static void SetEnd(NormPoint p) => currentFigure.EndCoord = p;
        public static void SetColor(System.Drawing.Color color) => currentFigure.BorderColor = color;
        public static void SetThickness(float thickness) => currentFigure.LineThick = thickness;
        public static void Initialization() => currentFigure.Init(currentFigure.BeginCoord, currentFigure.EndCoord);
        public static void Update() //Изменение координат фигур при изменении размеров полотна 
        {
            foreach (IFigure fig in ListOfFigures)
            {
                fig.BeginCoord.UpdateSize();
                fig.EndCoord.UpdateSize();
                fig.Init(fig.BeginCoord, fig.EndCoord);
            }
        }
        public static void AddFigureToFabric() //Добавление фигуры в список newfig - фигура
        {
            ListOfFigures.Add(currentFigure);
        }


        public static void DeleteSelectedFigureFromFabric()
        {
            if (SelectedItem != -1)
            {
                UndoStack.Push(ListOfFigures[SelectedItem]);
                ListOfFigures.Remove(ListOfFigures[SelectedItem]); 
            }
            SelectedItem = -1;
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
        public static System.Drawing.Color GetColor(NormPoint point)
        {
            foreach(IFigure elem in ListOfFigures)
            {
                if (elem.IsIn(point)) return elem.BorderColor;
            }
            return System.Drawing.Color.White;
                    
        }
        public static void Undo()
        {
            if (ListOfFigures.Count() > 0)
            {
                UndoStack.Push(ListOfFigures[ListOfFigures.Count() - 1]);
                ListOfFigures.RemoveAt(ListOfFigures.Count() - 1);
            }
            if (SelectedItem >= 0) SelectedItem--;
        }
        public static void Redo()
        {
            if (UndoStack.Count() > 0)
                ListOfFigures.Add(UndoStack.Pop());

        }

        //Legacy
        /// <summary>
        /// Ивенты для работы с фигурами
        /// </summary>

        public static void OpenEvent(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void AddFigureToFabric(IFigure newfig) //Добавление фигуры в список newfig - фигура
        {
            ListOfFigures.Add(newfig);
        }
        //пкркмещение фигур в пространстве
        internal static void UpEvent()
        {
            if(SelectedItem != -1)
            ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].MoveByVector(0,0.02f);
        }

        internal static void DownEvent()
        {
            if (SelectedItem != -1)
                ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].MoveByVector(0,-0.02f);
        }

        internal static void RightEvent()
        {
            if (SelectedItem != -1)
                ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].MoveByVector(0.02f, 0);
        }

        internal static void LeftEvent()
        {
            if (SelectedItem != -1)
                ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].MoveByVector(-0.02f, 0);
        }
        internal static void СounterClockWiseEvent()
        {
            if (SelectedItem != -1)
                ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Rotate(1);
        }
        internal static void ClockWiseEvent()
        {
            if (SelectedItem != -1)
                ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Rotate(-1);
        }
        internal static void PlusSizeEvent()
        {
            if (SelectedItem != -1)
                ListOfFigures[SelectedItem] = ListOfFigures[SelectedItem].Scale(1.1);
        }
        internal static void MinusSizeEvent()
        {
            if (SelectedItem != -1)
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

