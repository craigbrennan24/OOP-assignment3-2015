using UnityEngine;
using System.Collections;

public class TouchColor : MonoBehaviour {

	void Start()
	{
	}
	// Update is called once per frame
	void Update () {
		/*if (Input.touchCount == 1) {
			Vector3 wp = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2 (wp.x, wp.y);
			if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
				GetComponent<SpriteRenderer> ().color = Color.yellow;
			} else {
				GetComponent<SpriteRenderer> ().color = Color.red;
			}
		} else {
			GetComponent<SpriteRenderer> ().color = Color.blue;
		}*/
	}
}
