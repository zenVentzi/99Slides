using System;
using Assets.Scripts.Buttons;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.Audio;
using ChartboostSDK;
using GoogleMobileAds.Api;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Plugins
{
    public class AdsManager : MyMono
    {
        private static InterstitialAd adMobInterstitial;
        private static bool audioOnBeforeVideo;

        private static bool HasInterstitial
        {
            get { return adMobInterstitial.IsLoaded() || Chartboost.hasInterstitial(CBLocation.Default); }
        }

        private static bool HasVideo
        {
            get { return Chartboost.hasRewardedVideo(CBLocation.Default); }
        }

        [UsedImplicitly]
        private void Start()
        {
            SetChartboostEvents();
            CacheGoogleInterstitial();
            Chartboost.cacheInterstitial(CBLocation.Default);
            Chartboost.cacheRewardedVideo(CBLocation.Default);
        }

        private static void CacheGoogleInterstitial()
        {

#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3700426764385022/6957916999";
#elif UNITY_IPHONE
            string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

            if (adMobInterstitial != null) adMobInterstitial.Destroy();

            //Debug.Log("INITIALIZING ADMOB INTERSTITIAL Enzi 1.0");
            adMobInterstitial = new InterstitialAd(adUnitId);
            SetAdmobEvents();
            adMobInterstitial.LoadAd(CreateGoogleAdRequest());
        }

        private static AdRequest CreateGoogleAdRequest()
        {
            return new AdRequest.Builder()
                    .AddTestDevice(AdRequest.TestDeviceSimulator)
                    .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
                    .AddKeyword("game")
                    .SetGender(Gender.Male)
                    .SetBirthday(new DateTime(1985, 1, 1))
                    .TagForChildDirectedTreatment(false)
                    .AddExtra("color_bg", "9B30FF")
                    .Build();
        }

        private static void SetAdmobEvents()
        {
            adMobInterstitial.AdFailedToLoad += (sender, args) =>
            {
                //Debug.Log("ADMOB AD FAILED TO LAOD Enzi 1.0");
                CacheGoogleInterstitial();
                AbstractButton.ActivateButtons();
            };

            adMobInterstitial.AdClosed += (sender, args) =>
            {
                //Debug.Log("ADMOB AD CLOSED Enzi 1.0");
                AbstractButton.ActivateButtons();
                CacheGoogleInterstitial();
            };
        }

        private static void SetChartboostEvents()
        {
            Chartboost.didFailToLoadInterstitial += (location, error) =>
            {
                //Debug.Log("CHARTBOOST FAILED TO LOAD INTERSTITIAL Enzi 1.0");
                Chartboost.cacheInterstitial(location);
                AbstractButton.ActivateButtons();
            };

            Chartboost.didDismissInterstitial += location =>
            {
                //Debug.Log("CHARTBOOST DISMISSED INTERSTITIAL Enzi 1.0");
                Chartboost.cacheInterstitial(CBLocation.Default);
                AbstractButton.ActivateButtons();
            };

            Chartboost.didFailToLoadRewardedVideo += (location, error) =>
            {
                Debug.Log("CHARTBOOST FailedToLoadRewardedVideo Enzi 1.0" + "Error -> " + error);
                Chartboost.cacheRewardedVideo(location);
                AbstractButton.ActivateButtons();
            };

            Chartboost.didCloseRewardedVideo += location =>
            {
                //Debug.Log("CHARTBOOST didCloseRewardedVideo Enzi 1.0");
                Chartboost.cacheRewardedVideo(location);
                AbstractButton.ActivateButtons();

                if(audioOnBeforeVideo)
                    AudioManager.Instance.Unmute();
            };

            Chartboost.didCompleteRewardedVideo += (location, reward) =>
            {
                //Debug.Log("CHARTBOOST COMPLETED REWARD VIDEO Enzi 1.0");
                Chartboost.cacheRewardedVideo(CBLocation.Default);
                var playSlide = FindObjectOfType<PlaySlide>();
                EnergyManager.Instance.RewardFromVideo();
                AbstractButton.ActivateButtons();

                if (playSlide != null)
                    playSlide.Activate();                    
            };
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            adMobInterstitial.Destroy();
        }

        private static void ShowInterstitial()
        {
#if UNITY_EDITOR_64
            return;
#endif

            if (Chartboost.hasInterstitial(CBLocation.Default))
            {
                Chartboost.showInterstitial(CBLocation.Default);
            }
            else
            {
                adMobInterstitial.Show();
            }

            AbstractButton.DeactivateButtons();
        }

        private static void ShowVideo()
        {
            audioOnBeforeVideo = AudioManager.Instance.SoundOn;
            Chartboost.showRewardedVideo(CBLocation.Default);
            AbstractButton.DeactivateButtons();
            AudioManager.Instance.Mute();
        }

        public static void ShowAd()
        {
            if (HasVideo)
            {
                ShowVideo();
            }
            else if (HasInterstitial)
            {
                ShowInterstitial();
            }
        }
    }
}
