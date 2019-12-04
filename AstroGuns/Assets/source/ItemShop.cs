using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
	public int currentIndex = 0;


	public void SetItemIndex(int index)
	{
		currentIndex = index;
	}

	public void OpenConfirmWindow(int buyType)
	{
		OpenConfirmWindow((BuyType)buyType);
	}

	public void OpenConfirmWindow(BuyType buyType)
	{
		//MenuManager.Instance.
	}
	
}
