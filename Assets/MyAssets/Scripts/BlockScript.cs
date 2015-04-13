using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public Block block;
	Vector2 pos;

	// Use this for initialization
	void Start () {
		block = new Block ();
		if (GameController.accessGameController ().customSpawn) {
			//If I want to spawn a block at a custom location
			block.moveBlock(GetComponent<Transform> ().position);
		} else {
			//Give player control of the new falling block
			block.givePlayerControl ();
		}
		GetComponent<SpriteRenderer> ().color = block.getColor ();
	}

	// Update is called once per frame
	void Update () {
		if (!GameController._gameOver && !GameController.paused) {
			block.update ();
			verifyPosColor ();
		}
	}

	public void Remove()
	{
		Destroy (gameObject);
	}

	void verifyPosColor()
	{
		//Used to check to make sure the block gameobject matches up with its block
		pos = GameController.accessGameController().blockPositions [(int)block.blickPos.x, (int)block.blickPos.y];
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

