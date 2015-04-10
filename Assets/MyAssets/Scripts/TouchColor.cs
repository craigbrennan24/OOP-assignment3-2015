using UnityEngine;
using System.Collections;

public class TouchColor : MonoBehaviour {

	void Start()
	{
		GetComponent<Transform> ().localScale += new Vector3 (1.25f, 1.25f, 0);
	}
	// Update is called once per frame
	void Update () {

		if( GameController.playerIsTouching( GetComponent<Collider2D>() ) )
		{
			GetComponent<SpriteRenderer>().color = Color.green;
		}
		else
		{
			GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
}
