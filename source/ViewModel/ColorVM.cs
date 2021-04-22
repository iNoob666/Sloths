using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sloths.source.math;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;

namespace Sloths.source.ViewModel
{
    class ColorVM : INotifyPropertyChanged
    {
       //Цвет которым рисуются фигуры прямо сейчас
        private string currentColor;
        public string CurrentColor
        {
            get { return this.currentColor; }
            set
            {
                if (value != this.currentColor)
                {
                    this.currentColor = value;
                    FabricFiguries.SetColor(ColorTranslator.FromHtml(currentColor));
                    OnPropertyChanged("CurrentColor");
                }
            }
        }
        
        private ListBoxItem listOfColorElem;
        public ListBoxItem ListOfColorElem
        {
            get { return this.listOfColorElem; }
            set
            {
                if (value != this.listOfColorElem)
                {
                    this.listOfColorElem = value;
                    CurrentColor = this.listOfColorElem.Background.ToString();
                    OnPropertyChanged("PickedColor");
                }
            }
        }
        //TextBox для для ввода цвета в HTML форме 
        private string colorTextField;
        public string ColorTextField
        {
            get { return this.colorTextField; }
            set
            {
                this.colorTextField = value;
                OnPropertyChanged("ColorTextField");
            }
        }

        public ColorVM()
        {
            ColorTextField = "#000000"; //черный
            CurrentColor = "#000000"; //черный
        }
        //Ивент срабатывающи при изменении свойств 
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private System.Drawing.Color SetCustomColor(string HexCodeColor) => ColorTranslator.FromHtml(HexCodeColor);

        public ICommand _setCustomColorCommand;
        public ICommand SetCustomColorCommand
        {
            get
            {

                return _setCustomColorCommand ?? (_setCustomColorCommand = new ButtonCommand(
                    obj =>
                    {
                        FabricFiguries.SetColor(SetCustomColor(ColorTextField));
                        CurrentColor = ColorTextField;
                    }
                    ));
            }
        }


    }
}
