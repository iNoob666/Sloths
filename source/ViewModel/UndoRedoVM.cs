using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sloths.source.math;

namespace Sloths.source.ViewModel
{
    class UndoRedoVM
    {
        private ICommand _undoCommand;
        private ICommand _redoCommand;
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

        public UndoRedoVM(InputBindingCollection inputBinding)
        {
            KeyBinding Undokey = new KeyBinding(UndoCommand,Key.Z, ModifierKeys.Control);
            KeyBinding Redokey = new KeyBinding(RedoCommand, Key.Y, ModifierKeys.Control);

            inputBinding.Add(Undokey);
            inputBinding.Add(Redokey);
        }
    }
}
