using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.WPF;
using Sloths.source.gui;
using Sloths.source.math;
using Sloths.source.model;
using Sloths.source.file_system;

namespace Sloths.source.ViewModel
{
    class CanvasVM
    {
        private OpenGLControl GLCanvas;//Класс полотна
        private GLpainter Painter; //Класс рисования на полотне 
        private bool PreviousButtonFlag; //false если seclect, true если фигура
        public CanvasVM(OpenGLControl OpenGLCanvas)
        {
  
            GLCanvas = OpenGLCanvas;
            //Биндим ивенты
            GLCanvas.OpenGLInitialized += OpenGLControl_OpenGLInitialized;
            GLCanvas.OpenGLDraw += OpenGLControl_OpenGLDraw;
            GLCanvas.Resized += OpenGLControl_Resized;
            
            SetPaintEvents();//По умолчанию нажата кнопка фигуры. Назначаем его ивенты.

        }
        public void SetSelectFigureEvents()
        {
            PreviousButtonFlag = false;
            GLCanvas.MouseMove -= MouseMove_Event;
            GLCanvas.MouseLeftButtonUp -= MouseUp_Event;
            GLCanvas.MouseLeftButtonDown -= MouseDown_Event;
            GLCanvas.MouseLeftButtonDown += Select_Event;            

        }
        public void SetPaintEvents()
        {
            PreviousButtonFlag = true;
            GLCanvas.MouseLeftButtonDown -= Select_Event;
            GLCanvas.MouseLeftButtonDown += MouseDown_Event;
        }

        public void SetEventbyButtonName(string Name)
        {
            var CurrentBtnFlag = Name != "SelectMode";
            if(CurrentBtnFlag != PreviousButtonFlag)
                if (Name == "SelectMode") SetSelectFigureEvents();
                else if (!PreviousButtonFlag) SetPaintEvents();

        }

        private void Select_Event(object sender, MouseButtonEventArgs e)
        {
            var MouseCoord = e.GetPosition(GLCanvas);
            FabricFiguries.SelectFigure(new NormPoint(MouseCoord.X, MouseCoord.Y));
        }

        /*
            Евенты для рисования фигуры мышью
        */
        //Ивент срабатывающий при нажатии на холст левой кнопкой мыши.
        //При нажатии на холст создается фигура
        private void MouseDown_Event(object sender, MouseButtonEventArgs e)
        {
            GLCanvas.MouseLeftButtonDown -= MouseDown_Event;
            //FabricFiguries.Create(id); //Создаем экземляр класса фигуры
            var gLControl = (OpenGLControl)sender;
            var MouseCoord = e.GetPosition(GLCanvas); //Сичтываем позицию мыши на полотне
            //Назначаем начальную координату фигуры, которая в дальнейшем меняться не будет
            FabricFiguries.SetBegin(new NormPoint(MouseCoord.X, MouseCoord.Y));
            FabricFiguries.SetEnd(new NormPoint(MouseCoord.X, MouseCoord.Y));
            FabricFiguries.Initialization();
            //Figure.Init(new NormPoint(MouseCoord.X, MouseCoord.Y), new NormPoint(MouseCoord.X, MouseCoord.Y), Color_, Thickness);
            //Вторая координата фигуры, которая в дальнейшем будет меняться в ивенте MouseMove_Event
            GLCanvas.MouseMove += MouseMove_Event; //Назначаем ивент для движения мышью с помощью которого будем менять размер фигуры
            GLCanvas.MouseLeftButtonUp += MouseUp_Event; //Назначаем ивент при отпуске левой кнопки мыши заканчивающий создание фигуры
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            var MouseCoord = e.GetPosition(GLCanvas);//Сичтываем позицию мыши на полотне
            //Меняем вторую координату фигуры для изменения размера и положения фигуры 
            FabricFiguries.SetEnd(new NormPoint(MouseCoord.X, MouseCoord.Y));
            FabricFiguries.Initialization();
            //Figure.EndCoord = new NormPoint(MouseCoord.X, MouseCoord.Y);
        }

        private void MouseUp_Event(object sender, MouseButtonEventArgs e)
        {
            GLCanvas.MouseMove -= MouseMove_Event;
            GLCanvas.MouseLeftButtonUp -= MouseUp_Event;
            //Добавляем фигуру в фабрику для дальнейшей отрисовки
            FabricFiguries.AddFigureToFabric();
            FabricFiguries.ReCreate();


            GLCanvas.MouseLeftButtonDown += MouseDown_Event;

        }
        /*
          Сегмент кода про OpenGL
        */
        public void OpenGLControl_OpenGLInitialized(object sender, OpenGLRoutedEventArgs args) //Инициализация полотна 
        {
            var gl = args.OpenGL;
            Painter = new GLpainter(ref gl); //Создаем экземляр класса GLpainter с помощью которого рисуем графику
        }

        public void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args) //Ивент для отрисовки кадра
        {
            args.OpenGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT); //Отчищаем полотно
            //if (FabricFiguries.CurrentFigure != null) Figure.Draw(Painter); //Отрисовка новой фигуры
            FabricFiguries.DrawCurrenFigure(Painter);
            FabricFiguries.DrawAll(Painter); //Рисуем все фигуры из фабрики
        }

        public void OpenGLControl_Resized(object sender, OpenGLRoutedEventArgs args) //Ивент изменения размера окна
        {
            var glContol = (OpenGLControl)sender;
            //Присваиваем новые размеры полотна
            NormPoint.Height = glContol.ActualHeight;
            NormPoint.Widht = glContol.ActualWidth;
            //Изменяем положение фигур на новом холсте
            FabricFiguries.Update();
        }

    }
}
