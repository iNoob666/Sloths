using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sloths.source.model
{
    public interface IPaint
    {
        void drawline(IEnumerable<math.NormPoint> xy);
        void drawcircle(math.NormPoint xy, double rad);
        void _flush();
    }
}
