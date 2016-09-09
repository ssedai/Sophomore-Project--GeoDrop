using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highlight : MonoBehaviour {

	private SelectionCust sc;
	private SpriteRenderer sr;
	private Button bu;
	private Color col;

	//private bool hovering = false;

	// Use this for initialization
	void Start () {

		sc = GetComponentInParent <SelectionCust> ();
		sr = GetComponent <SpriteRenderer> ();
		bu = GetComponentInParent <Button> ();

		col = bu.colors.disabledColor;

	
	}
	
	// Update is called once per frame
	void Update () {

		if (sc.hovering || sc.selected)
			highlight ();
		else
			unHighlight ();

		if (bu.IsInteractable () == false)
			sr.color = col;
		//else
		//	sr.color = new Color (1f, 1f, 1f);
	
	}

	void highlight (){
		sr.color = new Color (1f, 0.5f, 0.5f, 0.7f);
	}

	void unHighlight(){
		sr.color = new Color (1f, 1f, 1f, 0.7f);
	}



}
