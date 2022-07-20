using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk
{
    public class Bufforable : Interfaces.Deletable
    {
        //this and class created with inheritance of this can acces this variables
        private protected int _elementBufferObject;
        private protected int _vertexBufferObject;
        private protected int _vertexArrayObject;

        public Bufforable()
        {
            //create buffer for vertex buffer object
            _vertexBufferObject = GL.GenBuffer();
            //bind object
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            //create vertex array
            _vertexArrayObject = GL.GenVertexArray();
            //bind object
            GL.BindVertexArray(_vertexArrayObject);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            //create buffer for element buffer obejct
            _elementBufferObject = GL.GenBuffer();
            //bind object
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        }

        public void Clear()
        {
            //delete all buffers
            GL.DeleteBuffer(_vertexArrayObject);
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteBuffer(_elementBufferObject);
        }
    }
}
