using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk
{
    /// <summary>
    /// point of this class to have generic class for storing multiple instances of this class but with different logics
    /// but still be able to access interfaces 
    /// </summary>
    class Entity : Bufforable, Interfaces.Drawable, Interfaces.Updatable
    {
        public Entity() : base()
        {

        }

        public virtual void Draw()
        {

        }

        public virtual void Update(double updateTime)
        {

        }
    }
}
