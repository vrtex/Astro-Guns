using EasyMobile;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{
	private static AdsManager instance;
	public static AdsManager Instance { get => instance; }

	public UnityEvent       rewarderComplited = new UnityEvent();
	public UnityEvent       rewarderSkiped = new UnityEvent();

	void Awake() // inicjalizacja EasyMobile
    {
		if(instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();

		Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
		Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
	}

    // testowe skrypty pod przyciskami w menu
    public void InterstitialAds()
    {
        if (Advertising.IsInterstitialAdReady())
        {
            Advertising.ShowInterstitialAd();
        }
    }

    public void RewardedAds()
    {
		if(Advertising.IsRewardedAdReady())
        {
            Advertising.ShowRewardedAd();
        }
    }

	void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
	{
		Debug.Log("complite rewarded");
		rewarderComplited.Invoke();
		//Debug.Log("Rewarded ad has completed. The user should be rewarded now.");
	}

	void RewardedAdSkippedHandler(RewardedAdNetwork network, AdLocation location)
	{
		Debug.Log("complite skiped");
		rewarderSkiped.Invoke();
		//Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
	}
}

// https://www.easymobile.sglibgames.com/docs/pro/chapters/advertising/scripting.html#working-with-banner-ads
// tutaj patrzymy jak używać banner Ads, Interstitial Ads i Rewarded Ads