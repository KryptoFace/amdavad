using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour 
{
	public GameObject StartingPanel;
	public GameObject ModeSeletPanel;
	public GameObject BetSelectPanel;

	public Text BetAmount_text;

	int BetAmount = 100;

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
	public void Plus()
	{
			BetAmount += 50;
	}
	public void Minus()
	{
		if (BetAmount > 100) {
			BetAmount -= 50;
		}
		else
		{
			BetAmount = 100;	
		}
	}
	void Update()
	{
		BetAmount_text.text = BetAmount.ToString ();
	}
}
