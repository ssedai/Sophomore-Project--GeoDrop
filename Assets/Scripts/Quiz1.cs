using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Quiz1 : MonoBehaviour {

	//Setting up Score for Correct answer
	private LevelManager levelManager;
	private QuizPopup quizPopup;
	private QuizRandomQ1 qr1;


	//Setting up Correct or Incorrect Feedback
	private Text question;
	private string correctAnswer;
	private Text corInc;
	private Button sAnswer;
	private Button nQuestion;
	private Button hint;
	private int myGuess = 0;
	private bool myHint = false;
	private bool correctFirstTry = true;



	//Setting up point value constants
	//This is the value for the threshold of each scoring tier.
	private const int THRESH0 = 0;
	private const int THRESH1 = 1;
	private const int THRESH2 = 2;
	private const int THRESH3 = 3;

	//This is the scoring tier for each threshold of attempts.
	private const int SCORE1 = 5;
	private const int SCORE2 = 3;
	private const int SCORE3 = 1;
	private const int SCORE0 = 0;

	//Setting up questions for quiz
	private float rSeed;
	private Dropdown answers1;
	private Dropdown cDD;
	private int cAns;

	//Setting up Stat Tracking
	private StatTracking statTracking;
	private ReportSelf rs;

	//Variable for Rect Transform
	private RectTransform rt;
	private const int WIDTH = 412;
	private const int HEIGHT = 518;

	// Use this for initialization
	void Start () {

		//Find needed components in children.
		question = transform.FindChild ("Question").GetComponent <Text>();
		corInc = transform.FindChild ("CorInc").GetComponent <Text> (); 
		sAnswer = transform.FindChild ("Answer Button").GetComponent <Button> ();
		nQuestion = transform.FindChild ("Next Button").GetComponent <Button>(); 
		hint = transform.FindChild ("Hint Button").GetComponent <Button>();
		answers1 = GetComponentInChildren <Dropdown> ();

		//Set the size of the canvas
		rt = GetComponent <RectTransform> ();
		rt.sizeDelta = new Vector2 (WIDTH, HEIGHT);


		/*Set the functions of the buttons.  Because Unity has a terrible prefab system.
		// ***TO BE USED ONLY IF BUTTONS ARE NOT ALREADY ASSIGNED IN INSPECTOR***
		sAnswer.onClick.RemoveAllListeners ();
		nQuestion.onClick.RemoveAllListeners ();
		hint.onClick.RemoveAllListeners ();
		if (statTracking.levelIdentity == "Q0")
			sAnswer.onClick.AddListener (() => submitTestAnswer ());
		else
			sAnswer.onClick.AddListener (() => submitAnswer());
		nQuestion.onClick.AddListener (() => nextQuestion());
		hint.onClick.AddListener (() => giveHint()); */

		hint.gameObject.SetActive (false);
		//Make sure only one canvas is enabled
		nQuestion.gameObject.SetActive (false);
		//question2.gameObject.SetActive (false);

		//Find the Level Manager
		levelManager = FindObjectOfType<LevelManager> ();

		//Find the Quiz Popup Script.
		quizPopup = GetComponentInParent<QuizPopup> ();

		//Find the QuizRandomQ1
		qr1 = GetComponent <QuizRandomQ1> ();

		//Set the Correct Answer to this string.
		correctAnswer = qr1.getAnswer ();



		//Find the Stat Tracker and ReportSelf scripts.
		statTracking = FindObjectOfType<StatTracking>();
		if (statTracking.identifyLevel () == "Q0" || statTracking.identifyLevel () == "QC")
			rs = transform.GetComponentInChildren <ReportSelf> ();
		else
			rs = GetComponentInParent<ReportSelf> ();

		setQuestion ();

		//When panel opens, make sure no other panel can open.
		levelManager.noObjections = false;
		levelManager.canvasOpen = true;

		//Set the feedback as empty.
		corInc.text = "";




	}

	//Sets the question for the level.
	void setQuestion(){
		if (statTracking.identifyLevel () == "Q1")
			question.text = "What is the name of this muscle?";
		if (statTracking.identifyLevel () == "Q3"){
			switch(this.name){
			case "Q0": 
				question.text = "What is the innervation of this muscle?";
				break;
			case "1-2":
				question.text = "What is the innnervation of this muscle (1-2)?";
				break;
			case "3-4":
				question.text = "What is the innervation of this muscle (3-4)?";
				break;
			default:
				break;
			}
		}
		if (statTracking.identifyLevel () == "Q5")
			question.text = "What is the name of this bone?";
		if (statTracking.identifyLevel () == "Q6")
			question.text = "What is the name of this nerve?";
		if (statTracking.identifyLevel () == "Q7")
			question.text = "What nerve controls this region?";

		if (statTracking.identifyLevel () == "Q2") {
			switch (this.name) {
			case "Proximal":
				question.text = "What is the PROXIMAL attachment of this muscle?";
				break;
			case "Short Head":
				question.text = "What is the PROXIMAL attachment of the SHORT HEAD of this muscle?";
				break;
			case "Long Head":
				question.text = "What is the PROXIMAL attachment of the LONG HEAD of this muscle?";
				break;
			case "Lateral":
				question.text = "What is the PROXIMAL attachment of the LATERAL HEAD of this muscle?";
				break;
			case "Medial":
				question.text = "What is the PROXIMAL attachment of the MEDIAL HEAD of this muscle?";
				break;
			case "Oblique":
				question.text = "What is the PROXIMAL attachment of the OBLIQUE HEAD of this muscle?";
				break;
			case "Transverse":
				question.text = "What is the PROXIMAL attachment of the TRANSVERSE HEAD of this muscle?";
				break;
			case "P1-2":
				question.text = "What is the PROXIMAL attachment of this muscle (1-2)?";
				break;
			case "P3-4":
				question.text = "What is the PROXIMAL attachment of this muscle (3-4)?";
				break;
			case "Distal":
				question.text = "What is the DISTAL attachment of this muscle?";
				break;			
			default:
				question.text = "I do not have a question set up.  Please report to your teacher.";
				break;
			}
		}

		if (statTracking.levelIdentity == "Q4") {
			switch (this.name){
			case "Q0":
				question.text = "What is the action of this muscle?";
				break;
			default:
				break;
			}
		}
	}







	//Submits answer when button is pressed.
	public void submitAnswer (){
		//If the answer is correct...

		if (answers1.value == qr1.getValue ()){ 
			sAnswer.gameObject.SetActive (false);
			nQuestion.gameObject.SetActive (true);
			hint.gameObject.SetActive (false); //hint button disables if question is answered correctly.
			corInc.text = "You Got It!";
			transform.gameObject.tag = "Placed";
			levelManager.addMuscPlaced ();
			rs.reportSelf ();
			if (correctFirstTry)
				levelManager.testScoreAddition ();

			//Evaluates the number of guesses and gives points accordingly.
			if (myGuess == THRESH0) {
				levelManager.score += SCORE1;
			} else if (myGuess == THRESH1) {
				levelManager.score += SCORE2;
			} else if (myGuess == THRESH2 && myHint == false) {
				levelManager.score += SCORE3;
			} else if (myGuess > THRESH3 && myHint == false) {
				levelManager.score += SCORE0;
			} else if (myHint == true) {
				levelManager.score += SCORE0;
			}

		} else if (answers1.value != qr1.getValue () && answers1.value != 0) {
			//Increases the guesses by 1 for each incorrect guess.
			myGuess++;
			if (SceneManager.GetActiveScene ().name != "UpperArm6B") {
				rs.addGuess ();
				correctFirstTry = false;
			} else if (SceneManager.GetActiveScene ().name == "UpperArm6B") {
				rs.addQguess ();
				correctFirstTry = false;
			}
			levelManager.attempts++;
			rs.reportSelf ();

			if (myHint == false) {
				corInc.text = "Please try again.";
			}

		} else if (answers1.value == 0) {
			//If player selects the null answer, change the text.
			if (myHint==false){
				corInc.text = "Please select an answer before submitting.";
			}
		}

		if (myGuess >= THRESH2) {
			hint.gameObject.SetActive (true);  //Reveal a button to give the answer...
		}


	}

	//Submits answer when button is pressed.
	public void submitTestAnswer (){
		//If the answer is correct...
		if (answers1.value == qr1.getValue ()){ 
			//Regardless of a correct or incorrect answer, the next question will come up.
			//No feedback or hints will be given.  Player gets one attempt to answer the question.
			sAnswer.gameObject.SetActive (false);
			nQuestion.gameObject.SetActive (true);
			hint.gameObject.SetActive (false);
			levelManager.addMuscPlaced ();
			rs.reportSelf ();
			if (correctFirstTry)
				levelManager.testScoreAddition ();

			//Give points for getting the correct answer.
			levelManager.score += SCORE1;


		} else if (answers1.value != qr1.getValue () && answers1.value != 0) {
			//Regardless of a correct or incorrect answer, the next question will come up.
			//No feedback or hints will be given.  Player gets one attempt to answer the question.
			sAnswer.gameObject.SetActive (false);
			nQuestion.gameObject.SetActive (true);
			hint.gameObject.SetActive (false);
			//Increases the guesses by 1 for each incorrect guess.
			myGuess++;
			if (SceneManager.GetActiveScene ().name != "UpperArm6B") {
				rs.addGuess ();
				correctFirstTry = false;
			} else if (SceneManager.GetActiveScene ().name == "UpperArm6B") {
				rs.addQguess ();
				correctFirstTry = false;
			}
			levelManager.attempts++;
			rs.reportSelf ();


		} else if (answers1.value == 0) {
			//If player selects the null answer, change the text.
			if (myHint==false){
				corInc.text = "Please select an answer before submitting.";
			}
		}

	}

	//Changes the dropdown's value to the correct answer.
	public void giveHint(){
		if (statTracking.identifyLevel () == "Q1" || statTracking.identifyLevel () == "Q5" || statTracking.identifyLevel () == "Q6") {			
			corInc.text = "This part is known as:\n" + correctAnswer + ".";
		}
		if (statTracking.identifyLevel () == "Q2") {
			corInc.text = "The " + transform.parent.name + " inserts into this bone:\n" + correctAnswer + ".";
		}
		if (statTracking.identifyLevel () == "Q3") {
			corInc.text = "The " + transform.parent.name + " innervates at:\n" + correctAnswer;
		}
		if (statTracking.identifyLevel () == "Q4") {
			corInc.text = "The " + transform.parent.name + " does this:\n" + correctAnswer;
		}
		if (statTracking.identifyLevel () == "Q7") {
			corInc.text = "The region is affected by this nerve:\n" + correctAnswer;
		}
		if (statTracking.identifyLevel () == "QC") {
			corInc.text = "No hints given for comprehension.";
			return;
		}
		myHint = true;
		rs.toggleHintTrue();  //Sets this boolean to true for our feedback purposes.
		rs.reportSelf();
	}

	//Closes question Panel and lowers the number of startObjects.
	//Will return to game unless startObjects reaches 0, in which case the results display.
	public void nextQuestion(){

		if (quizPopup.getCanvasLength () >= 2) {
			int q = quizPopup.getQuestionN ();
			quizPopup.setCanvas (q, false);
			quizPopup.setCanvas (++q, true);
			quizPopup.reduceCanvasLength ();
			quizPopup.nextQuestionN ();
			correctFirstTry = true;
		} else {
			transform.parent.tag = "Placed";
			rs.reportSelf ();
			quizPopup.isClickable = false;
			quizPopup.answered = true;
			quizPopup.setAlpha ();
			if (SceneManager.GetActiveScene ().name != "UpperArm6B")
				levelManager.noObjections = true; //Make other panels openable again.

			levelManager.canvasOpen = false;
			this.gameObject.SetActive (false);

			//Reduce the number of objects remaining by one.
			levelManager.startObjects--;
			//levelManager.addMuscPlaced ();

		}
	}
}
