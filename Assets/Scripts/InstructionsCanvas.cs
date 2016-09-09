using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionsCanvas : MonoBehaviour {

	//Setting up Instructions Canvas
	PlayerPrefsManager ppM;
	public Canvas instCanvas;
	private LevelManager lm;
	public Toggle toggle;
	private Magnify cam;


	// Use this for initialization
	void Start () {
		//instCanvas = GameObject.FindGameObjectWithTag ("Instructions Panel").GetComponent <Canvas> ();
		//toggle = GameObject.Find ("Toggle").GetComponent <Toggle> ();
		lm = FindObjectOfType <LevelManager> ();
		cam = FindObjectOfType <Magnify> ();

		//Display the Instructions canvas
		ppM = FindObjectOfType <PlayerPrefsManager>();


	}


	


	//Displays the instruction panel
	public void dispInstPanel(){
		cam.stopMagnifier ();
		instCanvas.gameObject.SetActive (true);
		lm.sethasStarted (false); //Pauses the timer and disables body parts from being clickable.
		//Decides whether the "Show Again?" toggle should be checked or unchecked.
		if (ppM.getLevelInst () == 1)
			toggle.isOn = true;
		else
			toggle.isOn = false;
		
		lm.sethasStarted (false);  //Pauses the timer and disables body parts from being clickable.
	}

	//Closes the panel.
	public void ClosePanel (){
		instCanvas.gameObject.SetActive (false);
		//Resumes the timer and enables body parts to be clickable again.
		lm.sethasStarted (true);
		cam.toggleMagnifier ();
	}

	//Toggles the hasRead boolean to true or false.
	public void toggleHR (){
		ppM.toggleHR ();
	}



}
