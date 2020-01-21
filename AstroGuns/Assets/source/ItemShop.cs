using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
	private static ItemShop  instance;
	public static ItemShop Instance { get => instance; }

	private int			currentIndex = 0;
	private BuyType     currentBuyType = BuyType.None;
	private bool        enoughtEther = false;

	public Button       comfirmBuyButton = null;
	public Text         buyText = null;

	public double       earningTime = 3; //w godzinach
	public double       creditMultipler = 1;

	void Awake()
	{
		if(instance == null) instance = this;
	}

	public void SetItemIndex(int index)
	{
		currentIndex = index;
	}

	public void OpenConfirmWindow(int buyType)
	{
		currentBuyType = (BuyType)buyType;
		OpenConfirmWindow();
	}

	public void OpenConfirmWindow()
	{
		buyText.text = "Do you wont buy " + PrepareItemInfo() + "?";
		MenuManager.Instance.OpenPanel(Panels.ConfirmBuy);
	}
	
	private string PrepareItemInfo()
	{
		string info = "";
		switch(currentBuyType)
		{
			case BuyType.SuperMegaPack:
				info = "Super Mega Pack";
				comfirmBuyButton.interactable = true;
			break;
			case BuyType.InstantBoost:
				if(currentIndex == 0)
				{
					info = "Instant Credit Boost + 4 Hours";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 35 ? true : false;
				}
				else if(currentIndex == 1)
				{
					info = "Instant Credit Boost + 12 Hours";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 90 ? true : false;
				}
				else if(currentIndex == 2)
				{
					info = "Instant Credit Boost + 3 Days";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 360 ? true : false;
				}
				else if(currentIndex == 3)
				{
					info = "Instant Credit Boost + 7 Days";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 700 ? true : false;
				}
			break;
			case BuyType.Chest:
				if(currentIndex == 0)
				{
					info = "Uncommon Space Chest";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 30 ? true : false;
				}
				else if(currentIndex == 1)
				{
					info = "Rare Space Chest";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 60 ? true : false;
				}
				else if(currentIndex == 2)
				{
					info = "Epic Space Chest";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 100 ? true : false;
				}
				else if(currentIndex == 3)
				{
					info = "Legendary Space Chest";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 150 ? true : false;
				}
			break;
			case BuyType.Multipler:
				if(currentIndex == 0)
				{
					info = "x2 Credit Multipler";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 200 ? true : false;
				}
				else if(currentIndex == 1)
				{
					info = "x5 Credit Multipler";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 450 ? true : false;
				}
			break;
			case BuyType.OfflineEarn:
				if(currentIndex == 0)
				{
					info = "+ 2 Hours Offline Earn";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 80 ? true : false;
				}
				else if(currentIndex == 1)
				{
					info = "+ 5 Hours Offline Earn";
					comfirmBuyButton.interactable = MoneyPocket.Instance.Ether.ActualValue >= 180 ? true : false;
				}
			break;
			case BuyType.Ether:
				if(currentIndex == 0)
				{
					info = "60 Ether";
					comfirmBuyButton.interactable = true;
				}
				else if(currentIndex == 1)
				{
					info = "150 Ether";
					comfirmBuyButton.interactable = true;
				}
				else if(currentIndex == 2)
				{
					info = "360 Ether";
					comfirmBuyButton.interactable = true;
				}
				else if(currentIndex == 3)
				{
					info = "900 Ether";
					comfirmBuyButton.interactable = true;
				}
			break;
		}

		return info;
	}

	public void Buy()
	{
		switch(currentBuyType)
		{
			case BuyType.SuperMegaPack: //W TYM TRZEBA SPRAWDZIC CZY ZAPŁACIŁ PIENIĄŻKI
				WarehouseManager.Instance.AddChest(3);
				WarehouseManager.Instance.AddChest(3);
				WarehouseManager.Instance.AddChest(4);
				WarehouseManager.Instance.AddChest(4);
				MoneyPocket.Instance.Ether.ActualValue += 600;
			break;
			case BuyType.InstantBoost:
				if(currentIndex == 0)
				{
					DailyReward.Instance.GiveCredits(1440);
					MoneyPocket.Instance.Ether.ActualValue -= 35;
				}
				else if(currentIndex == 1)
				{
					DailyReward.Instance.GiveCredits(4320);
					MoneyPocket.Instance.Ether.ActualValue -= 90;
				}
				else if(currentIndex == 2)
				{
					DailyReward.Instance.GiveCredits(25920);
					MoneyPocket.Instance.Ether.ActualValue -= 360;
				}
				else if(currentIndex == 3)
				{
					DailyReward.Instance.GiveCredits(60480);
					MoneyPocket.Instance.Ether.ActualValue -= 700;
				}
				break;
			case BuyType.Chest:
				if(currentIndex == 0)
				{
					WarehouseManager.Instance.AddChest(1);
					MoneyPocket.Instance.Ether.ActualValue -= 30;
				}
				else if(currentIndex == 1)
				{
					WarehouseManager.Instance.AddChest(2);
					MoneyPocket.Instance.Ether.ActualValue -= 60;
				}
				else if(currentIndex == 2)
				{
					WarehouseManager.Instance.AddChest(3);
					MoneyPocket.Instance.Ether.ActualValue -= 100;
				}
				else if(currentIndex == 3)
				{
					WarehouseManager.Instance.AddChest(4);
					MoneyPocket.Instance.Ether.ActualValue -= 150;
				}
			break;
			case BuyType.Multipler:
				if(currentIndex == 0)
				{
					creditMultipler += 2;
					MoneyPocket.Instance.Ether.ActualValue -= 200;
				}
				else if(currentIndex == 1)
				{
					creditMultipler += 5;
					MoneyPocket.Instance.Ether.ActualValue -= 450;
				}
			break;
			case BuyType.OfflineEarn:
				if(currentIndex == 0)
				{
					earningTime += 2;
					MoneyPocket.Instance.Ether.ActualValue -= 80;
				}
				else if(currentIndex == 1)
				{
					earningTime += 5;
					MoneyPocket.Instance.Ether.ActualValue -= 180;
				}
				break;
			case BuyType.Ether: //W TYM TRZEBA SPRAWDZIC CZY ZAPŁACIŁ PIENIĄŻKI
				if(currentIndex == 0)
				{
					//MoneyPocket.Instance.Ether.Add(60);
					IAPManager.Instance.PurchaseSampleProduct(EasyMobile.EM_IAPConstants.Product_ether_60);

				}
				else if(currentIndex == 1)
				{
					MoneyPocket.Instance.Ether.Add(150);
				}
				else if(currentIndex == 2)
				{
					MoneyPocket.Instance.Ether.Add(360);
				}
				else if(currentIndex == 3)
				{
					MoneyPocket.Instance.Ether.Add(900);
				}
			break;
		}
	}
}
