using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    internal abstract class Object
    {
        protected Texture2D texture;
        protected SpriteBatch spriteBatch;
        protected Vector2 position;
        protected Vector2 size;
        protected double time;
        protected int animation;
        protected int frames;
        protected int timeToTick;
        protected int state;
        protected int id;
        protected Random r;
        protected Object()
        {
            time = 0;
            timeToTick = 20;
            animation = 0;
            state = 0;
            id = 0;
            r = new Random();
        }

        public void Load(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
        }
        public void Draw()
        {
            Rectangle rect = new(animation * 96, state * (int)size.Y + id * (int)size.Y * 4, (int)size.X, (int)size.Y);
            spriteBatch.Draw(texture, position, rect, Color.White);
        }
        protected void Animation(double dTime)
        {
            time += dTime;
            if (time >= timeToTick)
            {
                time -= timeToTick;
                animation++;
            }
            if (animation >= frames) { animation = 0; }
        }
        public abstract void Update(double dTime);
        public void SetToOrigin(int id)
        {
            switch (id)
            {
                case 0:
                    position = new(-16, 512 - 160);
                    size = new(96, 64);
                    frames = 6;
                    timeToTick = 80;
                    break;
                case 1:
                    position = new(200, 512 - 160);
                    size = new(96, 64);
                    frames = 4;
                    timeToTick = 120;
                    break;
            }
        }
    }
}
