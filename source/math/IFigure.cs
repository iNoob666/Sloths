using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sloths.source.math
{
    public interface IFigure
    {
        //Начальная координа определяемая начало фигуры
        NormPoint BeginCoord { get; set; }
        //Коненая координата определяющая размер фигуры
        NormPoint EndCoord { get; set; }
        //void SetCoords(IEnumerable<NormPoint> xy);
        void Draw(model.IPaint screen);
    }
}
