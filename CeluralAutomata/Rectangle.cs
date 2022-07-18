using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk.PrayAndPredators
{
    public class Rectangle : Bufforable, OpenTk.Interfaces.Drawable
    {
        public uint[] indices = {  // note that we start from 0!
            0, 1, 2,  // first triangle
            2, 1, 3    // second triangle
         };
        private float x, y;
        public shader _Shader;

        public Rectangle(float x, float y) : base()
        {
            this.x = x;
            this.y = y;

            float[] _vertices = {
            x,y,0, //left top
            x + MainWindow.x_scaled,y,0, //right top
            x,y +MainWindow.y_scaled,0, // left bottom
            x + MainWindow.x_scaled,y +MainWindow.y_scaled,0, // right bottom
            };

            GL.NamedBufferData(_vertexBufferObject, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.NamedBufferData(_elementBufferObject, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public void Draw()
        {
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
