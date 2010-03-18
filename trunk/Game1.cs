using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TarreGame
{

    public class Bala
    {
        public Vector2 position;
        public float angulo;

    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D mava;
        Texture2D mouse;
        Texture2D mina;

        MouseState maus;

        KeyboardState ks;
        SpriteFont spriteFont;
        
        Vector2 posicion_mava;
        Vector2 posicion_mina;

        Vector2 posicion_mouse = new Vector2(30,30);

        List<Bala> lista_balas = new List<Bala>();
        int delay = 1;
        int delay2 = 0;

        float angulo_mouse;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            posicion_mina.X = 500;
            posicion_mava.X = 350;
            posicion_mava.Y = 300;
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mava = Content.Load<Texture2D>("mava");
            mouse = Content.Load<Texture2D>("mouse");
            mina = Content.Load<Texture2D>("mina");

            spriteFont = Content.Load<SpriteFont>("texto");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            double elapsedMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ks = Keyboard.GetState();
            maus = Mouse.GetState();
            
            if (maus.LeftButton == ButtonState.Pressed && delay == 1)
            {
                angulo_mouse = (float)Math.Atan2(maus.Y - posicion_mava.Y, maus.X - posicion_mava.X);

                posicion_mouse.X += (float)Math.Cos(angulo_mouse);
                posicion_mouse.Y += (float)Math.Sin(angulo_mouse);
                
                Bala bala = new Bala();
                bala.position = posicion_mava;
                bala.angulo = angulo_mouse;
                lista_balas.Add(bala);

                delay = 0;
            }

            if (delay2 < 15 && delay == 0)
            {
                delay2++;
            }else{
                delay = 1;
                delay2 = 0;
            }

            foreach (Bala bala in lista_balas)
            {
                float angulo_disparo = angulo_mouse;

                bala.position.X += (float)Math.Cos(bala.angulo) * 10;
                bala.position.Y += (float)Math.Sin(bala.angulo) * 10;
            }

            /*for (int i = 0; i < lista_balas.Count; i++)
            {
                float angulo_disparo = angulo_mouse;

                lista_balas[i].position.X += 10 * (float)Math.Cos(angulo_mouse);
                lista_balas[i].position.Y += 10 * (float)Math.Sin(angulo_mouse);

            }*/

            if (ks.IsKeyDown(Keys.Right)) { posicion_mava.X += 5; }
            if (ks.IsKeyDown(Keys.Left)) { posicion_mava.X -= 5; }
            if (ks.IsKeyDown(Keys.Up)) { posicion_mava.Y -= 5; }
            if (ks.IsKeyDown(Keys.Down)) { posicion_mava.Y += 5; }

            if (posicion_mina.X >= 700)
            {
                posicion_mina.X = 700;
            }

            if (posicion_mina.X <= 0)
            {
                posicion_mina.X = 0;
            }

            if (posicion_mina.Y >= 480)
            {
                posicion_mina.Y = 480;
            }

            if (posicion_mina.Y <= 0)
            {
                posicion_mina.Y = 0;
            }

            if ((posicion_mina.X - posicion_mava.X) < 200)
            {
                if (posicion_mava.X < posicion_mina.X)
                {
                    posicion_mina.X++;
                }
                else
                {
                    posicion_mina.X--;
                }
            }

            if ((posicion_mava.Y - posicion_mina.Y) < 200)
            {
                if (posicion_mava.Y < posicion_mina.Y)
                {
                    posicion_mina.Y++;
                }
                else
                {
                    posicion_mina.Y--;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(mava, new Rectangle((int)posicion_mava.X, (int)posicion_mava.Y, 80, 100), Color.White);
            spriteBatch.Draw(mina, new Rectangle((int)posicion_mina.X, (int)posicion_mina.Y, 100, 120), Color.White);
            spriteBatch.Draw(mouse, new Rectangle((int)posicion_mouse.X, (int)posicion_mouse.Y, 50, 50), Color.White);

            spriteBatch.DrawString(spriteFont, "MAVA_X= " + posicion_mava.X.ToString() + " / MAVA_Y= " + posicion_mava.Y.ToString(), new Vector2(50, 500), Color.Gold);
            spriteBatch.DrawString(spriteFont, "MINA_X= " + posicion_mina.X.ToString() + " / MINA_Y= " + posicion_mina.Y.ToString(), new Vector2(50, 550), Color.Gold);

            spriteBatch.DrawString(spriteFont, "Diff_X= " + (posicion_mina.X - posicion_mava.X).ToString(), new Vector2(200, 250), Color.Gold);

            spriteBatch.DrawString(spriteFont, "Angulo Mouse: " + angulo_mouse.ToString(), new Vector2(50, 50), Color.Gold);
            spriteBatch.DrawString(spriteFont, "Balas: " + lista_balas.Count.ToString(), new Vector2(50, 70), Color.Gold);
            spriteBatch.DrawString(spriteFont, "Delay Balas: " + delay2.ToString(), new Vector2(50, 90), Color.Gold);

            foreach (Bala bala in lista_balas)
            {
                spriteBatch.Draw(mouse, new Rectangle((int)bala.position.X, (int)bala.position.Y, 50, 50), Color.White);
            }

            /*for (int i = 0; i < lista_balas.Count; i++)
            {
                spriteBatch.Draw(mouse, new Rectangle((int)lista_balas[i].position.X, (int)lista_balas[i].position.Y, 50, 50), Color.White);
            }*/

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void disparar()
        {

        }
    }
}
