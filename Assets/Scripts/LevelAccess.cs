using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;
using System.Security;
using System;

public class LevelAccess : MonoBehaviour {


	
	//Setting up level access by grade.
	public Button armBone0;
	public Button armBone1;
	public Button armBone2;
	public Button armBone3;
	public Button nerve1;
	public Button nerve2;
	public Button nerve3;
	public Button upperArm;
	public Button upperArm0;
	public Button upperArm1;
	public Button upperArm2;
	public Button upperArm3;
	public Button upperArm4;
	public Button upperArm5;
	public Button forearm;
	public Button forearm1A;
	public Button forearm1B;
	public Button forearm2A;
	public Button forearm2B;
	public Button forearm3A;
	public Button forearm3B;
	public Button forearm4A;
	public Button forearm4B;
	public Button forearm5A;
	public Button forearm5B;
	//public Button forearm6A;
	//public Button forearm6B;
	public Button hand;
	public Button hand1A;
	public Button hand2A;
	public Button hand3A;
	public Button hand4A;
	public Button hand5A;
	public Button hand6A;

	//public Button finalLevelArm;

	//Setting up Grade by level.
	//Main URL to retrieve grade.
	private string urlRetrieve;

	//Integer variables for grade by level.
	private int igArmbone0;
	private int igArmbone1;
	private int igArmbone2;
	private int igArmbone3;
	//private int igArmbone4;
	//private int igNerves0;
	private int igNerves1;
	private int igNerves2;
	private int igNerves3;
	private int igUpperArm0;
	private int igUpperArm1;
	private int igUpperArm2;
	private int igUpperArm3;
	private int igUpperArm4;
	private int igUpperArm5;
	private int igLowerArm1A;
	private int igLowerArm1B;
	private int igLowerArm2A;
	private int igLowerArm2B;
	private int igLowerArm3A;
	private int igLowerArm3B;
	private int igLowerArm4A;
	private int igLowerArm4B;
	private int igLowerArm5A;
	private int igLowerArm5B;
	//private int igLowerArm6A;
	//private int igLowerArm6B;
	private int igHand1A;
	private int igHand2A;
	private int igHand3A;
	private int igHand4A;
	private int igHand5A;
	//private int igHand6A;



	//Access to other Scripts!
	//private LevelManager levelManager;
	private PlayerManager playerManager;

	//Enable or disable the quit button
	public Button quitButton;




	// Use this for initialization
	void Start () {
		//Find other Scripts!
		//levelManager = FindObjectOfType<LevelManager> ();
		playerManager = FindObjectOfType<PlayerManager> ();

		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.IPhonePlayer) {
			quitButton.gameObject.SetActive (true);
		} else {
			quitButton.gameObject.SetActive (false);
		}

		//Define the URL.
		urlRetrieve = ("https://ilta.oakland.edu/retrievegrades.php?Name=" + playerManager.userName); //Main Server
		//urlRetrieve = ("http://goeteeks.x10host.com/Tutorial/retrievegrades.php?Name=" + playerManager.userName); //Backup Server


		//Lock ALL levels except Armbone1 or Armbone0.
		armBone0.interactable = false;
		armBone1.interactable = false;
		armBone2.interactable = false;
		armBone3.interactable = false;
		upperArm.interactable = false;
		upperArm0.interactable = false;
		upperArm1.interactable = false;
		upperArm2.interactable = false;
		upperArm3.interactable = false;
		upperArm4.interactable = false;
		upperArm5.interactable = false;
		forearm.interactable = false;
		forearm1A.interactable = false;
		forearm1B.interactable = false;
		forearm2A.interactable = false;
		forearm2B.interactable = false;
		forearm3A.interactable = false;
		forearm3B.interactable = false;
		forearm4A.interactable = false;
		forearm4B.interactable = false;
		forearm5A.interactable = false;
		forearm5B.interactable = false;
		//forearm6A.interactable = false;
		//forearm6B.interactable = false;
		hand.interactable = false;
		hand1A.interactable = false;
		hand2A.interactable = false;
		hand3A.interactable = false;
		hand4A.interactable = false;
		hand5A.interactable = false;
		hand6A.interactable = false;
		nerve1.interactable = false;
		nerve2.interactable = false;
		nerve3.interactable = false;
		//finalLevelArm.gameObject.SetActive (false);
		//finalLevelArm.interactable = false;


		InvokeRepeating ("retrieval", 0f, 3f);

	








	}
	
	// Update is called once per frame
	void Update () {
		

		//Check the grade of each level and unlock the next level if grade is high enough.
		if (igArmbone0 <= 0) {
			armBone0.gameObject.SetActive (true);
			armBone0.interactable = true;
			armBone1.gameObject.SetActive (false);
		} else {
			armBone0.gameObject.SetActive (false);
			armBone1.gameObject.SetActive (true);
			armBone1.interactable = true;
		}

		if (igArmbone1 >= 10){// || playerManager.checkTestMode () == true){// || playerManager.checkDemo() == true){
			armBone2.interactable = true;
		}
		if (igArmbone2 >= 10){// || playerManager.checkTestMode () == true){// || playerManager.checkDemo() == true){
			armBone3.interactable = true;
		}
		if (igArmbone3 >= 10) {// || playerManager.checkTestMode () == true){// || playerManager.checkDemo() == true){
			nerve1.interactable = true;
		}
		if (igNerves1 >= 10) {
			nerve2.interactable = true;
		}
		if (igNerves2 >= 10) {
			nerve3.interactable = true;
		}
		if (igNerves3 >= 10){// || playerManager.checkTestMode () == true){// || playerManager.checkDemo() == true){
			upperArm.interactable = true;
		}

		if (igUpperArm0 <= 0) {
			upperArm0.gameObject.SetActive (true);
			upperArm0.interactable = true;
			upperArm1.gameObject.SetActive (false);
		} else {
			upperArm0.gameObject.SetActive (false);
			upperArm1.gameObject.SetActive (true);
			upperArm1.interactable = true;
		}
		if (igUpperArm1 >= 10){// || playerManager.checkTestMode () == true){// || playerManager.checkDemo() == true){
			upperArm2.interactable = true;
		}
		if (igUpperArm2 >= 10){// || playerManager.checkTestMode () == true){// && playerManager.checkDemo() == false){
			upperArm3.interactable = true;
		}
		if (igUpperArm3 >= 10){// || playerManager.checkTestMode () == true){// && playerManager.checkDemo() == false){
			upperArm4.interactable = true;
		}
		if (igUpperArm4 >= 10){// || playerManager.checkTestMode () == true){// && playerManager.checkDemo() == false){
			upperArm5.interactable = true;
		}
		/*if (igUpperArm5 >= 10){// || playerManager.checkTestMode () == true){// && playerManager.checkDemo() == false){
			upperArm6.interactable = true;
		}*/

		if (igUpperArm5 >= 10) {
			forearm.interactable = true;
			forearm1A.interactable = true;
			forearm1B.interactable = true;
		}
		if (igLowerArm1A >= 10 && igLowerArm1B >=10) {
			forearm2A.interactable = true;
			forearm2B.interactable = true;
		}
		if (igLowerArm2A >= 10 && igLowerArm2B >= 10) {
			forearm3A.interactable = true;
			forearm3B.interactable = true;
		}
		if (igLowerArm3A >= 10 && igLowerArm3B >= 10) {
			forearm4A.interactable = true;
			forearm4B.interactable = true;
		}			
		if (igLowerArm4A >= 10 && igLowerArm4B >= 10) {
			forearm5A.interactable = true;
			forearm5B.interactable = true;
		}
		/*if (igLowerArm5A >= 10 && igLowerArm5B >= 10) {
			forearm6A.interactable = true;
			forearm6B.interactable = true;
		}*/
			
		if (igLowerArm5A >= 10 && igLowerArm5B >= 10) {
			hand.interactable = true;
			hand1A.interactable = true;
		}
		if (igHand1A >= 10) {
			hand2A.interactable = true;
		}

		if (igHand2A >= 10) {
			hand3A.interactable = true;
		}

		if (igHand3A >= 10) {
			hand4A.interactable = true;
		}
		if (igHand4A >= 10) {
			hand5A.interactable = true;
		}
		if (igHand5A >= 10) {
			hand6A.interactable = true;
		}

		/*if (igHand6A >= 10 || igHand3A >= 10) {
			finalLevelArm.gameObject.SetActive (true);
			finalLevelArm.interactable = true;
		}*/



	}

	void retrieval ()
	{
		try {
			StartCoroutine (retrieveGrades ());
		}
		catch (UnityException) {
			StartCoroutine (retrieveGrades ());
		}
		catch (SecurityException) {
			StartCoroutine (retrieveGrades ());
		}
		catch (ArgumentException) {
			StartCoroutine (retrieveGrades ());
		}
	}

	//Retrieve grades for each level.

	//Retrieve grades for Upper Arm.
	IEnumerator retrieveGrades(){
		//UpperArm0
		string urlRetrieve0 = urlRetrieve + "&level=UpperArm0";
		WWW wwwurlRetrieve0 = new WWW (urlRetrieve0);
		yield return wwwurlRetrieve0;
		igUpperArm0 = int.Parse (wwwurlRetrieve0.text);

		//UpperArm1
		string urlRetrieve1 = urlRetrieve + "&level=UpperArm1";
		WWW wwwurlRetrieve1 = new WWW (urlRetrieve1);
		yield return wwwurlRetrieve1;
		igUpperArm1 = int.Parse (wwwurlRetrieve1.text);

		//UpperArm2
		string urlRetrieve2 = urlRetrieve + "&level=UpperArm2";
		WWW wwwurlRetrieve2 = new WWW (urlRetrieve2);
		yield return wwwurlRetrieve2;
		igUpperArm2 = int.Parse (wwwurlRetrieve2.text);

		//UpperArm3
		string urlRetrieve3 = urlRetrieve + "&level=UpperArm3";
		WWW wwwurlRetrieve3 = new WWW (urlRetrieve3);
		yield return wwwurlRetrieve3;
		igUpperArm3 = int.Parse (wwwurlRetrieve3.text);

		//UpperArm4
		string urlRetrieve4 = urlRetrieve + "&level=UpperArm4";
		WWW wwwurlRetrieve4 = new WWW (urlRetrieve4);
		yield return wwwurlRetrieve4;
		igUpperArm4 = int.Parse (wwwurlRetrieve4.text);

		//UpperArm5
		string urlRetrieve5 = urlRetrieve + "&level=UpperArm5";
		WWW wwwurlRetrieve5 = new WWW (urlRetrieve5);
		yield return wwwurlRetrieve5;
		igUpperArm5 = int.Parse (wwwurlRetrieve5.text);


		try{
			StartCoroutine (retrieveGrades2 ());
		}
		catch (UnityException){
			StartCoroutine (retrieveGrades2 ());
		}
		catch (SecurityException){
			StartCoroutine (retrieveGrades2 ());
		}
		catch (ArgumentException){
			StartCoroutine (retrieveGrades2 ());
		}



	}

	//Retrieve grades for Forearm
	IEnumerator retrieveGrades2(){
		//Forearm1A
		string urlRetrieve7A = urlRetrieve + "&level=Forearm1A";
		WWW wwwurlRetrieve7A = new WWW (urlRetrieve7A);
		yield return wwwurlRetrieve7A;
		igLowerArm1A = int.Parse (wwwurlRetrieve7A.text);

		//Forearm1B
		string urlRetrieve7B = urlRetrieve + "&level=Forearm1B";
		WWW wwwurlRetrieve7B = new WWW (urlRetrieve7B);
		yield return wwwurlRetrieve7B;
		igLowerArm1B = int.Parse (wwwurlRetrieve7B.text);

		//Forearm2A
		string urlRetrieve8A = urlRetrieve + "&level=Forearm2A";
		WWW wwwurlRetrieve8A = new WWW (urlRetrieve8A);
		yield return wwwurlRetrieve8A;
		igLowerArm2A = int.Parse (wwwurlRetrieve8A.text);

		//Forearm2B
		string urlRetrieve8B = urlRetrieve + "&level=Forearm2B";
		WWW wwwurlRetrieve8B = new WWW (urlRetrieve8B);
		yield return wwwurlRetrieve8B;
		igLowerArm2B = int.Parse (wwwurlRetrieve8B.text);

		//Forearm3A
		string urlRetrieve9A = urlRetrieve + "&level=Forearm3A";
		WWW wwwurlRetrieve9A = new WWW (urlRetrieve9A);
		yield return wwwurlRetrieve9A;
		igLowerArm3A = int.Parse (wwwurlRetrieve9A.text);

		//Forearm3B
		string urlRetrieve9B = urlRetrieve + "&level=Forearm3B";
		WWW wwwurlRetrieve9B = new WWW (urlRetrieve9B);
		yield return wwwurlRetrieve9B;
		igLowerArm3B = int.Parse (wwwurlRetrieve9B.text);

		//Forearm4A
		string urlRetrieve10A = urlRetrieve + "&level=Forearm4A";
		WWW wwwurlRetrieve10A = new WWW (urlRetrieve10A);
		yield return wwwurlRetrieve10A;
		igLowerArm4A = int.Parse (wwwurlRetrieve10A.text);

		//Forearm4B
		string urlRetrieve10B = urlRetrieve + "&level=Forearm4B";
		WWW wwwurlRetrieve10B = new WWW (urlRetrieve10B);
		yield return wwwurlRetrieve10B;
		igLowerArm4B = int.Parse (wwwurlRetrieve10B.text);

		//Forearm5A
		string urlRetrieve11A = urlRetrieve + "&level=Forearm5A";
		WWW wwwurlRetrieve11A = new WWW (urlRetrieve11A);
		yield return wwwurlRetrieve11A;
		igLowerArm5A = int.Parse (wwwurlRetrieve11A.text);

		//Forearm5B
		string urlRetrieve11B = urlRetrieve + "&level=Forearm5B";
		WWW wwwurlRetrieve11B = new WWW (urlRetrieve11B);
		yield return wwwurlRetrieve11B;
		igLowerArm5B = int.Parse (wwwurlRetrieve11B.text);

		/*Forearm6A
		string urlRetrieve12A = urlRetrieve + "&level=Forearm6A";
		WWW wwwurlRetrieve12A = new WWW (urlRetrieve12A);
		yield return wwwurlRetrieve12A;
		igLowerArm6A = int.Parse (wwwurlRetrieve12A.text);*/

		/*Forearm6B
		string urlRetrieve12B = urlRetrieve + "&level=Forearm6B";
		WWW wwwurlRetrieve12B = new WWW (urlRetrieve12B);
		yield return wwwurlRetrieve12B;
		igLowerArm6B = int.Parse (wwwurlRetrieve12B.text);*/

		try{
			StartCoroutine (retrieveGrades3 ());
		}
		catch (UnityException){
			StartCoroutine (retrieveGrades3 ());
		}
		catch (SecurityException){
			StartCoroutine (retrieveGrades3 ());
		}
		catch (ArgumentException){
			StartCoroutine (retrieveGrades3 ());
		}

	}

	//Retrieve grades for Hand.
	IEnumerator retrieveGrades3(){
		
		//Hand1A
		string urlRetrieve13A = urlRetrieve + "&level=Hand1A";
		WWW wwwurlRetrieve13A = new WWW (urlRetrieve13A);
		yield return wwwurlRetrieve13A;
		igHand1A = int.Parse (wwwurlRetrieve13A.text);

		//Hand2A
		string urlRetrieve14A = urlRetrieve + "&level=Hand2A";
		WWW wwwurlRetrieve14A = new WWW (urlRetrieve14A);
		yield return wwwurlRetrieve14A;
		igHand2A = int.Parse (wwwurlRetrieve14A.text);

		//Hand3
		string urlRetrieve15A = urlRetrieve + "&level=Hand3A";
		WWW wwwurlRetrieve15A = new WWW (urlRetrieve15A);
		yield return wwwurlRetrieve15A;
		igHand3A = int.Parse (wwwurlRetrieve15A.text);

		//Hand4
		string urlRetrieve16A = urlRetrieve + "&level=Hand4A";
		WWW wwwurlRetrieve16A = new WWW (urlRetrieve16A);
		yield return wwwurlRetrieve16A;
		igHand4A = int.Parse (wwwurlRetrieve16A.text);

		//Hand5
		string urlRetrieve17A = urlRetrieve + "&level=Hand5A";
		WWW wwwurlRetrieve17A = new WWW (urlRetrieve17A);
		yield return wwwurlRetrieve17A;
		igHand5A = int.Parse (wwwurlRetrieve17A.text);

		//Hand6
		/*string urlRetrieve18A = urlRetrieve + "&level=Hand6A";
		WWW wwwurlRetrieve18A = new WWW (urlRetrieve18A);
		yield return wwwurlRetrieve18A;
		igHand6A = int.Parse (wwwurlRetrieve18A.text);*/

		try{
			StartCoroutine (retrieveGrades4 ());
		}
		catch (UnityException){
			StartCoroutine (retrieveGrades4 ());
		}
		catch (SecurityException){
			StartCoroutine (retrieveGrades4 ());
		}
		catch (ArgumentException){
			StartCoroutine (retrieveGrades4 ());
		}
	}

	IEnumerator retrieveGrades4(){
		//Armbone0
		string urlRetrieve0 = urlRetrieve + "&level=Armbone0";
		WWW wwwurlRetrieve0 = new WWW (urlRetrieve0);
		yield return wwwurlRetrieve0;
		igArmbone0 = int.Parse (wwwurlRetrieve0.text);

		//Armbone1
		string urlRetrieve16 = urlRetrieve + "&level=Armbone1";
		WWW wwwurlRetrieve16 = new WWW (urlRetrieve16);
		yield return wwwurlRetrieve16;
		igArmbone1 = int.Parse (wwwurlRetrieve16.text);

		//Armbone2
		string urlRetrieve17 = urlRetrieve + "&level=Armbone2";
		WWW wwwurlRetrieve17 = new WWW (urlRetrieve17);
		yield return wwwurlRetrieve17;
		igArmbone2 = int.Parse (wwwurlRetrieve17.text);

		//Armbone3
		string urlRetrieve18 = urlRetrieve + "&level=Armbone3";
		WWW wwwurlRetrieve18 = new WWW (urlRetrieve18);
		yield return wwwurlRetrieve18;
		igArmbone3 = int.Parse (wwwurlRetrieve18.text);

		//Armbone4
		/*string urlRetrieve19 = urlRetrieve + "&level=Armbone4";
		WWW wwwurlRetrieve19 = new WWW (urlRetrieve19);
		yield return wwwurlRetrieve19;
		igArmbone4 = int.Parse (wwwurlRetrieve19.text);*/

		try{
			StartCoroutine (retrieveGrades5 ());
		}
		catch (UnityException){
			StartCoroutine (retrieveGrades5 ());
		}
		catch (SecurityException){
			StartCoroutine (retrieveGrades5 ());
		}
		catch (ArgumentException){
			StartCoroutine (retrieveGrades5 ());
		}
	}

	IEnumerator retrieveGrades5(){
		//Nerves0
		/*string urlRetrieve0A = urlRetrieve + "&level=Nerves0";
		WWW wwwurlRetrieve0A = new WWW (urlRetrieve0A);
		yield return wwwurlRetrieve0A;
		igNerves0 = int.Parse (wwwurlRetrieve0A.text);*/

		//Nerves1
		string urlRetrieve19A = urlRetrieve + "&level=Nerves1";
		WWW wwwurlRetrieve19A = new WWW (urlRetrieve19A);
		yield return wwwurlRetrieve19A;
		igNerves1 = int.Parse (wwwurlRetrieve19A.text);

		//Nerves2
		string urlRetrieve20A = urlRetrieve + "&level=Nerves2";
		WWW wwwurlRetrieve20A = new WWW (urlRetrieve20A);
		yield return wwwurlRetrieve20A;
		igNerves2 = int.Parse (wwwurlRetrieve20A.text);

		//Nerves3
		string urlRetrieve21 = urlRetrieve + "&level=Nerves3";
		WWW wwwurlRetrieve21 = new WWW (urlRetrieve21);
		yield return wwwurlRetrieve21;
		igNerves3 = int.Parse (wwwurlRetrieve21.text);
	}


}
