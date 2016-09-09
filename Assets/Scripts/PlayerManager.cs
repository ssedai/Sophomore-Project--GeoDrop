using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	//private string urlUser;  //Username by cookies
	//private string urlDisplay; //Displayname by cookies
	private string urlUploadLogin; //Upload login information to database
	public string displayName; //String to set the Displayname to
	public string userName; //String to set the Username to
	private string password; //String to set the user's password to
	private string slt; //The salt.
	public Text nameDisplay; //Displays the Username and Displayname
	public InputField userNameInput; //Username by manual input
	public InputField passwordInput; //Displayname by manual input
	private Button loginButton;
	private Button logoutButton;
	private string prof;
	private string optin;
	private string urlUG; //Url for uploading a dummy grade to a dummy level column.  The idea is to create a row of grades the first time the user logs in
	private string urlLogin; //Url to log the user in.
	//private string urlReg; //Url to register the user.
	private bool canLogin = false; //A bool to check if the user is allowed to login or not.
	private bool canStart = false; //If a Username AND Displayname are set, the user can start.
	private bool demoMode = false; //Set this to true to bring up the input fields instead of relying on cookies
	private bool testMode = false; //Set this to true to unlock specific levels.

	bool connectionOkay = false;
	static PlayerManager instance = null;

	//private LevelManager levelManager;


	void Awake(){
		//Make sure there is only one copy of the Player Manager.
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);  //Keep the Player Manager around when loading new scenes.
		}
	}
	// Use this for initialization
	void Start () {
		//levelManager = FindObjectOfType<LevelManager> ();


	

		if (SceneManager.GetActiveScene ().name == "main") {
			loginButton = GameObject.Find ("Login Button").GetComponent <Button> ();
			logoutButton = GameObject.Find ("Logout Button").GetComponent <Button> ();
			logoutButton.gameObject.SetActive (false);
			if (demoMode)
				loginButton.gameObject.SetActive (false);
		}
		

		

		//Sets the nameDisplay text to either instruct the user to input a Username and Displayname, or to wait until the cookies are retrieved.
		//if (demoMode) {
			if (nameDisplay != null)
			nameDisplay.text = ("Please enter a Username to start.");
		/*} else {
			if (nameDisplay != null)
			nameDisplay.text = ("Retrieving Information...");
		}*/

		//Make sure that the text fields are disabled at the start.
		//Only if demoMode is set should they be activated (checked below).
		if (SceneManager.GetActiveScene ().name == "main") {
			userNameInput.gameObject.SetActive (false);
			passwordInput.gameObject.SetActive (false);
		}

		if (PlayerPrefs.HasKey ("Username") && PlayerPrefs.HasKey ("Displayname") && PlayerPrefs.HasKey ("Proficiency")) {
			if (connectionOkay) {
				userName = PlayerPrefs.GetString ("Username");
				prof = PlayerPrefs.GetString ("Proficiency");		
				displayName = PlayerPrefs.GetString ("Displayname");
				canStart = true;
			} else
				logout ();
		} else {
			userNameInput.gameObject.SetActive (true);
			passwordInput.gameObject.SetActive (true);
		}

		if (PlayerPrefs.HasKey ("Optin"))
			optin = PlayerPrefs.GetString ("Optin");




		urlUG = ("https://ilta.oakland.edu/uploadgrades.php");  //The URL for uploading grade, main server.
		//urlUG = ("http://goeteeks.x10host.com/Tutorial/uploadgrades.php");  //The URL for uploading grade, backup server.
		urlUploadLogin = ("https://ilta.oakland.edu/uploadlogin.php?Privatekey=j5G1L23"); //The URL for uploading login information, main server.
		//urlUploadLogin = ("http://goeteeks.x10host.com/Tutorial/uploadlogin.php?Privatekey=j5G1L23"); //The URL for uploading login information, backup server.

		//urlUser = "http://www.secs.oakland.edu/~nferman/get_cookie_user.php"; //The URL for retrieving the Username cookies.

		//urlDisplay = "http://www.secs.oakland.edu/~nferman/get_cookie_display.php"; //The URL for retrieving the Displayname cookies.

		urlLogin = "https://ilta.oakland.edu/loginCS.php"; //main server.
		//urlLogin = "http://goeteeks.x10host.com/Tutorial/loginCS.php"; //backup server

//		urlReg = "http://goeteeks.x10host.com/Tutorial/registerCS.php";


		
		//if (!demoMode) {
			//If demoMode is off, retrieve the cookies for the Username and Displayname.
			/*WWW wwwUser = new WWW (urlUser);
			yield return wwwUser;
			userName = wwwUser.text;

			WWW wwwDisplay = new WWW (urlDisplay);
			yield return wwwDisplay;
			displayName = wwwDisplay.text;*/
	/*	} else {
			//If demoMode is on, display the text fields for manual input of Username and Displayname.
			if (SceneManager.GetActiveScene ().name == "main") {
				userNameInput.gameObject.SetActive (true);
				displayNameInput.gameObject.SetActive (true);
			}
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		//if (demoMode) {
			//Sets Username and Displayname to the text inserted into the text fields.
		/*	if (userNameInput != null)
			userName = userNameInput.text;
			if (displayNameInput != null)
			displayName = displayNameInput.text;*/
		//}

		//If the user has input at least one letter to the Username and Displayname, they may start the game.
		/*if (SceneManager.GetActiveScene ().name == "main") {
			if (displayName.Length > 0 && userName.Length > 0 && prof.Length > 0) {
				canStart = true;
			} else
				canStart = false;
		}*/

		//Changes the welcome text to reflect the Username and Displayname.
		if (canStart && SceneManager.GetActiveScene ().name == "main" && !demoMode) {
			if (nameDisplay != null)
				nameDisplay.text = ("Welcome " + userName + ".  You are logged in as " + displayName + "\nPress Start to begin learning anatomy!");
			
			userNameInput.gameObject.SetActive (false);
			passwordInput.gameObject.SetActive (false);
			loginButton.gameObject.SetActive (false);
			logoutButton.gameObject.SetActive (true);
		} else if (canLogin && SceneManager.GetActiveScene ().name == "main" && !demoMode) {
			//nameDisplay.text = ("Please enter your Username (Oakland Account) and Password to start.");

			userNameInput.gameObject.SetActive (true);
			passwordInput.gameObject.SetActive (true);
			loginButton.gameObject.SetActive (true);
			logoutButton.gameObject.SetActive (false);
		} /*else if (demoMode) {
			userNameInput.gameObject.SetActive (true);
			passwordInput.gameObject.SetActive (false);
			if (userNameInput.text.Length > 0)
				userName = userNameInput.text;
				displayName = userName;
				canStart = true;
			}*/
	}
	

	//Starts the game when button is pressed.
	public void startGame(){
		//Check if there is a Username and Displayname
		if (canStart) {
			if (!demoMode) {
				PlayerPrefs.SetString ("Username", userName);
				PlayerPrefs.SetString ("Proficiency", prof);
				PlayerPrefs.SetString ("Displayname", displayName);
				//if (optin != null)
				PlayerPrefs.SetString ("Optin", optin);
			}
			StartCoroutine (uploadGrade ());  //Upload 0 grade to Level0 to create a row of grade for the new user (does not affect a returning user).
			StartCoroutine (uploadLogin());
			SceneManager.LoadScene (levelName);  //Loads the  Level Select scene.
		}
	}

	string levelName = "LevelSelect";  //Sets the levelName string to the Level Select scene.

	//Uploads 0 grade to a dummy level.
	IEnumerator uploadGrade(){
		urlUG = urlUG  + "&Name=" + displayName + "&level=DummyLevel&DummyLevel=0";
		WWW wwwUploadGrade = new WWW (urlUG);
		yield return wwwUploadGrade;
		print (urlUploadLogin);

	}

	IEnumerator uploadLogin(){
		urlUploadLogin += "&Name=" + userName + "&Display=" + displayName + "&Expertise=" + prof;
		WWW wwwUploadLogin = new WWW (urlUploadLogin);
		yield return wwwUploadLogin;
	}

	//Find the user's information from the database.
	IEnumerator dlLogin(){
		
		//Set username.
		userName = userNameInput.text;
		string urlLogin0 = urlLogin + "?Name=" + userName;
		//Make sure username matches with database.
		string urlLogin1 = urlLogin0 + "&Column=Username";
		WWW wwwUrlLogin1 = new WWW (urlLogin1);
		yield return wwwUrlLogin1;
		print (wwwUrlLogin1.text); 
		if (userName != wwwUrlLogin1.text) {
			nameDisplay.text = "Username not recognized.  Try again or register.";
			canLogin = false;
		}
					
		password = passwordInput.text;
		string urlLogin2 = urlLogin0 + "&Column=Password&Password=" + password;
		WWW wwwUrlLogin2 = new WWW (urlLogin2);
		yield return wwwUrlLogin2;
		if (userName == wwwUrlLogin1.text && wwwUrlLogin2.text != "true") {
			nameDisplay.text = "Incorrect password.  Please try again.";
			canLogin = false;
		} else {
			canLogin = true;
		}

		//Get displayname from database and set it to this.
		string urlLogin3 = urlLogin0 + "&Column=Displayname";
		WWW wwwUrlLogin3 = new WWW (urlLogin3);
		yield return wwwUrlLogin3;
		displayName = wwwUrlLogin3.text;
		
		//Get proficiency from database and set it to this.	
		string urlLogin4 = urlLogin0 + "&Column=Proficiency";
		WWW wwwUrlLogin4 = new WWW (urlLogin4);
		yield return wwwUrlLogin4;
		prof = wwwUrlLogin4.text;

		//Get Opt-In permission from database and set it to this.	
		string urlLogin5 = urlLogin0 + "&Column=Optin";
		WWW wwwUrlLogin5 = new WWW (urlLogin5);
		yield return wwwUrlLogin5;
		optin = wwwUrlLogin5.text;

		if (displayName.Length > 0 && userName.Length > 0 && prof.Length > 0 && canLogin == true)
			canStart = true;

	}

	public void logout(){
		PlayerPrefs.DeleteKey ("Username");
		PlayerPrefs.DeleteKey ("Proficiency");
		PlayerPrefs.DeleteKey ("Displayname");
		PlayerPrefs.DeleteKey ("Optin");
		userName = null;
		displayName = null;
		prof = null;
		optin = null;
		canStart = false;
		if (SceneManager.GetActiveScene ().name == "main") {
			logoutButton.gameObject.SetActive (false);
			loginButton.gameObject.SetActive (true);
			userNameInput.gameObject.SetActive (true);
			userNameInput.text = "";
			passwordInput.gameObject.SetActive (true);
			passwordInput.text = "";
			nameDisplay.text = "Please enter your Username (Oakland Account) and Password to start.";
		}
	}

	public void startLogin(){
	StartCoroutine (dlLogin ());
	}

	private IEnumerator checkconnection(){
		
		//Set username.
		userName = PlayerPrefs.GetString ("Username");
		string urlLogin0 = urlLogin + "?Name=" + userName;
		//Make sure username matches with database.
		string urlLogin1 = urlLogin0 + "&Column=Username";
		WWW wwwUrlLogin1 = new WWW (urlLogin1);
		yield return wwwUrlLogin1;
		if (userName != wwwUrlLogin1.text) {
			nameDisplay.text = "Username not recognized.  Check your connection and login again.";
			connectionOkay = false;
		} else
			connectionOkay = true;
		return connectionOkay;
	}

	//Returns the Username
	public string getUserName(){
		return userName;
	}

	//Returns the Displayname
	public string getDisplayName(){
		return displayName;
	}

	//Returns whether the game is in Demo Mode or not
	public bool checkDemo(){
		return demoMode;
	}

	//Returns whether the game is in Test Mode or not
	public bool checkTestMode(){
		return testMode;
	}

	//Sets the string for User's submitted proficiency
	public void setProf(string expertise){
		prof = expertise;
		PlayerPrefs.SetString ("Proficiency", prof);
	}

	public string getProf(){
		return prof;
	}
	
	public string getOptin(){
	print (optin); 
		return optin;
	}
}