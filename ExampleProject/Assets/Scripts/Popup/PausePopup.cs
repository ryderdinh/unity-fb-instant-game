using UnityEngine;

namespace Popup
{
    public class PausePopup : MonoBehaviour
    {
        public void Show()
        {
        }

        public void OnClickHide()
        {
            gameObject.SetActive(false);
        }
    }
}