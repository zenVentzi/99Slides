using System.Collections;
using Assets.Scripts.Managers;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Plugins
{
    public class GPlayManager : MyMono
    {
        [UsedImplicitly]
        void Awake()
        {
            var config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }

        [UsedImplicitly]
        private IEnumerator Start()
        {
            //yield return new WaitForSeconds(1);
            yield return null;

            Social.localUser.Authenticate(success =>
            {
                if (!success)
                    GameManager.Instance.ShowInternetMsg();
            });
        }
    }
}
