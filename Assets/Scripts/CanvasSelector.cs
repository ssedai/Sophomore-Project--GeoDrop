using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasSelector : MonoBehaviour {

	private Canvas boneCan;
	private Canvas muscleCan;
	//private Canvas nervousCan;

	// Use this for initialization
	void Start () {
		boneCan = GameObject.Find ("Bone Canvas").GetComponent <Canvas> (); 
		muscleCan = GameObject.Find ("Muscle Canvas").GetComponent <Canvas> ();
		//nervousCan = GameObject.Find ("Nervous Canvas").GetComponent <Canvas> ();

		boneCan.gameObject.SetActive (true);
		muscleCan.gameObject.SetActive (false);
		//nervousCan.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void boneUp(){
		boneCan.gameObject.SetActive (true);
		muscleCan.gameObject.SetActive (false);
		//nervousCan.gameObject.SetActive (false);
	}

	public void muscleUp(){
		muscleCan.gameObject.SetActive (true);
		boneCan.gameObject.SetActive (false);
		//nervousCan.gameObject.SetActive (false);
	}

	/*public void nerveUp(){
		nervousCan.gameObject.SetActive (true);
		boneCan.gameObject.SetActive (false);
		muscleCan.gameObject.SetActive (false);
	}*/
}
