using Sloths.source.math;
using System;
using System.Collections.Generic;
using SharpGL.WPF;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sloths.source.ViewModel
{
    class SelectToolsVM
    {
        private CanvasVM canvasVM;



        private ICommand _delCommand;
        private ICommand _selectCommand;
        private ICommand _clearCommand;
        public SelectToolsVM(CanvasVM canVM)
        {
            canvasVM = canVM;
        }
      
    
        public ICommand DeleteCommand
        {
            get
            {
                return _delCommand ?? (_delCommand = new ButtonCommand(obj => FabricFiguries.DeleteSelectedFigureFromFabric()));
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new ButtonCommand(obj => FabricFiguries.ClearFabric()));
            }
        }
        public ICommand SelectCommand
        {
            get
            {
                return _selectCommand ?? (_selectCommand = new ButtonCommand(obj => canvasVM.SetEventbyButtonName((string)obj)));
            }
        }
       
    }
}
