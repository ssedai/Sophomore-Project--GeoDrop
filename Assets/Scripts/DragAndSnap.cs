using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]  //Ensure Object has a Polygon Collider.
public class DragAndSnap : MonoBehaviour {

	//Setting up Mouse Drag and Drop properties
	private Vector3 screenPoint; //Used with drag and drop raycasting
	private Vector3 offset; //Used with drag and drop raycasting
	private float releaseScale = 50f; //Size of the object when the mouse is released (should be the object's initial scale)
	private float snapScale = 50f; //Size of the object when it is selected/placed in its correct position

	//Setting up Snap location variables
	//The x and y positions of the correct location
	private float xParent;
	private float yParent;
	//The range of the "buffer" zone for correct placement in terms of x and y positions for the parent
	private float xRangeHigh; //X position + Offset
	private float xRangeLow; //X position - Offset
	private float yRangeHigh; //Y position + Offset
	private float yRangeLow; //Y position - Offset
	private const int OFFSET = 10; //The allowed leeway for placing the body part in the correct spot.

	//Setting up booleans to ensure object only snaps in place when correct x and y paramaters are met.
	private bool xPos = false;
	private bool yPos = false;
	private bool isClickable = true;  //If locked in place, set to false to prevent movability.

	//Setting up fade setting after object locks in place.
	private float alpha = 1.0f; //TODO: Replace fade-in/fade-out with a color (single color for correct placement)
	private SpriteRenderer spriteRenderer; //The sprite renderer for the game object

	//Setting up LevelManager script access
	private LevelManager levelManager;

	//Setting up Attempts
	//private int guesses = 0;  //NOTE: This is handled through ReportSelf now.

	//Setting up point value constants
	//This is the value for the threshold of each scoring tier.  The numbers represent the number of guesses made before new tier is reached.
	private const int THRESH0 = 0;
	private const int THRESH1 = 1;
	private const int THRESH2 = 3;
	private const int THRESH3 = 8;

	//This is the scoring tier for each threshold of attempts.  The numbers are the number of points awarded based on the number of guesses (above)
	private const int SCORE1 = 5;
	private const int SCORE2 = 3;
	private const int SCORE3 = 1;
	private const int SCORE0 = 0;

	/**
	 * Example of scoring tier.  Someone makes 3 guesses (THRESH2), they will be awarded 1 Point (SCORE3).
	 */

	//Setting up the Audio and Sprite Hints
	public Button hint2Toggle;  //Button for the sprite location hint.  May need to rework this or remove it altogether.
	public AudioSource incorrect; //A sound that plays if piece is placed in the wrong spot
	//If sound hint is on, following sounds will play according to the object's placed spot and how far object is from the correct spot.
	public AudioSource cold;
	public AudioSource warm;
	public AudioSource hot;
	//The ranges for how close the object needs to be to play a "warm" or "hot" sound.
	private float warmRange;
	private float hotRange;
	public SpriteRenderer hintBox; //This gets the sprite of the parent, which is in the correct place.

	//PolygonCollider needs to be removed when the object is placed.
	private PolygonCollider2D polyCol;

	//This will manage the pop-up quizzes
	private QuizPopup quizP;


	//Setting up Win Condition
	private bool piece;

	//Setting up Stat Tracking.  Stat tracking tracks a lot of statistics about the player.
	private StatTracking statTracking;
	private ReportSelf rs;
	private bool isGuessing = false;  //Boolean to check if body part is dropped in the Object Pane or not

	//Setting up a temporary boolean to mimic an easy/medium difficulty
	private bool normal = true;



	// Use this for initialization
	void Start () {

		//Get the position of the parent.
		//This is the correct position to place the object.
		xParent = transform.root.position.x;
		yParent = transform.root.position.y;

		//Set hint button toggles to inactive.
		hint2Toggle.gameObject.SetActive (false);  //We may need to rework how this feature works.

		//Find the Sprite Renderer for fading/recoloring purposes.
		spriteRenderer = GetComponent<SpriteRenderer> ();

		//Find the box collider so it can be removed when object is placed.
		polyCol = GetComponent <PolygonCollider2D> ();

		//Sets a range of + or - the Offset within the correct position.
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



		//Scaling objects to fit inside the box.  This is probably not needed.
		releaseScale = 50f;
		snapScale = 50f;


		//Following is code for if we decide to have both drag and drop AND quiz questions in one level.
		//levelManager.noObjections = true;
		quizP = GetComponent <QuizPopup> ();

			


		//Make sure that myAttempt starts at 0
		//guesses = 0; //NOTE: This is handled through ReportSelf now.

		//Count all body parts
		piece = (this.tag == "unPlaced");
		if (piece) {
			levelManager.startObjects++;
			levelManager.incObjCount(); //This may no longer be needed.
		}





	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//Select the object with the mouse.
	void OnMouseDown(){
		//Check that the game has started, the object is selectable, and various other conditions.
		if (isClickable && levelManager.gethasStarted () == true && !levelManager.canvasOpen && levelManager.noObjections) {
			//Following code basically raycasts the mouse's position to see if it's over the object's position, then sets the object to the mouse's position
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);

			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

			//Scales the object to a new size. May not be needed anymore.
			transform.localScale = new Vector3 (snapScale, snapScale, snapScale);
		}
	}

	//Drag the object with the mouse.
	void OnMouseDrag(){
		//If object is NOT locked in place.
		if (isClickable && levelManager.gethasStarted () == true && !levelManager.canvasOpen && levelManager.noObjections) {
			//Following code keeps the object locked into the mouse as it's dragged around.
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
			transform.position = curPosition;

			//Scales the object to a new size.  May not be needed anymore.
			transform.localScale = new Vector3 (snapScale, snapScale, snapScale);
		}
	}
	//When the mouse button is released...
	void OnMouseUp(){
		if (isClickable && levelManager.gethasStarted () == true && !levelManager.canvasOpen && levelManager.noObjections) {
			//Enable the Sprite Hint button when guesses exceed 11.  May need to be redone.
			if (rs.getGuesses() >= 11) {
				hint2Toggle.gameObject.SetActive (true);
			}
			getPosx ();  //Get the X position and check compared to the correct X position.
			getPosy ();  //Get the Y position and check compared to the correct Y position.
			clearBools ();  //If incorrect, ensure that the booleans are cleared.
			rs.reportSelf (); //Updates the reportSelf script with the number of guesses, whether the object was placed, and if a hint was used.
			if (xPos && yPos) {  //If the X and Y positions are both correct...
				hintBox.enabled = false; //Turn off the hint box if it was on.
				polyCol.enabled = false; //Disable the polygon collider so it's not in any other objects' way.
				hintBox.color = new Color (1f, 1f, 1f, 0f); //Clears hint box if it is active.
				setPos ();  //Lock object in place if X AND Y positions are in place.
				hint2Toggle.gameObject.SetActive (false); //Set image hint button toggle to inactive.
				if (rs.getHint() == true) {
					rs.toggleHintFalse(); //Set hint 2 bool back to false.
				}
				//Opens up the first question when object is placed correctly.  Note that for now, code is as if there will always be 1 question asked.
				if (normal){
					quizP.setCanvas (0, true); //Open up the canvas
					levelManager.addobjPlaced ();
				} else{
					//Otherwise reduce the number of objects left to place by 1.
					levelManager.startObjects--;
					levelManager.addobjPlaced ();
				}

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

			//Changes object to default size.
			transform.localScale = new Vector3 (releaseScale, releaseScale, releaseScale);

			//If the object is NOT in the Object Pane...
			if (isGuessing){
				hintSound();
				levelManager.attempts++;
				rs.addGuess ();

			}
		}
	}

	//Snaps the object to the parent location and locks it in place.
	void setPos(){
		transform.position = new Vector2 (xParent, yParent); //Sets the object to the parent's (correct) location
		isClickable = false; //Makes the object no longer able to be selected.
		//if (statTracking.identifyLevel () == "Q5") //This should be modified to check for difficulty.
		levelManager.noObjections = true; //Makes it so only the popped up quiz question can be answered.


		//Check the  number of guesses for this object and give the appropriate points.
		if (rs.getGuesses() == THRESH0) {
			levelManager.score += SCORE1;
		} else if (rs.getGuesses() >= THRESH1 && rs.getGuesses() <= THRESH2) {
			levelManager.score += SCORE2;
		} else if (rs.getGuesses() > THRESH2 && rs.getGuesses() <= THRESH3) {
			levelManager.score += SCORE3;
		} else if (rs.getGuesses() > THRESH3) {
			levelManager.score += SCORE0;
		}
		spriteRenderer.color = new Color (1f, 1f, 1f, alpha);  //Changes the color and fade of the object.
		transform.gameObject.tag = "Placed"; //Changes the tag of the object to Placed.
		rs.reportSelf (); //Report Self sends the message of the number of guesses, whether or not it was placed, and whether or not a hint was used.
	}

	//Plays a sound based on how close the object is to its proper location.
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
			hintBox.enabled = true; //Activates the hintbox
			hintBox.color = new Color (0.15f, 0.7f, 0.9f, 0.85f); //Allows the hintbox's alpha to be visible
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

	//If the object is placed in the Object Pane, don't add a guess.
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
