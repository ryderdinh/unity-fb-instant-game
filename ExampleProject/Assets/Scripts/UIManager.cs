using BZ_FIG_SDK.Scripts;
using Popup;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [HideInInspector] public PausePopup PausePopupController { get; set; }

    private void Awake()
    {
        LoadUIResources();
    }

    private void LoadUIResources()
    {
        PausePopupController = Instantiate(
            Resources.Load<PausePopup>("Prefabs/PausePopup"),
            Vector3.zero,
            Quaternion.identity
        );

        PausePopupController.OnClickHide();
    }


    public void ShowPopupPause()
    {
        PausePopupController.gameObject.SetActive(true);
        PausePopupController.transform.SetAsLastSibling();
        PausePopupController.Show();
    }
}