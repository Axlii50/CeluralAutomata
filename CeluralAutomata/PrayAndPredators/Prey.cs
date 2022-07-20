using OpenTk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace OpenTk.PrayAndPredators
{
    class Prey : Entity
    {
        public uint[] indices = {  // note that we start from 0!
            0, 1, 2,  // first triangle
            2, 1, 3    // second triangle
         };

        public int x, y;

        shader Shader;

        int size = 1;

        public Prey(ref shader shader, int x, int y) : base()
        {
            this.x = x;
            this.y = y;
            this.Shader = shader;

            float[] _vertices = {
            x,y,0, //left top
            x + size,y,0, //right top
            x,y +size,0, // left bottom
            x + size,y +size,0, // right bottom
            };

            GL.NamedBufferData(_vertexBufferObject, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.NamedBufferData(_elementBufferObject, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public override void Draw()
        {
            Shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public override void Update(double updateTime)
        {
            throw new NotImplementedException();
        }
    }
}
