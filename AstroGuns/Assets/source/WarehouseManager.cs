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

	public int                      boughtPlace = 0;

	public Text[]                   keysText    = new Text[KEYS];
	public Text[]                   keysMetalforgeText  = new Text[KEYS];
	public Text[]                   dustMetalforgeText  = new Text[KEYS];

	[Header("buy slots")]
	public Text                     slotCostText = null;
	public Button                   buySlotButton = null;

	private int[]                   costOfSlots = { 20, 30, 50, 90, 140, 200,
													300, 420, 540, 660, 780, 900,
													1200, 1500, 1800, 2500, 4000, 6000};

	[Header("open chest")]
	public int                      openedChestType = 0;	

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
		MenuManager.Instance.OpenPanel(Panels.OpenChest);
		openedChestType = chests[slot];
	}

	public void OpenChest()
	{
		//tutaj będzie losowanko rdzeni ze skrzynek
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

}
