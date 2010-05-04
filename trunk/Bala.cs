using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Bala
    {
        #region declaraciones
        bool iactivo;
        Vector2 posicion;
        Vector2 pantalla = new Vector2(795, 595);
        int valorrate = 10;
        double iangulo;
        Texture2D balatextura;
        string iorigen;
        #endregion

        #region propiedades
        public bool activo 
        {
            get { return iactivo; }
            set { iactivo = value; }
        }
        public Vector2 poss
        {
            get { return posicion; }
            set { posicion = value; }
        }
        public string origen 
        {
            get { return iorigen; }
            set { iorigen = value; }
        }
        public float X
        {
            get { return posicion.X; }
            set { posicion.X = value; }
        }
        public float Y
        {
            get { return posicion.Y; }
            set { posicion.Y = value; }
        }
        public double angulo
        { 
            get { return iangulo; }
            set { iangulo = value; }
        }
        public int rate 
        {
            get { return valorrate; }
            set { valorrate = value; }
        }
        #endregion

        #region constructores
        public Bala(ContentManager Content) 
        {
            balatextura = Content.Load<Texture2D>(@"Texture\Bala");
        }
        #endregion

        #region metodos
        public void Update(GameTime gametime)
        {
            posicion.X = posicion.X + (float)Math.Cos(iangulo) * valorrate;
            posicion.Y = posicion.Y + (float)Math.Sin(iangulo) * valorrate;
            if (posicion.X < 0 || posicion.X > 795 || posicion.Y < 0 || posicion.Y > 595) 
            { 
                iactivo = false; 
            }
        }  
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(balatextura, posicion, Color.White);
        }
        public void disparar(GameTime gametime, MouseState mouse,Jugador player)
        {
            Update(gametime);
            if (mouse.LeftButton == ButtonState.Pressed && activo == false)
            {
                activo = true;
                posicion.X = player.X + 20.5f;
                posicion.Y = player.Y + 25;
                angulo = Math.Atan2(mouse.Y - player.Y - 2.5f, mouse.X - player.X - 2.5f);
                origen = player.Nombre;
            }
        }
        #endregion
    }
}
