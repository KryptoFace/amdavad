using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Board : MonoBehaviour {

	[SerializeField] Text showDiceValue;
	[SerializeField] Text showPlayerTurn;
	[SerializeField] GameObject[] player1_pawns;
	[SerializeField] GameObject[] player2_pawns;
	[SerializeField] GameObject[] player3_pawns;
	[SerializeField] GameObject[] player4_pawns;

	public int totalPlayers=4;
	public GameObject diceButtonUI;
	public static int playerTurnValue = 0;
	public int diceValue;
	public Dictionary <string,GameObject[]> playerPawnMap{ get; private set;}
	public List <string> playerNameMap{ get; private set;}

	GameRulesUtility ruleChecker;

	void Start()
	{
		ruleChecker = this.gameObject.GetComponent<GameRulesUtility> ();
		playerNameMap= new List<string>(){"Red","Green","Pink","Blue"};
		playerPawnMap = new Dictionary <string,GameObject[]> ()
		{ {player1_pawns[0].tag,player1_pawns}, {player2_pawns[0].tag,player2_pawns}, {player3_pawns[0].tag,player3_pawns}, {player4_pawns[0].tag,player4_pawns} };
		changePlayerTurnName (playerNameMap [playerTurnValue]);
		removeNonPlayablePlayers ();
	}

	void Update () 
	{
		
	}
	public void StartPunchingAnimation()
	{
		bool ignore;
		playerPunchingExecuter (true, out ignore);
	}
	public void StartPunchingAnimation(out bool changeTurn)
	{
		playerPunchingExecuter (true, out changeTurn);
	}
	public void StopPunchingAnimation()
	{
		bool ignore;
		playerPunchingExecuter (false,out ignore);
	}

	void playerPunchingExecuter(bool isEnabled, out bool changeTurn)
	{
		changeTurn = true;
		GameObject[] playerPawn=playerPawnMap.ElementAt(playerTurnValue).Value;
		for(int i=0;i<playerPawn.Length;i++)
		{
			if(isEnabled == false)
			{
				playerActivator(playerPawn[i], isEnabled);
			}
			else if (ruleChecker.canAnyElgiblePlayerMove(playerPawn [i],diceValue)) {
				changeTurn = false;
				playerActivator(playerPawn[i], isEnabled);
			}
		}
	}

	void playerActivator(GameObject playerPawn,bool isEnabled)
	{
		playerPawn.GetComponent<Animator> ().enabled = isEnabled;
		playerPawn.GetComponent<Button> ().interactable = isEnabled;
		playerPawn.GetComponent<RectTransform> ().sizeDelta = new Vector2 (12, 12);
	}
	public void rollDice()
	{
		diceValue =Random.Range (1,5);
		showDiceValue.text = diceValue.ToString();
		bool skipTurn;
		this.gameObject.GetComponent<Board> ().StartPunchingAnimation (out skipTurn);
		if (skipTurn) {
			playerTurnChanger ();
		} else {
			diceButtonUI.GetComponent<Button> ().interactable = false;	
		}
	}
	public void playerTurnChanger()
	{
		if (playerTurnValue == playerPawnMap.Count - 1) {
			playerTurnValue = 0;
		} 
		else {
			playerTurnValue++;
		}
		changePlayerTurnName (playerNameMap [playerTurnValue]);
	}

	public void playTurnAgain()
	{
		diceButtonUI.GetComponent<Button> ().interactable = true;	
	}

	public void removeWinnerPlayer(string playerTag)
	{
		playerPawnMap.Remove (playerTag);
		playerTurnValue = 0;
	}
	public GameObject[] getAllPlayerPawns(string playerTag)
	{
		GameObject[] allPawns;
		playerPawnMap.TryGetValue (playerTag, out allPawns);

		return allPawns;
	}

	public void changePlayerTurnName(string text)
	{
		showPlayerTurn.text = text ;
	}

	void removeNonPlayablePlayers()
	{
		for (int i = totalPlayers+1; i <= 4; i++) {
			playerPawnMap.Remove (i.ToString());
		}
	}
		
}
