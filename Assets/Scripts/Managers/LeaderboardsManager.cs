using Assets.Scripts.Utilities;
using GooglePlayGames;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class LeaderboardsManager : MyMono
    {
        private readonly string[] avgTimeForSlides = new string[100];
        public static LeaderboardsManager Instance;

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;

            avgTimeForSlides[10] = "CgkItKGrmboQEAIQSg";
            avgTimeForSlides[15] = "CgkItKGrmboQEAIQSw";
            avgTimeForSlides[20] = "CgkItKGrmboQEAIQTA";
            avgTimeForSlides[25] = "CgkItKGrmboQEAIQTQ";
            avgTimeForSlides[30] = "CgkItKGrmboQEAIQTg";
            avgTimeForSlides[35] = "CgkItKGrmboQEAIQTw";
            avgTimeForSlides[40] = "CgkItKGrmboQEAIQUA";
            avgTimeForSlides[45] = "CgkItKGrmboQEAIQUQ";
            avgTimeForSlides[50] = "CgkItKGrmboQEAIQUg";
            avgTimeForSlides[55] = "CgkItKGrmboQEAIQUw";
            avgTimeForSlides[60] = "CgkItKGrmboQEAIQVA";
            avgTimeForSlides[65] = "CgkItKGrmboQEAIQVQ";
            avgTimeForSlides[70] = "CgkItKGrmboQEAIQVg";
            avgTimeForSlides[75] = "CgkItKGrmboQEAIQVw";
            avgTimeForSlides[80] = "CgkItKGrmboQEAIQWA";
            avgTimeForSlides[85] = "CgkItKGrmboQEAIQWQ";
            avgTimeForSlides[90] = "CgkItKGrmboQEAIQWg";
            avgTimeForSlides[99] = "CgkItKGrmboQEAIQWw";
        }

        private int GetBestReactionTime(string id, int slides, double elapsedTime)
        {
            var avgTime = (int)(elapsedTime / slides);
            var avgSoFar = GamePlayerPrefs.GetInt(id, int.MaxValue);

            if (avgTime < avgSoFar)
            {
                GamePlayerPrefs.SetInt(id, avgTime);
                return avgTime;
            }

            return avgSoFar;
        }

        public void UpdateLeaderboards()
        {
            PlayGamesPlatform.Instance.ReportScore(ScoreManager.Instance.GetHighScore(), "CgkItKGrmboQEAIQSQ", success =>
            {
            });

            UpdateReactionTimeLeaderboards();
        }

        private void UpdateReactionTimeLeaderboards()
        {
            for (int i = 10; i < 100; i++)
            {
                var id = avgTimeForSlides[i];
                if (id == null) continue;

                var avgTime = GamePlayerPrefs.GetInt(id, int.MaxValue);

                if (avgTime > 250 && avgTime < 2000)
                    PlayGamesPlatform.Instance.ReportScore(avgTime, id, success => { });
            }
        }

        public void SubmitReactionTime(int slides, double elapsedTime)
        {
            var id = avgTimeForSlides[slides];
            if (id == null) return;

            var avgTime = GetBestReactionTime(id, slides, elapsedTime);

            PlayGamesPlatform.Instance.ReportScore(avgTime, id, success =>
            {
            });
        }
    }
}
