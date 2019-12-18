using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusOrganizer: MonoBehaviour
{
	private static BonusOrganizer instance;
	public static BonusOrganizer Instance { get => instance; }

	public Button               fill		= null;
	public Button               multipler	= null;
	public Button               autoMerge	= null;
	public Button               buyAuto     = null;

	[Header("Active bar")]
	public GameObject           activeBonus     = null;
	public Slider               activeSlider    = null;
	public Image                activeImage     = null;

	[Header("Bonus List")]
	public List<Boost>          boosts          = new List<Boost>();

	private float               activeBoostTime = 0f;
	private int                 currentBoost    = 0;
	private bool                boostIsActive   = false;

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

	public void CheckBonus()
	{

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
				activeBonus.SetActive(false);
				boostIsActive = false;
			}
		}
	}

	public void ActiveBonus(int index)
	{
		currentBoost = index;

		activeBoostTime = boosts[currentBoost].time;
		activeSlider.value = 1f;
		activeImage.sprite = boosts[currentBoost].sprite;

		activeBonus.SetActive(true);
		boostIsActive = true;
	}
}
