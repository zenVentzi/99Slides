using System;
using System.Collections;
using Assets.Scripts.Managers;
using Assets.Scripts.Plugins;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameOverMenu : MyMono
    {
        private GameObject slides,
                           record,
                           newRecord,
                           feedback,
                           howToPlay,
                           reminder;

        private bool CanShowFeedback
        {
            get { return ScoreManager.Instance.LastGameScore > 40 && GameManager.Instance.TotalGamesPlayed % 3 == 0; }
        }

        private bool CanShowHowToPlay
        {
            get { return !CanShowFeedback && ScoreManager.Instance.GetHighScore() < 6; }
        }

        private bool CanShowReminder
        {
            get { return !CanShowFeedback && !CanShowHowToPlay && !Social.localUser.authenticated; }
        }

        [UsedImplicitly]
        private void Awake()
        {
            Tr.localScale = GameManager.Instance.GetScale();

            slides = GameObjectManager.GetGoInChildren(Go, "Slides");
            record = GameObjectManager.GetGoInChildren(Go, "Record");
            feedback = GameObjectManager.GetGoInChildren(Go, "Feedback");
            newRecord = GameObjectManager.GetGoInChildren(Go, "NewRecord");
            howToPlay = GameObjectManager.GetGoInChildren(Go, "HowToPlay");
            reminder = GameObjectManager.GetGoInChildren(Go, "Reminder");

            if (ScoreManager.Instance.HasNewHighScore)
            {
                Destroy(slides);
                Destroy(record);
                newRecord.GetComponent<Text>().text = ScoreManager.Instance.GetHighScore().ToString();
            }
            else
            {
                Destroy(newRecord);
                slides.GetComponent<Text>().text = ScoreManager.Instance.LastGameScore.ToString();
                record.GetComponent<Text>().text = ScoreManager.Instance.GetHighScore().ToString();
            }

            if(!CanShowHowToPlay)
                Destroy(howToPlay);

            if(!CanShowReminder)
                Destroy(reminder);

            if (!CanShowFeedback)
                Destroy(feedback);            
        }

        [UsedImplicitly]
        private IEnumerator OnAppearAnimFinish()
        {
            Anim.Play("GameOverIdle");

            var canShowAd = GameManager.Instance.TotalGamesPlayed >= 30 &&
                            (GameManager.Instance.TotalGamesPlayed % 10) % 5 == 0 &&
                           !EnergyManager.Instance.Unlimited();

            yield return new WaitForSeconds(0);

            if (canShowAd)
                AdsManager.ShowAd();
        }
    }
}
