using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public class Block
	{
		int type;
		bool inPlay;
		public static int numBlockTypes = 7;
		public static float size = 0.75f;
		public Vector2 blickPos;
		public static Vector2 startPos = new Vector2 (-2.625f, -4.375f);


		//Stores value of finishing shapes with blocks
		//type - 0 = i, 1 = j, 2 = l, 3 = o, 4 = s, 5 = t, 6 = z
		public static int[] finishedShapePointValues = { 5, 8, 8, 7, 9, 7, 9 };  
		public static Color[] blockColors = { new Color( 255, 135, 0 ), //Orange
											  new Color( 0, 0, 255 ), //Dark Blue
											  new Color( 0, 255, 0 ), //Green
											  new Color( 255, 255, 0 ), //Yellow
											  new Color( 255, 0, 0 ), //Red
											  new Color( 255, 0, 255 ), //Pink
											  new Color( 0, 255, 255 ) //Cyan
		};

		public Block() : this( Random.Range(0,numBlockTypes-1) ){
			//If no parameters, create block at one of the spawnpoints with random type
		}

		public Block( Vector2 pos ) : this( pos, Random.Range(0,numBlockTypes-1) ){
			//If position parameter, create block at position with random type
		}

		public Block( int type ) : this( GameObject.FindWithTag("GameController").GetComponent<GameController>().spawnPoints[Random.Range(0, GameController.cols)], Random.Range(0,numBlockTypes-1) ){
			//if only type parameter, create block at one of the spawnpoints with specified type
		}

		public Block( Vector2 pos, int type )
		{
			blickPos = pos;
			this.type = type;
			inPlay = false;
		}

		public bool isInPlay()
		{
			return inPlay;
		}

		public int getType()
		{
			return type;
		}

		public void Update()
		{

		}

		void updateBlick()
		{

		}

		public Color getColor()
		{
			return blockColors [type];
		}
	}

	public Block block;
	Vector2 pos;
	float spawned;

	// Use this for initialization
	void Start () {
		spawned = Time.time;
		block = new Block (GameObject.FindWithTag("GameController").GetComponent<GameController>().findAvailableSpawnPoint());
		GetComponent<SpriteRenderer> ().color = block.getColor ();
		pos = GameObject.FindWithTag ("GameController").GetComponent<GameController> ().blockPositions [(int)block.blickPos.x, (int)block.blickPos.y];

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - spawned > 5)
			Destroy (gameObject);
		block.Update ();
		float z_t = GetComponent<Transform> ().position.z;
		Vector2 t = GameObject.FindWithTag("GameController").GetComponent<GameController>().blockPositions[(int)block.blickPos.x, (int)block.blickPos.y];
		Vector3 cmp = new Vector3( t.x, t.y, z_t );
		if( GetComponent<Transform>().position != cmp ){
			GetComponent<Transform>().position = cmp;
		}

	}
}
