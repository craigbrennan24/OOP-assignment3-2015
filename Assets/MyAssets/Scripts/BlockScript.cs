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
			block.moveBlock(GetComponent<Transform> ().position);
		} else {
			//Give player control of the new falling block
			block.givePlayerControl ();
		}
		GetComponent<SpriteRenderer> ().color = block.getColor ();
		pos = GameController.accessGameController().blockPositions [(int)block.blickPos.x, (int)block.blickPos.y];
	}

	// Update is called once per frame
	void Update () {
		block.update ();
		verifyPosColor ();
	}

	void verifyPosColor()
	{
		//Used to check to make sure the block gameobject matches up with its block
		float z_t = GetComponent<Transform> ().position.z;
		Vector3 cmp = new Vector3( pos.x, pos.y, z_t );
		if( GetComponent<Transform>().position != cmp ){
			GetComponent<Transform>().position = cmp;
		}
		if (GetComponent<SpriteRenderer>().color != block.getColor ()) {
			GetComponent<SpriteRenderer>().color = block.getColor();
		}
	}
}

