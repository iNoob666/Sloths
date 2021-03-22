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
using Sloths.source.file_system;

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
<<<<<<< Updated upstream
        private IInOut saverLoader = new InOut();
=======
        private Sloths.source.model.Color Color_;
        private float Thickness;
        private IInOut saverLoader = new InOut();

>>>>>>> Stashed changes
        public MainWindow()
        {
            InitializeComponent();
            // Хоткеи
            // Дефолтные команды
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, UndoEvent));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo, RedoEvent));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, FabricFiguries.OpenEvent));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, FabricFiguries.SaveEvent));

            BrushSettings();



            //Присваиваем статическому классу NormPoint размеры холста для рисования
            //это необходимо для приобразования координат из wpf в OpenGL
            NormPoint.Height = DrawingPanel.ActualHeight;
            NormPoint.Widht = DrawingPanel.ActualWidth;

            

            DrawingPanel.MouseLeftButtonDown += MouseDown_Event;

            //Назначаем иветны на кнопки
            //**ДЛЯ ОТДЕЛА UI**
            //Когда будете пилить сюда интерфейс оберните кнопки фигур в контейнер плз))
            
            SelectMode.Click += SelectClick_Event;
            Delete.Click += DeleteFigure_Event;
            Undo.Click += UndoClick_Event;
            Redo.Click += RedoClick_Event;
            ColorPicker.Click += ColorPicker_Event;
            foreach (Button elem in PantTools.Children)
            {
                elem.Click += ButtonFigureActive_Event;
            }
<<<<<<< Updated upstream
=======

        }
        private void BrushSettings()
        {
            ThickSlider.ValueChanged += ValueChanged_Event;
            ColorList.SelectionChanged += ColorListChanged_Event;
            brushColorOk.Click += brushColorOk_Event;
        }

        private void ColorListChanged_Event(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
            var color = System.Drawing.ColorTranslator.FromHtml(selectedItem.Name);
            Color_ = source.model.Color.FromRGBA(color.R, color.G, color.B, color.A);

        }

        private void ValueChanged_Event(object sender, RoutedPropertyChangedEventArgs<double> e) => Thickness = (float)e.NewValue;



        private void ColorPicker_Event(object sender, RoutedEventArgs e) => DrawingPanel.MouseLeftButtonDown += ClickForColor_Event;


        private void ClickForColor_Event(object sender, MouseButtonEventArgs e)
        {
            var coords = e.GetPosition(DrawingPanel);
            Color_ = FabricFiguries.GetColor(new NormPoint(coords.X,coords.Y));
            DrawingPanel.MouseLeftButtonDown -= ClickForColor_Event;
        }

        private void RedoEvent(object sender, ExecutedRoutedEventArgs e)
        {
            FabricFiguries.Redo();
        }

        private void UndoEvent(object sender, ExecutedRoutedEventArgs e)
        {
            FabricFiguries.Undo();
        }

        private void RedoClick_Event(object sender, RoutedEventArgs e)
        {
            FabricFiguries.Redo();
        }

        private void UndoClick_Event(object sender, RoutedEventArgs e)
        {
            FabricFiguries.Undo();
        }

        private void DeleteFigure_Event(object sender, RoutedEventArgs e)
        {
            FabricFiguries.DeleteSelectedFigureFromFabric();
        }

        private void brushColorOk_Event(object sender, RoutedEventArgs e)
        {
            var color = System.Drawing.ColorTranslator.FromHtml(ColorTextBox.Text);
            Color_ = source.model.Color.FromRGBA(color.R, color.G, color.B, color.A);
>>>>>>> Stashed changes
            Saver.Click += SaveList;
        }

        private void SaveList(object sender, RoutedEventArgs e)
        {
            saverLoader.Save();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    FabricFiguries.UpEvent();
                    break;
                case Key.A:
                    FabricFiguries.LeftEvent();
                    break;
                case Key.S:
                    FabricFiguries.DownEvent();
                    break;
                case Key.D:
                    FabricFiguries.RightEvent();
                    break;
                case Key.E:
                    FabricFiguries.ClockWiseEvent();
                    break;
                case Key.Q:
                    FabricFiguries.СounterClockWiseEvent();
                    break;
                case Key.C:
                    FabricFiguries.PlusSizeEvent();
                    break; 
                case Key.X:
                    FabricFiguries.MinusSizeEvent();
                    break;
            }

        }

        private void SelectClick_Event(object sender, RoutedEventArgs e)
        {
            DrawingPanel.MouseLeftButtonDown -= MouseDown_Event;
            DrawingPanel.MouseLeftButtonDown += SelectMouseDown_Event;
        }
        private void SelectMouseDown_Event(object sender, MouseButtonEventArgs e)
        {
            var MouseCoord = e.GetPosition(this.DrawingPanel);
            var point = new NormPoint(MouseCoord.X, MouseCoord.Y);
            FabricFiguries.SelectFigure(point);
            KeyDown += MainWindow_KeyDown;
        }

        //Ивент срабатывающий при нажатии кнопки фигуры
        private void ButtonFigureActive_Event(object sender, RoutedEventArgs e)
        {
            KeyDown -= MainWindow_KeyDown;
            DrawingPanel.MouseLeftButtonDown -= MouseDown_Event;
            //Чистим цвета в левой части интерфейса
            foreach (Button elem in SelectTools.Children)
            {
                elem.Background = new SolidColorBrush(Colors.LightBlue);
            }
            // //Чистим цвета в правой части интерфейса
            foreach (Button elem in PantTools.Children)
            {
                elem.Background = new SolidColorBrush(Colors.LightBlue);
            }

            Button butt = sender as Button;
            id = butt.Name;
            butt.Background = new SolidColorBrush(Colors.Red); //Подсвечиваем красным нажатую кнопку
            if (id == "Save") DrawingPanel.MouseLeftButtonDown += MouseDown_Event;
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
            Figure.Init(new NormPoint(MouseCoord.X, MouseCoord.Y),new NormPoint(MouseCoord.X, MouseCoord.Y),Color_, Thickness);
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
            Figure = null;
            NormPoint p = new NormPoint();
            p.UpdateCoord(0, 0);
            
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
            if(Figure != null) Figure.Draw(engine); //Отрисовка новой фигуры
            FabricFiguries.DrawAll(engine); //Рисуем все фигуры из фабрики
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

        //Часть кода которая отвечает за ресайз всего окна, оставляя его aspect ratio , тем самым предотвращая изменение aspect ratio GL окна
        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            WindowAspectRatio.Register((Window)sender);
        }

 
    }
}
