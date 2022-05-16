using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SpaceSoldier2
{
    public static class MySounds
    {
        public static SoundEffect projectileShot;
        public static SoundEffect projectileHit;
        public static SoundEffect alienKilled;
        public static SoundEffect healing;
        public static SoundEffect bombShot;
        public static SoundEffect explosion;
        public static Song walking;
        public static Song scifiMusic;

        public static float musicVolume = 1f;
        public static float soundVolume = 1f;       
    }
}
