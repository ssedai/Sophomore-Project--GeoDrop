using UnityEngine;
using System.Collections;

public class SelectionCust : MonoBehaviour {

	public bool selected;
	public bool hovering = false;

	// Use this for initialization
	void Start () {
		selected = false;
	
	}
	

	void OnMouseClick (){
		selected = true;
	}

	public void Enter (){
		hovering = true;
	}

	public void Exit (){
		hovering = false;
	}
}
