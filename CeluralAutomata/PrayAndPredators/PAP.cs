﻿using OpenTk.Interfaces;
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

        private Entity[,] animals;

        public void Init()
        {
            animals = new Entity[1000, 1000];

            _Shaders = new shader[2] {
                new shader("PrayAndPredators/shaders/shader_Predator.vert",
                           "PrayAndPredators/shaders/shader_Predator.frag"),
                new shader("PrayAndPredators/shaders/shader_Prey.vert",
                           "PrayAndPredators/shaders/shader_Prey.frag")
            };

            animals[0, 1] = new Predator(ref _Shaders[0], 0, 0);
            animals[0, 2] = new Predator(ref _Shaders[0], 0, 0);
            animals[0, 3] = new Predator(ref _Shaders[0], 0, 0);
        }

        public void Draw()
        {
            foreach (Entity animal in animals)
                    animal?.Draw();
        }

        public void Update(double updateTime)
        {
            foreach (Entity animal in animals)
                animal?.Update(updateTime);
        }

        public void Clear()
        {
            foreach (Entity animal in animals)
                animal?.Clear();
        }
    }
}