﻿using OpenTk.Interfaces;
using OpenTK;
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
    class Creature : Updatable
    {
        private int x { get; set; }
        private int y { get; set; }
        public int health { get; set; }
        public CreatureType Type { get; set; }

        public uint[] Indices(uint offset) => new uint[]
        {
            offset , offset + 1, offset + 2,
            offset + 2, offset + 1, offset + 3
        };

        public Creature(int x, int y, CreatureType type = CreatureType.Empty) : base()
        {
            this.x = x;
            this.y = y;
            this.health = 0;

            //set type
            this.Type = type;
        }

        public void Update(double updateTime)
        {
            int x_change = 0;
            int y_change = 0;
            int x_changed = 0;
            int y_changed = 0;
            switch (this.Type)
            {
                case CreatureType.Predator:

                    --this.health;
                    if (health == 0) this.Type = CreatureType.Empty;

                    //choose direction 
                    chooseDirection(out x_change, out y_change);
                    x_changed = this.x + x_change;
                    y_changed = this.y + y_change;
                    if (x_changed > 0 && x_changed < PAP.width && y_changed > 0 && y_changed < PAP.height
                           && PAP.animals[x_changed, y_changed].Type == CreatureType.Prey)
                    {
                        PAP.animals[x_changed, y_changed].Type = CreatureType.Predator;
                        PAP.animals[x_changed, y_changed].health = 100;
                    }
                    move();
                    break;

                case CreatureType.Prey:
                    //increas health by one 
                    ++this.health;
                    //check for reproduction threshold
                    if (health > 250)
                    {
                        //choose direction 
                        chooseDirection(out x_change, out y_change);
                        x_changed = this.x + x_change;
                        y_changed = this.y + y_change;
                        //check if cordinates are inside bounds and if targeted cell is empty
                        if (x_changed > 0 && x_changed < PAP.width && y_changed > 0 && y_changed < PAP.height
                            && PAP.animals[x_changed, y_changed].Type == CreatureType.Empty)
                        {
                            PAP.animals[x_changed, y_changed].Type = CreatureType.Prey;
                            this.health = 0;
                        }
                    }
                    move();
                    break;
            }
        }

        private void move()
        {
            int x_change = 0;
            int y_change = 0;

            chooseDirection(out x_change, out y_change);

            int x_changed = this.x + x_change;
            int y_changed = this.y + y_change;
            if (x_changed > 0 && x_changed < PAP.width && y_changed > 0 && y_changed < PAP.height)
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
        }

        private void chooseDirection(out int x, out int y)
        {
            int chance = Program.rnd.Next(1, 102);
            if (chance >= 51)
            {
                chance = Program.rnd.Next(1, 102);
                y = 0;
                if (chance <= 51)
                    x = 1;
                else
                    x = -1;
            }
            else
            {
                chance = Program.rnd.Next(1, 102);
                x = 0;
                if (chance <= 51)
                    y = 1;
                else
                    y = -1;
            }
        }

        public float[] _vertices()
        {
            float xc = (x * MainWindow.x_scaled);
            float yc = (y * MainWindow.y_scaled);

            return new float[]{
            xc - 300,(yc) - 300,0, //left top
            (xc + MainWindow.x_scaled) - 300,yc - 300,0, //right top
            xc  - 300,(yc + MainWindow.y_scaled) - 300,0, // left bottom
            (xc+ MainWindow.x_scaled)  - 300,(yc + MainWindow.y_scaled) - 300,0, // right bottom
            };
        }
    }
}
