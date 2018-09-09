using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour 
{
	public GameObject[] pathPoints;
	RectTransform playerButton;
	public GameObject boardScript;
	public Vector3 initialPosition;
	public int stepValue = 0;
	bool myturn = false;

	void Start()
	{
		initialPosition = this.transform.position;
		playerButton = GetComponent<RectTransform> ();
	}
	public void movetonext()
	{
		Debug.Log ("Step value "+stepValue);
		boardScript.GetComponent<Board> ().StopPunching ();
		myturn = true;
		StartCoroutine (stepBystep());
	}
	IEnumerator stepBystep()
	{
		for(int j = stepValue;j<=stepValue+Board.diceValue;j++)
			{
				playerButton.localPosition = pathPoints [j].gameObject.transform.localPosition;
				yield return new WaitForSeconds (0.3f);
			Debug.Log ("My turn Value before : "+myturn);
			}
		myturn = false;
		Debug.Log ("My turn Value a : "+myturn);
		stepValue += Board.diceValue; 
		this.gameObject.GetComponent<Animator> ().enabled = false;
		this.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(12,12);
	}
//	void OnTriggerEnter(Collider other)
//	{
//			if(this.gameObject.tag != other.gameObject.tag)
//			{
//				other.gameObject.transform.position = other.GetComponent<player>().initialPosition;
//				other.gameObject.GetComponent<player> ().stepValue = 0;
//			}
//	}
}
