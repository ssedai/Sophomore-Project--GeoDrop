using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Quiz2A : MonoBehaviour {

	//Setting up Score for Correct answer
	private LevelManager levelManager;
	public QuizPopup quizPopup;
	//private int guesses = 0;

	//Setting up Correct or Incorrect Feedback
	public Text corInc;
	private string correction1;
	private string correction2;
	private string correction3;
	public Button sAnswer;
	public Button nQuestion;
	public Button hint;
	private int myGuess = 0;
	private bool myHint = false;
	private bool correctFirstTry = true;

	//Setting up point value constants.
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
	public Text answers4;
	public Text answers5;
	public Text answers6;
	private enum State {Biceps, Anconeou, Coracobrachialis, Triceps, Brachialis};  //States to switch the correct answer
	private State fPart, fPart2;  //Variable for the correct Answer.
	private int cAns;
	private bool cSel1 = true, cSel2 = true, cSel3 = true, cSel4 = true, cSel5 = true;


	//Setting up Stat Tracking
	private StatTracking statTracking;
	private ReportSelf rs;

	// Use this for initialization
	void Start () {
		
		nQuestion.gameObject.SetActive (false);

		//Find the Level Manager for scorekeeping
		levelManager = FindObjectOfType<LevelManager> ();

		//Find the Stat Tracker for stat tracking
		statTracking = FindObjectOfType<StatTracking>();
		rs = GetComponentInParent<ReportSelf> ();

		//When panel opens, make sure no other panel can open.
		levelManager.noObjections = false;

		//dragAndSnap = GetComponent<DragAndSnap> ();

		//Set the feedback as empty.
		corInc.text = "";

		//Find the name of the parent gameObject to figure out the state.
		//bPart changes the State which changes the correct answer.
		//correction texts are for displaying the correct answer if hint is used.
		readyHint ();


		//Setting up randomization of dropdown answers.
		answers4.gameObject.SetActive (false);
		answers5.gameObject.SetActive (false);
		answers6.gameObject.SetActive (false);
		rSeed = Random.Range (300f, 600f);

		if (this.name == "Q0") {
			displayQuestion0 ();
		} else if (this.name == "Q1") {
			displayQuestion1 ();
		}


	}

	void readyHint ()
	{
		switch (transform.parent.name) {
		case "Anterior Biceps Brachii":
			fPart = State.Biceps;
			fPart2 = State.Biceps;
			if (this.name == "Q0") {				
				correction1 = "Supinates forearm";
				correction2 = "Flexes elbow joint when forearm supinated";
				correction3 = "Flexes shoulder joint";
			} else if (this.name == "Q1") {
				correction1 = "Resists dislocation of shoulder";
				correction2 = "";
				correction3 = "";
			}
			break;
		case "Posterior Anconeus":
			fPart = State.Anconeou;
			correction1 = "Stabilizes elbow joint";
			correction2 = "Assists triceps in extending elbow joint";
			correction3 = "Abducts ulna during pronation";
			break;
		case "Anterior Coracobrachialis":
			fPart = State.Coracobrachialis;
			correction1 = "Flex and adduct shoulder joint";
			correction2 = "Resists dislocation of shoulder";
			correction3 = "";
			break;
		case "Posterior Triceps Brachii":
			fPart = State.Triceps;
			fPart2 = State.Triceps;
			if (this.name == "Q0") {				
				correction1 = "Extends elbow joint";
				correction2 = "";
				correction3 = "";
			} else if (this.name == "Q1") {				
				correction1 = "Extends shoulder joint";
				correction2 = "Resists shoulder dislocation during abduction";
				correction3 = "";
			}
			break;
		case "Anterior Brachialis":
			fPart = State.Brachialis;
			correction1 = "Flexes elbow joint in all positions";
			correction2 = "";
			correction3 = "";
			break;
		}


	}

	void displayQuestion0 ()
	{
		
		print (rSeed);
		if (rSeed >= 301 && rSeed < 400) {
			//Selects the first set of answers for randomization.
			answers5.gameObject.SetActive (false);
			answers6.gameObject.SetActive (false);
			answers4.gameObject.SetActive (true);
			//Selects the correct answer by making it so the bool is true WHEN the player checks the box.
			//Answers for Function
			//UpperArm
			if (statTracking.identifyLevel () == "Q4") {
				switch (fPart) {
				case State.Biceps:
					cSel1 = false;
					cSel4 = false;
					cSel5 = false;
					break;
				case State.Anconeou:
					cSel2 = false;
					cSel4 = false;
					cSel5 = false;
					break;
				case State.Coracobrachialis:
					cSel2 = false;
					cSel3 = false;
					break;
				case State.Triceps:
					cSel2 = false;
					break;
				case State.Brachialis:
					cSel1 = false;
					break;
				}
			}
		}
		//Selects the second set of answers for randomization.
		else
			if (rSeed >= 400 && rSeed < 500) {
				answers4.gameObject.SetActive (false);
				answers6.gameObject.SetActive (false);
				answers5.gameObject.SetActive (true);
				//Selects the correct answer by making it so the bool is true WHEN the player checks the box.
				//Answers for Function
				//UpperArm
			if (statTracking.identifyLevel () == "Q4") {
					switch (fPart) {
					case State.Biceps:
						cSel2 = false;
						cSel3 = false;
						cSel5 = false;
						break;
					case State.Anconeou:
						cSel1 = false;
						cSel2 = false;
						cSel3 = false;
						break;
					case State.Coracobrachialis:
						cSel2 = false;
						cSel4 = false;
						break;
					case State.Triceps:
						cSel4 = false;
						break;
					case State.Brachialis:
						cSel5 = false;
						break;
					}
				}
			}
			//Selects the third set of answers for randomization.
			else
				if (rSeed >= 500) {
					//Answers for Function
					//UpperArm
			if (statTracking.identifyLevel () == "Q4") {
				answers4.gameObject.SetActive (false);
				answers5.gameObject.SetActive (false);
				answers6.gameObject.SetActive (true);
				//Selects the correct answer by making it so the bool is true WHEN the player checks the box.
				switch (fPart) {
				case State.Biceps:
					cSel1 = false;
					cSel3 = false;
					cSel4 = false;
					break;
				case State.Anconeou:
					cSel1 = false;
					cSel3 = false;
					cSel5 = false;
					break;
				case State.Coracobrachialis:
					cSel1 = false;
					cSel5 = false;
					break;
				case State.Triceps:
					cSel1 = false;
					break;
				case State.Brachialis:
					cSel4 = false;
					break;
				}
			}
		}
	}

	void displayQuestion1(){
		//Selects the first set of answers for randomization.
		if (rSeed >= 301 && rSeed < 400) {
			//Answers for Function
			//UpperArm
			if (statTracking.identifyLevel () == "Q4") {
			answers5.gameObject.SetActive (false);
			answers6.gameObject.SetActive (false);
			answers4.gameObject.SetActive (true);
				switch (fPart2) {
				case State.Biceps:
					cSel4 = false;
					break;
				case State.Triceps:
					cSel1 = false;
					cSel2 = false;
					break;
				}
			}
		}
		//Selects the second set of answers for randomization.
		else if (rSeed >= 400 && rSeed < 500) {

			//Answers for Function
			//UpperArm
			if (statTracking.identifyLevel () == "Q4") {
			answers4.gameObject.SetActive (false);
			answers6.gameObject.SetActive (false);
			answers5.gameObject.SetActive (true);
				switch (fPart2) {
				case State.Biceps:
					cSel3 = false;
					break;
				case State.Triceps:
					cSel3 = false;
					cSel5 = false;
					break;
				}
			}
		}
		//Selects the third set of answers for randomization.
			else if (rSeed >= 500) {

			//Answers for Function
			//UpperArm
			if (statTracking.identifyLevel () == "Q4") {
			answers4.gameObject.SetActive (false);
			answers5.gameObject.SetActive (false);
			answers6.gameObject.SetActive (true);
				switch (fPart2) {
				case State.Biceps:
					cSel1 = false;
					break;
				case State.Triceps:
					cSel1 = false;
					cSel4 = false;
					break;
				}
			}
		}
	}



	//Submits answer when button is pressed.
	public void submitAnswer (){
		//If the answer is correct...
		if (cSel1 && cSel2 && cSel3 && cSel4 && cSel5) {
			sAnswer.gameObject.SetActive (false);  //Submit Button is replaced...
			nQuestion.gameObject.SetActive (true); //...By Next Question Button.
			hint.gameObject.SetActive (false);  //hint button disables if question is answered correctly.
			corInc.text = "That's The Way!";
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
			} else if (myHint == true){
				levelManager.score += SCORE0;
			}
		} else {
			//Increases the guesses by 1 for each incorrect guess.
			myGuess++;
			rs.addGuess();
			levelManager.attempts++;
			rs.reportSelf ();
			correctFirstTry = false;
			if (myHint == false){
				corInc.text = "That's not what this muscle does!";
			}
		}

		if (myGuess >= 2) {
			hint.gameObject.SetActive (true);  //Reveal a button to give the answer...
		}
	}

	//Displays the correct answer(s) in the correct/incorrect text box.
	public void giveHint ()
	{
		corInc.text = "The " + transform.parent.name + " does this:\n" + correction1 + "\n" + correction2 + "\n" + correction3;
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
			correctFirstTry = true;
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
			levelManager.addMuscPlaced ();
		}
	}
}
