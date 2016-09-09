using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour {

	//Tooltips
	public GameObject tooltip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	/*void Update () {
	
	}*/

	public void showToolTip(){
		tooltip.gameObject.SetActive (true);
	}

	public void hideToolTip(){
		tooltip.gameObject.SetActive (false);
	}
}
