using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRulesUtility : MonoBehaviour {

	[SerializeField] GameObject boardInput;
	Board boardObject;

	public static List <string> playerRanks {get; private set;} 
	const int eligibleMoveValue = 4;
	// Use this for initialization
	void Start () {
		playerRanks = new List<string> ();
		boardObject = boardInput.GetComponent<Board> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//This method checks if all player pawns has won
	public void hasMyPlayerPawnsWon (GameObject myPlayer)
	{
		GameObject[] allPawns = boardObject.getAllPlayerPawns (myPlayer.tag);
		int totalPawnsWon = 0;
		foreach (GameObject pawn in allPawns) {
			if (pawn.GetComponent<PlayerPawn> ().myCurrentPositionOnBoard == PlayerPawn.safeHouseList [PlayerPawn.safeHouseList.Count - 1]) {
				pawn.GetComponent<PlayerPawn> ().hasWon = true;
				if (++totalPawnsWon == allPawns.Length) {
					playerRanks.Add (pawn.tag);
					boardObject.removeWinnerPlayer (myPlayer.tag);
					isGameOver(pawn.tag);
				}
			}
		}
	}

	void isGameOver(string playerTag)
	{
		if (playerRanks.Count == boardObject.totalPlayers -1) {
			GameObject.Find("UI_Manager").GetComponent<UI_Manager> ().GameOver(playerRanks[0]);
		}
	}

	public bool isPlayerElgibleToMove(int dice)
	{
		return dice == eligibleMoveValue ;
	}

}
