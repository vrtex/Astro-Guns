using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	private static MenuManager	instance;
	public static MenuManager	Instance { get => instance; }

	public GameObject[]			panels     = new GameObject[13];

	public GameObject			shopBar    = null;
	public GameObject			pompBar    = null;

	public PreviewCamera        previewCamera   = null;

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

	public void CloseAllPanels()
	{
		for(int i = 0; i < panels.Length; ++i)
		{
			panels[i].SetActive(false);
		}
	}

    public void ClosePanelAndBackground(Panels panel)
    {
        ClosePanel(Panels.Backgorund);
        ClosePanel(panel);
    }

	public void ClosePanel(Panels panel)
	{
        ClosePanel((int)panel);
	}

	public void ClosePanel(int panel)
	{
		panels[panel].SetActive(false);
	}

	public void OpenPanel(Panels panel)
	{
		panels[0].SetActive(true);
		panels[(int)panel].SetActive(true);
		AdditionalWindowAction(panel);
	}

    public GameObject GetPanel(Panels panel)
    {
        return panels[(int)panel];
    }

	public void OpenPanel(int panelIndex)
	{
		OpenPanel((Panels)panelIndex);
	}

    public void OpenPanelWithoutPanel(int panelIndex)
    {
        AdditionalWindowAction((Panels)panelIndex);
    }

    public void MaxPomp()
	{
		shopBar.SetActive(false);
		pompBar.SetActive(true);
	}

	public void MinPomp()
	{
		shopBar.SetActive(true);
		pompBar.SetActive(false);
	}

	public void AdditionalWindowAction(Panels panel)
	{
		switch(panel)
		{
			case Panels.Library:
				previewCamera.RefreshWeapon();
			    break;
			case Panels.Warehouse:
				WarehouseManager.Instance.Refresh();
			    break;
			case Panels.Shop:
				MoneyDisplay.Instance.ShowEther();
			break;
			case Panels.Bonuses:
				BonusManager.Instance.CheckBonus();
			break;
			    break;
            case Panels.Achievements:
                AchievementManager.Instance.ShowAchievementsUI();
            break;
		}
	}
}
