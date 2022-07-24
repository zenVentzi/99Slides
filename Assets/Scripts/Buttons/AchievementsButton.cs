using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class AchievementsButton : AnimatedButton
    {
        protected override void OnClick()
        {
            base.OnClick();

            if (Social.localUser.authenticated)
            {
                AchievementsManager.Instance.UpdateAchievements();
                Social.ShowAchievementsUI();
            }
            else
            {
                GameManager.Instance.ShowInternetMsg();

                Social.localUser.Authenticate(success =>
                {
                    if (success)
                    {
                        AchievementsManager.Instance.UpdateAchievements();
                        Social.ShowAchievementsUI();
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
