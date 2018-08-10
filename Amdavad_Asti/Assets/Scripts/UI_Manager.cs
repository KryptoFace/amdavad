using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour 
{
	public GameObject StartingPanel;
	public GameObject ModeSeletPanel;
	public GameObject BetSelectPanel;

	public void Play()
	{
		DisableAllPanel ();
		ModeSeletPanel.SetActive (true);
	}

	public void SelectMode(GameObject modename)
	{
		if(modename.name == "VsComp")
		{
			//for now we are opening bet panel for both mode
			DisableAllPanel();
			BetSelectPanel.SetActive (true);
		}
		if(modename.name == "VsLocal")
		{
			//for now we are opening bet panel for both mode
			DisableAllPanel();
			BetSelectPanel.SetActive (true);
		}
	}


	public void DisableAllPanel()
	{
		StartingPanel.SetActive (false);
		ModeSeletPanel.SetActive (false);
		BetSelectPanel.SetActive (false);
	}
}
