using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    internal class Tile : Incoming
    {
        private int[] next;

        public Tile()
        {
            time = 0;
            shift = 0;
            next = new int[13];
            for (int i = 0; i < next.Length; i++) { next[i] = r.Next(0, 5); }
        }

        public override void Update(double dTime)
        {
            if (shift >= 16)
            {
                shift -= 16;
                for (int i = 0; i < next.Length-1; i++)
                {
                    next[i] = next[i + 1];
                }
                next[12] = r.Next(0, 5);
            }
        }

        public void DrawGround()
        {
            for (int i = 0; i < next.Length; i++)
            {
                spriteBatch.Draw(texture,
                    new Vector2(i*16 - shift, 512 - 96),
                    new Rectangle(16 * next[i], 4, 16, 32),
                    Color.White);
            }
        }
        public void DrawGrass()
        {
            for (int i = 0; i < next.Length; i++)
            {
                spriteBatch.Draw(texture,
                new Vector2(i * 16 - shift, 512 - 96 - 4),
                new Rectangle(16 * next[i], 0, 16, 4),
                Color.White);
            }
        }
    }
}
