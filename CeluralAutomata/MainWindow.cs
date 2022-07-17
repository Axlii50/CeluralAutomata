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
            //GL.Enable(EnableCap.DepthTest);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float),
                _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

           
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            // Enable variable 0 in the shader.
            GL.EnableVertexAttribArray(0);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            // We also upload data to the EBO the same way as we did with VBOs.
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            _Shader = new shader("Shaders/shader.vert", "Shaders/shader.frag");

            _Shader.Use();
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

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

        public void test()
        {

        }

        //private readonly float[] _vertices =
        //{
        //    -0.5f, -0.5f, 0.0f, // Bottom-left vertex
        //     0.5f, -0.5f, 0.0f, // Bottom-right vertex
        //     0.0f,  0.5f, 0.0f  // Top vertex
        //};
        //private readonly float[] _vertices =
        //{
        //    0.5f,  0.5f, 0.0f,  // top right
        //    0.5f, -0.5f, 0.0f,  // bottom right
        //    -0.5f, -0.5f, 0.0f,  // bottom left
        //    -0.5f,  0.5f, 0.0f   // top left

        //};

        public static float x_scaled = 1000 / 1920f;
        public static float y_scaled = 1000 / 1080f;

        private float[] _vertices =
                {
            0,0,0, //left top
            x_scaled,0,0, //right top
            0,y_scaled,0, // left bottom
            x_scaled,y_scaled,0, // right bottom

            0,0,0, //left top
            x_scaled,0,0, //right top
            0,y_scaled,0, // left bottom
            x_scaled,y_scaled,0 // right bottom
        };

        uint[] indices = {  // note that we start from 0!
            0, 1, 2,  // first triangle
            2, 1, 3    // second triangle
         };

        private int _elementBufferObject;
        private int _vertexBufferObject;
        private int _vertexArrayObject;

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _Shader.Use();

            GL.DrawElements(BeginMode.Triangles,indices.Length,DrawElementsType.UnsignedInt,0);

            SwapBuffers();
        }
    }
}
