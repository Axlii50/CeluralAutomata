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
    class Creature : Bufforable, Drawable, Updatable
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
            this.health = 0;

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

                    this.health--;
                    if (health == 0) this.Type = CreatureType.Empty;

                    break;

                case CreatureType.Prey:

                    this.health++;
                    if (health > 1000)
                    {
                        int x_change = 0;
                        int y_change = 0;

                        chooseDirection(out x_change, out y_change);
                        int x_changed = this.x + x_change;
                        int y_changed = this.y + y_change;
                        if (x_changed > 0 && x_changed < PAP.widht && y_changed > 0 && y_changed < PAP.height
                            && PAP.animals[x_changed, y_changed].Type == CreatureType.Empty)
                        {
                            do
                            {
                                x_changed = this.x + x_change;
                                y_changed = this.y + y_change;
                                chooseDirection(out x_change, out y_change);
                            } while (PAP.animals[x_changed, y_changed].Type != CreatureType.Empty);
                            PAP.animals[x_changed, y_changed].Type = CreatureType.Prey;
                        }
                    }

                    break;
            }

            if (this.Type != CreatureType.Empty)
                move();
        }

        private void move()
        {
            //int chance = Program.rnd.Next(1, 100);

            int x_change = 0;
            int y_change = 0;

            chooseDirection(out x_change,out y_change);

            int x_changed = this.x + x_change;
            int y_changed = this.y + y_change;
            if if (x_changed > 0 && x_changed < PAP.widht && y_changed > 0 && y_changed < PAP.height)
            {
                #region Cancel
                if (PAP.animals[x_changed, y_changed].Type != CreatureType.Empty) return;

                PAP.animals[x_changed, y_changed].health = this.health;
                PAP.animals[x, y].health = 0;
                PAP.animals[x_changed, y_changed].Type = this.Type;
                PAP.animals[x, y].Type = CreatureType.Empty;

                #endregion
                #region Swap
                //CreatureType temp_t;
                //int temp_h = 0;
                //temp_t = PAP.animals[x_changed, y_changed].Type;
                //temp_h = PAP.animals[x_changed, y_changed].health;

                //PAP.animals[x_changed, y_changed].health = this.health;
                //PAP.animals[x_changed, y_changed].Type = this.Type;

                //PAP.animals[x, y].health = temp_h;
                //PAP.animals[x, y].Type = temp_t; 
                #endregion
            }


            #region MyRegion
            //if (chance < 24 && x + 1 < 99 && PAP.animals[x + 1, y].Type == CreatureType.Empty)
            //{
            //    PAP.animals[x + 1, y].health = this.health;
            //    PAP.animals[x, y].health = 0;
            //    PAP.animals[x + 1, y].Type = this.Type;
            //    PAP.animals[x, y].Type = CreatureType.Empty;
            //}
            //else if (chance >= 25 && chance < 50 && x - 1 > 0 && PAP.animals[x - 1, y].Type == CreatureType.Empty)
            //{
            //    PAP.animals[x - 1, y].health = this.health;
            //    PAP.animals[x, y].health = 0;
            //    PAP.animals[x - 1, y].Type = this.Type;
            //    PAP.animals[x, y].Type = CreatureType.Empty;
            //}
            //else if (chance >= 50 && chance < 75 && y + 1 < 99 && PAP.animals[x, y + 1].Type == CreatureType.Empty)
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
            #region MyRegion
            //CreatureType temp_t;
            //int temp_h = 0;
            //if (chance < 24 && x + 1 < 99)
            //{
            //    temp_t = PAP.animals[x + 1, y].Type;
            //    temp_h = PAP.animals[x + 1, y].health;

            //    PAP.animals[x + 1, y].health = this.health;
            //    PAP.animals[x + 1, y].Type = this.Type;

            //    PAP.animals[x, y].health = temp_h;
            //    PAP.animals[x, y].Type = temp_t;
            //}
            //else if (chance >= 24 && chance < 50 && x - 1 > 0)
            //{
            //    temp_t = PAP.animals[x - 1, y].Type;
            //    temp_h = PAP.animals[x - 1, y].health;

            //    PAP.animals[x - 1, y].health = this.health;
            //    PAP.animals[x - 1, y].Type = this.Type;

            //    PAP.animals[x, y].health = temp_h;
            //    PAP.animals[x, y].Type = temp_t;
            //}
            //else if (chance >= 50 && chance < 75 && y + 1 < 99)
            //{
            //    temp_t = PAP.animals[x, y + 1].Type;
            //    temp_h = PAP.animals[x, y + 1].health;

            //    PAP.animals[x, y+1].health = this.health;
            //    PAP.animals[x, y + 1].Type = this.Type;

            //    PAP.animals[x, y].health = temp_h;
            //    PAP.animals[x, y].Type = temp_t;
            //}
            //else if (chance >= 75 && y - 1 > 0)
            //{
            //    temp_t = PAP.animals[x, y - 1].Type;
            //    temp_h = PAP.animals[x, y - 1].health;

            //    PAP.animals[x, y - 1].health = this.health;
            //    PAP.animals[x, y - 1].Type = this.Type;

            //    PAP.animals[x, y].health = temp_h;
            //    PAP.animals[x, y].Type = temp_t;
            //} 
            #endregion
        }

        private void chooseDirection(out int x, out int y)
        {
            if (Program.rnd.Next(1, 101) > 50)
            {
                y = 0;
                if (Program.rnd.Next(1, 102) <= 51)
                    x = 1;
                else
                    x = -1;
            }
            else
            {
                x = 0;
                if (Program.rnd.Next(1, 102) <= 51)
                    y = 1;
                else
                    y = -1;
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
