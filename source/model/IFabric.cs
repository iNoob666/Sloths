using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloths.source.math

namespace Sloths.source.model
{
    public interface IFabric
    {
        IFigure create();
    }
}
