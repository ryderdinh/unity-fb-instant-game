using System;
using BZ_FIG_SDK.Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace BZ_FIG_SDK.Scripts
{
    public class SDK_UIManager : Singleton<SDK_UIManager>
    {
        [FormerlySerializedAs("_popupAds")] public PopupAds popupAdsPrefab;

        private PopupAds _popupAds;

        private void Start()
        {
            _popupAds = Instantiate(
                popupAdsPrefab,
                Vector3.zero,
                Quaternion.identity
            );

            Transform transform2;
            (transform2 = _popupAds.transform).SetParent(gameObject.transform);
            var transform1 = transform2;
            transform1.localScale = Vector3.one;
            transform1.localPosition = Vector3.zero;

            _popupAds.gameObject.SetActive(false);
        }

        public void ShowPopupAds(Action onClose = null)
        {
            _popupAds.Show();
            _popupAds.SetOnClose(onClose);
            _popupAds.Init();
        }

        public void UpdatePopupAds(int status, Action callback = null)
        {
            _popupAds.Init(status, callback);
        }

        public void HidePopupAds()
        {
            _popupAds.OnClickClose();
        }
    }
}