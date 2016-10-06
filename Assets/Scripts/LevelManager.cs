using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour {

	//Setting up Saving Stats for feedback
	private string levelName; //The name of the level
	private string urlRetrieve; //Retrieves the top scores
	private string urlUpload; //Uploads the score of the current session
	private PlayerManager playerManager; //Accesses the Player Manager

	//Setting up Instructions Canvas (May not be needed, or may be replaced by a better pause menu)
	private InstructionsCanvas ic; //The Canvas for the instructions panel
	private PlayerPrefsManager ppM; //The PlayerPrefsManager Script

	//Setting up Attempt Tracker
	public int attempts; //Number of guesses the player has made.


	//Setting up bools to prevent more than one quiz panel opened at a time.
	public bool noObjections = true; //This makes sure that the levelmanager has no objects to an object being selected (for example if a question canvas is opened)
	public bool canvasOpen = false; //This is specifically to check if a question canvas is open.

	//Setting up Score Tracker
	public int score; //The integer for the score
	//public float fauxTestPoints; //Number of questions answered on first try. This is not needed anymore.
	//private float fauxTextScore; //A percentage as if the student were taking a test with their first submitted answers.  This is not needed anymore.
	private bool getScore = false;  //Used to ensure the score is only gotten when needed.
	private bool canCallCoroutine = true; //Used to make a Coroutine called only every so often.

	//Setting up timer;
	private float timeSpent; //Elapsed time
	private float seconds; //Seconds up to 59.5
	private float minutes; //Minutes when Seconds exceeds its callling
	private string time; //String to display the timer
	public bool timesUp = false; //Boolean to stop the timer when the level is cleared or reset
	public bool hasStarted = true; //Boolean to freeze the timer if the Instructions Canvas is open.

	//Setting up string text
	private Text numAtt; //Displays the number of guesses
	private Text timer; //Displays the timer
	private Text point; //Displays the points earned so far

	//Setting up Stat Tracking
	private StatTracking statTracking;

	//Setting up Win Canvas.  All of these are game objects of the Win Canvas that will be found.
	private Canvas winCanvas;
	private Text finalScore;
	private Text congratsText;
	private Text accuracy;
	private Text speed;
	private Text finalS;
	private Text targetText;
	private Text targetTime;
	private Text targetScore;
	private Text accScore; //Accuracy Score
	private Text targetAcc;
	private Text timeScore;
	private Button restartButton;
	private Button improveScoreButton;

	//Setting up Win Condition and scoring
	public int startObjects; //Used to determine how many objects are left before the game is over
	public float objCount; //Used to determine the number of objects in the final score's formula
	private float fscoring; //Final Score held as a float
	private float formulaAcc; //Formula for accuracy score
	private float percentAcc; //The accuracy percentage
	[Range(0, 999)] //A Range of formulaSpeed
	private float formulaSpeed; //Formula for speed
	public int objPlaced; //Number of objects placed

	[Tooltip ("The allowed amount of time to obtain a time bonus to the final score.  100 is default")]
	private float allowedTime; //What the above tooltip says.
	private const int BASETIME = 60; //The default basetime for displaying the target time to the player on the results screen.
	private const int SCOREM = 20; //The score multiplier
	private enum State {Bronze, Silver, Gold, Platinum} //Enum to determine the medal to be awarded
	[Tooltip("The min threshold needed for each rank.")]
	public float finSilv = 120, finGld = 160, finPlat = 190; //The minimum threshold needed for each medal rank for the rank.  Numbers are defaults.
	private int goalAcc = 70; //The minimum threshold to get a medal other than silver (pertaining to accuracy score).
	public Sprite[] medals; //An array for the medal images
	private bool hasCleared = false; //Boolean used to determine if the win has been evalutated yet.  If it has, it will become true, preventing multiple evaluations.

	//Setting up Rank Images for Win Canvas
	private Image bronze;
	private Text bronzeT;
	private Image silver;
	private Text silverT;
	private Image gold;
	private Text goldT;
	private Image platinum;
	private Text platinumT;
	//The amount of grade uploaded to server.  The whole grade system may not be needed for this game.
	private const int BRONGRADE = 0;
	private const int SILVGRADE = 5;
	private const int GOLDGRADE = 10;
	private const int PLATGRADE = 10;

	//Setting up strings for number of medals earned.  May not be needed.
	private string bronMedals;
	private string silvMedals;
	private string goldMedals;
	private string platMedals;
	private string urlRetrieveMedals;

	//Setting up ints to track number of medals earned.  May not be needed.
	public int bronMedalsi;
	public int silvMedalsi;
	public int goldMedalsi;
	public int platMedalsi;

	//Setting up graduation conditions.  Graduation (Unlocking a new level based on how well player has done on current level) may not be needed.
	private string userName; //The user's username
	private string stringGrade;  //Temporary string to retrieve the grade from database.
	public int grade; //Integer stored in Database. Determines when the player is ready to move on to the next level.
	private string urlUploadGrade; //A web url to upload the grade to
	private string urlRetrieveGrade; //A web url to retrieve the grade from
	//private Text graduation; //A text that will tell the player whether or not they have graduated
	//private Button nextLevel; //A button that will appear only if the next level has been unlocked.  May not be needed anymore.

	//Setting up level reset
	public bool resetting = false; //Prevents certain things from being done (such as grade being given and high scores being uploaded
	public bool resetted = false; //Used to reset StartPosition booleans.


	// Use this for initialization
	void Start () {



		//Text for Score, Timer, and Guesses from Main Canvas
		point = GameObject.Find ("Score Count").GetComponent <Text>();
		timer = GameObject.Find ("Timer Count").GetComponent <Text>();
		numAtt = GameObject.Find ("Attempts Count").GetComponent <Text>();

		//Find ALL of the components of the Win Canvas!!
		winCanvas = GameObject.Find ("Win Canvas").GetComponent <Canvas> ();

		//Text Boxes from Win Canvas
		finalScore = GameObject.Find ("High Score Content").GetComponent <Text> ();
		congratsText = GameObject.Find ("Win Text").GetComponent <Text> (); 
		accuracy = GameObject.Find ("Accuracy Display").GetComponent <Text> ();
		speed = GameObject.Find("Speed Display").GetComponent <Text>();
		finalS = GameObject.Find("Final Display").GetComponent <Text>();
		targetText = GameObject.Find("Target Text").GetComponent <Text>();
		targetTime = GameObject.Find("Target Time").GetComponent <Text>();
		targetScore = GameObject.Find("Target Score").GetComponent <Text>();
		accScore = GameObject.Find("Accuracy Score").GetComponent <Text>();
		targetAcc = GameObject.Find ("Accuracy Max").GetComponent <Text> ();
		timeScore = GameObject.Find("Time Score").GetComponent <Text>();
		//graduation = GameObject.Find("Graduation Text").GetComponent <Text>();

		//Sets up where to save player stats.
		statTracking = GetComponent <StatTracking>();

		//Medal Images and Texts from Win Canvas
		if (statTracking.identifyLevel () != "Q0") {
			bronze = GameObject.Find ("Bronze Info").GetComponent <Image> ();
			silver = GameObject.Find ("Silver Info").GetComponent <Image> ();
			gold = GameObject.Find ("Gold Info").GetComponent <Image> ();
			platinum = GameObject.Find ("Platinum Info").GetComponent <Image> ();
		}
		//Buttons from Win Canvas
		restartButton = GameObject.Find("Restart Button").GetComponent <Button>();
		improveScoreButton = GameObject.Find ("Improve Score Button").GetComponent <Button> ();


		//The following is if the Player Manager exists.  This should always be the case in practice, except when playtesting specific levels.
		if (GameObject.Find ("Player Manager") != null) { //See if the Player Manager exists in the level
			playerManager = FindObjectOfType<PlayerManager> (); //Find the Player Manager
			userName = playerManager.getUserName (); //Get the Username
		
			levelName = SceneManager.GetActiveScene ().name; //Get the name of the level name

			//Server URLS
			urlRetrieve = ("http://secs.oakland.edu/~nferman/retrieve.php?level=" + levelName); //Retrieves the high score
			urlUpload = ("http://secs.oakland.edu/~nferman/upload.php?level=" + levelName + "&Privatekey=j5G1L23@#"); //Upload's high score
			urlRetrieveGrade = ("http://secs.oakland.edu/~nferman/retrievegrades.php?level=" + levelName + "&Name=" + userName); //Retrieves grade for current user's current level

			urlUploadGrade = ("http://secs.oakland.edu/~nferman/uploadgrades.php?level=" + levelName + "&Privatekey=j5G1L23@#"); //Upload's grade of current level
			urlRetrieveMedals = ("http://secs.oakland.edu/~nferman/retrievemedals.php?level=" + levelName + "&Name=" + userName); //Retrieves the number of medals for current level

			ppM = FindObjectOfType <PlayerPrefsManager> (); // //Finds the Player Prefs Manager Script
			ic = FindObjectOfType <InstructionsCanvas> (); //Finds the Instructions Canvas Script

			StartCoroutine (retrieveMedals ()); //Starts the Coroutine to retrieve the user's number of medals for the level
			StartCoroutine (dispPan ()); //Displays the Instruction Canvas
			if (ppM.getLevelInst () == 0) //If the player has marked "Do not show this message again...
				hasStarted = true; //Start the game immediately
		}

		//NOTE:  Everything referring to grade may not be needed anymore.


		//Initialize the following code...
		resetting = false; //Sets the resetting boolean to false (if the game was reset)
		restartButton.gameObject.SetActive (true); //Activates the restart button (if the game was reset)
		//nextLevel.gameObject.SetActive (false); //Sets the next level button to false.  May not be needed anymore.
		attempts = 0; //Display the number of attempts
		objPlaced = 0; //Set the number of muscles placed so far to 0.
		//Display the timer
		timeSpent = 0f;
		seconds = 0f;
		minutes = 0f;
		calcScore ();  //Refresh the score

		//This is a formula to adjust the allowed time
		allowedTime = BASETIME + (int.Parse (objCount.ToString ()) * 10);

		roundTime (); //Rounds the allowed time to the nearest 5 seconds.

	}
		
	
	// Update is called once per frame
	void Update () {
		numAtt.text = attempts.ToString (); //Update the number of attempts text on the UI

		//If the level isn't finished, and the game has officially started...
		if (!timesUp && hasStarted) {
			runTimer (); //Runs the timer
		}
		//If the score hasn't already been calculated and all objects have been placed/questions answered...
		if (!hasCleared && startObjects <= 0) {
			levelClear (); //Calculate the score and display the results
		}

		//If the score hasn't been retrieved yet
		if (getScore) {
			//If it's time to call the Coroutine
			if (canCallCoroutine){
				StartCoroutine (displayHighScore()); //Display the High Score
				//StartCoroutine (retrieveGrade ()); //Check for graduation
			}
		}



	}

	//Displays the Instructions Canvas
	IEnumerator dispPan ()
	{
		yield return ppM.getLevelInst (); //Gets the type of instructions for the level
		if (ppM.getLevelInst () == 1) //If hasRead is false for this level...
			ic.dispInstPanel (); //Display the Instructions Panel
	}

	//Returns whether or not the game has started (If the Instructions Canvas is open, it has not started)
	public bool gethasStarted(){
		return hasStarted;
	}

	//Sets hasStarted to the argument hs
	public void sethasStarted(bool hs){
		hasStarted = hs;
	}

	//Retrieves the grade of this level
	/*IEnumerator retrieveGrade(){
		WWW wwwRetrieveGrade = new WWW (urlRetrieveGrade); //Retrieves the grade of the level.
		yield return wwwRetrieveGrade; //Waits until the grade is retrieved
		stringGrade = wwwRetrieveGrade.text; //The text of the web document (Should be a number 0-10)
		grade = int.Parse (stringGrade); //Parses the text to an int
		if (grade > 10)
			grade = 10; //Caps the grade at 10
	}*/

	//Retool this to retrieve the medal of the player's current high score *******
	IEnumerator retrieveMedals(){
		for (int i = 0; i < 3; i++) { //Loop through 3 times to make sure proper values are grabbed
			yield return new WaitForSeconds (1); //Wait 1 second between each loop
			//Retrieve the bronze medal values
			string urlRetrieveBronze = urlRetrieveMedals + "&medal=Bronze";
			WWW wwwRetrieveBronze = new WWW (urlRetrieveBronze);
			yield return wwwRetrieveBronze;
			bronMedals = wwwRetrieveBronze.text;
			bronMedalsi = int.Parse (bronMedals);
			print ("bronze: " + bronMedalsi); 

			//Retrieve the silver medal values
			string urlRetrieveSilver = urlRetrieveMedals + "&medal=Silver";
			WWW wwwRetrieveSilver = new WWW (urlRetrieveSilver);
			yield return wwwRetrieveSilver;
			silvMedals = wwwRetrieveSilver.text;
			silvMedalsi = int.Parse (silvMedals);
			print ("Silver: " + silvMedalsi);

			//Retrieve the gold medal values
			string urlRetrieveGold = urlRetrieveMedals + "&medal=Gold";
			WWW wwwRetrieveGold = new WWW (urlRetrieveGold);
			yield return wwwRetrieveGold;
			goldMedals = wwwRetrieveGold.text;
			goldMedalsi = int.Parse (goldMedals);
			print ("Gold: " + goldMedalsi);

			//Retrieve the platinum medal values
			string urlRetrievePlat = urlRetrieveMedals + "&medal=Platinum";
			WWW wwwRetrievePlatinum = new WWW (urlRetrievePlat);
			yield return wwwRetrievePlatinum;
			platMedals = wwwRetrievePlatinum.text;
			platMedalsi = int.Parse (platMedals);
			print ("Platinum: " + platMedalsi);
		}
	}

	//Appends the urlUpload string with the proper medal to upload
	void uploadMedal(string medColor/*, int medNum*/){
		urlUpload += "&medal=" + medColor /*+ "&medalnum=" + medNum*/; //Medal Color and Number of those medals
		//print (urlUpload);
	}

	//Uploads the high score, medal, and number of medals to the database
	IEnumerator uploadScore(){
		//Appends the urlUpload string with Username, Displayname, Final Score, Medal Rank, and Time
		urlUpload += "&Name=" + userName + "&Display=" + playerManager.displayName + "&Accuracy=" + fscoring.ToString("F2") + "&AccuracyRank=" + medalToUpload () + "&Time=" + time.ToString();
		WWW wwwUpload = new WWW (urlUpload); //Uploads the string
		yield return wwwUpload; //Waits until the upload goes through
		//print (urlUpload);  //Prints the string of the URL
	}

	//Uploads the grade to the database.  Can get rid of this.
	IEnumerator uploadGrade(){
		//Appends the urlUploadGrade string with the username, level name, and grade
		urlUploadGrade = urlUploadGrade  + "&Name=" + userName + "&" + levelName + "=" + grade;
		WWW wwwUploadGrade = new WWW (urlUploadGrade); //Uploads the grade
		yield return wwwUploadGrade; //Waits until the upload goes through

		//print (grade + " Grade uploaded to " + urlUploadGrade); //Prints the string of the URL
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

	//Evaluate the rank
	void displayRank(){
		//Set all medal images and text to half transparancy
		bronze.color = new Color (0.9f, 0.45f, 0.45f, 0.5f);
		//bronzeT.color = new Color (0f, 0f, 0f, 0.5f);
		silver.color = new Color (0.7f, 0.7f, 0.65f, 0.5f);
		//silverT.color = new Color (0f, 0f, 0f, 0.5f);
		gold.color = new Color (0.9f, 0.9f, 0f, 0.5f);
		//goldT.color = new Color (0f, 0f, 0f, 0.5f);
		platinum.color = new Color (0f, 0f, 0f, 0.5f);
		//platinumT.color = new Color (0f, 0f, 0f, 0.5f);

		//Set the text of each medal to the appropriate value
		//bronzeT.text = bronMedalsi.ToString ();
		//silverT.text = silvMedalsi.ToString ();
		//goldT.text = goldMedalsi.ToString ();
		//platinumT.text = platMedalsi.ToString ();

		//If the user didn't reset the game...
		if (!resetting) {
			if (fscoring < finSilv  || formulaAcc < goalAcc) {
				bronze.color = new Color (0.9f, 0.45f, 0.45f, 1f); //Make image solid color
				//bronzeT.color = new Color (0f, 0f, 0f, 1f); //Make text solid color
				//grade += BRONGRADE; //Increment Grade
				//print ("Grade: " + grade);
				//bronMedalsi++; //Increment number of Bronze medals
				uploadMedal ("Bronze"/*, bronMedalsi*/); //Upload the appropriate medal color and number
				//bronzeT.text = bronMedalsi.ToString (); //Display number of medals

			} else if (fscoring >= finSilv && fscoring <= finGld && formulaAcc >= goalAcc) {
				silver.color = new Color (0.7f, 0.7f, 0.65f, 1f);  //Make Image Visible
				//silverT.color = new Color (0f, 0f, 0f, 1f); //Make text solid color
				//Adds 5 points to player's grade, uploads to database.
				//grade += SILVGRADE;
				//print ("Grade: " + grade);
				//silvMedalsi++; //Increment number of silver medals
				uploadMedal ("Silver"/*, silvMedalsi*/); //Upload the appropriate medal color and number
				//silverT.text = silvMedalsi.ToString (); //Display number of medals

			} else if (fscoring > finGld && fscoring < finPlat && formulaAcc >= goalAcc) {
				gold.color = new Color (0.9f, 0.9f, 0f, 1f); //Make Image Visible
				//goldT.color = new Color (0f, 0f, 0f, 1f); //Make text solid color
				//Adds 10 points to player's grade, uploads to database.
				//grade += GOLDGRADE;
				//print ("Grade" + grade);
				//goldMedalsi++; //Increment number of Gold medals
				uploadMedal ("Gold"/*, goldMedalsi*/); //Upload the appropriate medal color and number
				//goldT.text = goldMedalsi.ToString (); //Display number of medals

			} else if (fscoring >= finPlat && formulaAcc >= goalAcc) {
				platinum.color = new Color (0f, 0f, 0f, 1f); //Make Image Visible
				//platinumT.color = new Color (0f, 0f, 0f, 1f); //Make text solid color
				//Adds 10 points to player's grade, uploads to database.
				//grade += PLATGRADE;
				//print ("Grade" + grade);
				//platMedalsi++; //Increment number of Platinum medals
				uploadMedal ("Platinum"/*, platMedalsi*/); //Upload the appropriate medal color and number
				//platinumT.text = platMedalsi.ToString (); //Display number of medals
			}
			//if (grade > 10)
				//grade = 10; //Caps grade at 10

//R			//medalToUpload ();  //Finds the appropriate medal to upload to the database (No ranking down)
		}
	}

	//Finds the appropriate medal to upload to the database (No ranking down)
	private string medalToUpload (){
		if (platMedalsi >= 1) //If they have at least one Platinum medal
			return "Platinum";
		else if (platMedalsi <= 0 && goldMedalsi >= 1) //If they have no Platinum medals but at least 1 Gold medal
			return "Gold";
		else if (platMedalsi <= 0 && goldMedalsi <= 0 && silvMedalsi >= 1) //If they have at least 1 Silver medal (but no Gold or Platinum medals)
			return "Silver";
		else { //If they don't have any medals above a bronze medal
			return "Bronze";
		}
	}

	//Displays the High Score in the final results screen.
	IEnumerator displayHighScore(){
		if (resetting) { //If they reset...
			congratsText.text = "You have reset..." + /*playerManager.displayName*/ "...";
		} else if (!resetting){ //If they cleared the level
			congratsText.text = "Congratulations" + /*playerManager.displayName*/ "!";
		}

		finalScore.text = ""; //Clear the final score text
		finalScore.text = "High Score List will go here.";
		yield return new WaitForSeconds (1);
//RB	int n = 0; //Set up a counter so the final score loops through twice to ensure the high score list is up to date
		/*while (n <= 2) {
			canCallCoroutine = false; //Wait for...
			yield return new WaitForSeconds (1); //..One second between loops
			WWW wwwRetrieve = new WWW (urlRetrieve); //Retrieves the data of the entire database.
			yield return wwwRetrieve; //Waits until the Web Doc is done retrieving values
			finalScore.text = "Retrieving Scores..."; //Prints a friendly message to the user while looping
			++n; //Increment the while loop counter
			canCallCoroutine = true; //Sets true after one loop is done
			if (n > 2) { //When the while loop is done
				finalScore.text = wwwRetrieve.text; //Display the high score list
			}
		}*/
	

		getScore = false; //Once this is all done, there is no need to retrieve the score again
		
	}

	//When level is clear, bring up the win canvas and display the results
	void levelClear (){
		timesUp = true; //Stops the timer.
		calcScore (); //Calculates the final score
		restrictScore(); //Makes sure score is never below 0.
		displayRank (); //Displays the rank.		
		//if (!resetting) //If the user didn't reset
			//StartCoroutine (uploadGrade()); //Upload their grade
		accuracy.text = percentAcc.ToString("F0") + "%"; //Formats the accuracy text for the win canvas display
		speed.text = time.ToString(); //Formats the text to display for the player's time
		timeScore.text = formulaSpeed.ToString ("F1"); //Displays the score the player got based on their time
		displayTargetScore (); //Displays the target score for the next medal rank up
		accScore.text = formulaAcc.ToString ("F1"); //Displays the score the player got based on their accuracy
		finalS.text = fscoring.ToString ("F2"); //Displays the final score the player got (calculated by speed and accuracy)
		statTracking.score = fscoring.ToString ("F2"); //The score to upload to the stat tracking database
		statTracking.time = time.ToString (); //The time to upload to the stat tracking database
		statTracking.seconds = timeSpent.ToString ("F2"); //The time (in seconds) to upload to the stat tracking database
		//statTracking.fauxGrade = fauxTextScore.ToString ("F2"); //This is SO NOT NEEDED!!!
		if (resetting) //Uploads whether or not the user quit to the stat tracking database.
			statTracking.quit = "Y";
		else
			statTracking.quit = "N";
		if (!resetting)
			Invoke ("displayCanvas", 1f); //If they cleared the level, put a short delay on displaying the win canvas
		else
			displayCanvas (); //If they quit the level, display the win canvas immediately

//R		//if (playerManager.getOptin () == "true") //If the player opted-in to be tracked by the stat tracker
//R			//statTracking.doTheUpload (); //Upload everything to the stat tracking database.
		
		if (!resetting){
//R			//StartCoroutine (uploadScore()); //If the player cleared the level, upload their high score to the database
		}
		getScore = true; //The score has been calculated, go and retrieve the high scores.

		//The following code checks for graduation (Whether or not the user is ready for the next level).  This may not be needed.
		/*if (grade >= 10) {
			//graduation.text = "Congratulations!  You may continue improving your score, or try the next level.";
			//graduation.color = new Color (.3f, .5f, 1f); //Sets the text color to a light blue.
			//nextLevel.gameObject.SetActive (true); //Sets the next level button to true.  May not be needed.
			//if (grade > 10) {
				//grade = 10; //Caps the grade at 10. This may not be needed.
			//} 
		//} else {
			//graduation.text = "Collect 2 silver, 1 gold, or 1 platinum medal(s) to reach the next level.";
			//graduation.color = new Color (1f, 0f, 0f); //Sets the text color to red
		//}*/

	
		hasCleared = true; //Ensures that this method is only run once.
	
	}

	//Displays the Win Canvas
	void displayCanvas ()
	{
		winCanvas.gameObject.SetActive (true); //Enables the Win Canvas
		winCanvas.transform.position = new Vector3 (0, 0, 0); //Sets the Win Canvas to about the center
		restartButton.gameObject.SetActive (false); //Disables the restart button (since all that does is bring up the win canvas)
	}

	public void addobjPlaced(){
		objPlaced++; //Increments the number of objects placed.  Used in the final calculation of the score
	}

	void calcScore ()
	{
		percentAcc = (objPlaced / (attempts+objCount)) * 100; //The accuracy based on number of objects cleared (In case of reset)
		formulaAcc = (score / objCount) * SCOREM; //The score based on accuracy.  SCOREM is a multiplier to make the max score always be 100
		if (resetting)
			formulaSpeed = 0; //No score for speed if user resets the game
		else
			formulaSpeed = allowedTime - timeSpent; //The score based on the time the player cleared the level.

		fscoring = formulaAcc + formulaSpeed; //The final score, adding accuracy score and speed score together
		//fauxTextScore = (fauxTestPoints / objCount) * 100; //Seriously. Not. Needed!  A score based on whether the user was correct their first try or not


	}


	//Restricts the final score to never be below zero.
	void restrictScore(){
		if (fscoring < 0) {
			fscoring = 0; //Final score
		}
		if (formulaAcc < 0) {
			formulaAcc = 0; //Score based on Accuracy
		}
		if (formulaSpeed < 0) {
			formulaSpeed = 0; //Score based on speed
		}
	}

	//Displays the target score for the next rank up
	void displayTargetScore(){
		int medalDivider; //How the target score should be divided based on rank
		if (medalToUpload () == "Platinum") {
			targetScore.text = "***"; //If platinum, no higher goal to reach
			medalDivider = 1;

		} else if (medalToUpload () == "Gold") {
			targetScore.text = " / " + finPlat; //Display target to reach Platinum
			medalDivider = 1;


		} else if (medalToUpload () == "Silver") {
			targetScore.text = " / " + finGld; //Display target to reach Gold
			medalDivider = 2;

		} else {
			targetScore.text = " / " + finSilv; //Display target to reach Silver
			medalDivider = 4;

		}
		targetAcc.text = goalAcc.ToString (); //Set the text to the target accuracy


		targetTime.text = displayTargetTime (allowedTime - BASETIME/medalDivider); //Set the text to the target time
	

	}

	//Rounds the target time to the nearest five seconds
	void roundTime ()
	{
		allowedTime = allowedTime / 5; //Divide by five
		allowedTime = (float)Math.Round (allowedTime) * 5; //Round the floating point off, then multiply by 5
	}

	//Format and display the target time
	string displayTargetTime(float time){
		//time = roundTime (time);
		float tarMinutes = 0; //The minutes
		float tarSeconds = 0; //The seconds
		while (time >= 60){
			tarMinutes = tarMinutes + 1; //For every 60 seconds, add 1 minute
			time -= 60; //The subtract 60 seconds
		}
		tarSeconds = time; //Remaining seconds after evaluating minutes
		string newTime; //A string to hold the entire value of time
		if (tarSeconds == 0)
			newTime = tarMinutes.ToString () + ":" + "00"; //Formats the string if there are 0 seconds
		else
			newTime = tarMinutes.ToString () + ":" + tarSeconds.ToString (); //Formats the string if there is NOT 0 seconds
		return newTime; //Returns the string

	}

	//Restarts the level if button is clicked.
	public void resetLevel(){
		resetted = true; //Sets this to true so the StartPosition Script can reset all of its booleans
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name); //Reloads the scene
	}

	//Quits the application if button is clicked.
	public void quit(){
		Application.Quit ();
	}

	public void restart(){
		resetting = true; //This boolean prevents a few things (like high scores being uploaded) if the user resets the level
		levelClear (); //Calcs the score and brings up the Win Canvas to display the score
	}

	//Loads the selected level
	public void loadLevel(string levelName){
		SceneManager.LoadScene (levelName);
	}

	//Loads the next level in the build settings
	public void loadNextLevel (){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
	}
		
	//Increment objCount
	public void incObjCount(){
		objCount++;
	}

	//Adds a number to objCount
	public void addObjCount(int m){
		objCount += m;
	}


}
