﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sloths.source.model;

namespace Sloths.source.math
{
    class FabricLine : IFabric
    {
        public static IFigure Create(string name)
        {
            return new Line();
        }


    }
}
