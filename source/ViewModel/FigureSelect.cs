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
    class FigureSelect
    {
        public ButtonCommand NewFigure = new ButtonCommand(obj =>
        {
            var btn = obj as Button;
            FabricFiguries.Create(btn.Name);
        });
        public FigureSelect(Grid FiguresSelectPanel)
        {        
            foreach (Button elem in FiguresSelectPanel.Children)
            {
                elem.Command = NewFigure;
                elem.CommandParameter = elem;
            }
        }
    }
}
