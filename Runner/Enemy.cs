using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    internal class Enemy : Incoming
    {
        private bool isAlive;
        private int type;
        public Enemy()
        {
            Spawn();
        }
        public override void Update(double dTime)
        {
            //Move(dTime);
            if (isAlive || animation != frames - 1) { Animation(dTime); }
            position = new(160 - shift, position.Y);
            if(shift >= 256)
            {
                Spawn();
            }
        }
        public void OnHit()
        {
            if(isAlive)
            {
                Die();
            }
        }
        private void Spawn()
        {
            shift -= 256;
            isAlive = true;
            state = 1;
            frames = 6;
            animation = 0;
            speed = 2;
            id = r.Next(0,3);
            switch (id)
            {
                case 0:
                    type = 1;
                    frames = 4;
                    speed = 1;
                    break;
                case 1:
                    type = 3;
                    break;
                case 2:
                    type = 2;
                    break;
            }
        }
        private void Die()
        {
            isAlive = false;
            state = 3;
            frames = 7;
            animation = 0;
            speed = 1;
            switch (id)
            {
                case 0:
                    frames = 4;
                    break;
            }
        }
        public int GetPosition()
        {
            return 160 - shift;
        }
        public int GetDamageType()
        {
            return type;
        }
    }
}
