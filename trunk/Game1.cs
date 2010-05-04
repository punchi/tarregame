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


namespace TarreeGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spritefont;
        Jugador player;
        Jugador Punchi;
        Bala bala;
        Vector2 inputplayer;
        Vector2 hudfrags=new Vector2(10,10);
        Pantallas menu_principal;
        string eleccionplayer;
        int indice_eleccionplayer = 0;
        string pantalla = "EleccionPlayer";

        #region Multiplayer

        /*NetworkSession networksession;
        PacketReader packetreader;
        PacketWriter packetwriter;*/

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Components.Add(new GamerServicesComponent(this));
            //SignedInGamer.SignedIn +=new EventHandler<SignedInEventArgs>(SignedInGamer_SignedIn);
        }

        void SignedInGamer_SignedIn(object sender, SignedInEventArgs e)
        {
            e.Gamer.Tag = new Jugador(Content, "Juba");
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
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
            IsMouseVisible = true;
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
            spritefont = Content.Load<SpriteFont>(@"font\font");

            player = new Jugador(Content,eleccionplayer);
            Punchi = new Jugador(Content, "Punchi");
            bala = new Bala(Content);

            menu_principal = new Pantallas(5);
            menu_principal.anadir(0, "Presiona F1 para comenzar po shoro! (Single Player)");
            menu_principal.anadir(1, "Presiona F2 para volver esta ventana");
            menu_principal.anadir(2, "Presiona F3 para cambiar personaje");
            menu_principal.anadir(3, "Presiona F4 para iniciar como SERVIDOR");
            menu_principal.anadir(4, "Presiona F5 para iniciar como CLIENTE");
            
            // TODO: use this.Content to load your game content here
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
            KeyboardState keystate = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            // Allows the game to exit
            if (keystate.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            if (pantalla == "EleccionPlayer")
            {
                UpdateEleccionPlayer(keystate);
            }
            
            if (pantalla == "Juego")
            {
                CheckMovmentPlayer(keystate);
                bala.disparar(gameTime, mouse, player);
                ImpactoPlayer();
            }

            if (pantalla == "Servidor")
            {

            }

            if (pantalla == "Cliente")
            {

            }

            UpdateMenu(keystate);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //spriteBatch.Begin();

            

            if (pantalla == "EleccionPlayer")
            {
                spriteBatch.Begin();
                
                DrawEleccionPlayer();
                menu_principal.Draw(spriteBatch, spritefont);
                
                spriteBatch.End();
            }
            else 
            {
                DrawGameplay(); 
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// funciones que van en update
        /// </summary>
        /// 
        void DrawEleccionPlayer() 
        {
            

            /*spriteBatch.DrawString(spritefont,"Presiona F1 para comenzar po shoro! (Single Player) "
                                               + "\nPresiona F2 para volver esta ventana"
                                               + "\nPresiona F3 para cambiar personaje"
                                               + "\nPresiona F4 para iniciar como SERVIDOR"
                                               + "\nPresiona F5 para iniciar como CLIENTE", new Vector2(160, 160), Color.Black);
            spriteBatch.DrawString(spritefont, "Presiona F1 para comenzar po shoro! "
                                               +"\nPresiona F2 para volver esta ventana"
                                               +"\nPresiona F3 para cambiar personaje", new Vector2(161, 161), Color.Red);*/

            switch (indice_eleccionplayer)
            {
                case 0:
                    eleccionplayer = "Juba";
                    break;
                case 1:
                    eleccionplayer = "Lito";
                    break;
                case 2:
                    eleccionplayer = "Mava";
                    break;
                case 3:
                    eleccionplayer = "Punchi";
                    break;
            }

            spriteBatch.DrawString(spritefont, "Jugador: " + eleccionplayer, new Vector2(160, 330), Color.Black);
            spriteBatch.DrawString(spritefont, "Jugador: " + eleccionplayer, new Vector2(161, 331), Color.Red);
            
        }

        void UpdateEleccionPlayer(KeyboardState ksstate)
        {

            if (ksstate.IsKeyDown(Keys.Left))
            {
                if (indice_eleccionplayer > 0)
                {
                    indice_eleccionplayer--;
                }
            }

            if (ksstate.IsKeyDown(Keys.Right))
            {
                if (indice_eleccionplayer < 4)
                {
                    indice_eleccionplayer++;
                }
            }
            

            /*if (eleccionplayer == "asd") { eleccionplayer = "Punchi"; }
            if (eleccionplayer == "Punchi") { eleccionplayer = "Lito"; }
            if (eleccionplayer == "Lito") { eleccionplayer = "Juba"; }
            if (eleccionplayer == "Juba") { eleccionplayer = "lol"; }*/

        }

        void DrawGameplay() 
        {
            spriteBatch.Begin();

            player.Draw(spriteBatch,spritefont);
            Punchi.Draw(spriteBatch,spritefont);
            if (bala.activo == true) { bala.Draw(spriteBatch); }
            spriteBatch.DrawString(spritefont, "frags: " + player.frags.ToString(), hudfrags, Color.White);
            spriteBatch.DrawString(spritefont, "frags: " + player.frags.ToString(), hudfrags+new Vector2(1,1), Color.YellowGreen);
            spriteBatch.End();

        }
        public void ImpactoPlayer() 
        {
            if (Punchi.ImpactoBala(bala))
            {
                bala.activo = false;
                Punchi.KillPlayer(player);
            }
        }

        protected void CheckMovmentPlayer(KeyboardState ksstate)
        {
            if (ksstate.IsKeyDown(Keys.Up))   { inputplayer.Y = -1;     player.Update(inputplayer); } else { inputplayer.Y = 0; }
            if (ksstate.IsKeyDown(Keys.Down)) { inputplayer.Y = 1;      player.Update(inputplayer); } else { inputplayer.Y = 0; }
            if (ksstate.IsKeyDown(Keys.Left)) { inputplayer.X = -1;     player.Update(inputplayer); } else { inputplayer.X = 0; }
            if (ksstate.IsKeyDown(Keys.Right)){ inputplayer.X = 1;      player.Update(inputplayer); } else { inputplayer.X = 0; }
            
        }
        public void UpdateMenu(KeyboardState ksstate) 
        {
            if (ksstate.IsKeyDown(Keys.F1)) { pantalla = "Juego"; }
            if (ksstate.IsKeyDown(Keys.F2)) { pantalla = "EleccionPlayer"; }
//            if (ksstate.IsKeyDown(Keys.F3)&& pantalla == "EleccionPlayer")
//            {
//                if (eleccionplayer == "Mava") { eleccionplayer = "Punchi"; }
//                if (eleccionplayer == "Punchi") { eleccionplayer = "Lito"; }
//                if (eleccionplayer == "Lito") { eleccionplayer = "Juba"; }
//                if (eleccionplayer == "Juba") { eleccionplayer = "Mava"; }
//            }

            if (ksstate.IsKeyDown(Keys.F4)) { pantalla = "Servidor"; }
            if (ksstate.IsKeyDown(Keys.F5)) { pantalla = "Cliente"; }
        }
    }
}
