using Assets.Scripts.Utilities;
using GooglePlayGames;
using JetBrains.Annotations;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MyMono
    {
        public static ScoreManager Instance;
        public int LastGameScore { get; private set; }

        public bool HasNewHighScore { get; private set; }

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
        }

        public int GetHighScore()
        {
            return GamePlayerPrefs.GetInt("HighScore");
        }

        public void Submit(int score)
        {
            LastGameScore = score;

            if (score > GetHighScore())
            {
                HasNewHighScore = true;
                GamePlayerPrefs.SetInt("HighScore", score);
            }
            else
            {
                HasNewHighScore = false;
            }

            PlayGamesPlatform.Instance.ReportScore(GetHighScore(), "CgkItKGrmboQEAIQSQ", success =>
            {

            });
        }
    }
}
