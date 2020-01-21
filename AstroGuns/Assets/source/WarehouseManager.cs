using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseManager: MonoBehaviour
{
	private static WarehouseManager  instance;
	public static WarehouseManager Instance { get => instance; }

	public const int                SIZE        = 24;
	public const int                KEYS        = 4;

	[Header("chests")]
	public int[]                    chests      = new int[SIZE];

	public Image[]                  chestImage  = new Image[SIZE];
	public Sprite[]                 chestSprite = new Sprite[5];

	public Sprite                   buyChest    = null;

	[Header("keys and dust")]
	public int[]                    keysAmount  = new int[KEYS];
	public int[]                    dustAmount  = new int[KEYS];

	public int                      boughtPlace	= 0;

	public Text[]                   keysText			= new Text[KEYS];
	public Text[]                   keysMetalforgeText  = new Text[KEYS];
	public Text[]                   dustMetalforgeText  = new Text[KEYS];

	[Header("metal forge")]
	public Text                     meltDescription     = null;
	public Text                     meltTimer           = null;
	public Image                    meltImage           = null;
	public Slider                   meltProgress        = null;
	public Sprite[]                 keyImage            = new Sprite[KEYS];
	public float                    timeToEndMelt       = 0f;
	public float                    fullMeltTime        = 0f;

	private int                     currentMeltType     = -1;
	private string                  chooseToMelt		= "Chose which type of key you want to melt";
	private string                  remainingMelting	= "Remaining melting time";

	[Header("buy slots")]
	public Text                     slotCostText		= null;
	public Button                   buySlotButton		= null;

	private int[]                   costOfSlots			= { 20, 30, 50, 90, 140, 200,
															300, 420, 540, 660, 780, 900,
															1200, 1500, 1800, 2500, 4000, 6000};

	[Header("open chest")]
	public int                      openedChestType		= 0;
	public Image                    chestToOpenImage    = null;

	[Header("banana")]
	public Slider                   bananaSlider        = null;
	public Image                    bananaView          = null;
	public Sprite[]                 bananaFrames        = new Sprite[27];
	public AudioSource              audioSource         = null;

	[Header("items from chest")]
	public Image[]                  items               = new Image[12];
	public Sprite                   noneGFX             = null;
	public Sprite[]                 coreProfit			= new Sprite[5];
	public Sprite[]                 coreHaste	        = new Sprite[5];
	public Sprite[]                 coreFortune	        = new Sprite[5];

	void Awake()
	{
		if(instance == null) instance = this;

		chests = new int[24];
	}

	public void AddChest(int chestType)
	{
		int empty = FindEmpty();
		if(empty == -1)
		{
			//tu powinno być okno
		}
		else chests[empty] = chestType;
	}

	int FindEmpty()
	{
		for(int i = 0; i < SIZE; ++i)
		{
			if(chests[i] == 0) return i;
		}
		return -1;
	}

	public void Refresh()
	{
		slotCostText.text = "" + costOfSlots[boughtPlace];
		for(int i = 0; i < SIZE; ++i)
		{
			if(chests[i] == -1) chestImage[i].sprite = buyChest;
			else chestImage[i].sprite = chestSprite[chests[i]];
		}

		for(int i = 0; i < KEYS; ++i)
		{
			keysText[i].text = keysAmount[i].ToString();
			keysMetalforgeText[i].text = keysAmount[i].ToString();
			dustMetalforgeText[i].text = dustAmount[i].ToString();
		}

		if(currentMeltType != -1)
		{
			meltImage.sprite = keyImage[currentMeltType];

			if(fullMeltTime > 0f)
				meltProgress.value = (fullMeltTime - timeToEndMelt) / fullMeltTime;
			else meltProgress.value = 0f;
		}
		else
		{
			meltImage.sprite = noneGFX;
			meltProgress.value = 0f;
		}
	}

	public void UnlockNext()
	{
		for(int i = 0; i < SIZE; ++i)
		{
			if(chests[i] == -1)
			{
				chests[i] = 0;
				return;
			}
		}
	}

	public void SelectWindowTypeToOpen(int slot)
	{
		if(slot < 11 + boughtPlace)
		{
			OpenChestWindow(slot);
		}
		else
		{
			BuySlotWindow();
		}
	}

	public void OpenChestWindow(int slot)
	{
		if(keysAmount[chests[slot] - 1] > 0) //otwarcie skrzyni kluczem
		{
			openedChestType = chests[slot];
			chestToOpenImage.sprite = chestSprite[openedChestType]; //ustawienie grafiki skrzyni
			--keysAmount[openedChestType - 1]; //zabranie klucza
			chests[slot] = 0;

			MenuManager.Instance.OpenPanel(Panels.OpenChest);
		}
		else
		{
			//jeśli nie ma kluczy
		}
	}

	public void OpenChest()
	{
		bananaSlider.value = 0f;
		bananaView.sprite = bananaFrames[0];
		MenuManager.Instance.OpenPanel(Panels.Banana);
		MenuManager.Instance.ClosePanel(Panels.OpenChest);
	}

	public void UpdateBanana()
	{
		int index = Mathf.Clamp((int)(bananaSlider.value * bananaFrames.Length), 0, 26);

		bananaView.sprite = bananaFrames[index];
		audioSource.volume = Mathf.Floor(bananaSlider.value * 100f) / 100f;

		if(bananaSlider.value > 0.95) OpenBanana();
	}

	public void OpenBanana()
	{
		int itemAmount = openedChestType * 2 + 2;
		for(int i = 0; i < items.Length; ++i)
		{
			if(i < itemAmount)
			{
				int type = Random.Range(0, 3);
				int level = 0;
				if(openedChestType == 0)
				{
					if(i == 0) level = Random.Range(0, 2);
				}
				else if(openedChestType == 1)
				{
					if(i == 0) level = Random.Range(0, 3);
					else level = Random.Range(0, 2);
				}
				else if(openedChestType == 2)
				{
					if(i == 2) level = Random.Range(0, 4);
					else if(i == 3) level = Random.Range(0, 4);
					else level = Random.Range(1, 4);
				}
				else if(openedChestType == 3)
				{
					if(i == 2) level = Random.Range(1, 4);
					else if(i == 3) level = Random.Range(1, 4);
					else level = Random.Range(2, 4);
				}

				if(type == 0)
					items[i].sprite = coreProfit[level];
				else if(type == 1)
					items[i].sprite = coreHaste[level];
				else if(type == 2)
					items[i].sprite = coreFortune[level];

				//tutaj powinno być dodanie rdzeni
			}
			else items[i].sprite = noneGFX;	
		}

		MenuManager.Instance.ClosePanel(Panels.Banana);
		MenuManager.Instance.OpenPanel(Panels.LootFromBanana);
	}

	public void BuySlotWindow()
	{
		MoneyDisplay.Instance.ShowEther();
		MenuManager.Instance.OpenPanel(Panels.BuyWarehouseSlot);
		buySlotButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= costOfSlots[boughtPlace] ? true : false;
	}

	public void BuySlot()
	{
		if(MoneyPocket.Instance.Ether.ActualValue >= costOfSlots[boughtPlace])
		{
			MoneyPocket.Instance.Ether.ActualValue = MoneyPocket.Instance.Ether.ActualValue - costOfSlots[boughtPlace];
			++boughtPlace;
			UnlockNext();
			Refresh();
			buySlotButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= costOfSlots[boughtPlace] ? true : false;
		}
	}

	public void MeltDust(int dustType)
	{
		if(dustAmount[dustType] >= 4)
		{
			dustAmount[dustType] -= 4;
			timeToEndMelt = 60 * 60 * (dustType + 1);
			fullMeltTime = timeToEndMelt;
			meltProgress.value = 0f;
			meltImage.sprite = keyImage[dustType];
			currentMeltType = dustType;
		}
	}
}
