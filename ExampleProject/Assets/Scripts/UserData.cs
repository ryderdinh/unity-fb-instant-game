using Manager;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static int coin
    {
        get => PlayerPrefs.GetInt("Coin", 0);
        set
        {
            WrapManager.setUserData("int", "Coin", value.ToString(), 1);
            PlayerPrefs.SetInt("Coin", value);
        }
    }

    #region Init Data From Browser

    public void InitCoin(int value)
    {
        coin = value;
    }

    #endregion
}