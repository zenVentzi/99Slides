using Assets.Scripts.Utilities;
using GooglePlayGames;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AchievementsManager : MyMono
    {
        private readonly string[] slides = new string[100];
        private readonly string[,] slidesInTime = new string[100, 58];

        public static AchievementsManager Instance;

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
            slides[1] = "CgkItKGrmboQEAIQAQ";
            slides[40] = "CgkItKGrmboQEAIQAg";
            slides[50] = "CgkItKGrmboQEAIQAw";
            slides[60] = "CgkItKGrmboQEAIQBA";
            slides[70] = "CgkItKGrmboQEAIQBQ";
            slides[80] = "CgkItKGrmboQEAIQBw";
            slides[90] = "CgkItKGrmboQEAIQCA";
            slides[99] = "CgkItKGrmboQEAIQCQ";

            slidesInTime[10, 8] = "CgkItKGrmboQEAIQCg";
            slidesInTime[10, 6] = "CgkItKGrmboQEAIQDg";
            slidesInTime[10, 5] = "CgkItKGrmboQEAIQCw";
            slidesInTime[10, 4] = "CgkItKGrmboQEAIQDA";
            slidesInTime[15, 11] = "CgkItKGrmboQEAIQDw";
            slidesInTime[15, 9] = "CgkItKGrmboQEAIQEA";
            slidesInTime[15, 8] = "CgkItKGrmboQEAIQEQ";
            slidesInTime[15, 7] = "CgkItKGrmboQEAIQEg";
            slidesInTime[20, 12] = "CgkItKGrmboQEAIQEw";
            slidesInTime[20, 10] = "CgkItKGrmboQEAIQFA";
            slidesInTime[20, 9] = "CgkItKGrmboQEAIQFQ";
            slidesInTime[20, 8] = "CgkItKGrmboQEAIQFg";
            slidesInTime[30, 19] = "CgkItKGrmboQEAIQFw";
            slidesInTime[30, 17] = "CgkItKGrmboQEAIQGQ";
            slidesInTime[30, 16] = "CgkItKGrmboQEAIQGA";
            slidesInTime[30, 15] = "CgkItKGrmboQEAIQGg";
            slidesInTime[40, 25] = "CgkItKGrmboQEAIQDQ";
            slidesInTime[40, 23] = "CgkItKGrmboQEAIQGw";
            slidesInTime[40, 22] = "CgkItKGrmboQEAIQHA";
            slidesInTime[40, 21] = "CgkItKGrmboQEAIQHQ";
            slidesInTime[50, 30] = "CgkItKGrmboQEAIQHg";
            slidesInTime[50, 28] = "CgkItKGrmboQEAIQHw";
            slidesInTime[50, 27] = "CgkItKGrmboQEAIQIA";
            slidesInTime[50, 26] = "CgkItKGrmboQEAIQIQ";
            slidesInTime[60, 36] = "CgkItKGrmboQEAIQIg";
            slidesInTime[60, 34] = "CgkItKGrmboQEAIQIw";
            slidesInTime[60, 33] = "CgkItKGrmboQEAIQJA";
            slidesInTime[60, 32] = "CgkItKGrmboQEAIQJQ";
            slidesInTime[70, 41] = "CgkItKGrmboQEAIQJg";
            slidesInTime[70, 39] = "CgkItKGrmboQEAIQJw";
            slidesInTime[70, 38] = "CgkItKGrmboQEAIQKA";
            slidesInTime[70, 37] = "CgkItKGrmboQEAIQKQ";
            slidesInTime[80, 47] = "CgkItKGrmboQEAIQKg";
            slidesInTime[80, 45] = "CgkItKGrmboQEAIQKw";
            slidesInTime[80, 44] = "CgkItKGrmboQEAIQLA";
            slidesInTime[80, 43] = "CgkItKGrmboQEAIQLQ";
            slidesInTime[90, 52] = "CgkItKGrmboQEAIQLg";
            slidesInTime[90, 50] = "CgkItKGrmboQEAIQLw";
            slidesInTime[90, 49] = "CgkItKGrmboQEAIQMA";
            slidesInTime[90, 48] = "CgkItKGrmboQEAIQMQ";
            slidesInTime[99, 57] = "CgkItKGrmboQEAIQMg";
            slidesInTime[99, 55] = "CgkItKGrmboQEAIQMw";
            slidesInTime[99, 54] = "CgkItKGrmboQEAIQNA";
            slidesInTime[99, 53] = "CgkItKGrmboQEAIQNQ";
        }

        private void SaveLocally(string id)
        {
            GamePlayerPrefs.SetBool(id, true);
        }

        private void UpdateBestSlides()
        {
            for (int i = 0; i < 100; i++)
            {
                var id = slides[i];

                if (id == null) continue;
                var availableLocally = GamePlayerPrefs.GetBool(id);

                Debug.Log("Fetching...  " + id + " " + availableLocally);

                if(availableLocally)
                    PlayGamesPlatform.Instance.ReportProgress(id, 100, success => { });
            }
        }

        private void UpdateSlidesInTime()
        {
            for (int numOfSlides = 10; numOfSlides < 100; numOfSlides++)
            {
                for (int time = 4; time < 58; time++)
                {
                    var id = slidesInTime[numOfSlides, time];

                    if (id != null)
                    {
                        var availableLocally = GamePlayerPrefs.GetBool(id);

                        Debug.Log("Fetching...  " + id + " " + availableLocally);

                        if(availableLocally)
                            PlayGamesPlatform.Instance.ReportProgress(id, 100, success => { });
                    }
                }
            }
        }

        public void UpdateAchievements()
        {
            UpdateBestSlides();
            UpdateSlidesInTime();
        }

        public void Submit(int slideCount, double timeElapsed)
        {
            SubmitBestSlides(slideCount);
            SubmitSlidesInTimeId(slideCount, timeElapsed);
        }

        private void SubmitSlidesInTimeId(int slideCount, double timeElapsed)
        {
            for (int i = (int)Mathf.Ceil((float)timeElapsed); i < slidesInTime.GetLength(1); i++)
            {
                var achievementId = slidesInTime[slideCount, i];

                if (achievementId != null)
                {
                    SaveLocally(achievementId);
                    PlayGamesPlatform.Instance.ReportProgress(achievementId, 100, success =>
                    {
                    });
                }
            }
        }

        private void SubmitBestSlides(int slideCount)
        {
            var achievementId = slides[slideCount];

            if (achievementId != null)
            {
                SaveLocally(achievementId);
                PlayGamesPlatform.Instance.ReportProgress(achievementId, 100, success => { });
            }
        }

        //public void Submit(int slideCount, double timeElapsed)
        //{
        //    for (int i = slideCount; i > 0; i--)
        //    {
        //        var achievementId = slides[slideCount];

        //        if (achievementId != null)
        //        {
        //            PlayGamesPlatform.Instance.ReportProgress(achievementId, 100, success =>
        //            {
        //            });
        //        }

        //        SubmitSlidesInTimeId(slideCount, timeElapsed);
        //    }
        //}
    }
}
