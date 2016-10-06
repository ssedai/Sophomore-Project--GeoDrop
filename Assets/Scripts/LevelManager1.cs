using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager1 : MonoBehaviour {
	
	public int attempts; //Number of guesses the player has made.
	public int score; //The integer for the score

	//Setting up timer;
	private float timeSpent; //Elapsed time
	private string time; //String to display the timer
	private float seconds; //Seconds up to 59.5
	private float minutes; //Minutes when Seconds exceeds its callling
	public bool timesUp = false; //Boolean to stop the timer when the level is cleared or reset

	//Setting up string text
	private Text numAtt; //Displays the number of guesses
	private Text timer; //Displays the timer
	private Text point; //Displays the points earned so far

	//Setting up Win Condition and scoring
	public int startObjects; //Used to determine how many objects are left before the game is over

	//Setting up level reset
	public bool resetting = false; //Prevents certain things from being done (such as grade being given and high scores being uploaded

	// Use this for initialization
	void Start () {

		//Text for Score, Timer, and Guesses from Main Canvas
		point = GameObject.Find ("Score Count").GetComponent <Text>();
		timer = GameObject.Find ("Timer Count").GetComponent <Text>();
		numAtt = GameObject.Find ("Attempts Count").GetComponent <Text>();

		//If the user resets, do the following...
		resetting = false; //Sets the resetting boolean to false (if the game was reset)
		attempts = 0; //Display the number of attempts

		//Display the timer
		timeSpent = 0f;
		seconds = 0f;
		minutes = 0f;

	}

	// Update is called once per frame
	void Update () {
		numAtt.text = attempts.ToString (); //Update the number of attempts text on the UI

		//If the level isn't finished, and the game has officially started...
		if (!timesUp) {
			runTimer (); //Runs the timer
		}

		if (startObjects <= 0)
			levelClear ();
	}

	//Runs the time.
	void runTimer (){
		//Timer Count
		timeSpent += Time.deltaTime; //The total time spent.  Used to calculate the score
		seconds += Time.deltaTime; //Time spent in seconds.  Used to display the timer to the user
		if (seconds > 59.5f) { //If seconds exceeds 59.5
			minutes += 1;  //Add a minute
			seconds = -0.5f; //Reset seconds to -0.5 seconds
		}
		//Display the timer
		time = minutes + ":";
		if (seconds < 9.5f)
			time += "0" + seconds.ToString ("F0");
		else 
			time += seconds.ToString ("F0");
		timer.text = time;
		string.Format ("00:00", time); //Formats the timer to have numbers and a colon in between
		//Display the score
		point.text = score.ToString(); //Displays the current number of points.
	}

	private void levelClear(){
		timesUp = true;
	}

	//Restarts the level if button is clicked.
	public void resetLevel(){
		resetting = true; //Sets this to true so the StartPosition Script can reset all of its booleans
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name); //Reloads the scene
	}

	//Quits the application if button is clicked.
	public void quit(){
		Application.Quit ();
	}

	//Loads the selected level
	public void loadLevel(string levelName){
		SceneManager.LoadScene (levelName);
	}

	//Adds the number of objects the player needs to place
	public void startObjCount(int s){
		startObjects = s;
	}


}