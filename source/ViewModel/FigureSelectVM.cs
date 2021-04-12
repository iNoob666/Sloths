using Sloths.source.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sloths.source.ViewModel
{
    class FigureSelectVM
    {
        private readonly CanvasVM canvasVM;
        public ButtonCommand NewFigure; 
        public FigureSelectVM(Grid FiguresSelectPanel, CanvasVM canvasVM, SliderVM slider)
        {
            this.canvasVM = canvasVM;
            NewFigure = new ButtonCommand(obj =>
            {
                var btn = obj as Button;
                canvasVM.SetEventbyButtonName(btn.Name);               
                FabricFiguries.Create(btn.Name);
                FabricFiguries.SetThickness(slider.SliderValue);
            });
            foreach (Button elem in FiguresSelectPanel.Children)
            {
                elem.Command = NewFigure;
                elem.CommandParameter = elem;
                
            }

        }
    }
}
