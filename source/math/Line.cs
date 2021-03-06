using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;

namespace Sloths.source.math
{
    class Line : IFigure
    {
        private int k;
        private int b;
        private int x0;
        private int x1;

        private void findY()
        {
            int y0 = this.k * this.x0 + this.b;
            int y1 = this.k * this.x1 + this.b;
        }

        public void draw(IPaint screen)
        {
            throw new NotImplementedException();
        }

    }
}
