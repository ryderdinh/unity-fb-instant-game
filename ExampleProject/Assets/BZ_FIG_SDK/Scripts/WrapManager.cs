// ReSharper disable once RedundantUsingDirective
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using System;
using System.Runtime.InteropServices;
using BZ_FIG_SDK.Scripts;
using UnityEngine;


namespace Manager
{
    public class WrapManager : Singleton<WrapManager>
    {
        protected static Action arrowFunc = () => { };
        public Action adsBonusFailed;
        public Action adsBonusSuccess;
        public Action claimItemSuccess;
        public Action createTournamentSuccess = arrowFunc;
        public Action interstitialAdFailed = arrowFunc;
        public Action interstitialAdSuccess = arrowFunc;
        public Action inviteFailed = arrowFunc;
        public Action inviteSuccess = arrowFunc;
        public Action reviveFailed;
        public Action reviveSuccess;
        public Action<string> rewardedAdFailed;
        public Action rewardedAdSuccess = arrowFunc;
        public Action shareFailed = arrowFunc;
        public Action shareSuccess = arrowFunc;
        public Action switchAsyncSuccess = arrowFunc;
        public Action onPause = arrowFunc;

        public void Test()
        {
            Debug.Log("OK!");
        }

        public void setShareAction(Action func1 = null, Action func2 = null)
        {
            shareSuccess = func1;
            shareFailed = func2;
        }

        public void onShareSuccess()
        {
            shareSuccess?.Invoke();
        }

        public void onShareFailed()
        {
            shareFailed?.Invoke();
        }

        public void SetRewardedAdAction(Action func1 = null, Action<string> func2 = null)
        {
            rewardedAdSuccess = func1;
            rewardedAdFailed = func2;
        }

        public void onRewardedAdSuccess()
        {
            AnalyticsManager.PushTrackingEvent("rewarded_video_ads:watched");
            rewardedAdSuccess?.Invoke();
        }

        public void onRewardedAdFailed(string error)
        {
            rewardedAdFailed?.Invoke(error);
        }

        public void SetInterstitialAdAction(Action func1 = null)
        {
            interstitialAdSuccess = func1;
        }

        public void onInterstitialAdSuccess()
        {
            AnalyticsManager.PushTrackingEvent("interstitial_ads:watched");
            interstitialAdSuccess?.Invoke();
            interstitialAdSuccess = null;
        }

        public void onInterstitialAdFailed()
        {
            interstitialAdSuccess?.Invoke();
            interstitialAdSuccess = null;
        }

        public void setInviteAction(Action func1 = null, Action func2 = null)
        {
            inviteSuccess = func1;
            inviteFailed = func2;
        }

        public void onInviteSuccess()
        {
            inviteSuccess?.Invoke();
            inviteSuccess = null;
        }

        public void onInviteFailed()
        {
            inviteFailed?.Invoke();
        }

        public void pauseGame()
        {
            // Handle when call pause
            onPause();
        }

        public void setOnPause(Action func)
        {
            onPause = func;
        }

        public void setCreateTournamentAction(Action func1 = null)
        {
            createTournamentSuccess = func1;
        }

        public void onCreateTournamentSuccess()
        {
            createTournamentSuccess?.Invoke();
        }

        public void OnLoadedBannerAds()
        {
            AnalyticsManager.PushTrackingEvent("banner_ads:showed");
        }

        public void SetSwitchAsyncAction(Action func1 = null)
        {
            switchAsyncSuccess = func1;
        }

        public void OnSwitchAsyncSuccess()
        {
            switchAsyncSuccess?.Invoke();
        }

        public void OpenTournamentPopup()
        {
            // UIManager.Instance.ShowPopupTournament();
        }

        public void StartLoadGame()
        {
            // Handle when call load game after get data from FB
            BZ_FIG_MANAGER.Instance.LoadGameAfterLoadData();
        }

        public void SetLoadedData(int value)
        {
            // Call when loaded user data from FB
            BZ_FIG_MANAGER.Instance.loadedData = value == 1;
        }

        #region DllImport

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        public static extern void WebGLWindowInit();

        [DllImport("__Internal")]
        public static extern void showRewardedAd(string placementId);

        [DllImport("__Internal")]
        public static extern void preloadInterstitialAd(string placementId);

        [DllImport("__Internal")]
        public static extern void preloadRewardedAd(string placementId);

        [DllImport("__Internal")]
        public static extern void showInterstitialAd(string placementId);

        [DllImport("__Internal")]
        public static extern void loadBannerAds(string placementId);

        [DllImport("__Internal")]
        public static extern void hideBannerAds();

        [DllImport("__Internal")]
        public static extern void setUserData(string type, string name, string value, int isFlush = 0);
        
        [DllImport("__Internal")]
        public static extern void inviteFriend(string imageText, string jsonData = "{}");
        
        [DllImport("__Internal")]
        public static extern void openFollowOfficialPage();
        
        [DllImport("__Internal")]
        public static extern void share(string image, string text, string switchContext);

        [DllImport("__Internal")]
        public static extern string getFbId();

        [DllImport("__Internal")]
        public static extern string getFbAvatar();

        [DllImport("__Internal")]
        public static extern string getFbName();
        
        [DllImport("__Internal")]
        public static extern void fetchCurrentContextData(int openTournamentPopup, int delayOpenTournamentPopup);
      
        [DllImport("__Internal")]
        public static extern void switchAsync(string contextId);
        
        [DllImport("__Internal")]
        public static extern void getFacebookFriend();

        [DllImport("__Internal")]
        public static extern void updateUserDataToCloud();
        
        [DllImport("__Internal")]
        public static extern void shareTournament(int score);
        
        [DllImport("__Internal")]
        public static extern void postTournamentScore(int score, int HIGHER_IS_BETTER = 1);
        
        [DllImport("__Internal")]
        public static extern void createTournament(int initialScore);
#else
        public static void WebGLWindowInit()
        {
        }

        public static void showRewardedAd(string placementId)
        {
        }

        public static void preloadInterstitialAd(string placementId)
        {
        }

        public static void preloadRewardedAd(string placementId)
        {
        }

        public static void showInterstitialAd(string placementId)
        {
        }

        public static void loadBannerAds(string placementId)
        {
        }

        public static void hideBannerAds()
        {
        }

        public static void setUserData(string type, string name, string value, int isFlush = 0)
        {
        }

        public static void inviteFriend(string imageText, string jsonData = "{}")
        {
            Debug.Log(imageText);
        }

        public static void openFollowOfficialPage()
        {
        }

        public static void share(string image, string text, string switchContext)
        {
        }

        public static string getFbId()
        {
            return "";
        }

        public static string getFbAvatar()
        {
            return "";
        }

        public static string getFbName()
        {
            return "";
        }

        public static void fetchCurrentContextData(int openTournamentPopup, int delayOpenTournamentPopup)
        {
        }

        public static void switchAsync(string contextId)
        {
        }

        public static void getFacebookFriend()
        {
        }

        public static void updateUserDataToCloud()
        {
        }

        public static void shareTournament(int score)
        {
        }

        public static void postTournamentScore(int score, int HIGHER_IS_BETTER = 1)
        {
        }

        public static void createTournament(int initialScore)
        {
        }
#endif

        #endregion
    }
}