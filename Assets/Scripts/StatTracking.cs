using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StatTracking : MonoBehaviour {
	//Setting up Database Access
	private string urlUploadStats;

	//Setting up Script Access
	private PlayerManager playerManager;

	//Setting up variables.
	public string levelName;  //Identify the name of the level
	public string levelIdentity; //Identify the level type (Position, Identify, etc.)

	//Strings for Name, Score, and Time
	public string userName;
	public string displayName;
	private string profi;
	public string score;
	public string time;
	public string quit;
	public string seconds;
	public string fauxGrade;
	private const int MAXBPART = 20;

	//public string[] sPart; //String Array for each body part's name.
	public string[] iPart; //String Array for each body part's attempt counter.


	// Use this for initialization
	void Start () {
		//Get the name of the level
		levelName = SceneManager.GetActiveScene ().name;
		identifyLevel ();

		//Grab PlayerManager
		playerManager = FindObjectOfType<PlayerManager>();

		if (GameObject.Find ("Player Manager") != null) { 
			//Identify Player;
			userName = playerManager.userName;
			displayName = playerManager.displayName;
			profi = playerManager.getProf ();
		

			urlUploadStats = ("https://ilta.oakland.edu/uploadstatTracking.php?Name=" + userName + "&Privatekey=j5G1L23" + "&Display=" + displayName + "&Expertise=" + profi + "&level=" + levelName); //Backup Server
			//urlUploadStats = ("http://goeteeks.x10host.com/Tutorial/uploadstatTracking.php?Name=" + userName + "&Privatekey=j5G1L23" + "&Display=" + displayName + "&Expertise=" + profi + "&level=" + levelName); //Backup Server
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (levelIdentity == null)
			identifyLevel ();
		
		if (iPart.Length < MAXBPART) 
			iPart = new string[MAXBPART];
	
	}

	//Get the type of level (IE: Placement, Identify, etc.)
	public string identifyLevel(){
		switch (levelName) {
		case "Armbone0":
			levelIdentity = "Q0";
			break;
		case "Armbone1":
			levelIdentity = "Q5";
			break;
		case "Armbone2":
			levelIdentity = "Q5";
			break;
		case "Armbone3":
			levelIdentity = "Q5";
			break;
		case "Nerves1":
			levelIdentity = "Q6";
			break;
		case "Nerves2":
			levelIdentity = "Q7";
			break;
		case "Nerves3":
			levelIdentity = "QC";
			break;
		case "UpperArm0":
			levelIdentity = "Q0";
			break;
		case "UpperArm1":
			levelIdentity = "P";
			break;
		case "UpperArm2":
			levelIdentity = "Q1";
			break;
		case "UpperArm3":
			levelIdentity = "Q2";
			break;
		case "UpperArm4":
			levelIdentity = "Q3";
			break;
		case "UpperArm5":
			levelIdentity = "Q4";
			break;
		case "Forearm1A":
			levelIdentity = "P";
			break;
		case "Forearm2A":
			levelIdentity = "Q1";
			break;
		case "Forearm1B":
			levelIdentity = "P";
			break;
		case "Forearm2B":
			levelIdentity = "Q1";
			break;
		case "Forearm3A":
			levelIdentity = "Q2";
			break;
		case "Forearm3B":
			levelIdentity = "Q2";
			break;
		case "Forearm4A":
			levelIdentity = "Q3";
			break;
		case "Forearm4B":
			levelIdentity = "Q3";
			break;
		case "Forearm5A":
			levelIdentity = "Q4";
			break;
		case "Forearm5B":
			levelIdentity = "Q4";
			break;
		case "Hand1A":
			levelIdentity = "P";
			break;
		case "Hand2A":
			levelIdentity = "Q1";
			break;
		case "Hand3A":
			levelIdentity = "Q2";
			break;
		case "Hand4A":
			levelIdentity = "Q3";
			break;
		case "Hand5A":
			levelIdentity = "Q4";
			break;
		case "Hand6A":
			levelIdentity = "QC";
			break;
		}
		return levelIdentity;
	}

	//Upload Everything to the database!
	IEnumerator uploadStats(){
		string urlUploadStats2 = urlUploadStats + "&quit=" + quit + "&Score=" + score + "&Seconds=" + seconds + "&fauxGrade=" + fauxGrade + "&Time=" + time;
		for (int n = 0; n < iPart.Length; n++)
			urlUploadStats2 += /*"&bPart" + (n+1) + "=" + "Part" + n + */"&Part" + n + "=" + iPart [n];

		WWW wwwUploadStats = new WWW (urlUploadStats2);
		yield return wwwUploadStats;
		print (urlUploadStats2);

	}

	public void doTheUpload(){
		StartCoroutine (uploadStats ());
	}

}
