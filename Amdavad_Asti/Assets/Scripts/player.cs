using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour 
{
	public GameObject[] pathPoints;
	RectTransform playerButton;
	public GameObject boardScript;
	Vector3 initialPosition;
	int stepValue = 0;


	void Start()
	{
		initialPosition = this.transform.position;
		playerButton = GetComponent<RectTransform> ();
	}
	public void movetonext()
	{
		Debug.Log ("Step value "+stepValue);
		boardScript.GetComponent<Board> ().StopPunching ();

		StartCoroutine (stepBystep());

	}
	IEnumerator stepBystep()
	{
		for(int j = stepValue;j<=stepValue+Board.diceValue;j++)
			{
				playerButton.localPosition = pathPoints [j].gameObject.transform.localPosition;
				yield return new WaitForSeconds (0.3f);
			}
		stepValue += Board.diceValue; 
		this.gameObject.GetComponent<Animator> ().enabled = false;
		this.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(12,12);
		yield return new WaitForSeconds (1.5f);

	}
	void OnTriggerStay(Collider other)
	{
			if(Board.playerTurnValue == 1)
			{
				if(other.gameObject.tag == "player2")
				{
					this.transform.position = initialPosition;
					stepValue = 0;
			//	playerButton.localPosition = pathPoints [0].gameObject.transform.position;
				}
			}
		if(Board.playerTurnValue == 2)
		{
			if(other.gameObject.tag == "player1")
			{
				this.transform.position = initialPosition;
				stepValue = 0;
			//	playerButton.localPosition = pathPoints [0].gameObject.transform.position;
			}
		}

	}
}
