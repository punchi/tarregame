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
    class Pantalla
    {
        private string[] elementos;
        private int separacion = 30;

        public Pantalla(int elements)
        {
            elementos = new string[elements];
            /*switch (pantalla)
            {
                case "menu_principal":
                    //
                    break;
                case "juego_single":
                    //
                    break;
                case "juego_servidor":
                    //
                    break;
                case "juego_cliente":
                    //
                    break;
            }*/
        }

        public void anadir(int posicion, string texto)
        {
            elementos[posicion] = texto;
        }

        public void Draw(SpriteBatch spritebatch, SpriteFont fuente)
        {
            int i=0;
            
            for (i=0; i < elementos.Length; i++){
                spritebatch.DrawString(fuente, elementos[i].ToString(), new Vector2(150, 180 + (separacion * i)), Color.Black);
            }
            
        }
 
    }
}
