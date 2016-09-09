using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/*This script will create a list of possible answers, shuffle the list, find the correct answer(s),
and ensure the correct answer(s) is/are in the first for choices.*/



public class QuizRandomQ2 : MonoBehaviour {

	private string levelName; //The name of the level
	public Text[] labels; //The dropdown list

	private const int MAXOPTIONS = 5; //Constant for the maximum number of choices we want.
	private List<String> ans = new List<String> (); //The list of answers
	[Tooltip("Type the Correct Answer in this field")]
	public List<String> correctAnswer = new List<String> (); //A public string for the correct answer.  MUST BE FILLED OUT IN INSPECTOR!
	private List<int> ansVal = new List<int> (); //The index value of the correct answer.

	// Use this for initialization
	void Start () {
		levelName = SceneManager.GetActiveScene ().name; //Get the name of the level.


		for (int n = 0; n<=MAXOPTIONS-1; n++)
			labels[n].text = ""; //Clear any current text in each answer option.


		getAnswerKey (); //Generates the list of possible answers.

		switch (correctAnswer.Count) {

		case 1:
			sortList0 ();
			break;
		case 2:
			sortList1 ();
			break;
		case 3:
			sortList2 (); //Shuffles the list of answers.
			break;

		}

		setAnswerOptions (); //Places the list in the Checkbox Field.

	
	}


	//Generates the list based on level name.
	void getAnswerKey(){
		switch (levelName) {

		//Arm Action
		case "UpperArm5":
			ans.Add ("Assists triceps in extending elbow joint");
			ans.Add ("Stabilizes elbow joint");
			ans.Add ("Abducts ulna during pronation");
			ans.Add ("Resists shoulder dislocation during abduction");
			ans.Add ("Extends shoulder joint");
			ans.Add ("Extends elbow joint");
			ans.Add ("Resists dislocation of shoulder");
			ans.Add ("Flex and adduct shoulder joint");
			ans.Add ("Flexes elbow joint in all positions");
			ans.Add ("Flexes shoulder joint");
			ans.Add ("Flexes elbow joint when forearm supinated");
			ans.Add ("Supinates forearm");
			break;
			 
		}


	}



	//Shuffles the list, ensuring that the correct answer is in the first five options.
	void sortList0(){


		//For loop.  Rearranges the list.
		for (int i = 0; i < ans.Count; i++) {
			string temp = ans [i]; //A temporary string to hold each list item.
			int randomIndex = UnityEngine.Random.Range (i, ans.Count); //Assign items to a new index.
			ans [i] = ans [randomIndex]; //assigns the index to its new index.
			ans [randomIndex] = temp; //assigns the string to the new index.
		}

		findAnswerValue0 (); //Finds the index value of the correct answer.

		//For loop.  Places correct answer in the first five choices of the list.
		if (ansVal[0] >= MAXOPTIONS){
			string temp = ans [ansVal[0]];
			int randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			ans [ansVal[0]] = ans [randomIndex0]; //assigns the index to its new index.
			ans [randomIndex0] = temp; //assigns the string to the new index.
			findAnswerValue0 ();  //Refinds the index of the correct answer.
			}



	}

	//Shuffles the list, ensuring that the 2 correct answers are in the first five options.
	void sortList1(){


		//For loop 1. Sorts the list.
		for (int i = 0; i < ans.Count; i++) {
			string temp = ans [i]; //A temporary string to hold each list item.
			int randomIndex = UnityEngine.Random.Range (i, ans.Count); //Assign items to a new index.
			ans [i] = ans [randomIndex]; //assigns the index to its new index.
			ans [randomIndex] = temp; //assigns the string to the new index.

		}



		findAnswerValue0 ();
		findAnswerValue1 ();
		if (ansVal [0] >= MAXOPTIONS) {
			string temp = ans [ansVal [0]];
			int randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			//Ensures correct answers will not replace each other.
			while (randomIndex0 == ansVal[1])
				randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			ans [ansVal [0]] = ans [randomIndex0]; //assigns the index to its new index.
			ans [randomIndex0] = temp; //assigns the string to the new index.
			findAnswerValue0 ();
		}

		findAnswerValue1 ();
		if (ansVal [1] >= MAXOPTIONS) {
			string temp = ans [ansVal [1]];
			int randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			//Ensures correct answers will not replace each other.
			while (randomIndex0 == ansVal[0])
				randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			ans [ansVal [1]] = ans [randomIndex0]; //assigns the index to its new index.
			ans [randomIndex0] = temp; //assigns the string to the new index.
			findAnswerValue0 ();
			findAnswerValue1 ();
		}
			


	}

	//Shuffles the list, ensuring that the 3 correct answers are in the first five options.
	void sortList2(){


		//For loop.  Sorts the list randomly.
		for (int i = 0; i < ans.Count; i++) {
			string temp = ans [i]; //A temporary string to hold each list item.
			int randomIndex = UnityEngine.Random.Range (i, ans.Count); //Assign items to a new index.
			ans [i] = ans [randomIndex]; //assigns the index to its new index.
			ans [randomIndex] = temp; //assigns the string to the new index.

		}

		findAnswerValue0 ();
		findAnswerValue1 ();
		findAnswerValue2 ();
		if (ansVal [0] >= MAXOPTIONS) {
			string temp = ans [ansVal [0]];
			int randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			//Ensures correct answers will not replace each other.
			while (randomIndex0 == ansVal[1] || randomIndex0 == ansVal[2])
				randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			ans [ansVal [0]] = ans [randomIndex0]; //assigns the index to its new index.
			ans [randomIndex0] = temp; //assigns the string to the new index.
			findAnswerValue0 ();
		}

		findAnswerValue1 ();
		if (ansVal [1] >= MAXOPTIONS) {
			string temp = ans [ansVal [1]];
			int randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			//Ensures correct answers will not replace each other.
			while (randomIndex0 == ansVal[0] || randomIndex0 == ansVal[2])
				randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			ans [ansVal [1]] = ans [randomIndex0]; //assigns the index to its new index.
			ans [randomIndex0] = temp; //assigns the string to the new index.
			findAnswerValue0 ();
			findAnswerValue1 ();
		}
			
		findAnswerValue2 ();
		if (ansVal [2] >= MAXOPTIONS) {
			string temp = ans [ansVal [2]];
			int randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			//Ensures correct answers will not replace each other.
			while (randomIndex0 == ansVal[0] || randomIndex0 == ansVal[1])
				randomIndex0 = UnityEngine.Random.Range (0, MAXOPTIONS - 1); //Assign items to a new index.
			ans [ansVal [2]] = ans [randomIndex0]; //assigns the index to its new index.
			ans [randomIndex0] = temp; //assigns the string to the new index.
			findAnswerValue0 ();
			findAnswerValue1 ();
			findAnswerValue2 ();
		}





	}


	//Adds the first 5 items in the list to the available options
	void setAnswerOptions(){
		for (int i=0; i<=MAXOPTIONS-1; i++)
			labels[i].text = ans[i];
	}
		
	//Find the index value of the correct answer.
	void findAnswerValue0(){
		//Look through each index value in the list.
		for (int i = 0; i < ans.Count; i++) {
		//If the correct answer is found...
		if (ans[i].Equals (correctAnswer[0])) {
				if (ansVal.Count >= 1) {
					ansVal [0] = i;//...Set the int to its value.
					return;
				} else {
					ansVal.Add (i);
					return;
				}
			}

		}
		//Otherwise we're in trouble. Check that the Correct Answer and desired answer in the list match.
		print ("Warning: Correct answer not found! Check that the Correct Answer and desired answer in the list match.");
	}

	//Find the index value of the correct answer.
	void findAnswerValue1(){
		//Look through each index value in the list.
		for (int i = 0; i < ans.Count; i++) {
			//If the correct answer is found...
			if (ans [i].Equals (correctAnswer [1])) {
				if (ansVal.Count >= 2) {
					ansVal [1] = i; //...Set the int to its value.
					return;
				} else {
					ansVal.Add (i);
					return;
				}
			}
		}
		//Otherwise we're in trouble. Check that the Correct Answer and desired answer in the list match.
		print ("Warning: Correct answer not found! Check that the Correct Answer and desired answer in the list match.");
	}

	//Find the index value of the correct answer.
	void findAnswerValue2(){
		//Look through each index value in the list.
		for (int i = 0; i < ans.Count; i++) {
			//If the correct answer is found...
			if (ans [i].Equals (correctAnswer [2])) {
				if (ansVal.Count >= 3) {
					ansVal [2] = i;//...Set the int to its value.
					return;
				} else {
					ansVal.Add (i);
					return;
				}
			}
		}
		//Otherwise we're in trouble. Check that the Correct Answer and desired answer in the list match.
		print ("Warning: Correct answer not found! Check that the Correct Answer and desired answer in the list match.");
	}

	//A getter for the Correct Answer.
	public List<String> getAnswer(){
		return correctAnswer;
	}

	//A getter for the Index number of the Correct Answer.
	public List<int> getValue(){
		return ansVal;
	}

}
