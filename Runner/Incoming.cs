using Android.Views.Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    internal abstract class Incoming : Object
    {
        protected int shift;
        protected int speed;
        public Incoming() { speed = 1; }
        public void Move()
        {
            shift+=speed;
        }
    }
}
