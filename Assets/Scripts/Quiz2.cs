using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Quiz2 : MonoBehaviour {

	//Setting up Score for Correct answer
	private LevelManager levelManager;
	private QuizPopup quizPopup;
	private QuizRandomQ2 qr2;
	//private int guesses = 0;

	//Setting up Correct or Incorrect Feedback
	private Text question;
	private Text corInc;
	private List<String> answerSet = new List<String> ();
	private List<int> correctValue = new List<int> ();
	private Button sAnswer;
	private Button nQuestion;
	private Button hint;
	private int myGuess = 0;
	private bool myHint = false;

	//Setting up point value constants.
	//This is the value for the threshold of each scoring tier.
	private const int THRESH0 = 1;
	private const int THRESH1 = 2;
	private const int THRESH2 = 3;
	private const int THRESH3 = 5;

	//This is the scoring tier for each threshold of attempts.
	private const int SCORE1 = 5;
	private const int SCORE2 = 3;
	private const int SCORE3 = 1;
	private const int SCORE0 = 0;

	//Setting up questions for quiz
	private int cAns;
	private bool cSel1 = true, cSel2 = true, cSel3 = true, cSel4 = true, cSel5 = true;


	//Setting up Stat Tracking
	private StatTracking statTracking;
	private ReportSelf rs;

	//Variable for Rect Transform
	private RectTransform rt;
	private const int WIDTH = 412;
	private const int HEIGHT = 518;

	// Use this for initialization
	void Start () {
		
		question = transform.FindChild ("Question").GetComponent <Text>();
		corInc = transform.FindChild ("CorInc").GetComponent <Text> (); 
		sAnswer = transform.FindChild ("Answer Button").GetComponent <Button> ();
		nQuestion = transform.FindChild ("Next Button").GetComponent <Button>(); 
		hint = transform.FindChild ("Hint Button").GetComponent <Button>();

		//Set the size of the canvas
		rt = GetComponent <RectTransform> ();
		rt.sizeDelta = new Vector2 (WIDTH, HEIGHT);
		
		nQuestion.gameObject.SetActive (false);

		//Find the Level Manager for scorekeeping
		levelManager = FindObjectOfType<LevelManager> ();

		//Find other components of this game object
		qr2 = GetComponent <QuizRandomQ2> ();
		quizPopup = GetComponentInParent <QuizPopup> ();

		//Find the Stat Tracker for stat tracking
		statTracking = FindObjectOfType<StatTracking>();
		rs = GetComponentInParent<ReportSelf> ();

		//When panel opens, make sure no other panel can open.
		levelManager.noObjections = false;

		//dragAndSnap = GetComponent<DragAndSnap> ();
		setQuestion ();
		//Set the feedback as empty.
		corInc.text = "";


		//Gather the set of correct answers as well as their values.
		answerSet = qr2.getAnswer ();
		StartCoroutine (setAnswers ());






	}

	void setQuestion(){
		if (statTracking.identifyLevel () == "Q1")
			question.text = "What is the name of this muscle?\nMore than one answer may apply.";
		if (statTracking.identifyLevel () == "Q3")
			question.text = "What is the innervation of this muscle?\nMore than one answer may apply.";
		if (statTracking.identifyLevel () == "Q5")
			question.text = "What is the name of this bone?\nMore than one answer may apply.";
		if (statTracking.identifyLevel () == "Q6")
			question.text = "What is the name of this nerve?\nMore than one answer may apply.";
		if (statTracking.identifyLevel () == "Q7")
			question.text = "What nerve controls this region?\nMore than one answer may apply.";

		if (statTracking.identifyLevel () == "Q2") {
			switch (this.name) {
			case "Proximal":
				question.text = "What is the PROXIMAL attachment of this muscle?\nMore than one answer may apply.";
				break;
			case "Short Head":
				question.text = "What is the PROXIMAL attachment of the SHORT HEAD of this muscle?\nMore than one answer may apply.";
				break;
			case "Long Head":
				question.text = "What is the PROXIMAL attachment of the LONG HEAD of this muscle?\nMore than one answer may apply.";
				break;
			case "Lateral":
				question.text = "What is the PROXIMAL attachment of the LATERAL HEAD of this muscle?\nMore than one answer may apply.";
				break;
			case "Medial":
				question.text = "What is the PROXIMAL attachment of the MEDIAL HEAD of this muscle?\nMore than one answer may apply.";
				break;
			case "Distal":
				question.text = "What is the DISTAL attachment of this muscle?\nMore than one answer may apply.";
				break;
			default:
				question.text = "I do not have a question set up.  Please report to your teacher.";
				break;
			}
		}

		if (statTracking.identifyLevel () == "Q4") {
			switch (this.name) {
			case "Q0":
				question.text = "What is the action of this muscle?\nMore than one answer may apply.";
				break;
			case "Short Head":
				question.text = "What is the action of the SHORT HEAD of this muscle?\nMore than one answer may apply.";
				break;
			case "Long Head":
				question.text = "What is the action of the LONG HEAD of this muscle?\nMore than one answer may apply.";
				break;
			default:
				question.text = "I do not have a question set up.  Please report to your teacher.";
				break;
			}
		}

	}

	IEnumerator setAnswers (){
		yield return qr2.GetType ();
		correctValue = qr2.getValue ();
		//Find each correct answer and set the appropriate bool
		displayQuestion ();
	}


	//Find each correct answer in the list and sets the appropriate bool(s).
	void displayQuestion ()
	{
		//for (int n=0; n<3; n++){
			foreach (int item in correctValue) {
				if (item == 0)
					cSel1 = false;
				if (item == 1)
					cSel2 = false;
				if (item == 2)
					cSel3 = false;
				if (item == 3)
					cSel4 = false;
				if (item == 4)
					cSel5 = false;
			}					
		//}
	}






	//Submits answer when button is pressed.
	public void submitAnswer (){
		//If the answer is correct...
		if (cSel1 && cSel2 && cSel3 && cSel4 && cSel5) {
			sAnswer.gameObject.SetActive (false);  //Submit Button is replaced...
			nQuestion.gameObject.SetActive (true); //...By Next Question Button.
			hint.gameObject.SetActive (false);  //hint button disables if question is answered correctly.
			corInc.text = "That's The Way!";
			levelManager.addobjPlaced ();
			rs.reportSelf ();



			//Evaluates the number of guesses and gives points accordingly.
			if (myGuess <= THRESH0) {
				levelManager.score += SCORE1;
			} else if (myGuess == THRESH1) {
				levelManager.score += SCORE2;
			} else if (myGuess >= THRESH2 && myGuess < THRESH3 && myHint == false) {
				levelManager.score += SCORE3;
			} else if (myGuess > THRESH3 && myHint == false) {
				levelManager.score += SCORE0;
			} else if (myHint == true){
				levelManager.score += SCORE0;
			}
		} else {
			//Increases the guesses by 1 for each incorrect guess.
			myGuess++;
			rs.addGuess();
			levelManager.attempts++;
			rs.reportSelf ();

			if (myHint == false){
				corInc.text = "That's not what this muscle does!";
			}
		}

		if (myGuess >= THRESH2) {
			hint.gameObject.SetActive (true);  //Reveal a button to give the answer...
		}
	}

	//Displays the correct answer(s) in the correct/incorrect text box.
	public void giveHint ()
	{
		corInc.text = "The " + transform.parent.name + " does this:";
		for (int n = 0; n < answerSet.Count; n++)
			corInc.text += "\n" + answerSet [n];
		myHint = true;
		rs.toggleHintTrue();  //Sets this boolean to true for feedback purposes.
		rs.reportSelf ();
	}

	//Reverses the boolean when each checkbox is set.
	//Set up this way so that the answer is only correct when ONLY the correct answers are checked.
	public void toggleCorrect1(){
		cSel1 = !cSel1;
	}

	public void toggleCorrect2(){
		cSel2 = !cSel2;
	}

	public void toggleCorrect3(){
		cSel3 = !cSel3;
	}

	public void toggleCorrect4(){
		cSel4 = !cSel4;
	}

	public void toggleCorrect5(){
		cSel5 = !cSel5;
	}

	//Closes question panel and lowers the number of startObjects.
	//Will return to game unless startObjects reaches 0, in which case the results display.
	public void nextQuestion(){
		if (quizPopup.getCanvasLength () >= 2) {
			int q = quizPopup.getQuestionN ();
			quizPopup.setCanvas (q, false);
			quizPopup.setCanvas (++q, true);
			quizPopup.reduceCanvasLength ();
			quizPopup.nextQuestionN ();

		} else {
			transform.parent.tag = "Placed";
			rs.reportSelf ();
			quizPopup.isClickable = false;
			quizPopup.answered = true;
			quizPopup.setAlpha ();
			if (statTracking.identifyLevel () != "Q5")
				levelManager.noObjections = true; //Make other panels openable again.

			this.gameObject.SetActive (false);
			//Reduce the number of objects remaining by one
			levelManager.startObjects--;
			levelManager.addobjPlaced ();
		}
	}
}
