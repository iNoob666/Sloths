using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sloths.source.model;

using SharpGL;
using Sloths.source.math;

namespace Sloths.source.gui
{
    class GLpainter : IPaint
    {
        private OpenGL openGL;
        public GLpainter(ref OpenGL gl)
        {
            openGL = gl;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            openGL.ClearColor(0f, 0f, 0f, 0.3f);
        }
        void IPaint.drawline(IEnumerable<NormPoint> xy)
        {

            openGL.Begin(OpenGL.GL_LINES);
            openGL.Color(1f, 1f, 1f);
            foreach(NormPoint p in xy) openGL.Vertex(p.X, p.Y);
            openGL.End();
        }
    }
}
