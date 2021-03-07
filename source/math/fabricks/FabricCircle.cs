using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;

namespace Sloths.source.math
{
    class FabricCircle : IFabric
    {
        public IFigure create()
        {
            IFigure tmp = new Circle();
            return tmp;
        }
    }
}
