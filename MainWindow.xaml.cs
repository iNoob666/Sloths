using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.WPF;
using Sloths.source.gui;
using Sloths.source.math;
using Sloths.source.model;

namespace Sloths
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        IPaint engine;
        IFigure Figure;


        //private Single x1 = 0.0f, x2 = 0.0f, y1 = 0.0f, y2 = 0.0f;
        private string id = "";
        public MainWindow()
        {

            InitializeComponent();
            
            foreach (Button elem in Tools.Children)
            {
                elem.Click += ButtonActive_Event;
            }
            //line.Click += LineButtonClick;
            //circle.Click += CircleButtonClick;
            //rectangle.Click += RectButtonClick;



        }
        private void ButtonActive_Event(object sender, RoutedEventArgs e)
        {
            foreach (Button elem in Tools.Children)
            {
                elem.Background = new SolidColorBrush(Colors.White);
            }
            Button butt = sender as Button;
            id = butt.Name;
            butt.Background = new SolidColorBrush(Colors.Red);
            Figure = FabricFiguries.Create(id);
            DrawingPanel.MouseLeftButtonDown += MouseDown_Event;
        }
       
        private void MouseDown_Event(object sender, MouseButtonEventArgs e)
        {
            Figure = FabricFiguries.Create(id);
            DrawingPanel.MouseMove += MouseMove_Event;
            DrawingPanel.MouseLeftButtonUp += MouseUp_Event;
            var _Mouse = e;
            var gLControl = (OpenGLControl)sender;
            var MouseCoord = _Mouse.GetPosition(this.DrawingPanel);
            
            Figure.BeginCoord = new source.math.NormPoint(MouseCoord.X, MouseCoord.Y, gLControl.ActualWidth, gLControl.ActualHeight);

        }

        private void MouseUp_Event(object sender, MouseButtonEventArgs e)
        {
            FabricFiguries.AddFigureToFabric(Figure);

            DrawingPanel.MouseMove -= MouseMove_Event;
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            var _Mouse = e;
            var gLControl = (OpenGLControl)sender;
            var MouseCoord = _Mouse.GetPosition(this.DrawingPanel);
            Figure.EndCoord = new source.math.NormPoint(MouseCoord.X, MouseCoord.Y, gLControl.ActualWidth, gLControl.ActualHeight);
        }

        //Мусорный код
        //private void CircleButtonClick(object sender, RoutedEventArgs e)
        //{
        //    x1 = 0.66f;
        //    y1 = 0.66f;
        //    x2 = -0.66f;
        //    y2 = -0.66f;
        //}
        //private void RectButtonClick(object sender, RoutedEventArgs e)
        //{
        //    x1 = 0.33f;
        //    y1 = 0.33f;
        //    x2 = -0.33f;
        //    y2 = -0.33f;
        //}
        //DEBUG ДЛЯ ПРОВЕКТИ РИСОВКИ ЛИНИЙ
        //private void LineButtonClick(object sender, RoutedEventArgs e)
        //{
        //    var b = sender as Button;

        //    id = b.Name;



        //}


        public void OpenGLControl_OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        {
            var gl = args.OpenGL;
            engine = new GLpainter(ref gl);
            
        }

        public void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            var gl = args.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            FabricFiguries.DrawAll(engine);
            if(Figure != null) Figure.Draw(engine);
            //var gl = args.OpenGL;
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //gl.Begin(OpenGL.GL_LINES);
            //gl.Color(0f, 1f, 0f);

            //gl.Vertex(x1, y1);
            //gl.Vertex(x2, y2);
            //gl.End();
        }

        public void OpenGLControl_Resized(object sender, OpenGLRoutedEventArgs args)
        {
        }


        //private Point Nornmalize(Point point, double w, double h)
        //{

        //    double X = 2*point.X / w - 1;
        //    double Y = 2*(h -point.Y) / h - 1;

        //    return new Point(X, Y);
        //}
    }
}
