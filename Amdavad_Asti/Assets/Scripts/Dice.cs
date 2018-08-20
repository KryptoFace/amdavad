using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

	int rand;
	public static int diceValue;
	public GameObject boardScript;


	public void rollDice()
	{
		rand = Random.Range (1,5);
		diceValue = rand;
		boardScript.GetComponent<Board> ().PlayerPunching ();
//		StartCoroutine (PlayerPunching());
	}


}
