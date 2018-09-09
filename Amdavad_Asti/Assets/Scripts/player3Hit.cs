using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player3Hit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
		{
			if(Board.playerTurnValue == 2)
				{
					if(this.gameObject.tag != other.gameObject.tag)
					{
						other.gameObject.transform.position = other.GetComponent<player>().initialPosition;
						other.gameObject.GetComponent<player> ().stepValue = 0;
					}
				}
		}
}
