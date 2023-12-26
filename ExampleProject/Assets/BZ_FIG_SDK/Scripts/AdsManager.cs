using System;
using System.Collections;
using Manager;
using UnityEngine;
using Random = System.Random;

namespace BZ_FIG_SDK.Scripts
{
    public class AdsManager : Singleton<AdsManager>
    {
        [SerializeField] private string INTERSTITIAL_AD_ID;
        [SerializeField] private string REWARDED_AD_ID;
        [SerializeField] private string BANNER_AD_ID;

        [HideInInspector] public bool isPreloadInterAds;

        private void Start()
        {
            // Do nothing...
        }

        #region Banner ADS

        public void ShowBannerAd(bool isShow)
        {
            if (string.IsNullOrEmpty(BANNER_AD_ID))
            {
                Debug.Log("BANNER_AD_ID empty");
                return;
            }

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

        public void RandomRunAdsInterstitial(int randomTo = 2)
        {
            var random = new Random();

            var randomNumber = random.Next(1, randomTo);

            switch (randomNumber)
            {
                case 1:
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
            if (string.IsNullOrEmpty(INTERSTITIAL_AD_ID))
            {
                Debug.Log("INTERSTITIAL_AD_ID empty");
                return;
            }

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
                    RandomRunAdsInterstitial();
                },
                () =>
                {
                    Instance.isPreloadInterAds = false;
                    WrapManager.Instance.onInterstitialAdSuccess();
                    RandomRunAdsInterstitial();
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
            if (string.IsNullOrEmpty(REWARDED_AD_ID))
            {
                Debug.Log("REWARDED_AD_ID empty");
                return;
            }

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
                () =>
                {
                    // Default call rewarded failed in unity editor. You can change this to:  WrapManager.Instance.onRewardedAdSuccess();
                    WrapManager.Instance.onRewardedAdFailed("errrrrrrrr");
                }
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
            if (string.IsNullOrEmpty(REWARDED_AD_ID))
            {
                Debug.Log("REWARDED_AD_ID empty");
                return;
            }

            SDK_UIManager.Instance.ShowPopupAds(() => { onClickCloseLoadAdsPopup?.Invoke(); });
            StartCoroutine(DelayShowAds(
                () =>
                {
                    onSuccess?.Invoke();
                    SDK_UIManager.Instance.HidePopupAds();
                },
                err =>
                {
                    switch (err)
                    {
                        case "USER_INPUT":
                            onFailed?.Invoke();
                            SDK_UIManager.Instance.HidePopupAds();
                            break;
                        default:
                            SDK_UIManager.Instance.UpdatePopupAds(1, () => { onSuccess?.Invoke(); });
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