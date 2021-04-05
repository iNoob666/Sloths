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
        private GLpainter Painter;
        private OpenGLControl openGLControl;
        public CanvasVM(OpenGLControl OpenGLCanvas)
        {
            openGLControl = OpenGLCanvas;
            openGLControl.OpenGLInitialized += OpenGLControl_OpenGLInitialized;
            openGLControl.OpenGLDraw += OpenGLControl_OpenGLDraw;
            openGLControl.Resized += OpenGLControl_Resized;
            openGLControl.MouseLeftButtonDown += MouseDown_Event;

        }
        /*
         Евенты для рисования фигуры мышью
             */
        //Ивент срабатывающий при нажатии на холст левой кнопкой мыши.
        //При нажатии на холст создается фигура
        private void MouseDown_Event(object sender, MouseButtonEventArgs e)
        {
            openGLControl.MouseLeftButtonDown -= MouseDown_Event;
            //FabricFiguries.Create(id); //Создаем экземляр класса фигуры
            var gLControl = (OpenGLControl)sender;
            var MouseCoord = e.GetPosition(this.openGLControl); //Сичтываем позицию мыши на полотне
            //Назначаем начальную координату фигуры, которая в дальнейшем меняться не будет
            FabricFiguries.SetBegin(new NormPoint(MouseCoord.X, MouseCoord.Y));
            FabricFiguries.SetEnd(new NormPoint(MouseCoord.X, MouseCoord.Y));
            //Figure.Init(new NormPoint(MouseCoord.X, MouseCoord.Y), new NormPoint(MouseCoord.X, MouseCoord.Y), Color_, Thickness);
            //Вторая координата фигуры, которая в дальнейшем будет меняться в ивенте MouseMove_Event
            openGLControl.MouseMove += MouseMove_Event; //Назначаем ивент для движения мышью с помощью которого будем менять размер фигуры
            openGLControl.MouseLeftButtonUp += MouseUp_Event; //Назначаем ивент при отпуске левой кнопки мыши заканчивающий создание фигуры
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            var gLControl = (OpenGLControl)sender;
            var MouseCoord = e.GetPosition(this.openGLControl);//Сичтываем позицию мыши на полотне
            //Меняем вторую координату фигуры для изменения размера и положения фигуры 
            FabricFiguries.SetEnd(new NormPoint(MouseCoord.X, MouseCoord.Y));
            //Figure.EndCoord = new NormPoint(MouseCoord.X, MouseCoord.Y);
        }

        private void MouseUp_Event(object sender, MouseButtonEventArgs e)
        {
            openGLControl.MouseMove -= MouseMove_Event;
            openGLControl.MouseLeftButtonUp -= MouseUp_Event;
            //Добавляем фигуру в фабрику для дальнейшей отрисовки
            FabricFiguries.AddFigureToFabric();
            FabricFiguries.ReCreate();


            openGLControl.MouseLeftButtonDown += MouseDown_Event;

            //SelectMode.IsEnabled = true;
            //ColorPicker.IsEnabled = true;
            //Delete.IsEnabled = true;
            //Undo.IsEnabled = true;
            //Redo.IsEnabled = true;
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
