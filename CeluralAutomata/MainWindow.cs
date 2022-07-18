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
        public MainWindow() : base(1920, 1080,
            GraphicsMode.Default,
            "OpenTk",
            GameWindowFlags.Default,
            DisplayDevice.Default,
            4,
            0,
            GraphicsContextFlags.ForwardCompatible)
        {

        }


        protected override void OnResize(EventArgs e)
        {
            //GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            GL.Viewport(0, 0, 1920, 1080);
        }

        private shader _Shader;
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            rects = new Rectangle[] { new Rectangle(1, 1), new Rectangle(3, 3) };

            _Shader = new shader("Shaders/shader.vert", "Shaders/shader.frag");

            _Shader.Use();
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            foreach (OpenTk.Interfaces.Deletable x in rects)
                x.Clear();

            GL.DeleteProgram(_Shader.Handle);
            base.OnUnload(e);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);


            if (e.Keyboard.IsKeyDown(Key.F))
            {

            }
        }

        public static float x_scaled = 1000 / 1920f;
        public static float y_scaled = 1000 / 1080f;

        Rectangle[] rects;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _Shader.Use();


            foreach (OpenTk.Interfaces.Drawable x in rects)
                x.Draw();

            SwapBuffers();
        }
    }
}
