using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    void Awake() // inicjalizacja EasyMobile
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }

    // testowe skrypty pod przyciskami w menu
    public void InterstitialAds()
    {
        Debug.Log("b");

        if (Advertising.IsInterstitialAdReady())
        {
            Advertising.ShowInterstitialAd();
        }
    }

    public void RewardedAds()
    {
        Debug.Log("a");
        if (Advertising.IsRewardedAdReady())
        {
            Advertising.ShowRewardedAd();
        }
    }
}

// https://www.easymobile.sglibgames.com/docs/pro/chapters/advertising/scripting.html#working-with-banner-ads
// tutaj patrzymy jak używać banner Ads, Interstitial Ads i Rewarded Ads