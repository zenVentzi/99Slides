using System;
using System.Collections;
using System.Reflection;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Audio;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Game : MyMono
    {
        #region variables
        private const float AdditionalTime = .25f;

        [SerializeField, UsedImplicitly]
        private Sprite greenBackground;
        [SerializeField, UsedImplicitly]
        private Sprite redBackground;
        [SerializeField, UsedImplicitly]
        private Sprite greenArrow;
        [SerializeField, UsedImplicitly]
        private Sprite blueArrow;

        private int redInARow,
                    greenInARow,
                    lastRotationChoice;

        private GameObject time,
                           score,
                           arrow,
                           message;

        private bool gameOver;

        private float timeLeft;
        private DateTime launchTime,
                         lastCorrectTime;
        private int slides;

        private float TimeLeft
        {
            get { return timeLeft; }
            set
            {
                timeLeft = value;
                time.GetComponent<Text>().text = GetFormattedTime();
            }
        }
        private int Slides
        {
            get { return slides; }
            set
            {
                slides = value;
                score.GetComponent<Text>().text = slides.ToString();
            }
        }

        #endregion

        #region methods

        [UsedImplicitly]
        private void Awake()
        {
            timeLeft = 40;
            lastRotationChoice = -1;
            launchTime = DateTime.Now;
            Tr.localScale = GameManager.Instance.GetScale();
            time = GameObjectManager.GetGoInChildren(Go, "Time");
            score = GameObjectManager.GetGoInChildren(Go, "Slides");
            arrow = GameObjectManager.GetGoInChildren(Go, "Arrow");

            StartCoroutine(HurryUp());
            StartCoroutine(RefreshTime());
            InputManager.Instance.OnPlayerSlideEvent += OnPlayerSlided;
            GenerateNew();
        }

        private void OnPlayerSlided(Directions direction)
        {
            if (IsCorrect(direction))
            {
                ValidateCorrect();
            }
            else if(!gameOver)
            {
                LoadGameOver();
            }
        }

        private IEnumerator ShowCorrectIndicator()
        {
            var scoreArrow = GameObjectManager.GetGoInChildren(Go, "ScoreArrow");

            scoreArrow.GetComponent<SpriteRenderer>().sprite = greenArrow;
            yield return new WaitForSeconds(.25f);
            scoreArrow.GetComponent<SpriteRenderer>().sprite = blueArrow;
        }

        private void ValidateCorrect()
        {
            Slides++;
            GenerateNew();
            message.SetActive(false);
            arrow.GetComponent<Animation>().Stop();
            arrow.transform.position = new Vector2(0, -90);
            TimeLeft += AdditionalTime;
            lastCorrectTime = DateTime.Now;
            StartCoroutine(ShowCorrectIndicator());
            EffectsAudioManager.Instance.PlayCorrect();
            var timeElapsed = DateTime.Now - launchTime;
            AchievementsManager.Instance.Submit(slides, timeElapsed.TotalSeconds);
            LeaderboardsManager.Instance.SubmitReactionTime(Slides, timeElapsed.TotalMilliseconds);
        }

        private void LoadGameOver()
        {
            gameOver = true;
            GameAudioManager.Instance.Stop();
            MenuAudioManager.Instance.Play();
            EffectsAudioManager.Instance.PlayGameOver();
            Application.LoadLevelAdditive("GameOverScene");
            Destroy(Go);
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            gameOver = true;
            InputManager.Instance.OnPlayerSlideEvent -= OnPlayerSlided;
            ScoreManager.Instance.Submit(slides);
        }

        private IEnumerator HurryUp()
        {
            message = GameObjectManager.GetGoInChildren(Go, "Message");
            message.SetActive(false);

            while (!gameOver)
            {
                yield return new WaitForSeconds(3);

                var span = DateTime.Now - lastCorrectTime;

                if (span.TotalSeconds > 3)
                {
                    Handheld.Vibrate();
                    message.SetActive(true);
                    arrow.GetComponent<Animation>().Play();
                }
            }
        }

        private IEnumerator RefreshTime()
        {
            while (!gameOver)
            {
                timeLeft--;

                if (timeLeft <= 0)
                {
                    LoadGameOver();
                    yield break;
                }

                time.GetComponent<Text>().text = GetFormattedTime();
                yield return new WaitForSeconds(1);
            }
        }

        private string GetFormattedTime()
        {
            var mins = (int)timeLeft / 60;
            var secs = (int)timeLeft % 60;

            return string.Format(secs > 9 ? "{0}:{1}" : "{0}:0{1}", mins, secs);
        }

        private Directions GetCorrectDirection()
        {
            if (SpriteRend.sprite == greenBackground)
            {
                switch ((int)arrow.transform.eulerAngles.z)
                {
                    case 0:
                        return Directions.Right;
                    case 90:
                        return Directions.Up;
                    case 180:
                        return Directions.Left;
                    case 270:
                        return Directions.Down;
                }
            }
            else
            {
                switch ((int)arrow.transform.eulerAngles.z)
                {
                    case 0:
                        return Directions.Left;
                    case 90:
                        return Directions.Down;
                    case 180:
                        return Directions.Right;
                    case 270:
                        return Directions.Up;
                }
            }

            return Directions.None;
        }

        private void GenerateNewBackground()
        {
            var choice = GetBackgroundChoice();
            SpriteRend.sprite = (choice == 1 ? greenBackground : redBackground);
        }

        private int GetBackgroundChoice()
        {
            int choice;
            var maxInARow = Random.Range(2, 4);

            do
            {
                choice = Random.Range(0, 2);

                if (choice == 1)
                {
                    redInARow = 0;
                    greenInARow++;
                }
                else
                {
                    greenInARow = 0;
                    redInARow++;
                }
            } while (greenInARow > maxInARow || redInARow > maxInARow);

            return choice;
        }

        //public static void ClearConsole()
        //{
        //    // This simply does "LogEntries.Clear()" the long way:
        //    Type logEntries = Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        //    MethodInfo clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        //    clearMethod.Invoke(null, null);
        //}

        private bool IsCorrect(Directions dir)
        {
            return dir == GetCorrectDirection();
        }

        private void GenerateNew()
        {
            GenerateNewBackground();
            RotateArrow();
        }

        private void RotateArrow()
        {
            var rotationChoice = Random.Range(0, 4);
            var sameBackground = (greenInARow > 1 || redInARow > 1);

            while (sameBackground && rotationChoice == lastRotationChoice)
            {
                rotationChoice = Random.Range(0, 4);
            }

            lastRotationChoice = rotationChoice;

            switch (rotationChoice)
            {
                case 0:
                    arrow.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case 1:
                    arrow.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case 2:
                    arrow.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 3:
                    arrow.transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
            }
        }

        #endregion
    }
}
