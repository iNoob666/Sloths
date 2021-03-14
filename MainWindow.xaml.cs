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
        private IPaint engine; //Движок на котором рисуем все
        private IFigure Figure = FabricFiguries.Create("Line"); //Поле для новой фигуры
        private string id = ""; //Название нажатой кнопки  
        public MainWindow()
        {
            InitializeComponent();
            //Присваиваем статическому классу NormPoint размеры холста для рисования
            //это необходимо для приобразования координат из wpf в OpenGL
            NormPoint.Height = DrawingPanel.ActualHeight;
            NormPoint.Widht = DrawingPanel.ActualWidth;

            DrawingPanel.MouseLeftButtonDown += MouseDown_Event;

            //Назначаем иветны на кнопки
            //**ДЛЯ ОТДЕЛА UI**
            //Когда будете пилить сюда интерфейс оберните кнопки фигур в контейнер плз))
            foreach (Button elem in Tools.Children)
            {
                elem.Click += ButtonActive_Event;
            }
        }

        //Ивент срабатывающий при нажатии кнопки фигуры
        private void ButtonActive_Event(object sender, RoutedEventArgs e)
        {
            DrawingPanel.MouseLeftButtonDown -= MouseDown_Event;
            //Чистим цвета
            foreach (Button elem in Tools.Children)
            {
                elem.Background = new SolidColorBrush(Colors.White);
            }
            Button butt = sender as Button;
            id = butt.Name;
            butt.Background = new SolidColorBrush(Colors.Red); //Подсвечиваем красным нажатую кнопку
            DrawingPanel.MouseLeftButtonDown += MouseDown_Event; //Назначаем на левую кнопку мыши евент для начала рисования
        }

        //Ивент срабатывающий при нажатии на холст левой кнопкой мыши.
        //При нажатии на холст создается фигура
        private void MouseDown_Event(object sender, MouseButtonEventArgs e)
        {
            DrawingPanel.MouseLeftButtonDown -= MouseDown_Event;
            Figure = FabricFiguries.Create(id); //Создаем экземляр класса фигуры
            var gLControl = (OpenGLControl)sender;
            var MouseCoord = e.GetPosition(this.DrawingPanel); //Сичтываем позицию мыши на полотне
            //Назначаем начальную координату фигуры, которая в дальнейшем меняться не будет
            Figure.Init(new NormPoint(MouseCoord.X, MouseCoord.Y),new NormPoint(MouseCoord.X, MouseCoord.Y));
            //Вторая координата фигуры, которая в дальнейшем будет меняться в ивенте MouseMove_Event
            DrawingPanel.MouseMove += MouseMove_Event; //Назначаем ивент для движения мышью с помощью которого будем менять размер фигуры
            DrawingPanel.MouseLeftButtonUp += MouseUp_Event; //Назначаем ивент при отпуске левой кнопки мыши заканчивающий создание фигуры
        }

        private void MouseMove_Event(object sender, MouseEventArgs e)
        {
            var gLControl = (OpenGLControl)sender;
            var MouseCoord = e.GetPosition(this.DrawingPanel);//Сичтываем позицию мыши на полотне
            //Меняем вторую координату фигуры для изменения размера и положения фигуры 
            Figure.Init(Figure.BeginCoord, new NormPoint(MouseCoord.X, MouseCoord.Y));
            //Figure.EndCoord = new NormPoint(MouseCoord.X, MouseCoord.Y);
        }

        private void MouseUp_Event(object sender, MouseButtonEventArgs e)
        {
            DrawingPanel.MouseMove -= MouseMove_Event;
            DrawingPanel.MouseLeftButtonUp -= MouseUp_Event;
            //Добавляем фигуру в фабрику для дальнейшей отрисовки
            FabricFiguries.AddFigureToFabric(Figure);
            
            DrawingPanel.MouseLeftButtonDown += MouseDown_Event;
        }

        /*
          Сегмент кода про OpenGL
        */
        public void OpenGLControl_OpenGLInitialized(object sender, OpenGLRoutedEventArgs args) //Инициализация полотна 
        {
            var gl = args.OpenGL;
            engine = new GLpainter(ref gl); //Создаем экземляр класса GLpainter с помощью которого рисуем графику
        }

        public void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args) //Ивент для отрисовки кадра
        {
            args.OpenGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT); //Отчищаем полотно
            FabricFiguries.DrawAll(engine); //Рисуем все фугу из фабрики
            Figure.Draw(engine); //Отрисовка новой фигуры
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
