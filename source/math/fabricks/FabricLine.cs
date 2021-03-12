using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;

namespace Sloths.source.math
{
    class FabricLine : IFabric
    {
        public IFigure Create(string name)
        {
            return new Line();
        }


    }
}
