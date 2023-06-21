using Android.Gestures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Runner
{
    internal class Magic
    {
        private SpriteBatch spriteBatch;
        private Texture2D effect;
        private Texture2D icon;
        private double time;
        private int shift;
        private int ticks;
        private int type;
        private int id;
        private Point size;
        public bool active;
        private Vector2 position;
        public Magic(int type, int id)
        {
            this.type = type;
            SelectSpell();
            this.id = id;
        }

        public void Load(Texture2D _texture, Texture2D _icon, SpriteBatch _spriteBatch)
        {
            effect = _texture;
            icon = _icon;
            spriteBatch = _spriteBatch;
            active = false;
        }

        public void Update(double dTime)
        {
            if (active)
            {
                time += dTime;
                if (time >= 80)
                {
                    time -= 80;
                    shift++;
                }
                if (shift >= ticks)
                {
                    shift = 0;
                    active = false;
                }
            }  
        }

        public int GetDamageType()
        {
            if (active)
            {
                switch (type)
                {
                    case 1:
                        if (shift >= 4)
                        {
                            return type;
                        }
                        break;
                    case 2:
                        if (shift >= 4 && shift <= 13)
                        {
                            return type;
                        }
                        break;
                    case 3:
                        if (shift >= 2)
                        {
                            return type;
                        }
                        break;
                }
            }
            return 0;
        }

        public void Draw(int type)
        {
            if (active)
            {
                spriteBatch.Draw(effect,
                    position,
                    new Rectangle(0, shift * size.Y, size.X, size.Y),
                    Color.White);
            }
            Color iconColor = Color.Gray;
            if (type == this.type)
                iconColor = Color.White;
            spriteBatch.Draw(icon,
                        new Vector2(4 + 52 * id, 512 - 56),
                        new Rectangle(0, 0, 48, 48),
                        iconColor);
        }
        public void Cast()
        {
            active = true;
            time = 0;
        }
        public void SelectSpell()
        {
            switch (type)
            {
                case 1:
                    size = new Point(80, 80);
                    ticks = 17;
                    position = new Vector2(-6, 512 - 80 - 80);
                    break;
                case 2:
                    size = new Point(96, 32);
                    ticks = 16;
                    position = new Vector2(32, 512 - 80 - 48);
                    break;
                case 3:
                    size = new Point(80, 32);
                    position = new Vector2(32, 512 - 80 - 48);
                    ticks = 6;
                    break;
                default: break;
            }
        }
        public int GetRange()
        {
            switch (type)
            {
                case 1:
                    return 30;
                case 2:
                    return 70;
                case 3:
                    return 70;
            }
            return 0;
        }
    }
}
