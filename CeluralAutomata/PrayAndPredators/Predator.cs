using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk.PrayAndPredators
{
    class Predator : Entity
    {
        private uint[] indices = {  // note that we start from 0!
            0, 1, 2,  // first triangle
            2, 1, 3    // second triangle
         };
      
        private float x, y;
        shader Shader;
        //healt of predator
        private int health;

        public Predator(ref shader shader , int x , int y) : base()
        {
            this.x = x;
            this.y = y;
            this.Shader = shader;

            GL.NamedBufferData(_vertexBufferObject, _vertices().Length * sizeof(float), _vertices(), BufferUsageHint.StaticDraw);

            GL.NamedBufferData(_elementBufferObject, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public override void Draw()
        {
            //change shader 
            Shader.Use();
            //bind vertex array of this specific object
            GL.BindVertexArray(_vertexArrayObject);
            //draw binded array
            GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public override void Update(double updateTime)
        {
            //randomly choose in what direction it will move 50 percent for every direction
            //TEMP
            if(Program.rnd.Next(1,100) > 50)
            {
                if (Program.rnd.Next(1, 100) > 50)
                    x += MainWindow.x_scaled;
                else
                    x -= MainWindow.x_scaled;
            }
            else
            {
                if (Program.rnd.Next(1, 100) > 50)
                    y += MainWindow.y_scaled;
                else
                    y -= MainWindow.y_scaled;
            }
            
            //TODO when cell moves, moves it within array that contains all Entitis 

            //TODO when update reduce health by some Amount

            //Update data in buffer
            GL.NamedBufferData(_vertexBufferObject, _vertices().Length * sizeof(float), _vertices(), BufferUsageHint.StaticDraw);

            GL.NamedBufferData(_elementBufferObject, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        //return vertices bassed on x and y 
        private float[] _vertices() => new float[]{
            x,y,0, //left top
            x + MainWindow.x_scaled,y,0, //right top
            x,y + MainWindow.y_scaled,0, // left bottom
            x + MainWindow.x_scaled,y + MainWindow.y_scaled,0, // right bottom
            };

    }
}
