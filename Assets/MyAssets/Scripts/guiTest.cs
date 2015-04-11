using UnityEngine;
using System.Collections;

public class guiTest : MonoBehaviour {
		//TOUCH TEST SCRIPT
		public GameController g;
		public Vector2 t = new Vector2(0,0);
		Vector2 t1 = new Vector2 (0, 0);
		Vector2 t2 = new Vector2 (0, 0);
		Vector2 cmp = new Vector2(0,0);
		
		void OnGUI()
		{
			string s = "Test";
			float x = 100;
			string s1, s2;
			if (t != cmp)
			{
				s = t.x + "," + t.y;
				s1 = t1.x + "," + t1.y;
				s2 = t2.x + "," + t2.y;
				GUI.TextArea (new Rect ((Screen.width / 2) - x, (Screen.height / 2) - x, x, x), s + "\n" + s1 + "\n" + s2); 
			}
			else
			{
				GUI.TextArea (new Rect ((Screen.width / 2) - x, (Screen.height / 2) - x, x, x / 2), s); 
			}
		}

		void Update()
		{

			if (Input.touchCount > 0) {
			t = Input.GetTouch(0).position;
			t1 = Input.GetTouch(0).deltaPosition;
			t2 = Input.GetTouch(0).rawPosition;
			}
		}
}
