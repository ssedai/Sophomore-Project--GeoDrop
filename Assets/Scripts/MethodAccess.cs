using UnityEngine;
using System.Collections;

//A list of methods to be placed on a button who does not have the appropriate script attached.
//This makes prefab buttons easier to work with.
public class MethodAccess : MonoBehaviour {

	private LevelManager lm;
	private Magnify cam;
	private PlayerManager pm;

	// Use this for initialization
	void Start () {
		lm = FindObjectOfType <LevelManager> ();
		cam = FindObjectOfType <Magnify> ();
		pm = FindObjectOfType <PlayerManager> ();
	}

	public void restart(){
		lm.restart ();
	}

	public void toggleMagnifier(){
		cam.toggleMagnifier ();
	}

	public void toggleLockMagnifier(){
		cam.toggleLockMagnifier ();
	}

	public void loadLevelSelect(){
		lm.loadLevel ("LevelSelect");
	}

	public void resetLevel(){
		lm.resetLevel ();
	}

	public void loadNextLevel(){
		lm.loadNextLevel ();
	}

	public void setProf(string expertise){
		pm.setProf (expertise);
	}
}
