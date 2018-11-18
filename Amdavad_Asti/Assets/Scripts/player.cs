using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
	[SerializeField] GameObject[] pathPoints;
	[SerializeField] GameObject boardInput;
	//[SerializeField] int simulatedDice;

	RectTransform playerButton;
	Vector3 spawnPosition;
	Board boardObject;

	public bool hasWon = false;
	bool isMyTurn = false;

	int stepsMovedFromSpawn = 0;
	int myLivePosition = 1;
	int myCurrentPositionOnBoard{ get {Debug.Log ("Current destination index " + (myLivePosition - 1).ToString()); return getPostionOnBoard (myLivePosition - 1); } }
	int myFinalDestinationOnBoard;

	List <int> safeHouseList = new List<int> (){ 1, 5, 9, 13, 25 };

	void Start ()
	{
		boardObject = boardInput.GetComponent<Board> ();
		spawnPosition = this.transform.position;
		playerButton = GetComponent<RectTransform> ();
	}

	public void movetonext ()
	{
		myFinalDestinationOnBoard = getPostionOnBoard (stepsMovedFromSpawn + boardObject.diceValue);
		isMyTurn = true;
		if (stepsMovedFromSpawn + boardObject.diceValue < 24) {
			StartCoroutine (stepBystep ());
		}
	}

	IEnumerator stepBystep ()
	{
		myLivePosition = stepsMovedFromSpawn;
		for (int j = stepsMovedFromSpawn; j <= stepsMovedFromSpawn + boardObject.diceValue; j++) {
			playerButton.localPosition = pathPoints [j].gameObject.transform.localPosition;
			myLivePosition++;
			yield return new WaitForSeconds (0.3f);
		}
		stepsMovedFromSpawn += boardObject.diceValue;
		yield return new WaitForSeconds (0.1f);
		isMyTurn = false;
		hasMyPlayerPawnsWon ();	
		boardObject.StopPunchingAnimation ();
		boardInput.GetComponent<Board> ().diceButtonUI.GetComponent<Button> ().interactable = true;

	}
		
	void OnTriggerEnter (Collider other)
	{
		GameObject otherPlayer = other.gameObject;
		if (canIKill (otherPlayer)) {
			otherPlayer.transform.position = otherPlayer.GetComponent<player> ().spawnPosition;
			otherPlayer.GetComponent<player> ().stepsMovedFromSpawn = 0;
		}
	}

	// Method checks if otherPlayer is killable
	bool canIKill (GameObject otherPlayer)
	{
		return
	    this.myCurrentPositionOnBoard == this.myFinalDestinationOnBoard &&
		this.isMyTurn &&
		this.gameObject.tag != otherPlayer.gameObject.tag &&
		!isSafeHouse (this.myFinalDestinationOnBoard);
	}

	// Method checks if given position is safe house
	bool isSafeHouse (int position)
	{
		return safeHouseList.Contains (position);
	}

	// Method generates position of player on board
	int getPostionOnBoard (int arrayIndex)
	{
		Debug.Log (arrayIndex);
		if (arrayIndex < 24 && arrayIndex>0)
			return int.Parse (pathPoints [arrayIndex].gameObject.name);
		else
			return 0;
	}

	//This method checks if all player pawns has won
	void hasMyPlayerPawnsWon ()
	{
		GameObject[] allPawns = GameObject.FindGameObjectsWithTag (this.gameObject.tag);
		int totalPawnsWon = 0;
		foreach (GameObject pawn in allPawns) {
			if (pawn.GetComponent<player> ().myCurrentPositionOnBoard == safeHouseList [safeHouseList.Count - 1]) {
				pawn.GetComponent<player> ().hasWon = true;
				if (++totalPawnsWon == allPawns.Length) {
					annouceWinner (pawn);
				}
			}
		}
	}

	void annouceWinner (GameObject player)
	{
		Debug.Log ("The Winner - " + player.tag);
		GameObject.Find("UI_Manager").GetComponent<UI_Manager> ().GameOver(player.tag);
	}
}
