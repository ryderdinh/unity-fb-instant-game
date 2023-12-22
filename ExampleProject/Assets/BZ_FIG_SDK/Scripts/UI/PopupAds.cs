using System;
using System.Collections;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace BZ_FIG_SDK.Scripts.UI
{
    public class PopupAds : MonoBehaviour
    {
        [SerializeField] private Text txtCountdown;
        [SerializeField] private Button backBtn;
        [SerializeField] private CanvasGroup panel;

        private int _countdownTime;
        private bool _isCountingDown;
        private Action _onWaitSuccess;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Init(int status = 0, Action callback = null)
        {
            panel.gameObject.SetActive(status >= 1);
            _onWaitSuccess = callback;

            switch (status)
            {
                case 0:
                    ResetData();
                    break;
                case 1:
                    AnalyticsManager.Instance.PushEventNoAds();
                    StartCoroutine(TimeCountdown());
                    break;
            }
        }

        private void ResetData()
        {
            UpdateTimeoutNumber(20);
        }

        private IEnumerator TimeCountdown()
        {
            while (_countdownTime > 0)
            {
                yield return new WaitForSeconds(1.0f);
                DecreaseTimeout();
            }

            _onWaitSuccess?.Invoke();
            OnClickClose();
        }

        public void OnClickClose()
        {
            gameObject.SetActive(false);
        }

        private void DecreaseTimeout()
        {
            UpdateTimeoutNumber(_countdownTime - 1);
        }

        public void SetOnClose(Action onClose = null)
        {
            backBtn.onClick.AddListener(() =>
            {
                OnClickClose();
                onClose?.Invoke();
            });
        }

        private void UpdateTimeoutNumber(int value)
        {
            _countdownTime = value;
            txtCountdown.text = $"Reward after: {_countdownTime}";
        }
    }
}