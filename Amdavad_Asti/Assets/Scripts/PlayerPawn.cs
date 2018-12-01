using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerPawn : MonoBehaviour
{
	[SerializeField] GameObject[] pathPoints;
	[SerializeField] GameObject boardInput;
	[SerializeField] int simulatedDice;

	RectTransform playerButton;
	Vector3 spawnPosition;
	Board boardObject;
	GameRulesUtility ruleChecker;

	public bool hasWon = false;
	public bool isEligibleForMove=false;
	bool isMyTurn = false;

	public int stepsMovedFromSpawn { get; private set;}
	int myLivePosition = 1;
	public int myCurrentPositionOnBoard{ get { return getPostionOnBoard (myLivePosition - 1); } }
	int myFinalDestinationOnBoard;

	public static readonly List <int> safeHouseList=new List<int> (){ 1, 5, 9, 13, 25 };

	void Start ()
	{
		stepsMovedFromSpawn = 0;
		boardObject = boardInput.GetComponent<Board> ();
		spawnPosition = this.transform.position;
		playerButton = GetComponent<RectTransform> ();
		ruleChecker = boardInput.GetComponent<GameRulesUtility> ();
	}

	public void movetonext ()
	{
		try
		{
			//boardObject.diceValue = simulatedDice;
			myFinalDestinationOnBoard = getPostionOnBoard (stepsMovedFromSpawn + boardObject.diceValue);
			isMyTurn = true;
			if (canPlayerMove()) {
				StartCoroutine (stepBystep ());
			} else {
				//Popup: Move other pawn
			}
		}
		catch(Exception exception)
		{
			Debug.LogError (exception.Message);
		}
	}

	IEnumerator stepBystep () 
	{
		boardObject.StopPunchingAnimation ();
		isEligibleForMove = true;
		myLivePosition = stepsMovedFromSpawn;
		for (int j = stepsMovedFromSpawn; j <= stepsMovedFromSpawn + boardObject.diceValue; j++) {
			playerButton.localPosition = pathPoints [j].gameObject.transform.localPosition;
			myLivePosition++;
			yield return new WaitForSeconds (0.3f);
		}
		stepsMovedFromSpawn += boardObject.diceValue;
		yield return new WaitForSeconds (0.1f);
		isMyTurn = false;
		ruleChecker.hasMyPlayerPawnsWon (this.gameObject);	
		boardObject.diceButtonUI.GetComponent<Button> ().interactable = true;
		boardObject.playerTurnChanger ();
	}
		
	void OnTriggerEnter (Collider other)
	{
		GameObject otherPlayer = other.gameObject;
		if (canIKill (otherPlayer)) {
			otherPlayer.transform.position = otherPlayer.GetComponent<PlayerPawn> ().spawnPosition;
			otherPlayer.GetComponent<PlayerPawn> ().stepsMovedFromSpawn = 0;
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

	bool canPlayerMove(){
		return stepsMovedFromSpawn + boardObject.diceValue <= getFinalHouse ();
	}

	// Method generates position of player on board
	int getPostionOnBoard (int arrayIndex)
	{
		Debug.Log (arrayIndex);
		if (arrayIndex <= getFinalHouse() && arrayIndex>0)
			return int.Parse (pathPoints [arrayIndex].gameObject.name);
		else
			return 0;
	}

	public int getFinalHouse()
	{
		return safeHouseList [safeHouseList.Count - 1];
	}
		
}
