using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

	public Text showDiceValue;
	public GameObject[] player1_pawns;

	void Update () 
	{
		showDiceValue.text = Dice.diceValue.ToString();
	}
	public void PlayerPunching()
	{
		for(int i=0;i<player1_pawns.Length;i++)
		{
			player1_pawns [i].GetComponent<Animator> ().enabled = true;
		}
	}
	public void StopPunching()
	{
		for(int i=0;i<player1_pawns.Length;i++)
		{
			player1_pawns [i].GetComponent<Animator> ().enabled = false;
			player1_pawns [i].GetComponent<RectTransform> ().sizeDelta = new Vector2 (12,12);
		}
	}
}
