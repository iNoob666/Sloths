using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.math;

namespace Sloths.source.model
{
    public class IFigureFabric
    {
        public IEnumerable<string> AvailableFigures 
        { 
            get;
        }
        public IFigure Create(string name)
        {
            switch (name)
            {
                case "line":
                    return new Line();
                case "circle":
                    return new Circle();
                case "rectangle":
                    return new Rectangle();
                default:
                    throw new NotImplementedException();
            }
        }
        public void AddFigureToFabric(string name, IFigure newfig)
        {

            throw new NotImplementedException();
        }
    }

    public interface IFigure
    {
        void draw(IPaint screen);
    }
}
