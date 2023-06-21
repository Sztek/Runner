using Android.Icu.Number;
using Java.Lang.Annotation;
using Java.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using static Android.Icu.Text.Transliterator;

namespace Runner
{
    public class Game1 : Game
    {
        Point gameResolution = new(160, 512);
        Point resolution;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        RenderTarget2D renderTarget;
        Rectangle renderTargetDestination;
        SpriteFont gameFont;
        bool showDebug;
        Color colorDebug;
        double timeToTick;

        Texture2D hud_bot;

        Tile tile;
        Background background;
        Character player;
        Enemy enemy;
        Magic[] spell;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            showDebug = false;
            colorDebug = Color.White;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            tile = new Tile();
            player = new Character();
            enemy = new Enemy();
            spell = new Magic[3];
            background = new Background();
            for (int i = 0; i < spell.Length; i++)
            {
                spell[i] = new Magic(i + 1, i);
                //spell[i].SelectSpell(i);
            }
            timeToTick = 0;
            player.SetToOrigin(0);
            enemy.SetToOrigin(1);
            resolution.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            resolution.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            renderTarget = new RenderTarget2D(GraphicsDevice, gameResolution.X, gameResolution.Y);
            renderTargetDestination = new Rectangle(0, -1 * (resolution.X / gameResolution.X * gameResolution.Y - resolution.Y),
                resolution.X, resolution.X/gameResolution.X*gameResolution.Y);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            enemy.Load(Content.Load<Texture2D>("Enemy"), spriteBatch);
            background.Load(Content.Load<Texture2D>("background"),spriteBatch);
            player.Load(Content.Load<Texture2D>("Hero"), spriteBatch);
            //gameFont = Content.Load<SpriteFont>("gameFont");
            hud_bot = Content.Load<Texture2D>("hud_bottom");
            tile.Load(Content.Load<Texture2D>("ground"), spriteBatch);
            spell[0].Load(Content.Load<Texture2D>("Water_Bubble"), Content.Load<Texture2D>("icon1"), spriteBatch);
            spell[1].Load(Content.Load<Texture2D>("Fire_Incendiary"), Content.Load<Texture2D>("icon2"), spriteBatch);
            spell[2].Load(Content.Load<Texture2D>("Lightning_II"), Content.Load<Texture2D>("icon3"), spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            double dTime = gameTime.ElapsedGameTime.TotalMilliseconds;
            TouchCollection touchState = TouchPanel.GetState();
            bool casting = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++) { if (spell[j].active) { casting = true; } }
                if (!casting)
                {
                    foreach (var touch in touchState)
                    {
                        if (new Rectangle(i * (resolution.X / 3), 0, resolution.X / 3, resolution.Y).Contains(touch.Position))
                        {
                            spell[i].Cast();
                        }
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                spell[i].Update(dTime);
                if (spell[i].GetDamageType() != 0 &&
                    (spell[i].GetDamageType() == enemy.GetDamageType() || enemy.GetDamageType() == 0) &&
                    spell[i].GetRange() >= enemy.GetPosition())
                {
                    enemy.OnHit();
                }
            }
            enemy.Update(dTime);
            background.Update(dTime);
            player.Update(dTime);
            tile.Update(dTime);
            timeToTick += dTime;
            if (timeToTick >= 20)
            {
                timeToTick -= 20;
                tile.Move();
                enemy.Move();
            }
            player.GetHit(enemy.OnAttack());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            background.Draw();
            tile.DrawGround();
           
            enemy.Draw();
            player.Draw();

            tile.DrawGrass();

            spriteBatch.Draw(hud_bot, new Vector2(0, gameResolution.Y - 64), new Rectangle(0, 0, 192, 64), Color.White);

            for (int i = 0; i < spell.Length; i++)
            {
                spell[i].Draw(enemy.GetDamageType());
            }

            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(renderTarget, renderTargetDestination, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}