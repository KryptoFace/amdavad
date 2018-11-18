using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour 
{
	[SerializeField] GameObject StartingPanel;
	[SerializeField] GameObject ModeSelectPanel;
	[SerializeField] GameObject BetSelectPanel;
	[SerializeField] GameObject GamePlayPanel;
	[SerializeField] GameObject GameOverPanel;
	[SerializeField] Text BetAmount_text;
	[SerializeField] Text winnerName_text;
	int BetAmount = 100;

	public void Play()
	{
		DisableAllPanel ();
		GamePlayPanel.SetActive (true);
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
		ModeSelectPanel.SetActive (false);
		BetSelectPanel.SetActive (false);
		GamePlayPanel.SetActive (false);
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
	public void betToPlay()
	{
		DisableAllPanel ();
		GamePlayPanel.SetActive (true);
	}
	void Update()
	{
		BetAmount_text.text = BetAmount.ToString ();
	}
	public void restartGame()
	{
		SceneManager.LoadScene (0);
	}
	public void GameOver(string winnerName)
	{
		GamePlayPanel.SetActive (false);
		GameOverPanel.SetActive (true);
		winnerName_text.text += winnerName;
	}
}
