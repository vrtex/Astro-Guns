using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Panels
{
	Backgorund			= 0,
	Upgrades			= 1,
	Shop				= 2,
	Warehouse			= 3,
	Metalforge			= 4,
	Library				= 5,
	Options				= 6,
	Bonuses				= 7,
	Achievements		= 8,
	Meteors				= 9,
	PushablePopup		= 10,
	EnergyCore			= 11,
	EnergyCoreDet		= 12,
	Boost				= 13,
	DailyRewards		= 14,
	ItemInfo			= 15,
	BuyWarehouseSlot	= 16,
	OpenChest			= 17,
	ConfirmBuy			= 18,
};

[System.Serializable]
public enum BuyType
{
	None				= -1,
	SuperMegaPack		= 0,
	InstantBoost		= 1,
	Chest				= 2,
	Multipler			= 3,
	OfflineEarn			= 4,
	Ether				= 5,
};