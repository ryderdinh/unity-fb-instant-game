using BZ_FIG_SDK.Scripts;
using Manager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ActionPanel;

    public void LoadedUserData()
    {
        // Call when you want to end the "loading" process of the game (depending on usage needs)
        WrapManager.Instance.SetLoadedData(1);
    }

    public void LoadGame()
    {
        // Call when loaded user data. Example:
        BZ_FIG_MANAGER.Instance.LoadGameAfterLoadData();
    }

    private void LoadAsync()
    {
        ActionPanel.SetActive(true);
    }

    public void JoinGame()
    {
        LoadedUserData();
        LoadGame();
        LoadAsync();
    }
}