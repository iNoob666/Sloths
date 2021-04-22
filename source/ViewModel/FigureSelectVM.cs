using Sloths.source.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Input;
using MaterialDesignColors;

namespace Sloths.source.ViewModel
{
    class FigureSelectVM
    {
        private readonly CanvasVM canvasVM;
        public ButtonCommand NewFigure; 
        public FigureSelectVM(Grid FiguresSelectPanel, CanvasVM canvasVM, SliderVM slider, ColorVM colorVM)
        {
            this.canvasVM = canvasVM;
            NewFigure = new ButtonCommand(obj =>
            {
                foreach (Button elem in FiguresSelectPanel.Children)
                    elem.Background = new SolidColorBrush(MaterialDesignColors.Recommended.CyanSwatch.Cyan500);
                var btn = obj as Button;
                btn.Background = new SolidColorBrush(MaterialDesignColors.Recommended.RedSwatch.Red500);
                canvasVM.SetEventbyButtonName(btn.Name);               
                FabricFiguries.Create(btn.Name);
                FabricFiguries.SetThickness(slider.SliderValue);
                FabricFiguries.SetColor(ColorTranslator.FromHtml(colorVM.CurrentColor));
                
            });
            foreach (Button elem in FiguresSelectPanel.Children)
            {
                elem.Command = NewFigure;
                elem.CommandParameter = elem;
                
            }

        }
    }
}
