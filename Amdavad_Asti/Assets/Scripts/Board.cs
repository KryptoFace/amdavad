using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

	[SerializeField] Text showDiceValue;
	[SerializeField] GameObject[] player1_pawns;
	[SerializeField] GameObject[] player2_pawns;
	[SerializeField] GameObject[] player3_pawns;
	[SerializeField] GameObject[] player4_pawns;
	[SerializeField] int totalPlayers=4;

	public GameObject diceButtonUI;
	public static int playerTurnValue = 0;
	public int diceValue;
	public List<GameObject[]> playerPawnMap;

	void Start()
	{
		playerPawnMap = new List <GameObject[]> ()
		{ {player1_pawns}, {player2_pawns}, {player3_pawns}, {player4_pawns} };
	}

	void Update () 
	{
		showDiceValue.text = diceValue.ToString();
	}
	public void StartPunchingAnimation()
	{
		playerPunchingExecuter (playerTurnValue,true);
	}
	public void StopPunchingAnimation()
	{
		playerPunchingExecuter (playerTurnValue,false);
	}

	void playerPunchingExecuter(int playerTurnValue,bool isEnabled)
	{
		GameObject[] playerPawn=playerPawnMap[--playerTurnValue];
		for(int i=0;i<playerPawn.Length;i++)
		{
			if (!playerPawn [i].GetComponent<player> ().hasWon) {
				playerPawn [i].GetComponent<Animator> ().enabled = isEnabled;
				playerPawn [i].GetComponent<Button> ().interactable = isEnabled;
				playerPawn [i].GetComponent<RectTransform> ().sizeDelta = new Vector2 (12, 12);
			}
		}
	}
	public void rollDice()
	{
		if (playerTurnValue == totalPlayers) {
			playerTurnValue = 1;
		} 
		else {
			playerTurnValue++;
		}
		Debug.Log ("Player Turn Value : "+playerTurnValue);
		diceValue =Random.Range (1,5);
		this.gameObject.GetComponent<Board> ().StartPunchingAnimation ();
		diceButtonUI.GetComponent<Button> ().interactable = false;
	}

}
