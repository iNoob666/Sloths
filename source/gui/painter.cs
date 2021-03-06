using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;

namespace Sloths.source.gui
{
    class painter : IPaint
    {
        void IPaint.drawline(IEnumerable<int> xy)
        {
            throw new NotImplementedException();
        }
    }
}
