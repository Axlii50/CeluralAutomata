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

        public static Creature[,] animals;

        public void Init()
        {
            animals = new Creature[100, 100];

            //load all needed shaders for all entites
            _Shaders = new shader[2] {
                new shader("PrayAndPredators/shaders/shader_Predator.vert",
                           "PrayAndPredators/shaders/shader_Predator.frag"),
                new shader("PrayAndPredators/shaders/shader_Prey.vert",
                           "PrayAndPredators/shaders/shader_Prey.frag")
            };

            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    //if (x % 2 == 0)
                    //    animals[x, y] = new Creature(x, y, CreatureType.Predator);
                    //else
                    //    animals[x, y] = new Creature(x, y, CreatureType.Prey);
                    animals[x, y] = new Creature(x, y, CreatureType.Empty);
                }
            }
        }

        public void Draw()
        {
            //draw all Entitis
            _Shaders[0].Use();

            foreach (Creature animal in animals)
                if(animal.Type == CreatureType.Predator)
                    animal?.Draw();

            _Shaders[1].Use();

            foreach (Creature animal in animals)
                if (animal.Type == CreatureType.Prey)
                    animal?.Draw();
        }

        public void Update(double updateTime)
        {
            //update all Entitis
            foreach (Creature animal in animals)
                animal?.Update(updateTime);
        }

        public void Add()
        {
            if (Program.rnd.Next(1, 100) > 50)
            {
                int x = Program.rnd.Next(0, 99),
                    y = Program.rnd.Next(0, 99);
                if (Program.rnd.Next(1, 100) > 50)
                    PAP.animals[x, y].Type = CreatureType.Prey;
                else
                    PAP.animals[x, y].Type = CreatureType.Prey;
            }
        }

        public void Clear()
        {
            //cleare data from all Entitis
            foreach (Creature animal in animals)
                animal?.Clear();
        }
    }
}
