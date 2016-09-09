using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScorePanel : MonoBehaviour {
	//ScorePanel
	public GameObject scorePanel;

	//Url
	public string urlRetrievePersonal;
	private string urlRetrievePanel;
	private string levelName;
	public string levelHover;
	private string displayName;
	private string grizzName;

	//Player Name
	private PlayerManager playerManager;

	//Level Name
	//private IdentifyLevel identifyLevel;

	//Player Stats
	string acc;
	string accRank;
	string time;
	string spdRank;
	string top3;

	//Text and Images to display the stats

	public Text username;
	public Text score;
	public Image rankAcc;
	public Image rankSpd;
	public Text topThree;
	public Sprite[] medals;

	//Bool to slow down the number of times the website is requested
	private bool canCallCoroutine = true;

	// Use this for initialization
	void Start () {
		//Get the Player's display name
		playerManager = FindObjectOfType<PlayerManager>();

		grizzName = playerManager.getUserName();
		displayName = playerManager.getDisplayName();

		//Get the name of the button's level
		//identifyLevel = FindObjectOfType<IdentifyLevel>();

		//Hide all tooltips
		//scorePanel.gameObject.SetActive (false);

		levelHover = transform.parent.name;



		if (levelHover != null) {
			getLevelName ();
		} else {
			Debug.Log ("No level name found");
		}

		username.text = displayName;

	}

	// Update is called once per frame
	void Update () {
		//levelName = levelName;

		urlRetrievePersonal = "https://ilta.oakland.edu/retrievePersonal.php?level=" + levelName + "&user=" + grizzName; //Main Server.
		urlRetrievePanel = "https://ilta.oakland.edu/retrievePanel.php?level=" + levelName; //Main Server.

		//urlRetrievePersonal = "goeteeks.x10host.com/Tutorial/retrievePersonal.php?level=" + levelName + "&user=" + grizzName; //Backup Server.
		//urlRetrievePanel = "goeteeks.x10host.com/Tutorial/retrievePanel.php?level=" + levelName; //Backup Server.

		if (acc == null){
			score.text = "Loading Personal Score and Time";
			score.alignment = TextAnchor.MiddleCenter;
			topThree.text = "Loading High Scores...";
			topThree.alignment = TextAnchor.UpperCenter;
		}

		if (canCallCoroutine) {
			StartCoroutine (retrievePersonal ());
			StartCoroutine (retrievePanel ());
			score.text = "Score: " + acc + "\n\nTime: " + time;
			score.alignment = TextAnchor.UpperLeft;
			getMedalSprite ();
			topThree.text = top3;
			topThree.alignment = TextAnchor.UpperLeft;
		}
	}
	//Get score, ranks, and time for the current level highlighted by mouse.
	IEnumerator retrievePersonal(){
		//Slow down the number of calls
		canCallCoroutine = false;
		yield return new WaitForSeconds (1);


		//Get Accuracy Score
		string retrieveAccuracy = urlRetrievePersonal + "&column=Accuracy";
		WWW wwwRetrieveAccuracy = new WWW (retrieveAccuracy);
		yield return wwwRetrieveAccuracy;
		acc = wwwRetrieveAccuracy.text;
		//print (levelName + ": " + acc);

		//Get Accuracy Rank
		string retrieveAccRank = urlRetrievePersonal + "&column=AccuracyRank";
		WWW wwwRetrieveAccRank = new WWW (retrieveAccRank);
		yield return wwwRetrieveAccRank;
		accRank = wwwRetrieveAccRank.text;
		//print (levelName + ": " + accRank);

		//Get Time
		string retrieveTime = urlRetrievePersonal + "&column=Time";
		WWW wwwRetrieveTime = new WWW (retrieveTime);
		yield return wwwRetrieveTime;
		time = wwwRetrieveTime.text;
		//print (levelName + ": " + time);

		//Get Time Rank
		string retrieveSpdRank = urlRetrievePersonal + "&column=SpeedRank";
		WWW wwwRetrieveSpdRank = new WWW (retrieveSpdRank);
		yield return wwwRetrieveSpdRank;
		spdRank = wwwRetrieveSpdRank.text;
		//print (levelName + ": " + spdRank);
		canCallCoroutine = true;
	}

	//Get the top 3 scores
	IEnumerator retrievePanel(){
		canCallCoroutine = false;
		yield return new WaitForSeconds (1);
		WWW wwwRetrievePanel = new WWW (urlRetrievePanel);
		yield return wwwRetrievePanel;
		top3 = wwwRetrievePanel.text;
		canCallCoroutine = true;
	}

	void getLevelName(){
		switch (levelHover) {
		case "Placement Button":
			levelName = "UpperArm1";
			break;
		case "Identify Button":
			levelName = "UpperArm2";
			break;
		case "Insertion Button":
			levelName = "UpperArm3";
			break;
		case "Nerves Button":
			levelName = "UpperArm4";
			break;
		case "Function Button":
			levelName = "UpperArm5";
			break;
		case "Bone Button":
			levelName = "UpperArm6";
			break;
		case "FA Placement Button":
			levelName = "Forearm1A";
			break;
		case "FP Placement Button":
			levelName = "Forearm1B";
			break;
		case "FA Naming Button":
			levelName = "Forearm2A";
			break;
		case "FP Naming Button":
			levelName = "Forearm2B";
			break;
		case "FA Attachments Button":
			levelName = "Forearm4A";
			break;
		case "FP Attachments Button":
			levelName = "Forearm4B";
			break;
		case "FA Innervation Button":
			levelName = "Forearm5A";
			break;
		case "FP Innervation Button":
			levelName = "Forearm5B";
			break;
		case "FA Action Button":
			levelName = "Forearm6A";
			break;
		case "FP Action Button":
			levelName = "Forearm6B";
			break;
		case "HA Placement Button":
			levelName = "Hand1A";
			break;
		case "HA Identify Button":
			levelName = "Hand2A";
			break;
		case "HA Bony Structure Button":
			levelName = "Hand3";
			break;
		case "HA Attachments Button":
			levelName = "Hand4A";
			break;
		case "HA Innervation Button":
			levelName = "Hand5A";
			break;
		case "HA Action Button":
			levelName = "Hand6A";
			break;
		/*default:
			levelName = "UpperArm1";
			break;*/
		}	
	}
		
	//Set the Image of the medal by rank
	void getMedalSprite(){
		//Accuracy Rank gets the blue banner medals
		switch (accRank) {
		case "Bronze":
			rankAcc.gameObject.SetActive(true);
			rankAcc.sprite = medals [5];
			break;
		case "Silver":
			rankAcc.gameObject.SetActive(true);
			rankAcc.sprite = medals [6];
			break;
		case "Gold":
			rankAcc.gameObject.SetActive(true);
			rankAcc.sprite = medals [7];
			break;
		case "Platinum":
			rankAcc.gameObject.SetActive(true);
			rankAcc.sprite = medals [8];
			break;
		default:
			rankAcc.gameObject.SetActive (false);
			break;
		}

		//Speed Rank gets the yellow banner medals
		switch (spdRank) {
		case "Bronze":
			rankSpd.gameObject.SetActive(true);
			rankSpd.sprite = medals [10];
			break;
		case "Silver":
			rankSpd.gameObject.SetActive(true);
			rankSpd.sprite = medals [11];
			break;
		case "Gold":
			rankSpd.gameObject.SetActive(true);
			rankSpd.sprite = medals [12];
			break;
		case "Platinum":
			rankSpd.gameObject.SetActive(true);
			rankSpd.sprite = medals [13];
			break;
		default:
			rankSpd.gameObject.SetActive(false);
			break;
		}
	}

	//Show the score panel (when mouse is hovering over the level button)
	public void showScorePanal(){
		if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.WP8Player && Application.platform != RuntimePlatform.BlackBerryPlayer && Application.platform != RuntimePlatform.IPhonePlayer) {
			scorePanel.gameObject.SetActive (true);
			canCallCoroutine = true;
		}
	}

	//Hide the score panel (when mouse is not hovering over any level buttons)
	public void hideScorePanel(){
		scorePanel.gameObject.SetActive (false);
		canCallCoroutine = true;
	}
}