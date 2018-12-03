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

	public Vector3 spawnPosition;
	RectTransform playerButton;
	Board boardObject;
	GameRulesUtility ruleChecker;
	GameObject [] doubleFriend= new GameObject[1];

	public bool hasWon = false;
	public bool isEligibleForMove=false;
	public int isDouble = 0;
	bool isMyTurn = false;
	int diceValue;

	public int stepsMovedFromSpawn { get; private set;}
	int myLivePosition = 1;
	public int myCurrentPositionOnBoard{ get { return getPostionOnBoard (myLivePosition - 1); } }
	int myFinalDestinationOnBoard;

	public static readonly List <int> safeHouseList=new List<int> (){ 1, 5, 9, 13, 25 };

	void Awake()
	{
		spawnPosition = this.gameObject.transform.position;
	}
	void Start ()
	{
		stepsMovedFromSpawn = 0;
		boardObject = boardInput.GetComponent<Board> ();
		playerButton = GetComponent<RectTransform> ();
		ruleChecker = boardInput.GetComponent<GameRulesUtility> ();

	}

	public void movetonext ()
	{
		diceValue = boardObject.diceValue;
		isMyTurn = true;
		if(isEligibleForMove == false)
		{
			isEligibleForMove = true;
			boardObject.changePlayerTurnName ("PlayAgain");
			boardObject.diceButtonUI.GetComponent<Button> ().interactable = true;
			boardObject.diceValue=0;
			boardObject.StopPunchingAnimation ();
			this.gameObject.GetComponent<Outline> ().effectColor = Color.yellow;
		}
		else if (canPlayerMove () && isDouble == 0) {
			StartCoroutine (performPlayerMove ());
		}
		else if (canPlayerMove () && isDouble > 0 && diceValue % 2 == 0 ) {
			diceValue /= 2;
			StartCoroutine (performPlayerMove ());
		}
		else {
			//Popup: Move other pawn
		}
	}

	IEnumerator performPlayerMove()
	{
		boardObject.StopPunchingAnimation ();
		myFinalDestinationOnBoard = getPostionOnBoard (stepsMovedFromSpawn + diceValue);
		StartCoroutine (stepBystep ());
		yield return new WaitUntil (()=> !isMyTurn);
		boardObject.playerTurnChanger ();
		boardObject.diceButtonUI.GetComponent<Button> ().interactable = true;
		this.gameObject.GetComponent<Outline> ().effectColor = Color.white;
	}

	IEnumerator stepBystep () 
	{
		myLivePosition = stepsMovedFromSpawn;
		for (int j = stepsMovedFromSpawn; j <= stepsMovedFromSpawn + diceValue; j++) {
			playerButton.localPosition = pathPoints [j].gameObject.transform.localPosition;
			myLivePosition++;
			yield return new WaitForSeconds (0.3f);
		}
		stepsMovedFromSpawn += diceValue;
		yield return new WaitForSeconds (0.1f);
		isMyTurn = false;
		ruleChecker.hasMyPlayerPawnsWon (this.gameObject);	
	}
		
	void OnTriggerStay (Collider other)
	{
		GameObject otherPlayer = other.gameObject;

		if (hasReachedDestination()) {
			if (otherPlayer.tag == this.gameObject.tag && otherPlayer.GetComponent<PlayerPawn> ().stepsMovedFromSpawn != 0 && otherPlayer.GetComponent<PlayerPawn> ().isDouble == 0 && isDouble == 0) {
				otherPlayer.GetComponent<PlayerPawn> ().isDouble = isDouble = 1;
				doubleFriend [0] = otherPlayer;
				doubleFriend [0].SetActive (false);
				StartCoroutine (changeMySize ());
			}
				moveAwayFromOther (5, 5, otherPlayer);
			    performKill(otherPlayer);
		}
	}

	void performKill(GameObject otherPlayer)
	{
		if (this.gameObject.tag != otherPlayer.gameObject.tag && canIKill (otherPlayer)) {
			otherPlayer.transform.position = otherPlayer.GetComponent<PlayerPawn> ().spawnPosition;
			otherPlayer.GetComponent<PlayerPawn> ().stepsMovedFromSpawn = 0;
			otherPlayer.GetComponent<PlayerPawn> ().isEligibleForMove = false;
			Board.playerTurnValue--;
		}
	}
	bool hasReachedDestination()
	{
		return this.myCurrentPositionOnBoard == this.myFinalDestinationOnBoard && this.isMyTurn;
	}
	// Method checks if otherPlayer is killable
	bool canIKill (GameObject otherPlayer)
	{
		return
		!isSafeHouse (this.myCurrentPositionOnBoard) &&
		!isSafeHouse (this.myFinalDestinationOnBoard) &&
		this.gameObject.GetComponent<PlayerPawn> ().isDouble >= otherPlayer.gameObject.GetComponent<PlayerPawn> ().isDouble;
	}

	// Method checks if given position is safe house
	bool isSafeHouse (int position)
	{
		return safeHouseList.Contains (position);
	}

	bool canPlayerMove(){
		return stepsMovedFromSpawn + diceValue < getFinalHouse ();
	}

	// Method generates position of player on board
	int getPostionOnBoard (int arrayIndex)
	{
		if (arrayIndex <= getFinalHouse() && arrayIndex>0)
			return int.Parse (pathPoints [arrayIndex].gameObject.name);
		else
			return 0;
	}

	public int getFinalHouse()
	{
		return safeHouseList [safeHouseList.Count - 1];
	}
	void moveAwayFromOther(int x,int y, GameObject otherPlayer)
	{
		this.gameObject.transform.localPosition = new Vector3 (this.gameObject.transform.localPosition.x - x , this.gameObject.transform.localPosition.y);
		otherPlayer.gameObject.transform.localPosition = new Vector3 (otherPlayer.gameObject.transform.localPosition.x + y, otherPlayer.gameObject.transform.localPosition.y);
	}
	IEnumerator changeMySize()
	{
		this.gameObject.transform.localScale = new Vector3 (this.gameObject.transform.localScale.x *1.4f, this.gameObject.transform.localScale.y  *1.4f);
		this.gameObject.GetComponent<BoxCollider> ().size *= 2;
		yield return new WaitForSeconds (0.1f);
		this.gameObject.GetComponent<BoxCollider> ().size /= 2;
	}
}
