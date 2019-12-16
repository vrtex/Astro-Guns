using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class BoostManager : MonoBehaviour
{
	private static BoostManager instance;
	public static BoostManager Instance { get => instance; }

	[Header("To connect")]
	public GameObject           boostButton     = null;
	public Image		        boostImage		= null;
	public Text                 boostText       = null;
	public GameObject           activeBoost     = null;
	public Slider               activeSlider    = null;
	public Image                activeImage     = null;

	public GameObject	        activeWatchText	= null;
	public Text                 boostName       = null;

	public GameObject           buttonsNormal   = null;
	public GameObject           buttonsAds      = null;

	[Header("Parameters")]
	public float                minTimeToBoost  = 10f;
	public float                maxTimeToBoost  = 120f;

	[Header("Bonus List")]
	public List<Boost>          boosts          = new List<Boost>();
	public List<Boost>          adBoosts        = new List<Boost>();

	//---
	private float               timeToNextBoost = 0f;
	private float               activeBoostTime = 0f;
	private int                 currentBoost    = 0;
	private bool                boostIsActive   = false;
	private bool                boostReady      = false;

	private bool                adIsWatch       = false;
	private bool                adUnlock        = true;

	public float				timeToAdUnlock  = 60f;
	private float               timerUnlock     = 60f;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
	}

    void Start()
    {
		timeToNextBoost = Random.Range(minTimeToBoost, maxTimeToBoost);

		AdsManager.Instance.rewarderComplited.AddListener(AfterWatchAd);
		AdsManager.Instance.rewarderSkiped.AddListener(AfterSkipAd);
	}

    void Update()
    {
		if(timerUnlock == 0f)
		{
			adUnlock = true;
			timerUnlock = timeToAdUnlock;
		}
		else if(!adUnlock) timerUnlock -= Time.deltaTime;


		if(boostIsActive)
		{
			if(activeBoostTime > 0f) //aktywny przyśpieszacz
			{
				activeBoostTime -= Time.deltaTime;
				if(activeBoostTime < 0f) activeBoostTime = 0f;
				if(adIsWatch) activeSlider.value = activeBoostTime / adBoosts[currentBoost].time;
				else activeSlider.value = activeBoostTime / boosts[currentBoost].time;
			}
			else //zkończenie przyśpieszacza
			{
				activeBoost.SetActive(false);
				boostIsActive = false;
				boostReady = false;
			}
		}
        else if(!boostReady)
		{
			if(timeToNextBoost > 0f)
			{
				timeToNextBoost -= Time.deltaTime;
			}
			else
			{
				timeToNextBoost = Random.Range(minTimeToBoost, maxTimeToBoost);
				//losowanie bonusu
				int chanceSum = 0;
				foreach(var c in boosts)
				{
					chanceSum += c.chance;
				}
				int temp = Random.Range(0, chanceSum);
				for(int i = 0; i < boosts.Count; ++i)
				{
					temp -= boosts[i].chance;
					if(temp <= 0)
					{
						currentBoost = i;
						break;
					}
				}
				//zapisanie informacji o bonusie
				adIsWatch = false;
				boostName.text = "BOOSTS";

				activeBoostTime = boosts[currentBoost].time;
				activeImage.sprite = boosts[currentBoost].sprite;
				activeSlider.value = 1f;
				boostImage.sprite = boosts[currentBoost].sprite;
				boostText.text = boosts[currentBoost].description;
				boostButton.SetActive(true);
				boostReady = true;
			}
		}
    }

	public void ActiveBoost()
	{ 
		if(Advertising.IsRewardedAdReady()) //połączenie z netem i wyświetlenie reklamy
		{
			activeWatchText.SetActive(true);
			boostName.text = "SUPERBOOSTS";

			activeBoostTime = adBoosts[currentBoost].time;
			activeImage.sprite = adBoosts[currentBoost].sprite;
			activeSlider.value = 1f;
			boostImage.sprite = adBoosts[currentBoost].sprite;
			boostText.text = adBoosts[currentBoost].description;

			buttonsAds.SetActive(true);
			buttonsNormal.SetActive(false);

			adUnlock = false;
		}
		else //gdy nie można wyświetlić reklam
		{
			activeWatchText.SetActive(false);
			boostName.text = "BOOSTS";

			boostIsActive = true;
			boostButton.SetActive(false);
			activeBoost.SetActive(true);

			MenuManager.Instance.CloseAllPanels();
		}
	}

	public void AfterWatchAd()
	{
		boostIsActive = true;
		boostButton.SetActive(false);
		activeBoost.SetActive(true);

		adIsWatch = true;

		MenuManager.Instance.CloseAllPanels();
	}

	public void AfterSkipAd()
	{
		activeBoostTime = boosts[currentBoost].time;
		activeImage.sprite = boosts[currentBoost].sprite;
		activeSlider.value = 1f;
		boostImage.sprite = boosts[currentBoost].sprite;
		boostText.text = boosts[currentBoost].description;

		boostName.text = "BOOSTS";

		boostIsActive = false;
		adIsWatch = false;
		//boostButton.SetActive(false);
		//activeBoost.SetActive(true);

		buttonsAds.SetActive(false);
		buttonsNormal.SetActive(true);

		activeWatchText.SetActive(false);

		
	}

	public bool IsActive()
	{
		return boostIsActive;
	}

	public Boost GetCurrentBoost()
	{
		if(adIsWatch) return adBoosts[currentBoost];
		return boosts[currentBoost];
	}
}
