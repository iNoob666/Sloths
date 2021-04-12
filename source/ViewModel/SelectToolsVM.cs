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


        private ICommand _undoCommand;
        private ICommand _redoCommand;
        private ICommand _delCommand;
        private ICommand _selectCommand;
        public SelectToolsVM(CanvasVM canVM)
        {
            canvasVM = canVM;
        }
        public ICommand UndoCommand
        {
            get
            {
                return _undoCommand ?? (_undoCommand = new ButtonCommand(obj => FabricFiguries.Undo()));
            }
        }
            
        public ICommand RedoCommand
        {
            get
            {
                return _redoCommand ?? (_redoCommand = new ButtonCommand(obj => FabricFiguries.Redo()));
            }
        }
    
        public ICommand DeleteCommand
        {
            get
            {
                return _delCommand ?? (_delCommand = new ButtonCommand(obj => FabricFiguries.DeleteSelectedFigureFromFabric()));
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
