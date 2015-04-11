using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public Block block;
	Vector2 pos;
	float spawned;

	// Use this for initialization
	void Start () {
		spawned = Time.time;
		block = new Block ();
		if (GameController.accessGameController ().customSpawn) {
			//If I want to spawn a block at a custom location
			block = new Block (GetComponent<Transform>().position);
		}
		GetComponent<SpriteRenderer> ().color = block.getColor ();
		pos = GameObject.FindWithTag ("GameController").GetComponent<GameController> ().blockPositions [(int)block.blickPos.x, (int)block.blickPos.y];
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Time.time - spawned > 4)
			Destroy (gameObject);*/
		block.update ();
		float z_t = GetComponent<Transform> ().position.z;
		Vector3 cmp = new Vector3( pos.x, pos.y, z_t );
		if( GetComponent<Transform>().position != cmp ){
			GetComponent<Transform>().position = cmp;
		}

	}
}

