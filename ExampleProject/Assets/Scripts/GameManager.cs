using BZ_FIG_SDK.Scripts;
using Manager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void OnClickLoadedUserData(bool show)
    {
        WrapManager.Instance.SetLoadedData(1);
            
            
    }

    public void OnClickLoadGame()
    {
        // Call when loaded user data. Example:
        BZ_FIG_MANAGER.Instance.LoadGameAfterLoadData();
    }
}