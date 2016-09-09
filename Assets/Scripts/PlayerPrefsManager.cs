using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	private string levelName;
	private int hasRead; //Integer, because PlayerPrefs doesn't do booleans :(
	private bool hr; //A boolean for hasRead
	private string prefLine; //This is a line used to determine which bool from Player Prefs we are working with.



	// Use this for initialization
	void Start () {
		//Get the name of the level.
		levelName = SceneManager.GetActiveScene ().name;

		/*If the player is new, create the necessary player preference variables.*/
				
		if (!PlayerPrefs.HasKey ("PreTestInstRead"))
			PlayerPrefs.SetInt ("PreTestInstRead", 1);
		
		if (!PlayerPrefs.HasKey ("PlaceInstRead"))
			PlayerPrefs.SetInt ("PlaceInstRead", 1);

		if (!PlayerPrefs.HasKey ("PlaceAndQRead"))
			PlayerPrefs.SetInt ("PlaceAndQRead", 1);

		if (!PlayerPrefs.HasKey ("Q1InstRead"))
			PlayerPrefs.SetInt ("Q1InstRead", 1);

		if (!PlayerPrefs.HasKey ("Q2InstRead"))
			PlayerPrefs.SetInt ("Q2InstRead", 1);

		if (!PlayerPrefs.HasKey ("TestInstRead"))
			PlayerPrefs.SetInt ("TestInstRead", 1);


		getLevelInst (); //Returns the 0 or 1 of the Player Pref line we are working with.
		print (hasRead); //Prints a 0 or 1 to console.
	}
	
	// Update is called once per frame
	void Update () {
		Invoke ("getLevelInst", 1f);  //Invokes getting this level after 1 second.
	}

	//Gets the name of the level, finds the Player Prefs key we're looking for, then sets the hasRead int to 0 or 1.
	public int getLevelInst(){
		levelName = SceneManager.GetActiveScene ().name;
		//Depending on the level name...
		switch (levelName) {
		case "Armbone0":
			prefLine = "PreTestInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Armbone1":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Armbone2":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Armbone3":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Armbone4":
			prefLine = "TestInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "UpperArm0":
			prefLine = "PreTestInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "UpperArm1":
			prefLine = "PlaceInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "UpperArm2":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "UpperArm3":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "UpperArm4":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "UpperArm5":
			prefLine = "Q2InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm1A":
			prefLine = "PlaceInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm1B":
			prefLine = "PlaceInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm2A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm2B":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm3A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm3B":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm4A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm4B":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm5A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm5B":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm6A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Forearm6B":
			prefLine = "Q2InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Hand1A":
			prefLine = "PlaceInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Hand2A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Hand3A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Hand4A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Hand5A":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Hand6A":
			prefLine = "TestInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Nerves0":
			prefLine = "PreTestInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Nerves1":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Nerves2":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Nerves3":
			prefLine = "Q1InstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		case "Nerves4":
			prefLine = "TestInstRead";  //Finds the key in Player Prefs
			hasRead = PlayerPrefs.GetInt (prefLine);  //Sets the hasRead boolean to the 0 or 1 key from PlayerPrefs
			break;
		default:
			prefLine = "";  //If the level we're on doesn't have a key, clear the prefLine.
			break;
		}

		return hasRead; //Return a 0 or 1.
	}

	//Toggles the hr Boolean by getting the 0 or 1 from getLevelInst() and behaving appropriately.
	public void toggleHR(){
		getLevelInst ();  //Get the 0 or 1.
		if (hasRead == 0)
			hr = false;  //If 0, set hr to False.
		else if (hasRead == 1)
			hr = true; //If 1, set hr to True.

		hr = !hr; //Swap the boolean!
			
		print (hr); //Print the boolean to the console.
		setRead (hr); //Set whether the Instructions were read or not to the canvas.
	}

	//Sets the Player Prefs key according to the bool submitted.
	public void setRead (bool i){
		
		
		if (i == true)
			PlayerPrefs.SetInt (prefLine, 1);  //Set the prefLine key to 1
		else if (i == false)
			PlayerPrefs.SetInt (prefLine, 0); //Set the prefLine key to 0
		print (prefLine + ": " + PlayerPrefs.GetInt (prefLine));  //Print which Player Prefs key has been set, and what it's been set to.
	}

	//Returns the prefLine the level is working with.
	public string getPrefLine(){
		return prefLine;
	}


}
