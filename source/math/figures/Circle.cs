using Sloths.source.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;

namespace Sloths.source.math
{
    class Circle : IFigure
    {
        public NormPoint BeginCoord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public NormPoint EndCoord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw()
        {

        }

        public void Draw(IPaint screen)
        {
            throw new NotImplementedException();
        }
    }
}
