using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sloths.source.math;

namespace Sloths.source.ViewModel
{
    class SliderVM : INotifyPropertyChanged
    {
        //Слайдер определяет толщину линии фигуры
        private float sliderValue;

        public float SliderValue
        {
            get { return this.sliderValue; }
            set
            {
                if (value != this.sliderValue)
                {
                    this.sliderValue = value;
                    FabricFiguries.SetThickness(sliderValue);
                    OnPropertyChanged("SliderValue");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public SliderVM()
        {
            SliderValue = 1;
        }


    }
}
