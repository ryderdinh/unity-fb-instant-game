using System.Collections.Generic;
using GameAnalyticsSDK;

namespace Manager
{
    public class AnalyticsManager : Singleton<AnalyticsManager>
    {
        private void Awake()
        {
            GameAnalytics.Initialize();
        }

        private void Start()
        {
            PushEventLoadedGame();
        }

        private static void PushEventLoadedGame()
        {
            GameAnalytics.NewDesignEvent("game_loaded");
        }

        public void PushEventNoAds()
        {
            GameAnalytics.NewDesignEvent("no_ads_available");
        }

        public static void PushTrackingEvent(string eventName = "")
        {
            GameAnalytics.NewDesignEvent(eventName);
        }
    }
}