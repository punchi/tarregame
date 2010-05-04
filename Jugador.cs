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
    class Jugador
    {
        #region declaraciones
        Vector2 posicion;
        Vector2 pantalla = new Vector2(759, 549);
        int valorrate = 5;
        string iNombre=null;
        int ifrags = 0;
        int ividas = 2;
        Texture2D playertexture;
        #endregion

        #region propiedades
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
        public int rate
        {
            get { return valorrate; }
            set { valorrate = value; }
        }
        public string Nombre
        {
            get { return iNombre; }
            set { iNombre = value; }
        }
        public int frags 
        {
            get { return ifrags; }
            set { ifrags = value;  }
         }
        public int vidas
        {
            get { return ividas; }
            set { ividas = value; }
        }
        public Vector2 poss {
            get { return posicion; }
            set { posicion = value; }
        }
        #endregion

        #region constructores
        public Jugador( ContentManager Content,string eleccion) {
            if (eleccion == "Mava")
            {
                posicion.X = 10;
                posicion.Y = 10;
                iNombre = "Mava";
                playertexture = Content.Load<Texture2D>(@"Texture\Mava");
            }
            if (eleccion == "Lito")
            {
                posicion.X = 749;
                posicion.Y = 10;
                iNombre = "Lito";
                playertexture = Content.Load<Texture2D>(@"Texture\Lito");
            }
            if (eleccion == "Juba")
            {
                posicion.X = 10;
                posicion.Y = 539;
                iNombre = "Juba";
                playertexture = Content.Load<Texture2D>(@"Texture\Juba");
            }
            if (eleccion == "Punchi")
            {
                posicion.X = 400;
                posicion.Y = 400;
                //posicion.X = 749;
                //posicion.Y = 539;
                iNombre = "Punchi";
                playertexture = Content.Load<Texture2D>(@"Texture\Punchi");
            }
        }
        #endregion

        #region metodos
        public void Draw(SpriteBatch spritebatch,SpriteFont spritefont) 
        {
           spritebatch.DrawString(spritefont, "(" + Nombre + ")", poss, Color.Black, 0, new Vector2(10, 30), 1f, SpriteEffects.None, 0);
           spritebatch.DrawString(spritefont, "(" + Nombre + ")", poss + new Vector2(1, 1), Color.Red, 0, new Vector2(10, 30), 1f, SpriteEffects.None, 0);
           spritebatch.DrawString(spritefont, vidas.ToString(), poss, Color.Black, 0, new Vector2(-15, -50), 1f, SpriteEffects.None, 0);
           spritebatch.DrawString(spritefont, vidas.ToString(), poss+new Vector2(1,1), Color.Red, 0, new Vector2(-15, -50), 1f, SpriteEffects.None, 0);
           spritebatch.Draw(playertexture,posicion,Color.White);
        }
        public bool ImpactoBala(Bala bala) 
        {
            if (FastDistance(posicion + new Vector2(25, 25), bala.poss + new Vector2(3, 3)) - 26 <= 0 && bala.activo)
                return true;
            else return false;
        }
        public void Update(Vector2 playerinput) 
        {

                posicion.X = posicion.X + playerinput.X * rate;
                posicion.Y = posicion.Y + playerinput.Y * rate;
                posicion = Vector2.Clamp(posicion, Vector2.Zero, pantalla);
        }
        public static int FastDistance(Vector2 p1, Vector2 p2)
        {
            int x = (int)System.Math.Abs(p2.X - p1.X);
            int y = (int)System.Math.Abs(p2.Y - p1.Y);

            int min = x < y ? x : y;

            return System.Math.Abs(x + y - (min >> 1) - (min >> 2) + (min >> 4));
        } 
        public void KillPlayer(Jugador player) 
        {
            if (vidas <= 2) { vidas--;  }
            if (vidas == 0)
                {
                    vidas = 2;
                    player.frags++;
                    if (Nombre == "Mava")
                    {
                        posicion.X = 10;
                        posicion.Y = 10;
                    }
                    if (Nombre == "Lito")
                    {
                        posicion.X = 749;
                        posicion.Y = 10;
                    }
                    if (Nombre == "Juba")
                    {
                        posicion.X = 10;
                        posicion.Y = 539;
                    }
                    if (Nombre == "Punchi")
                    {
                        posicion.X = 749;
                        posicion.Y = 539;
                    }
            }
        }
        #endregion

    }
}
