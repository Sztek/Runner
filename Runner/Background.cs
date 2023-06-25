using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    internal class Background
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private int positionY;
        private int[] positionX;
        private Vector2 size;
        private double[] timeToTick;
        private int id;

        public void Load(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
            this.positionY = 512-216-96;
            this.positionX = new int[7];
            this.timeToTick = new double[7];
            for (int i = 0; i < 7; i++) { positionX[i] = 0; timeToTick[i] = 0; }
            this.size = new Vector2(384, 216);
            id = 0;
            
        }
        public void Update(double dTime)
        {
            for (int i = 0; i < 7; i++)
            {
                timeToTick[i] += dTime;
                if (timeToTick[i] > 25 + i * 10)
                {
                    positionX[i]--;
                    timeToTick[i] -= 25 + i * 10;
                    if (positionX[i] <= -size.X) { positionX[i] += (int)size.X; }
                }
            }
        }
        public void Draw()
        {
            
            for (int i = 6; i >= 0; i--)
            {
                Rectangle rect = new(384 * id, 216 * i, (int)size.X, (int)size.Y);
                if (i == 6)
                {
                    spriteBatch.Draw(texture, new Vector2(positionX[i], positionY - 96), rect, Color.White);
                    spriteBatch.Draw(texture, new Vector2(positionX[i] + size.X, positionY - 96), rect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(texture, new Vector2(positionX[i], positionY), rect, Color.White);
                    spriteBatch.Draw(texture, new Vector2(positionX[i] + size.X, positionY), rect, Color.White);
                }
            }
        }
    }
}
