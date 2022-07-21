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
        private int health;
        shader Shader;

        public Prey(shader shader, int x, int y) : base()
        {
            this.x = x;
            this.y = y;
            this.Shader = shader;

            GL.NamedBufferData(_vertexBufferObject, _vertices().Length * sizeof(float), _vertices(), BufferUsageHint.DynamicDraw);

            GL.NamedBufferData(_elementBufferObject, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);
        }


        public override void Draw()
        {
            //Shader.Use();
            //System.Diagnostics.Debug.WriteLine("test");
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public int GetHealth() => health;

        public override void Update(double updateTime)
        {
            //randomly choose in what direction it will move 50 percent for every direction
            //TEMP
            //PAP.animals[x_unscaled, y_unscaled] = null;
            //System.Diagnostics.Debug.WriteLine("test1");
            int chance = Program.rnd.Next(1, 100);

            try
            {
                if (chance < 24 && x + 1 < 249)
                {
                    PAP.animals[x, y] = null;
                    x++;
                    PAP.animals[x + 1, y] = this;
                }
                else if (chance >= 24 && chance < 50 && x  - 1 > 0)
                {
                    PAP.animals[x, y] = null;
                    x--;
                    PAP.animals[x - 1, y] = this;
                }
                else if (chance >= 50 && chance < 75 && y + 1 < 249)
                {
                    PAP.animals[x, y] = null;
                    y++;
                    PAP.animals[x, y + 1] = this;
                }
                else if (chance >= 75 && y  - 1 > 0)
                {
                    PAP.animals[x, y] = null;
                    y--;
                    PAP.animals[x, y - 1] = this;
                }


                health += 1;

                if (health >= 75)
                {
                    health = 0;
                    if (x + 1 < 250)
                        PAP.animals[x + 1, y] = new Prey(this.Shader, x + 1, y);
                    else if (x - 1 > 0)
                        PAP.animals[x - 1, y] = new Prey(this.Shader, x - 1, y);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                System.Diagnostics.Debug.WriteLine(x + ":" + y);
                PAP.animals[x, y] = null;
            }
            //TODO when cell moves, moves it within array that contains all Entitis 

            //TODO when update reduce health by some Amount

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
    }
}
