using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseManager : MonoBehaviour
{
	private static WarehouseManager  instance;
	public static WarehouseManager Instance { get => instance; }

	public const int				SIZE		= 24;

	public int[]                    chests		= new int[SIZE];

	public Image[]                  chestImage	= new Image[SIZE];
	public Sprite[]					chestSprite	= new Sprite[5];

	public Sprite                   buyChest    = null;

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
		for(int i = 0; i < SIZE; ++i)
		{
			if(chests[i] == -1) chestImage[i].sprite = buyChest;
			else chestImage[i].sprite = chestSprite[chests[i]];
		}
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
