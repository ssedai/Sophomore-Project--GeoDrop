using UnityEngine;
using System.Collections;

public class Magnify : MonoBehaviour {
	private Camera cam;
	private float WIDTH = Screen.width/5;
	private float HEIGHT = Screen.height/5;
	private bool fullzoom = false;
	private bool on = true;
	private bool locked = true;

	// Use this for initialization
	void Start () {
		cam = GetComponent <Camera> ();
		cam.orthographicSize = 50;
		on = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (on) {
			WIDTH = Screen.width / 5;
			HEIGHT = Screen.height / 4;
			if (!locked) {
				if (this.transform.position.x < -200)
					cam.pixelRect = (new Rect (Input.mousePosition.x + WIDTH / 8, Input.mousePosition.y + HEIGHT / 4, WIDTH, HEIGHT));
				else
					cam.pixelRect = (new Rect (Input.mousePosition.x - WIDTH / 4, Input.mousePosition.y + HEIGHT / 4, WIDTH, HEIGHT));
			} else {
				cam.pixelRect = (new Rect (0, 0, WIDTH, HEIGHT));
			}
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
			transform.position = curPosition;


			if (Input.GetKeyDown (KeyCode.Z))
				ZoomControl ();

			if (Input.GetKeyDown (KeyCode.X))
				toggleLockMagnifier ();
			

		}
	}


	void ZoomControl ()
	{
		fullzoom = !fullzoom;

		if (fullzoom)
			cam.orthographicSize = 25;
		else
			cam.orthographicSize = 50;
	}

	public void toggleMagnifier ()
	{
		on = !on;

		if (on)
			cam.gameObject.SetActive (true);
		else
			cam.gameObject.SetActive (false);
	}

	public void stopMagnifier() {
		on = false;

		cam.gameObject.SetActive (false);
	}

	public void toggleLockMagnifier(){
		locked = !locked;
	}
}
