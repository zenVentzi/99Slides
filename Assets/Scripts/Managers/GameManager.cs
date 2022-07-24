using System.Collections;
using Assets.Scripts.Buttons;
using Assets.Scripts.Utilities;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MyMono
    {
        public static GameManager Instance;

        [SerializeField, UsedImplicitly] private GameObject internetPrefab;

        public int TotalGamesPlayed
        {
            get { return GamePlayerPrefs.GetInt("PlayedGames"); }
            set
            {
                GamePlayerPrefs.SetInt("PlayedGames", value);
            }
        }

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
            Application.LoadLevelAdditive(Levels.SplashScreen);
            Application.LoadLevelAdditive(Levels.StartMenu);
        }

        private IEnumerator ShowInternetMessage()
        {
            var go = Instantiate(internetPrefab);
            go.transform.localScale = GetScale();

            yield return new WaitForSeconds(3);
            Destroy(go);
        }

        public void ShowInternetMsg()
        {
            StartCoroutine(ShowInternetMessage());
        }

        public Vector2 GetScale()
        {
            var scale = new Vector2(GamePlayerPrefs.GetFloat("ScaleX"), GamePlayerPrefs.GetFloat("ScaleY"));
            return scale;
        }
    }
}
