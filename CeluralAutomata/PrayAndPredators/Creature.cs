using OpenTk.Interfaces;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk.PrayAndPredators
{
    enum CreatureType
    {
        Prey,
        Predator,
        Empty
    }
    class Creature : Bufforable , Drawable , Updatable
    {
        private int x;
        private int y;
        private int health;

        private uint[] indices = {  // note that we start from 0!
            0, 1, 2,  // first triangle
            2, 1, 3    // second triangle
         };

        public Creature(int x, int y, CreatureType type = CreatureType.Empty) : base()
        {
            this.x = x;
            this.y = y;

            this.Type = type;

            GL.NamedBufferData(_vertexBufferObject, _vertices().Length * sizeof(float), _vertices(), BufferUsageHint.DynamicDraw);

            GL.NamedBufferData(_elementBufferObject, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);
        }

        public CreatureType Type { get; set; }

        public void Draw()
        {
            //bind vertex array of this specific object
            GL.BindVertexArray(_vertexArrayObject);
            //draw binded array
            GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public void Update(double updateTime)
        {
            switch (this.Type)
            {
                 case CreatureType.Predator:
                    break;

                case CreatureType.Prey:
                    break;
            }

            move();
        }

        private void move()
        {
            int chance = Program.rnd.Next(1, 100);

            #region MyRegion
            //if (chance < 24 && x + 1 < 249 && PAP.animals[x+1, y].Type == CreatureType.Empty)
            //{
            //    PAP.animals[x + 1, y].health = this.health;
            //    PAP.animals[x, y].health = 0;
            //    PAP.animals[x + 1, y].Type = this.Type;
            //    PAP.animals[x, y].Type = CreatureType.Empty;
            //}
            //else if (chance >= 24 && chance < 50 && x - 1 > 0 && PAP.animals[x-1, y].Type == CreatureType.Empty)
            //{
            //    PAP.animals[x - 1, y].health = this.health;
            //    PAP.animals[x, y].health = 0;
            //    PAP.animals[x - 1, y].Type = this.Type;
            //    PAP.animals[x, y].Type = CreatureType.Empty;

            //}
            //else if (chance >= 50 && chance < 75 && y + 1 < 249 && PAP.animals[x, y + 1].Type == CreatureType.Empty)
            //{
            //    PAP.animals[x, y + 1].health = this.health;
            //    PAP.animals[x, y].health = 0;
            //    PAP.animals[x, y + 1].Type = this.Type;
            //    PAP.animals[x, y].Type = CreatureType.Empty;
            //}
            //else if (chance >= 75 && y - 1 > 0 && PAP.animals[x, y - 1].Type == CreatureType.Empty)
            //{
            //    PAP.animals[x, y - 1].health = this.health;
            //    PAP.animals[x, y].health = 0;
            //    PAP.animals[x, y - 1].Type = this.Type;
            //    PAP.animals[x, y].Type = CreatureType.Empty; 
            //} 
            #endregion
            CreatureType temp_t;
            int temp_h = 0;
            if (chance < 24 && x + 1 < 99)
            {
                temp_t = PAP.animals[x + 1, y].Type;
                temp_h = PAP.animals[x + 1, y].health;

                PAP.animals[x + 1, y].health = this.health;
                PAP.animals[x + 1, y].Type = this.Type;

                PAP.animals[x, y].health = temp_h;
                PAP.animals[x, y].Type = temp_t;
            }
            else if (chance >= 24 && chance < 50 && x - 1 > 0)
            {
                temp_t = PAP.animals[x - 1, y].Type;
                temp_h = PAP.animals[x - 1, y].health;

                PAP.animals[x - 1, y].health = this.health;
                PAP.animals[x - 1, y].Type = this.Type;

                PAP.animals[x, y].health = temp_h;
                PAP.animals[x, y].Type = temp_t;
            }
            else if (chance >= 50 && chance < 75 && y + 1 < 99)
            {
                temp_t = PAP.animals[x, y + 1].Type;
                temp_h = PAP.animals[x, y + 1].health;

                PAP.animals[x, y+1].health = this.health;
                PAP.animals[x, y + 1].Type = this.Type;

                PAP.animals[x, y].health = temp_h;
                PAP.animals[x, y].Type = temp_t;
            }
            else if (chance >= 75 && y - 1 > 0)
            {
                temp_t = PAP.animals[x, y - 1].Type;
                temp_h = PAP.animals[x, y - 1].health;

                PAP.animals[x, y - 1].health = this.health;
                PAP.animals[x, y - 1].Type = this.Type;

                PAP.animals[x, y].health = temp_h;
                PAP.animals[x, y].Type = temp_t;
            }
        }

        private float[] _vertices() => new float[]{
            x * MainWindow.x_scaled,y * MainWindow.y_scaled,0, //left top
            x * MainWindow.x_scaled + MainWindow.x_scaled,y * MainWindow.y_scaled,0, //right top
            x * MainWindow.x_scaled,y * MainWindow.y_scaled + MainWindow.y_scaled,0, // left bottom
            x * MainWindow.x_scaled+ MainWindow.x_scaled,y * MainWindow.y_scaled + MainWindow.y_scaled,0, // right bottom
            };
    }
}
