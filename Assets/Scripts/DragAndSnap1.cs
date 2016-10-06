using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]  //Ensure Object has a Polygon Collider.
public class DragAndSnap1 : MonoBehaviour {

	//Setting up Mouse Drag and Drop properties
	private Vector3 screenPoint; //Used with drag and drop raycasting
	private Vector3 offset; //Used with drag and drop raycasting

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
	private LevelManager1 levelManager;
	private ArrayShuffler arrayShuffler; //Access to ArrayShuffler script

	//Setting up Attempts
	private int guesses = 0;
	private bool isGuessing = false;  //Boolean to check if body part is dropped in the Object Pane or not

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


	//PolygonCollider needs to be removed when the object is placed.
	private PolygonCollider2D polyCol;

	//Setting up Win Condition
	private bool piece;


	// Use this for initialization
	void Start () {

		//Get the position of the parent.
		//This is the correct position to place the object.
		xParent = transform.root.position.x;
		yParent = transform.root.position.y;

		//Find the Sprite Renderer for fading/recoloring purposes.
		spriteRenderer = GetComponent<SpriteRenderer> ();

		//Find the box collider so it can be removed when object is placed.
		polyCol = GetComponent <PolygonCollider2D> ();

		//Sets a range of + or - the Offset within the correct position.
		xRangeHigh = xParent + OFFSET;
		xRangeLow = xParent - OFFSET;
		yRangeHigh = yParent + OFFSET;
		yRangeLow = yParent - OFFSET;



		//Find the Level Manager.
		levelManager = FindObjectOfType<LevelManager1> ();

		//Find the Array Shuffer
		arrayShuffler = FindObjectOfType <ArrayShuffler>();


		//Make sure that myAttempt starts at 0
		guesses = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
	}



	//Select the object with the mouse.
	void OnMouseDown(){
		//Check that the game has started, the object is selectable, and various other conditions.
		if (isClickable) {
			//Following code basically raycasts the mouse's position to see if it's over the object's position, then sets the object to the mouse's position
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);

			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	//Drag the object with the mouse.
	void OnMouseDrag(){
		//If object is NOT locked in place.
		if (isClickable) {
			//Following code keeps the object locked into the mouse as it's dragged around.
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
			transform.position = curPosition;

		}
	}
	//When the mouse button is released...
	void OnMouseUp(){
		if (isClickable) {
			getPosx ();  //Get the X position and check compared to the correct X position.
			getPosy ();  //Get the Y position and check compared to the correct Y position.
			clearBools ();  //If incorrect, ensure that the booleans are cleared.
			if (xPos && yPos) {  //If the X and Y positions are both correct...
				polyCol.enabled = false; //Disable the polygon collider so it's not in any other objects' way.
				setPos ();  //Lock object in place if X AND Y positions are in place.
				levelManager.startObjects--; //Decrements number of objects left to be placed
				arrayShuffler.nextState ();
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

			//If the object is NOT in the Object Pane...
			if (isGuessing){
				levelManager.attempts++;
				guesses++;

			}
		}
	}

	//Snaps the object to the parent location and locks it in place.
	void setPos(){
		transform.position = new Vector2 (xParent, yParent); //Sets the object to the parent's (correct) location
		isClickable = false; //Makes the object no longer able to be selected.

		//Check the  number of guesses for this object and give the appropriate points.
		if (guesses == THRESH0) {
			levelManager.score += SCORE1;
		} else if (guesses >= THRESH1 && guesses <= THRESH2) {
			levelManager.score += SCORE2;
		} else if (guesses > THRESH2 && guesses <= THRESH3) {
			levelManager.score += SCORE3;
		} else if (guesses > THRESH3) {
			levelManager.score += SCORE0;
		}
		spriteRenderer.color = new Color (1f, 1f, 1f, alpha);  //Changes the color and fade of the object.
		transform.gameObject.tag = "Placed"; //Changes the tag of the object to Placed.

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
