using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class LeaderboardsButton : AnimatedButton
    {
        protected override void OnClick()
        {
            base.OnClick();

            if (Social.localUser.authenticated)
            {
                LeaderboardsManager.Instance.UpdateLeaderboards();
                Social.ShowLeaderboardUI();
            }
            else
            {
                GameManager.Instance.ShowInternetMsg();

                Social.localUser.Authenticate(success =>
                {
                    if (success)
                    {
                        LeaderboardsManager.Instance.UpdateLeaderboards();
                        Social.ShowLeaderboardUI();
                    }
                    else
                    {
                        GameManager.Instance.ShowInternetMsg();                        
                    }
                });
            }
        }
    }
}
