using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System;

public class LevelManager : MonoBehaviour {

	//Setting up Saving Stats for feedback
	private string levelName;
	private string urlRetrieve; //Retrieves the top scores
	private string urlUpload; //Uploads the score of the current session
	private PlayerManager playerManager;

	//Setting up Instructions Canvas
	private InstructionsCanvas ic;
	private PlayerPrefsManager ppM;

	//Setting up Attempt Tracker
	public int attempts; //Number of guesses the player has made.


	//Setting up bools to prevent more than one quiz panel opened at a time.
	public bool noObjections = true;
	public bool canvasOpen = false;

	//Setting up Score Tracker
	public int score;
	public float fauxTestPoints; //Number of questions answered on first try.
	private float fauxTextScore; //A percentage as if the student were taking a test with their first submitted answers.
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
	private Text numAtt;  //Displays the number of guesses
	private Text timer; //Displays the timer
	private Text point; //Displays the points earned so far

	//Setting up Stat Tracking
	private StatTracking statTracking;

	//Setting up Win Canvas
	private Canvas winCanvas;
	private Text finalScore;
	private Text congratsText;
	private Text accuracy;
	private Text speed;
	private Text finalS;
	private Text targetText;
	private Text targetTime;
	private Text targetScore;
	private Text accScore;
	private Text targetAcc;
	private Text timeScore;
	private Button restartButton;
	private Button improveScoreButton;

	//Setting up Win Condition and scoring
	public int startObjects; //Used to determine how many objects are left before the game is over
	public float muscleCount; //Used to determine the number of muscles in the final score's formula
	private float fscoring; //Final Score held as a float
	private float formulaAcc; //Formula for accuracy score
	private float percentAcc; //The accuracy percentage
	[Range(0, 999)]
	private float formulaSpeed; //Formula for speed
	public int musclesPlaced; //Number of muscles placed

	[Tooltip ("The allowed amount of time to obtain a time bonus to the final score.  100 is default")]
	private float allowedTime;
	private const int BASETIME = 60;
	private const int SCOREM = 20; //The score multiplier
	private enum State {Bronze, Silver, Gold, Platinum} //Enum to determine the medal to be awarded
	private State /*rankAcc,*/ rankSpd; //States for Accuracy Rank and Speed Rank
	//public float accSilv = 20, accGld = 30, accPlat = 40; //The minimum threshold needed for each medal rank for accuracy.  Numbers are defaults.
	public float spdSilv = 70, spdGld = 60, spdPlat = 20; //The minimum threshold needed for each medal rank for speed.  Numbers are defaults.
	public float finSilv = 120, finGld = 160, finPlat = 190; //The minimum threshold needed for each medal rank for the final rank.  Numbers are defaults.
	public int goalAcc = 70; //The minimum threshold to get a medal other than silver (pertaining to accuracy score).
	public Sprite[] medals; //An array for the medal images
	private bool hasCleared = false; //Boolean used to determine if the win has been evalutated yet.  If it has, it will become true.

	//Setting up Rank Images
	private Image bronze;
	private Text bronzeT;
	private Image silver;
	private Text silverT;
	private Image gold;
	private Text goldT;
	private Image platinum;
	private Text platinumT;
	private const int BRONGRADE = 0;
	private const int SILVGRADE = 5;
	private const int GOLDGRADE = 10;
	private const int PLATGRADE = 10;

	//Setting up number of medals
	private string bronMedals;
	private string silvMedals;
	private string goldMedals;
	private string platMedals;
	private string urlRetrieveMedals;
	//private string urlUploadMedals;

	public int bronMedalsi;
	public int silvMedalsi;
	public int goldMedalsi;
	public int platMedalsi;

	//Setting up graduation condition
	private string userName;
	private string stringGrade;  //Temporary string to retrieve the grade from database.
	public int grade; //Integer stored in Database. Determines when the player is ready to move on to the next level.
	private string urlUploadGrade;
	private string urlRetrieveGrade;
	private Text graduation;
	private Button nextLevel;

	//Setting up level reset
	public bool resetting = false;
	public bool resetted = false;

	//The Zoom Camera
	private Magnify cam;

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
		graduation = GameObject.Find("Graduation Text").GetComponent <Text>();

		//Sets up where to save player stats.
		statTracking = GetComponent <StatTracking>();

		//Medal Images and Texts from Win Canvas
		if (statTracking.identifyLevel () != "Q0") {
			bronze = GameObject.Find ("Bronze Info").GetComponent <Image> ();
			bronzeT = GameObject.Find ("Bronze Grade").GetComponent <Text> ();
			silver = GameObject.Find ("Silver Info").GetComponent <Image> ();
			silverT = GameObject.Find ("Silver Grade").GetComponent <Text> ();
			gold = GameObject.Find ("Gold Info").GetComponent <Image> ();
			goldT = GameObject.Find ("Gold Grade").GetComponent <Text> ();
			platinum = GameObject.Find ("Platinum Info").GetComponent <Image> ();
			platinumT = GameObject.Find ("Platinum Grade").GetComponent <Text> ();
		}
		//Buttons from Win Canvas
		restartButton = GameObject.Find("Restart Button").GetComponent <Button>();
		nextLevel = GameObject.Find("Next Level Button").GetComponent <Button>();
		improveScoreButton = GameObject.Find ("Improve Score Button").GetComponent <Button> ();

		cam = FindObjectOfType <Magnify> ();

		if (GameObject.Find ("Player Manager") != null) {
			playerManager = FindObjectOfType<PlayerManager> ();
			userName = playerManager.getUserName ();
		
			levelName = SceneManager.GetActiveScene ().name;

			//Main Server URLS
			urlRetrieve = ("https://ilta.oakland.edu/retrieve.php?level=" + levelName);
			urlUpload = ("https://ilta.oakland.edu/upload.php?level=" + levelName + "&Privatekey=j5G1L23");
			urlRetrieveGrade = ("https://ilta.oakland.edu/retrievegrades.php?level=" + levelName + "&Name=" + userName);
			StartCoroutine (retrieveGrade ());
			urlUploadGrade = ("https://ilta.oakland.edu/uploadgrades.php?level=" + levelName + "&Privatekey=j5G1L23");
			urlRetrieveMedals = ("https://ilta.oakland.edu/retrievemedals.php?level=" + levelName + "&Name=" + userName);
			//urlUploadMedals = ("http://goeteeks.x10host.com/Tutorial/uploadmedals.php?Privatekey=j5G1L23&level=" + levelName + "&Name=" + userName);



			//Backup Server URLS
			/*urlRetrieve = ("http://goeteeks.x10host.com/Tutorial/retrieve.php?level=" + levelName);
			urlUpload = ("http://goeteeks.x10host.com/Tutorial/upload.php?level=" + levelName + "&Privatekey=j5G1L23");
			urlRetrieveGrade = ("http://goeteeks.x10host.com/Tutorial/retrievegrades.php?level=" + levelName + "&Name=" + userName);
			StartCoroutine (retrieveGrade ());
			urlUploadGrade = ("http://goeteeks.x10host.com/Tutorial/uploadgrades.php?level=" + levelName + "&Privatekey=j5G1L23");
			urlRetrieveMedals = ("http://goeteeks.x10host.com/Tutorial/retrievemedals.php?level=" + levelName + "&Name=" + userName);
			//urlUploadMedals = ("http://goeteeks.x10host.com/Tutorial/uploadmedals.php?Privatekey=j5G1L23&level=" + levelName + "&Name=" + userName);*/

			ppM = FindObjectOfType <PlayerPrefsManager> ();
			ic = FindObjectOfType <InstructionsCanvas> ();

			StartCoroutine (retrieveMedals ());
			StartCoroutine (dispPan ());
			if (ppM.getLevelInst () == 0)
				hasStarted = true;
		}





		resetting = false;





		restartButton.gameObject.SetActive (true);
		//quitButton.gameObject.SetActive (false);
		nextLevel.gameObject.SetActive (false);

		//Display the number of attempts
		attempts = 0;
		musclesPlaced = 0;
		 
		//Display the timer
		timeSpent = 0f;
		seconds = 0f;
		minutes = 0f;

		//Formulas for the score
		calcScore ();

		if (statTracking.levelIdentity == "Q0")
			restartButton.gameObject.SetActive (false);

		if (statTracking.levelIdentity == "P")
			allowedTime = BASETIME + (int.Parse (muscleCount.ToString ()) * 3);
		else if (SceneManager.GetActiveScene ().name == "UpperArm5")
			allowedTime = BASETIME + (int.Parse (muscleCount.ToString ()) * 16);
		else
			allowedTime = BASETIME + (int.Parse (muscleCount.ToString ()) * 8);

		roundTime ();

	}
		
	
	// Update is called once per frame
	void Update () {
		if (statTracking.identifyLevel () == "Q0")
			numAtt.text = "";
		else
			numAtt.text = attempts.ToString ();

		if (Input.GetKeyDown (KeyCode.C))
			cam.toggleMagnifier ();


		if (!timesUp && hasStarted) {
			runTimer ();
		}
		if (!hasCleared && startObjects <= 0) {
			levelClear ();
		}

		if (getScore) {
			if (canCallCoroutine){
				StartCoroutine (displayHighScore());
				StartCoroutine (retrieveGrade ());
			}
		}



	}

	IEnumerator dispPan ()
	{
		yield return ppM.getLevelInst ();
		if (ppM.getLevelInst () == 1)
			ic.dispInstPanel ();
	}

	public bool gethasStarted(){
		return hasStarted;
	}

	public void sethasStarted(bool hs){
		hasStarted = hs;
	}

	IEnumerator retrieveGrade(){
		WWW wwwRetrieveGrade = new WWW (urlRetrieveGrade); //Retrieves the data of the entire database.
		yield return wwwRetrieveGrade;
		stringGrade = wwwRetrieveGrade.text;
		grade = int.Parse (stringGrade);
		print ("Grade Retrieved: " + grade);
		if (grade > 10)
			grade = 10;
	}

	IEnumerator retrieveMedals(){
		for (int i = 0; i < 3; i++) {
			yield return new WaitForSeconds (1);
			string urlRetrieveBronze = urlRetrieveMedals + "&medal=Bronze";
			WWW wwwRetrieveBronze = new WWW (urlRetrieveBronze);
			yield return wwwRetrieveBronze;
			bronMedals = wwwRetrieveBronze.text;
			bronMedalsi = int.Parse (bronMedals);
			print ("bronze: " + bronMedalsi); 

			string urlRetrieveSilver = urlRetrieveMedals + "&medal=Silver";
			WWW wwwRetrieveSilver = new WWW (urlRetrieveSilver);
			yield return wwwRetrieveSilver;
			silvMedals = wwwRetrieveSilver.text;
			silvMedalsi = int.Parse (silvMedals);
			print ("Silver: " + silvMedalsi);

			string urlRetrieveGold = urlRetrieveMedals + "&medal=Gold";
			WWW wwwRetrieveGold = new WWW (urlRetrieveGold);
			yield return wwwRetrieveGold;
			goldMedals = wwwRetrieveGold.text;
			goldMedalsi = int.Parse (goldMedals);
			print ("Gold: " + goldMedalsi);

			string urlRetrievePlat = urlRetrieveMedals + "&medal=Platinum";
			WWW wwwRetrievePlatinum = new WWW (urlRetrievePlat);
			yield return wwwRetrievePlatinum;
			platMedals = wwwRetrievePlatinum.text;
			platMedalsi = int.Parse (platMedals);
			print ("Platinum: " + platMedalsi);
		}
	}

	void uploadMedal(string medColor, int medNum){
		urlUpload += "&medal=" + medColor + "&medalnum=" + medNum;
		print (urlUpload); 
	}

	IEnumerator uploadScore(){
		urlUpload += "&Name=" + userName + "&Display=" + playerManager.displayName + "&Accuracy=" + fscoring.ToString("F2") + "&AccuracyRank=" + medalToUpload () + "&Time=" + time.ToString() + "&SpeedRank=" + rankSpd.ToString();
		WWW wwwUpload = new WWW (urlUpload);
		yield return wwwUpload;
		print (urlUpload);  
	}

	IEnumerator uploadGrade(){
		urlUploadGrade = urlUploadGrade  + "&Name=" + userName + "&" + levelName + "=" + grade;
		WWW wwwUploadGrade = new WWW (urlUploadGrade);
		yield return wwwUploadGrade;

		print (grade + " Grade uploaded to " + urlUploadGrade);
	}



	//Runs the time.
	void runTimer (){
		//Timer Count
		timeSpent += Time.deltaTime;
		seconds += Time.deltaTime;
		if (seconds > 59.5f) {
			minutes += 1;
			seconds = -0.5f;
		}
		//Display the timer
		time = minutes + ":";
		if (seconds < 9.5f)
			time += "0" + seconds.ToString ("F0");
		else 
			time += seconds.ToString ("F0");
		timer.text = time;
		string.Format ("00:00", time);

		//Display the score
		point.text = score.ToString();
	}

	//Evaluate the rank
	void displayRank(){
		bronze.color = new Color (1f, 1f, 1f, 0.5f);
		bronzeT.color = new Color (0f, 0f, 0f, 0.5f);
		silver.color = new Color (1f, 1f, 1f, 0.5f);
		silverT.color = new Color (0f, 0f, 0f, 0.5f);
		gold.color = new Color (1f, 1f, 1f, 0.5f);
		goldT.color = new Color (0f, 0f, 0f, 0.5f);
		platinum.color = new Color (1f, 1f, 1f, 0.5f);
		platinumT.color = new Color (0f, 0f, 0f, 0.5f);

		bronzeT.text = bronMedalsi.ToString ();
		silverT.text = silvMedalsi.ToString ();
		goldT.text = goldMedalsi.ToString ();
		platinumT.text = platMedalsi.ToString ();
		if (!resetting) {
			if (fscoring < finSilv  || formulaAcc < goalAcc) {
				//rankAcc = State.Bronze;
				bronze.color = new Color (1f, 1f, 1f, 1f); //Make Image Visible
				bronzeT.color = new Color (0f, 0f, 0f, 1f);
				//accRank.sprite = medals[0]; //Array 0 is Bronze Medal
				grade += BRONGRADE;
				print ("Grade: " + grade);
				bronMedalsi++;
				uploadMedal ("Bronze", bronMedalsi);
				bronzeT.text = bronMedalsi.ToString ();

			} else if (fscoring >= finSilv && fscoring <= finGld && formulaAcc >= goalAcc) {
				//rankAcc = State.Silver;
				silver.color = new Color (1f, 1f, 1f, 1f);  //Make Image Visible
				silverT.color = new Color (0f, 0f, 0f, 1f);
				//accRank.sprite = medals[1]; //Array 1 is Silver Medal
				//Adds 1 point to player's grade, uploads to database.
				grade += SILVGRADE;
				print ("Grade: " + grade);
				silvMedalsi++;
				uploadMedal ("Silver", silvMedalsi);
				silverT.text = silvMedalsi.ToString ();

			} else if (fscoring > finGld && fscoring < finPlat && formulaAcc >= goalAcc) {
				//rankAcc = State.Gold;
				gold.color = new Color (1f, 1f, 1f, 1f); //Make Image Visible
				goldT.color = new Color (0f, 0f, 0f, 1f);
				//accRank.sprite = medals[2]; //Array 2 is Gold Medal
				//Adds 2 points to player's grade, uploads to database.
				grade += GOLDGRADE;
				print ("Grade" + grade);
				goldMedalsi++;
				uploadMedal ("Gold", goldMedalsi);
				goldT.text = goldMedalsi.ToString ();

			} else if (fscoring >= finPlat && formulaAcc >= goalAcc) {
				//rankAcc = State.Platinum;
				platinum.color = new Color (1f, 1f, 1f, 1f); //Make Image Visible
				platinumT.color = new Color (0f, 0f, 0f, 1f);
				//accRank.sprite = medals[3]; //Array 3 is Platinum Medal
				//Adds 3 points to player's grade, uploads to database.
				grade += PLATGRADE;
				print ("Grade" + grade);
				platMedalsi++;
				uploadMedal ("Platinum", platMedalsi);
				platinumT.text = platMedalsi.ToString ();
			}
			if (grade > 10)
				grade = 10;

			if (statTracking.levelIdentity == "Q0" && grade <= 0)
				grade = 1;

			if (formulaSpeed < spdSilv) {
				rankSpd = State.Bronze;
				//bronze.color = new Color (1f, 1f, 1f, 1f); //Make Image Visible
				//spdRank.sprite = medals[0]; //Array 0 is Bronze Medal
				//print ("Grade" + grade);
		
			} else if (formulaSpeed >= spdSilv && formulaSpeed <= spdGld) {
				rankSpd = State.Silver;
				//silver.color = new Color (1f, 1f, 1f, 1f); //Make Image Visible
				//spdRank.sprite = medals[1]; //Array 1 is Silver Medal
				//Adds 1 points to player's grade, uploads to database.
				//grade += 1;
				//print ("Grade" + grade);
		
			} else if (formulaSpeed > spdGld && formulaSpeed < spdPlat) {
				rankSpd = State.Gold;
				//gold.color = new Color (1f, 1f, 1f, 1f); //Make Image Visible
				//spdRank.sprite = medals[2]; //Array 2 is Gold Medal
				//Adds 2 points to player's grade, uploads to database.
				//grade += 2;
				//print ("Grade" + grade);
		
			} else if (formulaSpeed >= spdPlat) {
				rankSpd = State.Platinum;
				//platinum.color = new Color (1f, 1f, 1f, 1f); //Make Image Visible
				//spdRank.sprite = medals[3]; //Array 3 is Platinum Medal
				//Adds 3 points to player's grade, uploads to database.
				//grade += 3;
				//print ("Grade" + grade);
		
			}
			medalToUpload ();
		}
	}

	private string medalToUpload (){
		if (platMedalsi >= 1)
			return "Platinum";
		else if (platMedalsi <= 0 && goldMedalsi >= 1)
			return "Gold";
		else if (platMedalsi <= 0 && goldMedalsi <= 0 && silvMedalsi >= 1)
			return "Silver";
		else {
			return "Bronze";
		}
	}

	//Displays the High Score in the final results screen.
	IEnumerator displayHighScore(){
		if (resetting) {
			congratsText.text = "You have reset " + playerManager.displayName + "...";
		} else if (!resetting) {
			congratsText.text = "Congratulations " + playerManager.displayName + "!";
		}

		if (statTracking.levelIdentity != "Q0") {
			finalScore.text = "";

			int n = 0;
			while (n <= 2) {
				canCallCoroutine = false;
				yield return new WaitForSeconds (1);
				WWW wwwRetrieve = new WWW (urlRetrieve); //Retrieves the data of the entire database.
				yield return wwwRetrieve;
				finalScore.text = "Retrieving Scores...";
				print ("Score Retrieved.");
				++n;
				canCallCoroutine = true;
				if (n > 2) {
					finalScore.text = wwwRetrieve.text;
				}
			}
		} else {
			finalScore.alignment = TextAnchor.MiddleCenter;
			finalScore.text = "In non-pre-test levels\nthis area will display\nyour score among your\nclassmates' scores.";
		}

			getScore = false;
		
	}

	//When level is clear, bring up the win canvas,
	//display the results,
	//and write final output to file for player stats file.
	void levelClear (){
	//if (startObjects <= 0) {
		timesUp = true; //Stops the timer.
		if (statTracking.levelIdentity == "Q0")
			improveScoreButton.gameObject.SetActive (false);
		calcScore ();
		restrictScore(); //Makes sure score is never below 0.
		if (statTracking.identifyLevel () != "Q0")
			displayRank ();
		else {
			grade = 10;
			medalToUpload ();
		}
		if (!resetting)
			StartCoroutine (uploadGrade());
		accuracy.text = percentAcc.ToString("F0") + "%";
		speed.text = time.ToString();
		if (statTracking.identifyLevel () != "Q0") {
			timeScore.text = formulaSpeed.ToString ("F1");
			displayTargetScore ();
			accScore.text = formulaAcc.ToString ("F1");
			finalS.text = fscoring.ToString ("F2");
		}
		statTracking.score = fscoring.ToString ("F2");
		statTracking.time = time.ToString ();
		statTracking.seconds = timeSpent.ToString ("F2");
		statTracking.fauxGrade = fauxTextScore.ToString ("F2");
		if (resetting)
			statTracking.quit = "Y";
		else
			statTracking.quit = "N";
		cam.stopMagnifier ();
		if (!resetting)
			Invoke ("displayCanvas", 1f);
		else
			displayCanvas ();

		if (playerManager.getOptin () == "true")
			statTracking.doTheUpload ();
		if (!resetting){
			StartCoroutine (uploadScore());
		}
		getScore = true;

		if (statTracking.levelIdentity != "Q0") {
			if (grade >= 10) {
				graduation.text = "Congratulations!  You may continue improving your score, or try the next level.";
				graduation.color = new Color (.3f, .5f, 1f);
				if (SceneManager.GetActiveScene ().name != "Nerves3")
					nextLevel.gameObject.SetActive (true);
				if (SceneManager.GetActiveScene ().name == "Nerves3") {
					graduation.text = "";
					graduation.text = "Congratulations!  You have completed the Bone and Nerves Region of the game.";
					graduation.text += " Return to the main menu to move to the Muscle Region of the game!";
				}
				if (grade > 10) {
					grade = 5;
				} 
			} else {
				//int gradeLeft = 10 - grade;
				graduation.text = "Collect 2 silver, 1 gold, or 1 platinum medal(s) to reach the next level.";
				graduation.color = new Color (1f, 0f, 0f);
			}
		} else {
			graduation.text = "Congratulations on getting through the Baseline Test.  Return to the main menu to begin learning anatomy!";
		}
	
		hasCleared = true; //Ensures that this method is only run once.
	//}
	}

	void displayCanvas ()
	{
		winCanvas.gameObject.SetActive (true);
		winCanvas.transform.position = new Vector3 (-91, -22, 0);
		restartButton.gameObject.SetActive (false);
	}

	public void addMuscPlaced(){
		musclesPlaced++;
	}

	void calcScore ()
	{
		percentAcc = (musclesPlaced / (attempts+muscleCount)) * 100;
		formulaAcc = (score / muscleCount) * SCOREM;
		if (resetting)
			formulaSpeed = 0;
		else
			formulaSpeed = allowedTime - timeSpent;

		fscoring = formulaAcc + formulaSpeed;
		fauxTextScore = (fauxTestPoints / muscleCount) * 100;


	}


	//Restricts the final score to never be below zero.
	void restrictScore(){
		if (fscoring < 0) {
			fscoring = 0;
		}
		if (formulaAcc < 0) {
			formulaAcc = 0;
		}
		/*if (formulaSpeed < 0) {
			formulaSpeed = 0;
		}*/
	}

	void displayTargetScore(){
		int medalDivider;
		if (statTracking.levelIdentity != "Q0") {
			if (medalToUpload () == "Platinum") {
				targetScore.text = "***";
				medalDivider = 1;

			} else if (medalToUpload () == "Gold") {
				targetScore.text = " / " + finPlat;
				medalDivider = 1;


			} else if (medalToUpload () == "Silver") {
				targetScore.text = " / " + finGld;
				medalDivider = 2;

			} else {
				targetScore.text = " / " + finSilv;
				medalDivider = 4;

			}
			targetAcc.text = goalAcc.ToString ();


			targetTime.text = displayTargetTime (allowedTime - BASETIME/medalDivider);
		} else {
			targetText.text = "";
			targetAcc.text = "";
			targetScore.text = "";
			targetTime.text = "";

		}

	}

	void roundTime ()
	{
		allowedTime = allowedTime / 5;
		allowedTime = (float)Math.Round (allowedTime) * 5;
	}

	string displayTargetTime(float time){
//		time = roundTime (time);
		float tarMinutes = 0;
		float tarSeconds = 0;
		while (time >= 60){
			tarMinutes = tarMinutes + 1;
			time -= 60;
		}
		tarSeconds = time;
		string newTime;
		if (tarSeconds == 0)
			newTime = tarMinutes.ToString () + ":" + "00";
		else
			newTime = tarMinutes.ToString () + ":" + tarSeconds.ToString ();
		return newTime;

	}

	//Restarts the level if button is clicked.
	public void resetLevel(){
		resetted = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	//Quits the application if button is clicked.
	//NOTE: May not be needed, depends on final version.
	public void quit(){
		Application.Quit ();
	}

	public void restart(){
		resetting = true;
		levelClear ();

	}

	//Loads the selected level
	public void loadLevel(string levelName){
		SceneManager.LoadScene (levelName);
	}

	public void loadNextLevel (){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
	}
		
	//Mutate muscleCount
	public void incMuscleCount(){
		muscleCount++;
		//fauxTestPoints = muscleCount;
	}

	public void addMuscleCount(int m){
		muscleCount += m;
		//fauxTestPoints = muscleCount;
	}

	public void testScoreAddition(){
		fauxTestPoints++;
	}
}
