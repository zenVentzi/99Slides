using Assets.Scripts.Utilities;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Managers.Audio
{
    public class AudioManager : MyMono
    {
        public static AudioManager Instance;

        private int Volume
        {
            get { return GamePlayerPrefs.GetInt("Sound", 1); }
            set
            {
                GamePlayerPrefs.SetInt("Sound", value);
            }
        }

        public bool SoundOn
        {
            get { return Volume == 1; }
        }

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
            AudioListener.volume = Volume;
        }

        public void Mute()
        {
            AudioListener.volume = Volume = 0;
        }

        public void Unmute()
        {
            AudioListener.volume = Volume = 1;
        }
    }
}