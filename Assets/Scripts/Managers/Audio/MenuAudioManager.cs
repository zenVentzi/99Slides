using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Managers.Audio
{
    public class MenuAudioManager : MyMono
    {
        public static MenuAudioManager Instance;

        [SerializeField, UsedImplicitly]
        private AudioClip clip;

        private int turnOffTime;

        private Coroutine turnOffCoroutine;

        private bool SlowlyIncreaseVolume
        {
            get { return turnOffTime >= 3; }
        }

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
            Play(2);
        }

        private IEnumerator TurnOff()
        {
            while (Audioo.volume > 0)
            {
                turnOffTime = Audioo.timeSamples / clip.frequency;
                Audioo.volume -= .02f;
                yield return null;
            }

            Audioo.Stop();
        }

        private IEnumerator TurnOn(float delay)
        {
            if (delay > 0)
                yield return new WaitForSeconds(delay);

            Audioo.timeSamples = turnOffTime * clip.frequency;
            Audioo.volume = 0;
            Audioo.Play();

            if (SlowlyIncreaseVolume)
            {
                while (Audioo.volume < 1)
                {
                    Audioo.volume += .005f;
                    yield return null;
                }
            }
            else
            {
                Audioo.volume = 1;
            }
        }

        public void Play(float delay = 0)
        {
            if (turnOffCoroutine != null)
            {
                StopCoroutine(turnOffCoroutine);
            }

            //turnOnCoroutine = StartCoroutine(TurnOn(delay));
            StartCoroutine(TurnOn(delay));
        }

        public void Stop()
        {
            if (Audioo.isPlaying)
                turnOffCoroutine = StartCoroutine(TurnOff());
        }
    }
}
