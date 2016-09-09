using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]  //Ensure Object has a Box Collider.
public class DragAndSnap : MonoBehaviour {

	//Setting up Mouse Drag and Drop properties
	private Vector3 screenPoint;
	private Vector3 offset;
	private float releaseScale = 10f;
	private float snapScale = 15f;

	//Setting up Snap location variables
	private float xParent;
	private float yParent;
	private float xRangeHigh;
	private float xRangeLow;
	private float yRangeHigh;
	private float yRangeLow;
	private const int OFFSET = 15; //The allowed leeway for placing the body part in the correct spot.

	//Setting up booleans to ensure object only snaps in place when correct x and y paramaters are met.
	private bool xPos = false;
	private bool yPos = false;
	private bool isClickable = true;  //If locked in place, set to false to prevent movability.

	//Setting up fade setting after object locks in place.
	private float alpha = 0.6f;
	private SpriteRenderer spriteRenderer;

	//Setting up LevelManager script access
	private LevelManager levelManager;

	//Setting up Attempts
	//private int guesses = 0;

	//Setting up point value constants
	//This is the value for the threshold of each scoring tier.  The numbers represent the number of guesses made.
	private const int THRESH0 = 0;
	private const int THRESH1 = 1;
	private const int THRESH2 = 3;
	private const int THRESH3 = 8;

	//This is the scoring tier for each threshold of attempts.  The numbers are the number of points awarded based on the number of guesses.
	private const int SCORE1 = 5;
	private const int SCORE2 = 3;
	private const int SCORE3 = 1;
	private const int SCORE0 = 0;

	//Setting up the Audio and Sprite Hints
	public Button hint2Toggle;
	public AudioSource incorrect; //Obsolete.
	public AudioSource cold;
	public AudioSource warm;
	public AudioSource hot;
	private float warmRange;
	private float hotRange;
	public SpriteRenderer hintBox;

	//BoxCollider needs to be removed when the object is placed.
	private PolygonCollider2D boxCol;

	//Setting up Quiz if we need to have a level with a quiz after object is placed.
	//private QuizPopup quizP;


	//Setting up Win Condition
	private bool bodyPart;

	//Setting up Stat Tracking
	private StatTracking statTracking;
	private ReportSelf rs;
	private bool isGuessing = false;  //Boolean to check if body part is dropped in the Object Pane or not
	private bool correctFirstTry = true;



	// Use this for initialization
	void Start () {

		//Get the position of the parent.
		//This is the correct position to place the object.
		xParent = transform.root.position.x;
		yParent = transform.root.position.y;

		//Set hint button toggles to inactive.
		hint2Toggle.gameObject.SetActive (false);

		//Find the Sprite Renderer for fading purposes.
		spriteRenderer = GetComponent<SpriteRenderer> ();

		//Find the box collider so it can be removed when object is placed.
		boxCol = GetComponent <PolygonCollider2D> ();

		//Sets a range of + or - the Offset (15) within the correct position.
		xRangeHigh = xParent + OFFSET;
		xRangeLow = xParent - OFFSET;
		yRangeHigh = yParent + OFFSET;
		yRangeLow = yParent - OFFSET;

		//Set range for audio hint system.
		warmRange = 120;
		hotRange = 15;

		//Make the sprite hint invisible at the start.
		hintBox.enabled = false;
		hintBox.color = new Color (1f, 1f, 1f, 0f);


		//Find the Level Manager.
		levelManager = FindObjectOfType<LevelManager> ();

		//Find the Stat Tracking and ReportSelf Scripts.
		statTracking = FindObjectOfType<StatTracking>();
		rs = GetComponentInChildren<ReportSelf> ();


		//Bony Structure level needs a different snap and release scaling because the bones are small.
		if (SceneManager.GetActiveScene ().name == "UpperArm6") {
			releaseScale = 30f;
			snapScale = 18f;


			//Following is code for if we decide to have both drag and drop AND quiz questions in one level.
			//levelManager.noObjections = false;
			//quizP = GetComponent <QuizPopup> ();
		} else if (SceneManager.GetActiveScene ().name == "Forearm1A" || SceneManager.GetActiveScene ().name == "Forearm1B") {
			releaseScale = 10f;
			snapScale = 10f;
		} else if (SceneManager.GetActiveScene ().name == "Hand1A") {
			releaseScale = 15f;
			snapScale = 20f;
		}
			


		//Make sure that myAttempt starts at 0
		//guesses = 0;

		//Count all body parts
		bodyPart = (this.tag == "unPlaced");
		if (bodyPart) {
			levelManager.startObjects++;
			levelManager.incMuscleCount();
		}





	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//Select the object with the mouse.
	void OnMouseDown(){
		if (isClickable && levelManager.gethasStarted () == true && !levelManager.canvasOpen) {
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);

			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

			//Scales the object to a larger size.
			transform.localScale = new Vector3 (snapScale, snapScale, snapScale);
		}
	}

	//Drag the object with the mouse.
	void OnMouseDrag(){
		//If object is NOT locked in place.
		if (isClickable && levelManager.gethasStarted () == true && !levelManager.canvasOpen) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
			transform.position = curPosition;

			//Scales the object to a larger size.
			transform.localScale = new Vector3 (snapScale, snapScale, snapScale);
		}
	}
	//When the mouse button is released...
	void OnMouseUp(){
		if (isClickable && levelManager.gethasStarted () == true && !levelManager.canvasOpen) {
			//Enable the Sprite Hint button when guesses exceed 11.
			if (rs.getGuesses() >= 11) {
				hint2Toggle.gameObject.SetActive (true);
			}
			getPosx ();  //Get the X position and check compared to the correct X position.
			getPosy ();  //Get the Y position and check compared to the correct Y position.
			clearBools ();  //If incorrect, ensure that the booleans are cleared.
			rs.reportSelf (); //Updates the reportSelf script with the number of guesses, whether the object was placed, and if a hint was used.
			if (xPos && yPos) {
				hintBox.enabled = false; //Turn off the hint box if it was on.
				boxCol.enabled = false; //Disable the box collider so it's not in any other objects' way.
				hintBox.color = new Color (1f, 1f, 1f, 0f); //Clears hint box if it is active.
				setPos ();  //Lock object in place if X AND Y positions are in place.
				hint2Toggle.gameObject.SetActive (false); //Set image hint button toggle to inactive.
				if (rs.getHint() == true) {
					rs.toggleHintFalse(); //Set hint 2 bool back to false.
				}
				// Following is used IF we have a level that has a quiz question AND you have to drag it in place!
				 /* if (statTracking.identifyLevel () == "Q5") {
					quizQuestion1 ();
				} else {
					*/levelManager.startObjects--;
				levelManager.addMuscPlaced ();
				//}
			}
		}
	}

	//Check the X position with the parent X position
	//If in the right parameters, set xPos to true
	void getPosx(){
		if (transform.position.x >= xRangeLow && transform.position.x <= xRangeHigh) {
			xPos = true;
		}
	}

	//Check the Y position with the parent Y position
	//If in the right parameters, set yPos to true
	void getPosy(){
		if (transform.position.y >= yRangeLow && transform.position.y <= yRangeHigh) {
			yPos = true;
		}
	}

	//If X OR Y is true (but not both), make sure to reset to false.
	void clearBools(){
		if (!xPos || !yPos) {
			xPos = false;
			yPos = false;

			//Shrinks object back down to default size.
			transform.localScale = new Vector3 (releaseScale, releaseScale, releaseScale);

			//If the object is NOT in the Object Pane...
			if (isGuessing){
				hintSound();
				levelManager.attempts++;
				rs.addGuess ();
				if (rs.getGuesses () == 1)
					correctFirstTry = false;
			}
		}
	}

	//Snap the object to the parent location and lock it in place.
	void setPos(){
		transform.position = new Vector2 (xParent, yParent);
		isClickable = false;
		if (statTracking.identifyLevel () == "Q5")
			levelManager.noObjections = true;
		if (correctFirstTry)
			levelManager.testScoreAddition ();

		//Check the  number of guesses for this body part and give the appropriate points.
		if (rs.getGuesses() == THRESH0) {
			levelManager.score += SCORE1;
		} else if (rs.getGuesses() >= THRESH1 && rs.getGuesses() <= THRESH2) {
			levelManager.score += SCORE2;
		} else if (rs.getGuesses() > THRESH2 && rs.getGuesses() <= THRESH3) {
			levelManager.score += SCORE3;
		} else if (rs.getGuesses() > THRESH3) {
			levelManager.score += SCORE0;
		}
		spriteRenderer.color = new Color (1f, 1f, 1f, alpha);
		transform.gameObject.tag = "Placed";
		rs.reportSelf (); //Report Self sends the message of the number of guesses, whether or not it was placed, and whether or not a hint was used.
	}

	//Plays a sound based on how close the body part is to its proper location.
	void hintSound(){
		if (transform.position.x >= xRangeLow - hotRange && transform.position.x <= xRangeHigh + hotRange) {
			hot.Play();
		}
		else if (transform.position.x >= xRangeLow - warmRange && transform.position.x <= xRangeHigh + warmRange) {
			warm.Play ();
		} else {
			cold.Play();
		}
	}

	//enables the hint sprite and sets is alpha to a visible color.
	void hintSprite(){
		if (!xPos || !yPos) {
			hintBox.enabled = true;
			hintBox.color = new Color (0.15f, 0.7f, 0.9f, 0.85f);
		}
	}

	//Turns the hint sprite on if the hint button is pressed.
	//Sets the hintUsed boolean to true as well.
	public void toggleSpriteHint(){
		rs.toggleHintTrue();
		if (rs.getHint()==true){
			hintSprite();
			rs.reportSelf ();
		}
	}

	//Toggles the isGuessing boolean to true or false.
	//IsGuessing will prevent a guess being added and a sound playing
	//while the object is in the Object Pane.
	public void toggleGuessing(bool g){
		isGuessing = g;
	}

	//If the body part is placed in the Object Pane, don't add a guess.
	void OnTriggerEnter2D (Collider2D col){
		if (col.tag == "ObjectPane")
		toggleGuessing (false);
	}

	//When the object is placed outside of the Object Pane, add a guess.
	void OnTriggerExit2D (Collider2D col){
		if (col.tag == "ObjectPane")
		toggleGuessing (true);
	}
}
