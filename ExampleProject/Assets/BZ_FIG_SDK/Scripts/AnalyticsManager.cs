using BZ_FIG_SDK.Scripts;
using UnityEngine;

namespace Manager
{
    public class AnalyticsManager : Singleton<AnalyticsManager>
    {
        private void Awake()
        {
            // Initialize...
        }

        private void Start()
        {
            PushEventLoadedGame();
        }

        private static void PushEventLoadedGame()
        {
            Debug.Log("game_loaded");
        }

        public void PushEventNoAds()
        {
            Debug.Log("no_ads_available");
        }

        public static void PushTrackingEvent(string eventName = "")
        {
            Debug.Log(eventName);
        }
    }
}