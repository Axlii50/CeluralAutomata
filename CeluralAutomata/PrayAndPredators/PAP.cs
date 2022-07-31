using OpenTk.Interfaces;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk.PrayAndPredators
{
    internal class PAP : OpenTk.Interfaces.Program
    {
        public Bufforable Predatorbuffor;
        public Bufforable Preybuffor;

        public shader[] _Shaders;

        public static Creature[,] animals;

        public static int height = 600, width = 600;

        public void Init()
        {
            animals = new Creature[width, height];

            Predatorbuffor = new Bufforable();
            Preybuffor = new Bufforable();

            //load all needed shaders for all entites
            _Shaders = new shader[2] {
                new shader("PrayAndPredators/shaders/shader_Predator.vert",
                           "PrayAndPredators/shaders/shader_Predator.frag"),
                new shader("PrayAndPredators/shaders/shader_Prey.vert",
                           "PrayAndPredators/shaders/shader_Prey.frag")
            };

            //initialize all cells
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    animals[x, y] = new Creature(x, y, CreatureType.Empty);
                }
            }
        }

        public void Draw()
        {
            _Shaders[1].Use();
            GL.BindVertexArray(Preybuffor._vertexArrayObject);
            GL.DrawElements(BeginMode.Triangles, indicies_prey, DrawElementsType.UnsignedInt, 0);

            _Shaders[0].Use();
            GL.BindVertexArray(Predatorbuffor._vertexArrayObject);
            GL.DrawElements(BeginMode.Triangles, indicies_predator, DrawElementsType.UnsignedInt,0);
        }

        int indicies_predator = 0;
        int indicies_prey = 0;

        public void Update(double updateTime)
        {
            int predators = 0;
            int prey = 0;
            
            //update all entitis
            foreach (Creature animal in animals)
                animal?.Update(updateTime);

            //count all entitis of each type
            foreach (Creature animal in animals)
            {
                if (animal.Type == CreatureType.Predator)
                {
                    predators++;
                }
                else if (animal.Type == CreatureType.Prey)
                {
                    prey++;
                }
            }

            //prepare all needed arrays of specif length
            uint[] list_Indicies_predator = new uint[predators * 6];
            float[] list_Vertex_predator = new float[predators * 12];
            uint[] list_Indicies_prey = new uint[prey * 6];
            float[] list_Vertex_prey = new float[prey * 12];

            prey = 0;
            predators = 0;
            uint[] temp_ui = null;
            float[] temp_f = null;
            int index = 0;
            //prepare all arrays with indices and vertex
            foreach (Creature animal in animals)
            {
                temp_ui = null;
                temp_f = null;
                index = 0;
                if (animal.Type == CreatureType.Predator && list_Indicies_predator.Length != 0 && predators * 6 <= list_Indicies_predator.Length)
                {
                    index = predators * 6;
                    //set indicies of predator 
                    temp_ui = animal.Indices((uint)(predators * 4));
                    list_Indicies_predator[index] = temp_ui[0];
                    list_Indicies_predator[index + 1] = temp_ui[1];
                    list_Indicies_predator[index + 2] = temp_ui[2];
                    list_Indicies_predator[index + 3] = temp_ui[3];
                    list_Indicies_predator[index + 4] = temp_ui[4];
                    list_Indicies_predator[index + 5] = temp_ui[5];

                    index = predators * 12;
                    temp_f = animal._vertices();
                    //set vertex of predator
                    list_Vertex_predator[index] = temp_f[0];
                    list_Vertex_predator[index + 1] = temp_f[1];
                    list_Vertex_predator[index + 2] = temp_f[2];
                    list_Vertex_predator[index + 3] = temp_f[3];
                    list_Vertex_predator[index + 4] = temp_f[4];
                    list_Vertex_predator[index + 5] = temp_f[5];
                    list_Vertex_predator[index + 6] = temp_f[6];
                    list_Vertex_predator[index + 7] = temp_f[7];
                    list_Vertex_predator[index + 8] = temp_f[8];
                    list_Vertex_predator[index + 9] = temp_f[9];
                    list_Vertex_predator[index + 10] = temp_f[10];
                    list_Vertex_predator[index + 11] = temp_f[11];
                    predators++;
                }
                if (animal.Type == CreatureType.Prey && list_Indicies_prey.Length != 0 && prey * 6 <= list_Indicies_prey.Length)
                {
                    index = prey * 6;
                    temp_ui = animal.Indices((uint)(prey * 4));
                    //set indicies of prey
                    list_Indicies_prey[index] = temp_ui[0];
                    list_Indicies_prey[index + 1] = temp_ui[1];
                    list_Indicies_prey[index + 2] = temp_ui[2];
                    list_Indicies_prey[index + 3] = temp_ui[3];
                    list_Indicies_prey[index + 4] = temp_ui[4];
                    list_Indicies_prey[index + 5] = temp_ui[5];

                    index = prey * 12;
                    temp_f = animal._vertices();
                    //set vertex of prey
                    list_Vertex_prey[index] = temp_f[0];
                    list_Vertex_prey[index + 1] = temp_f[1];
                    list_Vertex_prey[index + 2] = temp_f[2];
                    list_Vertex_prey[index + 3] = temp_f[3];
                    list_Vertex_prey[index + 4] = temp_f[4];
                    list_Vertex_prey[index + 5] = temp_f[5];
                    list_Vertex_prey[index + 6] = temp_f[6];
                    list_Vertex_prey[index + 7] = temp_f[7];
                    list_Vertex_prey[index + 8] = temp_f[8];
                    list_Vertex_prey[index + 9] = temp_f[9];
                    list_Vertex_prey[index + 10] = temp_f[10];
                    list_Vertex_prey[index + 11] = temp_f[11];
                    prey++;
                }
            }

            this.indicies_predator = predators * 6;
            this.indicies_prey = prey * 6;

            //learn how to use subdata
            //GL.NamedBufferSubData(Predatorbuffor._vertexBufferObject,
            //     IntPtr.Zero,
            //     list_Vertex_predator.Length * sizeof(float),
            //     list_Vertex_predator
            //     );
            //GL.NamedBufferSubData(Predatorbuffor._elementBufferObject,
            //    IntPtr.Zero,
            //    list_Indicies_predator.Length * sizeof(uint),
            //    list_Indicies_predator
            //    );

            //GL.NamedBufferSubData(Preybuffor._vertexBufferObject,
            //     IntPtr.Zero,
            //     list_Vertex_prey.Length * sizeof(float),
            //     list_Vertex_prey
            //     );

            //GL.NamedBufferSubData(Preybuffor._elementBufferObject,
            //     IntPtr.Zero,
            //     list_Indicies_prey.Length * sizeof(uint),
            //     list_Indicies_prey
            //     );

            //update all data in buffers
            //predator
            GL.NamedBufferData(Predatorbuffor._vertexBufferObject,
                list_Vertex_predator.Length * sizeof(float),
                list_Vertex_predator.ToArray(),
                BufferUsageHint.StreamDraw);

            GL.NamedBufferData(Predatorbuffor._elementBufferObject,
                list_Indicies_predator.Length * sizeof(uint),
                list_Indicies_predator.ToArray(),
                BufferUsageHint.StreamDraw);
            //prey
            GL.NamedBufferData(Preybuffor._vertexBufferObject,
                list_Vertex_prey.Length * sizeof(float),
                list_Vertex_prey.ToArray(),
                BufferUsageHint.StreamDraw);

            GL.NamedBufferData(Preybuffor._elementBufferObject,
                list_Indicies_prey.Length * sizeof(uint),
                list_Indicies_prey.ToArray(),
                BufferUsageHint.StreamDraw);
        }

        public void Add()
        {
            if (Program.rnd.Next(1, 100) > 80)
            {
                int x = Program.rnd.Next(0, width - 1),
                    y = Program.rnd.Next(0, height - 1);
                if (Program.rnd.Next(1, 100) > 50)
                {

                    PAP.animals[x, y].Type = CreatureType.Prey;
                    PAP.animals[x, y].health = 0;
                }
                else
                {
                    PAP.animals[x, y].Type = CreatureType.Predator;
                    PAP.animals[x, y].health = 100;
                }
            }
        }

        public void Clear()
        {
            ////cleare data from all Entitis
            Predatorbuffor.Clear();
            Preybuffor.Clear();
        }
    }
}
