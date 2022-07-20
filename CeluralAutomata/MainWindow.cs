using OpenTK;
using OpenTK.Graphics;
using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTk.PrayAndPredators;

namespace OpenTk
{
    public sealed class MainWindow : GameWindow
    {
        public static float x_scaled;
        public static float y_scaled;

        public MainWindow() : base(1920, 1080,
            GraphicsMode.Default,
            "Cellural Automata",
            GameWindowFlags.Default,
            DisplayDevice.Default,
            4,
            0,
            GraphicsContextFlags.ForwardCompatible)
        {
            //disable vsync
            this.VSync = VSyncMode.Off;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(this.ClientRectangle);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 1000, 1000, 0, -1, 0);
        }

        private PAP preyandpredator;
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            //set x/y scaled values
            x_scaled = 1000 / 1920f;
            y_scaled = 1000 / 1080f;

            //initialize object of PredatorAndPray
            preyandpredator = new PAP();
            //init the object
            preyandpredator.Init();
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            //clear all data from object
            preyandpredator.Clear();
            base.OnUnload(e);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Keyboard.IsKeyDown(Key.F))
            {

            }

            if (e.Keyboard.IsKeyDown(Key.W))
            {

            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //update all objects
            preyandpredator.Update(this.UpdateTime);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            //clear buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //draw objects owned by PAP
            preyandpredator.Draw();

            //swap buffers
            SwapBuffers();
        }
    }
}
