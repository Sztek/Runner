using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    internal class Character : Object
    {
        public Character() { }
        public override void Update(double dTime)
        {
            Animation(dTime);
        }
    }
}
