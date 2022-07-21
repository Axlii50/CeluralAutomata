using OpenTk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk.PrayAndPredators
{
    internal class PAP : OpenTk.Interfaces.Program
    {
        private shader[] _Shaders;

        public static Entity[,] animals;

        public void Init()
        {
            animals = new Entity[250, 250];

            //load all needed shaders for all entites
            _Shaders = new shader[2] {
                new shader("PrayAndPredators/shaders/shader_Predator.vert",
                           "PrayAndPredators/shaders/shader_Predator.frag"),
                new shader("PrayAndPredators/shaders/shader_Prey.vert",
                           "PrayAndPredators/shaders/shader_Prey.frag")
            };

            //test entitis 
            //animals[0, 1] = new Predator(_Shaders[0], 0, 1);
            //animals[0, 2] = new Predator(_Shaders[0], 0, 2);
            //animals[0, 3] = new Predator(_Shaders[0], 0, 3);
            //animals[0, 4] = new Predator(_Shaders[0], 0, 4);
            //animals[0, 5] = new Predator(_Shaders[0], 0, 5);
            //animals[0, 6] = new Predator(_Shaders[0], 0, 6);
            //animals[2, 7] = new Prey(_Shaders[1], 2, 7);
            //animals[2, 8] = new Prey(_Shaders[1], 2, 8);
            //animals[2, 9] = new Prey(_Shaders[1], 2, 9);
            //animals[2, 10] = new Prey(_Shaders[1], 2, 10);
            //animals[2, 11] = new Prey(_Shaders[1], 2, 11);
            //animals[2, 12] = new Prey(_Shaders[1], 2, 12);

        }

        public void Draw()
        {
            //draw all Entitis
            _Shaders[1].Use();

            foreach (Entity animal in animals)
                if(animal is Prey)
                    animal?.Draw();

            _Shaders[0].Use();

            foreach (Entity animal in animals)
                if (animal is Predator)
                    animal?.Draw();
        }

        public void Update(double updateTime)
        {
            //update all Entitis
            foreach (Entity animal in animals)
                animal?.Update(updateTime);

            
        }

        public void Add()
        {
            if (Program.rnd.Next(1, 100) > 50)
            {
                int x = Program.rnd.Next(0, 199),
                    y = Program.rnd.Next(0, 199);
                if (Program.rnd.Next(1, 100) > 50)
                    PAP.animals[x, y] = new Prey(this._Shaders[1], x-30, y-30);
                else
                    PAP.animals[x, y] = new Predator(this._Shaders[0], x-30, y-30);
            }
        }

        public void Clear()
        {
            //cleare data from all Entitis
            foreach (Entity animal in animals)
                animal?.Clear();
        }
    }
}
