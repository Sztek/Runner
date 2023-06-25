using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    internal class Menu
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        public void Load(Texture2D texture, SpriteBatch spriteBatch)
        {
            this.texture = texture;
            this.spriteBatch = spriteBatch;
        }
        public bool Update()
        {
            return true;
        }
        public void Draw()
        {
            spriteBatch.Draw(texture, new Vector2(0), new Rectangle(0, 0, 160, 512), Color.White);
        }
    }
}