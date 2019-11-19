using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
		if(instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
		timeToNextBoost = Random.Range(minTimeToBoost, maxTimeToBoost);
	}

    void Update()
    {
		if(boostIsActive)
		{
			if(activeBoostTime > 0f) //aktywny przyśpieszacz
			{
				activeBoostTime -= Time.deltaTime;
				if(activeBoostTime < 0f) activeBoostTime = 0f;
				activeSlider.value = activeBoostTime / boosts[currentBoost].time;
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
		boostIsActive = true;
		boostButton.SetActive(false);
		activeBoost.SetActive(true);
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
