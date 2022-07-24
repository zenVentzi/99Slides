using System.Collections;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Audio;
using Assets.Scripts.Plugins;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class BackButton : AnimatedButton
    {
        [SerializeField, UsedImplicitly]
        private bool menuBackButton;

        private void GoBack()
        {
            base.OnClick();

            if (!menuBackButton)
            {
                GameAudioManager.Instance.Stop();
                MenuAudioManager.Instance.Play();
            }


            Application.LoadLevelAdditive(Levels.StartMenu);
            Destroy(Tr.root.gameObject);
        }

        [UsedImplicitly]
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                GoBack();
        }

        private IEnumerator ShowAd()
        {
            var canShowAd = !menuBackButton && ((GameManager.Instance.TotalGamesPlayed % 10) % 2 == 0);

            if (!canShowAd) yield break;

            yield return new WaitForSeconds(2.1f);
            AdsManager.ShowAd();
        }

        protected override void OnClick()
        {
            if (InputManager.Instance.HasSlided) return;

            base.OnClick();
            GoBack();
            StartCoroutine(ShowAd());
        }
    }
}
