using Manager;
using UnityEngine;

namespace BZ_FIG_SDK.Scripts
{
    public class BZ_FIG_MANAGER : Singleton<BZ_FIG_MANAGER>
    {
        public bool loadedData;

        private void Awake()
        {
            WrapManager.WebGLWindowInit();
            DontDestroyOnLoad(gameObject);
            Debug.Log("Initialize BZ_FIG_MANAGER successfully");
        }

        public void LoadGameAfterLoadData()
        {
            AdsManager.Instance.ShowBannerAd(true);
            AdsManager.Instance.RandomRunAdsInterstitial();
            AdsManager.Instance.PreloadAdsRewarded();
            // Edit function if you need (don't edit parameter)...
            // ...
        }
    }
}