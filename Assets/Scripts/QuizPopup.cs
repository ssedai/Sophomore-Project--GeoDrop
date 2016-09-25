using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]  //Ensure Object has a Polygon Collider.
public class QuizPopup : MonoBehaviour {

	//Setting up fade setting after question is answered.
	public float alpha = 1.0f; //The alpha (visibility) of the sprite
	private SpriteRenderer childSprite; //The sprite renderer of the object
	private PolygonCollider2D[] polyCol; //The Collider of the object (To be removed once set in place)
	public bool isClickable = true;  //Once question is answered, set to false.
	public bool answered = false;  //Set to true when the question is answered for.

	//Will need access to LevelManager script!
	private LevelManager levelManager;
	//Also the StatTracking script.
	private StatTracking statTracking;



	//Setting up Quiz
	public Canvas[] question; //The Canvas that hosts the question.
	private int numQ; //Number of questions in the Question Canvas Arryay.
	private int Qnum = 0; //The current question being asked in Question Canvas Array.


	//Setting up Win Condition
	private bool piece;

	// Use this for initialization
	void Start () {

		//parentSprite = GetComponentInParent <SpriteRenderer> ();
		childSprite = GetComponent <SpriteRenderer> ();

		//Find the needed scripts.
		levelManager = FindObjectOfType<LevelManager> ();
		statTracking = FindObjectOfType <StatTracking> ();

		//Find the Stat Tracker.
		//statTracking = FindObjectOfType<StatTracking>();

		//parentSprite.color = new Color (1f, 1f, 1f, 1f);
		polyCol = GetComponents <PolygonCollider2D> ();

		//Get number of questions for body part by getting the Length of the array.
		numQ = question.Length;

		//Count all body parts and questions
		piece = (this.tag == "unPlaced");
		if (piece) {
			//levelManager.startObjects++;
			levelManager.addObjCount(numQ);
		}



		if (SceneManager.GetActiveScene ().name == "Armbone0" || SceneManager.GetActiveScene ().name == "Armbone1" || SceneManager.GetActiveScene ().name == "Armbone2" || SceneManager.GetActiveScene ().name == "Armbone3" || SceneManager.GetActiveScene ().name == "Armbone4") {
			childSprite.color = new Color (0.9f, 0.5f, 0.5f, 1f);
		}

		if (statTracking.identifyLevel () == "Q0" || statTracking.identifyLevel () == "QC") {
				setCanvas (Qnum, true);
				isClickable = false;

		}
	}
	
	// Update is called once per frame
	void Update () {

		if (answered) {
			setAlpha();
			//parentSprite.color = new Color (1, 1, 1, 0);
			for (int n = 0; n < polyCol.Length; n++)
				polyCol [n].enabled = false;
		}
	}

	void OnMouseUp (){
		if (levelManager.noObjections == true && isClickable == true && levelManager.gethasStarted () == true) {
			//setCanvas(Qnum, true);
			//isClickable = false;

		}

	}

	public int getCanvasLength(){
		//print (name + " " + numQ);
		return numQ;
	}

	public int reduceCanvasLength(){
		numQ--;
		//print (name + " " + numQ);
		return numQ;
		
	}

	public int getQuestionN(){
		//print (name + " " + Qnum);
		return Qnum;
	}

	public int nextQuestionN(){
		Qnum++;
		//print (name + " " + Qnum);
		return Qnum;
	}

	public void setCanvas(int q, bool sw){
		question [q].gameObject.SetActive (sw);
	}


	//hilight object when mouse hovers over it.
	void OnMouseEnter (){
		if (isClickable && levelManager.noObjections) {
			childSprite.color = new Color (0.2f, 0.2f, 1f, 1f);
		}
	}

	//Un-hilight object when mouse no longer hovers over it.
	void OnMouseExit (){
		if (isClickable || levelManager.noObjections) {
			if (SceneManager.GetActiveScene ().name == "Armbone0" || SceneManager.GetActiveScene ().name == "Armbone1" || SceneManager.GetActiveScene ().name == "Armbone2" || SceneManager.GetActiveScene ().name == "Armbone3" || SceneManager.GetActiveScene ().name == "Armbone4")
				childSprite.color = new Color (0.9f, 0.5f, 0.5f, 1f);
			else
			childSprite.color = new Color (255, 255, 255, 1);
		}
	}

	public void setAlpha (){
		if (answered) {
			childSprite.color = new Color (255, 255, 255, alpha);
		}
	}
}
