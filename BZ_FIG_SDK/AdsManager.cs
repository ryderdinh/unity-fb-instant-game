using System;
using System.Collections;
using UniRx;
using UnityEngine;
using Random = System.Random;

namespace Manager
{
    public class AdsManager : Singleton<AdsManager>
    {
        [SerializeField] private string INTERSTITIAL_AD_ID;
        [SerializeField] private string REWARDED_AD_ID;
        [SerializeField] private string BANNER_AD_ID;

        [HideInInspector] public bool isPreloadInterAds;

        private void Start()
        {
            ShowBannerAd(true);
        }

        #region Banner ADS

        public void ShowBannerAd(bool isShow)
        {
            Ryder.Instance.CheckUnityEditor(() =>
            {
                if (isShow)
                {
                    WrapManager.loadBannerAds(BANNER_AD_ID);
                    return;
                }

                WrapManager.hideBannerAds(); 
            });
        }

        #endregion

        #region Interstitial ADS

        public void RandomRunAdsInterstitial()
        {
            var random = new Random();

            var randomNumber = random.Next(1, 5);

            switch (randomNumber)
            {
                case 3:
                    PreloadAdsInterstitial();
                    break;
            }
        }

        public void PreloadAdsInterstitial()
        {
            isPreloadInterAds = true;
            Ryder.Instance.CheckUnityEditor(() => WrapManager.preloadInterstitialAd(INTERSTITIAL_AD_ID), () => { });
        }

        public void ShowAdsInterstitial(Action callback = null)
        {
            WrapManager.Instance.SetInterstitialAdAction(callback);

            if (!isPreloadInterAds)
            {
                WrapManager.Instance.onInterstitialAdSuccess();
                return;
            }

            Ryder.Instance.CheckUnityEditor(
                () =>
                {
                    Instance.isPreloadInterAds = false;
                    WrapManager.showInterstitialAd(INTERSTITIAL_AD_ID);
                },
                () =>
                {
                    Instance.isPreloadInterAds = false;
                    WrapManager.Instance.onInterstitialAdSuccess();
                }
            );
        }

        #endregion

        #region RewardedVideo ADS

        public void PreloadAdsRewarded()
        {
            Ryder.Instance.CheckUnityEditor(() => WrapManager.preloadRewardedAd(REWARDED_AD_ID), () => { });
        }

        public void ShowAdsRewarded(Action onSuccess = null, Action<string> onFailed = null)
        {
            WrapManager.Instance.SetRewardedAdAction(
                () =>
                {
                    onSuccess?.Invoke();
                    PreloadAdsRewarded();
                },
                err =>
                {
                    onFailed?.Invoke(err);
                    PreloadAdsRewarded();
                });

            Ryder.Instance.CheckUnityEditor(
                LoadRewardedAd,
                () => { WrapManager.Instance.onRewardedAdSuccess(); }
            );
        }

        private void LoadRewardedAd()
        {
            WrapManager.showRewardedAd(REWARDED_AD_ID);
        }

        public void CoroutineStartRewardedAds(
            Action onClickCloseLoadAdsPopup = null,
            Action onSuccess = null,
            Action onFailed = null
        )
        {
            MessageBroker.Default.Publish(new ShowRewardedAds { IsShow = true });
            UIManager.Instance.ShowLoadAdsPopup(() =>
            {
                MessageBroker.Default.Publish(new ShowRewardedAds { IsShow = false });
                onClickCloseLoadAdsPopup?.Invoke();
            });
            StartCoroutine(DelayShowAds(
                () =>
                {
                    onSuccess?.Invoke();
                    UIManager.Instance.HideLoadAds();
                    MessageBroker.Default.Publish(new ShowRewardedAds { IsShow = false });
                },
                err =>
                {
                    switch (err)
                    {
                        case "USER_INPUT":
                            onFailed?.Invoke();
                            MessageBroker.Default.Publish(new ShowRewardedAds { IsShow = false });
                            UIManager.Instance.HideLoadAds();
                            break;
                        default:
                            UIManager.Instance.UpdateLoadAds(1, () =>
                            {
                                onSuccess?.Invoke();
                                MessageBroker.Default.Publish(new ShowRewardedAds { IsShow = false });
                            });
                            break;
                    }
                }));
        }

        private IEnumerator DelayShowAds(Action onSuccess, Action<string> onFailed)
        {
            yield return new WaitForSeconds(0.4f);
            ShowAdsRewarded(onSuccess, onFailed);
        }

        #endregion
    }
}