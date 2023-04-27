using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Slime_Game
{
    /// <summary>
    /// Will, Josie
    /// Art Singleton
    /// Simplifies loading assets, allowing you to get Texture2Ds and Spritefonts from any class!
    /// </summary>
    public sealed class Art
    {
        // Fields
        private static Art instance = null;
        private ContentManager content;


        /// <summary>
        /// Create and sets the only instance of Art
        /// </summary>
        public static Art Instance
        {
            get
            {
                // Does it exist yet? No? Make it!
                if (instance == null)
                {
                    // Call the default constructor.
                    instance = new Art();
                }

                // Either way, return the (newly made or already made) instance
                return instance;
            }

            // NEVER a set for the instance
        }

        /// <summary>
        /// Sets Art's content loader. Needed to load any assets.
        /// </summary>
        /// <param name="content">Content loader to use (just set it as Content).</param>
        public void SetContentLoader(ContentManager content)
        {
            this.content = content;
        }

        /// <summary>
        /// Loads a Texture2D from a path.
        /// </summary>
        /// <param name="path">Path to load from (i.e. "ice.png")</param>
        /// <returns>Texture2D of the sprite.</returns>
        public Texture2D LoadTexture2D(string path)
        {
            return content.Load<Texture2D>(path);
        }

        /// <summary>
        /// Loads a Spritefont from a path.
        /// </summary>
        /// <param name="path">Path to load from (i.e. "comicSans36.spritefont")</param>
        /// <returns>Spritefont from the path.</returns>
        public SpriteFont LoadSpritefont(string path)
        {
            return content.Load<SpriteFont>(path);
        }

        /// <summary>
        /// Loads a SoundEffect from a path.
        /// </summary>
        /// <param name="path">Path to load from.</param>
        /// <returns>SoundEffect from path.</returns>
        public SoundEffect LoadSoundEffect(string path) 
        {
            return content.Load<SoundEffect>(path);
        }
    }
}
