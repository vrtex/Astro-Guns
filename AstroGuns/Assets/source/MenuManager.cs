using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public GameObject[]        panels     = new GameObject[13];

	public GameObject          shopBar    = null;
	public GameObject          pompBar    = null;

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void CloseAllPanels()
	{
		for(int i = 0; i < panels.Length; ++i)
		{
			panels[i].SetActive(false);
		}
	}

	public void ClosePanel(Panels panel)
	{
		panels[(int)panel].SetActive(false);
	}
	public void ClosePanel(int panel)
	{
		panels[panel].SetActive(false);
	}

	public void OpenPanel(Panels panel)
	{
		panels[0].SetActive(true);
		panels[(int)panel].SetActive(true);
	}

	public void OpenPanel(int panelIndex)
	{
		OpenPanel((Panels)panelIndex);
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
}
