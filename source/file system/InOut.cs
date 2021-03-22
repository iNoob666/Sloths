using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using Sloths.source.model;
using Sloths.source.math;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Sloths.source.file_system
{
    class InOut : IInOut
    {
        private DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<IFigure>));

        public void Save()
        {
            using(var file = new FileStream("picture.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(file, FabricFiguries.ListOfFigures);
            }
        }
    }
}
