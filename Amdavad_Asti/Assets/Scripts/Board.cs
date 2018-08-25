using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

	public Text showDiceValue;
	public GameObject[] player1_pawns;
	public GameObject[] player2_pawns;
	public GameObject dice_obj;
	public static int playerTurnValue = 1;

	int rand;
	public static int diceValue;
	public GameObject diceBtn;


	void Update () 
	{
		showDiceValue.text = diceValue.ToString();
	}
	public void PlayerPunching()
	{
		if(playerTurnValue == 1)
		{
			for(int i=0;i<player1_pawns.Length;i++)
			{
				player1_pawns [i].GetComponent<Animator> ().enabled = true;
				player1_pawns [i].GetComponent<Button> ().interactable = true;
			}
		}
		else if(playerTurnValue == 2)
		{
			for(int i=0;i<player2_pawns.Length;i++)
			{
				player2_pawns [i].GetComponent<Animator> ().enabled = true;
				player2_pawns [i].GetComponent<Button> ().interactable = true;
			}
		}
	}
	public void StopPunching()
	{
		
		dice_obj.GetComponent<Button> ().interactable = true;
		if (playerTurnValue == 1) {
			for (int i = 0; i < player1_pawns.Length; i++) {
				player1_pawns [i].GetComponent<Animator> ().enabled = false;
				player1_pawns [i].GetComponent<Button> ().interactable = false;
				player1_pawns [i].GetComponent<RectTransform> ().sizeDelta = new Vector2 (12, 12);
			}
		}
		else if(playerTurnValue == 2)
		{
			for(int i=0;i<player2_pawns.Length;i++){
				player2_pawns [i].GetComponent<Animator> ().enabled = false;
				player2_pawns [i].GetComponent<Button> ().interactable = false;
				player2_pawns [i].GetComponent<RectTransform> ().sizeDelta = new Vector2 (12,12);
			}
		}
		if (playerTurnValue < 2) {
			playerTurnValue++;
		} 
		else {
			playerTurnValue = 1;
		}
		Debug.Log ("Player Turn Value : "+playerTurnValue);
	}
	public void rollDice()
	{
		
		rand = Random.Range (1,5);
		diceValue = rand;
		this.gameObject.GetComponent<Board> ().PlayerPunching ();
		diceBtn.GetComponent<Button> ().interactable = false;
	}
}
