using Sloths.source.math;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloths.source.gui
{
    class CustomComands
    {
        //Список команд
        public static RoutedCommand Up { get; set; }
        public static RoutedCommand Down { get; set; }
        public static RoutedCommand Right { get; set; }
        public static RoutedCommand Left { get; set; }

        public static RoutedCommand PlusSize { get; set; }
        public static RoutedCommand MinusSize { get; set; }

        public static RoutedCommand СounterClockWise { get; set; }
        public static RoutedCommand ClockWise { get; set; }

        public static RoutedCommand СounterClockWiseAroundCenter { get; set; }
        public static RoutedCommand ClockWiseAroundCenter { get; set; }
        static CustomComands()
        {
            //Перемещение фигуры 
            Up = new RoutedCommand();
            
            Down = new RoutedCommand();
            
            Right = new RoutedCommand();
           
            Left = new RoutedCommand();
           
            //Изменение размеров фигуры
            PlusSize = new RoutedCommand();
            
            MinusSize = new RoutedCommand();
            
            //Поворот фигуры
            СounterClockWise = new RoutedCommand();
            
            ClockWise = new RoutedCommand();

            //Поворот фигуры относительно центра
            СounterClockWiseAroundCenter = new RoutedCommand();
            
            ClockWiseAroundCenter = new RoutedCommand();

        }

    }
}
