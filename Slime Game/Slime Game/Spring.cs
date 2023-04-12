using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime_Game
{
    /// <summary>
    /// Spring Class
    /// Gives player a bounce on collision
    /// </summary>
    internal class Spring : GameObject
    {
        // ===== Fields =====
        public Spring(Rectangle rect) : base(null, new Rectangle())
        {
            this.position = rect;
            this.texture = Art.Instance.LoadTexture2D("debug_solid");
        }
    }
}
