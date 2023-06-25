using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    internal class Character : Object
    {
        public bool isAlive;
        public Character()
        {
            isAlive = true;
            state = 1;
            frames = 6;
        }
        public override void Update(double dTime)
        {
            Animation(dTime);
        }
        public void GetHit(bool attack)
        {
            if (!isAlive) { position.X--; }
            if (attack)
            {
                state = 3;
                frames = 7;
                animation = 0;
                isAlive = false;
            }
        }
        public bool GameEnd()
        {
            if(!isAlive && position.X < -128)
            {
                isAlive = true;
                state = 1;
                frames = 6;
                SetToOrigin(0);
                return true;
            }
            return false;
        }
    }
}
