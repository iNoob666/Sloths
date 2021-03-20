﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sloths.source.model
{
    public interface IPaint
    {
        void drawline(IEnumerable<math.NormPoint> xy, Color BorderColor, float LineThick);
        void drawcircle(math.NormPoint xy, double rad, Color BorderColor, float LineThick);
        void drawhighlight(IEnumerable<math.NormPoint> xy);
        void _flush();
    }
}
