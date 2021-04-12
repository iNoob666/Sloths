using Sloths.source.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloths.source.ViewModel
{
    class FigureMoveVM
    {
        public FigureMoveVM()
        {

        }
        public ButtonCommand UpCommand => new ButtonCommand(obj => FabricFiguries.UpEvent());
        public ButtonCommand DownCommand => new ButtonCommand(obj => FabricFiguries.DownEvent());
        public ButtonCommand LeftCommand => new ButtonCommand(obj => FabricFiguries.LeftEvent());
        public ButtonCommand RightCommand => new ButtonCommand(obj => FabricFiguries.RightEvent());
        public ButtonCommand RightRotateCommand => new ButtonCommand(obj => FabricFiguries.ClockWiseEvent());
        public ButtonCommand LeftRotateCommand => new ButtonCommand(obj => FabricFiguries.СounterClockWiseEvent());
        public ButtonCommand PlusSizeCommand => new ButtonCommand(obj => FabricFiguries.PlusSizeEvent());
        public ButtonCommand MinusSizeCommand => new ButtonCommand(obj => FabricFiguries.MinusSizeEvent());

    }
}
