using BZ_FIG_SDK.Scripts;
using Manager;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    private const string FakeDataContextJSONString =
        "{\"id\":\"7029677243815790\",\"type\":\"THREAD:embedded_player\",\"tournament\":{\"id_tour\":\"7029677243815790\",\"tour_name\":\"The World Fruit Merge Tournament\",\"payload\":null,\"start_at\":null,\"end_at\":1702607733,\"is_current\":false},\"players\":[{\"id\":\"6294019030699433\",\"name\":\"Hằng\",\"photo\":\"https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=2554059101416284&gaming_photo_type=unified_picture&ext=1704253829&hash=AfrfylPxbrmJI271n7zQh1tiFBolP71WGlk4F4icaMXkcA\"},{\"id\":\"6876170049105548\",\"name\":\"Chinhs\",\"photo\":\"https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=7047637455296753&gaming_photo_type=unified_picture&ext=1704253829&hash=AfqrR-Ylap1TyrkVNcOFzvrdHyDSHK5oaFtE-Cyg05Q6YQ\"},{\"id\":\"6818573238225015\",\"name\":\"Quang Anh\",\"photo\":\"https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=3614266968816894&gaming_photo_type=unified_picture&ext=1704253829&hash=Afp-u_JkQPVEXfTHXZ4oh8ua1ZGmfUgCYezzZSWFTBWUiQ\"},{\"id\":\"6843480775699313\",\"name\":\"Ivonne\",\"photo\":\"https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=724251436390753&gaming_photo_type=unified_picture&ext=1704253829&hash=AfpPsCuiIjW2_Cgi6L4WzDE24LDmcGIfy-ky1bwzAA6URg\"},{\"id\":\"7334121979944604\",\"name\":\"Tilda\",\"photo\":\"https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=1090589895640479&gaming_photo_type=unified_picture&ext=1704253829&hash=AfrQn_zPTXkkLE1GOy7sLvpD1RIY3oN5V3fhyvya3lG-Pw\"},{\"id\":\"7825357360856764\",\"name\":\"Tr-Ngọc\",\"photo\":\"https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=2247252365468145&gaming_photo_type=unified_picture&ext=1704253829&hash=Afr8KCHEFkwFcOWQi3XT0nfl5x5arnAD8Y7GWwV7QI-8IA\"},{\"id\":\"7131588376899698\",\"name\":\"Bùi\",\"photo\":\"https://platform-lookaside.fbsbx.com/platform/profilepic/?asid=2844002932409360&gaming_photo_type=unified_picture&ext=1704253829&hash=Afoa46Hocp70g5XaJySytLDrXF3Bh7ilna-4TOcHjFKseQ\"}]}";

    private void Start()
    {
    }

    public void OnClickInvite()
    {
        WrapManager.Instance.setInviteAction(() => { Debug.Log("Invited friend!"); });

        // The "CaptureFullScreen" function will capture a full screen image and give you two parameters: a base64 string image and a Texture2D image.
        Ryder.Instance.CaptureFullScreen(
            (base64Image, texture2DImage) => { WrapManager.inviteFriend(base64Image); },
            true
        );
    }

    public void OnClickShare()
    {
        WrapManager.Instance.setShareAction(() => { Debug.Log("Shared!"); });

        // The "CaptureFullScreen" function will capture a full screen image and give you two parameters: a base64 string image and a Texture2D image.
        Ryder.Instance.CaptureFullScreen(
            (base64Image, texture2DImage) => { WrapManager.share(base64Image, "Come with me!", "false"); },
            true
        );
    }

    public void OnClickOpenFanpage()
    {
        Debug.Log("Open fan-page");
        WrapManager.openFollowOfficialPage();
    }


    public void OnClickBanner(bool show)
    {
        Debug.Log(show ? "Show banner" : "Hide banner");
        AdsManager.Instance.ShowBannerAd(show);
    }

    public void OnClickRewardAds()
    {
        Debug.Log("Show rewarded video ads");
        AdsManager.Instance.CoroutineStartRewardedAds(
            null,
            () => { Debug.Log("Rewarded!"); }
        );
    }

    public void OnClickInterAds()
    {
        Debug.Log("Show interstitial ads");
        AdsManager.Instance.ShowAdsInterstitial();
    }

    public void OnClickJoinTournament()
    {
        WrapManager.Instance.SetSwitchAsyncAction(() =>
        {
            Ryder.Instance.CheckUnityEditor(
                () => { WrapManager.fetchCurrentContextData(1, 1500); },
                () => { BZ_FIG_DATA.Instance.InitCurrentContext(FakeDataContextJSONString); }
            );
        });

        Ryder.Instance.CheckUnityEditor(
            () => WrapManager.switchAsync("7029677243815790"),
            () => { WrapManager.Instance.OnSwitchAsyncSuccess(); }
        );
    }

    public void OnClickCreateTournament()
    {
        if (BZ_FIG_DATA.Instance.FbContext.IsInTournament()) return;

        WrapManager.createTournament(123);
    }

    public void OnClickPostTournamentScore()
    {
        if (!BZ_FIG_DATA.Instance.FbContext.IsInTournament()) return;

        WrapManager.postTournamentScore(1234);
    }
}