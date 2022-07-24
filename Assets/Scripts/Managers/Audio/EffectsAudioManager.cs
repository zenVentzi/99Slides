using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Managers.Audio
{
    public class EffectsAudioManager : MyMono
    {
        public static EffectsAudioManager Instance;
        [SerializeField, UsedImplicitly] private AudioClip click;
        [SerializeField, UsedImplicitly] private AudioClip correct;
        [SerializeField, UsedImplicitly] private AudioClip gameOver;
                          

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
        }

        public void PlayCorrect()
        {
            Audioo.clip = correct;
            Audioo.Play();
        }

        public void PlayGameOver()
        {
            Audioo.clip = gameOver;
            Audioo.Play();
        }

        public void PlayClick()
        {
            Audioo.clip = click;
            Audioo.Play();
        }
    }
}
