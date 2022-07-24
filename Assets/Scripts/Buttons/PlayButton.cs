using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Audio;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class PlayButton : AnimatedButton
    {
        [SerializeField] private bool menuPlayButton;
        private GameObject padlock;

        [UsedImplicitly]
        private void Awake()
        {
            padlock = Tr.GetChild(0).gameObject;

            if (EnergyManager.Instance.HasEnergy())
            {
                Unlock();
            }
            else
            {
                Lock();
            }
        }

        public override void Unlock()
        {
            base.Unlock();
            padlock.SetActive(false);
        }

        public override void Lock()
        {
            base.Lock();
            padlock.SetActive(true);
        }

        protected override void OnClick()
        {
            if (InputManager.Instance.HasSlided) return;

            base.OnClick();

            if (menuPlayButton)
            {
                MenuAudioManager.Instance.Stop();
                GameAudioManager.Instance.Play();
            }

            EnergyManager.Instance.ReduceEnergy();
            GameManager.Instance.TotalGamesPlayed++;
            Application.LoadLevelAdditive(Levels.Game);
            Destroy(Tr.root.gameObject);
        }
    }
}
