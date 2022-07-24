using System;
using System.Collections;
using Assets.Scripts.Managers;
using Assets.Scripts.Plugins;
using Assets.Scripts.Utilities;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CooldownTime : MyMono
    {
        private const int TotalCooldownTime = 1800;//1800 = 30mins
        public const string CooldownLaunchTimeKey = "CooldownLaunchTime";

        private Text txt;

        private int TimeElapsedSinceFirstLaunch
        {
            get
            {
                var span = DateTime.Now - GamePlayerPrefs.GetTime(CooldownLaunchTimeKey, DateTime.Now);
                return (int)span.TotalSeconds;
            }
        }

        private int TimeLeft
        {
            get { return TotalCooldownTime - TimeElapsedSinceFirstLaunch; }
        }

        [UsedImplicitly]
        private void Awake()
        {
            txt = GetComponent<Text>();
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            if (EnergyManager.Instance.HasEnergy())
            {
                Go.SetActive(false);
                return;
            }

            if (TimeLeft == TotalCooldownTime)
            {
                StartCoroutine(RefreshCooldownTime());
                GamePlayerPrefs.SetTime(CooldownLaunchTimeKey, DateTime.Now);
            }
            else if (TimeLeft > 0)
            {
                StartCoroutine(RefreshCooldownTime());
            }
            else
            {
                FinishCoooldown();
            }
        }

        private IEnumerator RefreshCooldownTime()
        {
            while (TimeLeft > 0)
            {
                var timeForAd = TimeElapsedSinceFirstLaunch % 90 == 0 &&
                                TimeElapsedSinceFirstLaunch > 120;

                if (timeForAd)
                {
                    AdsManager.ShowAd();
                }

                txt.text = GetFormattedTime();
                yield return new WaitForSeconds(1);
            }

            FinishCoooldown();
        }

        private string GetFormattedTime()
        {
            var mins = TimeLeft / 60;
            var secs = TimeLeft % 60;

            string result;

            if (secs > 9 && mins > 9)
            {
                result = string.Format("{0}:{1}", mins, secs);
            }
            else if (secs > 9)
            {
                result = string.Format("0{0}:{1}", mins, secs);
            }
            else if (mins > 9)
            {
                result = string.Format("{0}:0{1}", mins, secs);
            }
            else
            {
                result = string.Format("0{0}:0{1}", mins, secs);
            }

            return result;
        }

        private void FinishCoooldown()
        {
            EnergyManager.Instance.RefreshEnergy();
            GetComponentInParent<PlaySlide>().Activate();
            GamePlayerPrefs.DeleteKey(CooldownLaunchTimeKey);
        }
    }
}
