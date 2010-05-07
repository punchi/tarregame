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
        Pantalla menu_principal;
        string eleccionplayer;
        List<Texture2D> fotoplayer = new List<Texture2D>(3);
        int indice_eleccionplayer = 0;
        string pantalla = "EleccionPlayer";
        string texto_player;
        
        bool bloqueo_izq = false;
        bool bloqueo_der = false;

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
            
            fotoplayer.Add(Content.Load<Texture2D>(@"Texture\Juba"));
            fotoplayer.Add(Content.Load<Texture2D>(@"Texture\Lito"));
            fotoplayer.Add(Content.Load<Texture2D>(@"Texture\Mava"));
            fotoplayer.Add(Content.Load<Texture2D>(@"Texture\Punchi"));

            //player = new Jugador(Content, "Mava");
            Punchi = new Jugador(Content, "Punchi");
            bala = new Bala(Content);

            menu_principal = new Pantalla(5);
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
                UpdateEleccionPlayer(keystate, gameTime);
            }
            
            if (pantalla == "Juego")
            {
                CheckMovmentPlayer(keystate);
                /*if (!player.en_juego)
                {
                    player = new Jugador(Content, eleccionplayer);
                    player.en_juego = true;
                }*/
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
            Console.WriteLine(eleccionplayer);
            spriteBatch.Begin();
            player.Draw(spriteBatch,spritefont);
            Punchi.Draw(spriteBatch,spritefont);
            if (bala.activo == true) { bala.Draw(spriteBatch); }
            //spriteBatch.DrawString(spritefont, "frags: " + player.frags.ToString(), hudfrags, Color.White);
            //spriteBatch.DrawString(spritefont, "frags: " + player.frags.ToString(), hudfrags+new Vector2(1,1), Color.YellowGreen);
            spriteBatch.End();

            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// funciones que van en update
        /// </summary>
        /// 
        void DrawEleccionPlayer() 
        {

            
            switch (indice_eleccionplayer)
            {
                case 0:
                    eleccionplayer = "Juba";
                    texto_player = "Jugador gay, dispara tulas. Un maraco de seleccion.";
                    break;
                case 1:
                    eleccionplayer = "Lito";
                    texto_player = "Cagamos. Jugai con este wn y lagazo o reseteo\ninmediato.";
                    break;
                case 2:
                    eleccionplayer = "Mava";
                    texto_player = "Mala cuea maxima, cualquier wea mala que pase\nen el mundo, es culpa de Mava.";
                    break;
                case 3:
                    eleccionplayer = "Punchi";
                    texto_player = "Macabeismo es su definicion, 99% de su vida\ncon pololi.";
                    break;
            }

            spriteBatch.DrawString(spritefont, "Jugador: " + eleccionplayer, new Vector2(160, 330), Color.Black);
            spriteBatch.DrawString(spritefont, "Jugador: " + eleccionplayer, new Vector2(161, 331), Color.Red);
            spriteBatch.Draw(fotoplayer[indice_eleccionplayer], new Rectangle(180, 360, 80, 100), Color.White);
            spriteBatch.DrawString(spritefont, texto_player, new Vector2(280, 360), Color.Red);
            spriteBatch.DrawString(spritefont, "Temporal: " + "", new Vector2(161, 40), Color.Red);
            
        }

        void UpdateEleccionPlayer(KeyboardState ksstate, GameTime gametime)
        {

            if (ksstate.IsKeyUp(Keys.Left))
            {
                bloqueo_izq = true;
            }

            if (ksstate.IsKeyUp(Keys.Right))
            {
                bloqueo_der = true;
            }

            if (ksstate.IsKeyDown(Keys.Left))
            {
                if (bloqueo_izq == true && indice_eleccionplayer > 0)
                {
                    indice_eleccionplayer--;
                    bloqueo_izq = false;
                }
            }

            if (ksstate.IsKeyDown(Keys.Right))
            {
                if (bloqueo_der == true && indice_eleccionplayer < 3)
                {
                    indice_eleccionplayer++;
                    bloqueo_der = false;
                }
            }
            

            /*if (eleccionplayer == "asd") { eleccionplayer = "Punchi"; }
            if (eleccionplayer == "Punchi") { eleccionplayer = "Lito"; }
            if (eleccionplayer == "Lito") { eleccionplayer = "Juba"; }
            if (eleccionplayer == "Juba") { eleccionplayer = "lol"; }*/

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
            if (ksstate.IsKeyDown(Keys.F1))
            {
                player = new Jugador(Content, eleccionplayer);
                pantalla = "Juego";
            }
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
