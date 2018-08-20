using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour 
{
	public GameObject[] pathPoints;
	RectTransform playerButton;
	public GameObject boardScript;

	int stepValue = 0;

	void Start()
	{
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
		for(int j = stepValue;j<=stepValue+Dice.diceValue;j++)
			{
				playerButton.localPosition = pathPoints [j].gameObject.transform.localPosition;
				yield return new WaitForSeconds (0.3f);
			}
		stepValue += Dice.diceValue; 

		this.gameObject.GetComponent<Animator> ().enabled = false;
		this.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(12,12);
//
//		GameObject.FindGameObjectWithTag ("player").GetComponent<Animator>().enabled = false;
//		GameObject.FindGameObjectWithTag ("player").GetComponent<RectTransform> ().sizeDelta = new Vector2(12,12);
	}
}
