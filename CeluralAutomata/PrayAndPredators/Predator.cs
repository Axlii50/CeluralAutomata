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
      
        private int x, y;
        shader Shader;
        //healt of predator
        private int health = 20;

        public Predator(shader shader , int x , int y) : base()
        {
            this.x = x;
            this.y = y;
            this.Shader = shader;

            GL.NamedBufferData(_vertexBufferObject, _vertices().Length * sizeof(float), _vertices(), BufferUsageHint.DynamicDraw);

            GL.NamedBufferData(_elementBufferObject, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);
        }

      

        public override void Draw()
        {
            //change shader 
            //Shader.Use();
            //bind vertex array of this specific object
            GL.BindVertexArray(_vertexArrayObject);
            //draw binded array
            GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public override void Update(double updateTime)
        {
            //randomly choose in what direction it will move 50 percent for every direction
            //TEMP
            int chance = Program.rnd.Next(1, 100);

            if (chance < 24 && x + 1 < 249)
            {
                PAP.animals[x, y] = null;
                x++;
                if (Checkcell(x + 1, y))
                    health = (PAP.animals[x + 1, y] as Prey).GetHealth();
                PAP.animals[x + 1, y] = this;
            }
            else if (chance >= 24 && chance < 50 && x - 1 > 0)
            {
                PAP.animals[x, y] = null;
                x--;
                if (Checkcell(x - 1, y))
                    health = (PAP.animals[x - 1, y] as Prey).GetHealth();
                PAP.animals[x - 1, y] = this;
            }
            else if (chance >= 50 && chance < 75 && y + 1 < 249)
            {
                PAP.animals[x, y] = null;
                y++;
                if (Checkcell(x, y + 1))
                    health = (PAP.animals[x, y + 1] as Prey).GetHealth();
                PAP.animals[x, y + 1] = this;
            }
            else if (chance >= 75 && y - 1 > 0)
            {
                PAP.animals[x, y] = null;
                y--;
                if (Checkcell(x, y - 1))
                    health = (PAP.animals[x, y - 1] as Prey).GetHealth();
                PAP.animals[x, y - 1] = this;
            }

            health--;
            if (health < 0)
            {
                PAP.animals[x, y] = null;
            }

            //Update data in buffer
            GL.NamedBufferSubData(_vertexBufferObject, IntPtr.Zero, _vertices().Length * sizeof(float), _vertices());
            //GL.NamedBufferData(_vertexBufferObject, _vertices().Length * sizeof(float), _vertices(), BufferUsageHint.DynamicDraw);

            GL.NamedBufferSubData(_elementBufferObject, IntPtr.Zero, indices.Length * sizeof(uint), indices);
            //GL.NamedBufferData(_elementBufferObject, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);
        }

        //return vertices bassed on x and y 
        private float[] _vertices() => new float[]{
            x * MainWindow.x_scaled,y * MainWindow.y_scaled,0, //left top
            x * MainWindow.x_scaled + MainWindow.x_scaled,y * MainWindow.y_scaled,0, //right top
            x * MainWindow.x_scaled,y * MainWindow.y_scaled + MainWindow.y_scaled,0, // left bottom
            x * MainWindow.x_scaled+ MainWindow.x_scaled,y * MainWindow.y_scaled + MainWindow.y_scaled,0, // right bottom
            };

        private bool Checkcell(int x, int y)
        {
            if (PAP.animals[x, y] is Prey)
            {
                if (x - 1 > 0)
                    PAP.animals[x - 1, y] = new Predator(this.Shader, x - 1, y);
                else
                    PAP.animals[x + 1, y] = new Predator(this.Shader, x + 1, y);
                return true;
            }

            return false;
        }
    }
}
