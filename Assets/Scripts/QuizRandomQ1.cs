using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/*This script will create a list of possible answers, find the correct answer, shuffle the list,
and reduce the number of choices to 5, ensuring the correct answer is in one of those choices.*/


public class QuizRandomQ1 : MonoBehaviour {

	private string levelName; //The name of the level
	private Dropdown drpDwn; //The dropdown list

	private const int MAXOPTIONS = 6; //Constant for the maximum number of choices we want.
	private List<String> ans = new List<String> (); //The list of answers
	[Tooltip("Type the Correct Answer in this field")]
	public string correctAnswer; //A public string for the correct answer.  MUST BE FILLED OUT IN INSPECTOR!
	private int ansVal; //The index value of the correct answer.

	// Use this for initialization
	void Start () {
		levelName = SceneManager.GetActiveScene ().name; //Get the name of the level.


		drpDwn = GetComponentInChildren <Dropdown> (); //Find the Drop Down Menu
		drpDwn.options.Clear (); //Clear any current options in the Drop Down Menu.

		ans.Add ("Please select an answer."); //Add the first option, always this value.

		getAnswerKey (); //Generates the list of possible answers.
		 
		sortList (); //Shuffles the list of answers.
		cutExtra (); //Truncates the number of options to 5 answers plus the default answer.

		setAnswerOptions (); //Places the list in the Drop Down Menu.

	
	}


	//Generates the list based on level name.
	void getAnswerKey(){
		switch (levelName) {

		//Bone & Nerves Baseline and Comprehension
		case "Armbone0": case "Nerves3":
			if (this.name.Equals ("Question1")) {
				ans.Add ("Musculocutaneous");
				ans.Add ("Axillary");
				ans.Add ("Radial");
				ans.Add ("Medial");
				ans.Add ("Ulnar Nerves");
			} else if (this.name.Equals ("Question2")){
				ans.Add ("Musculocutaneous");
				ans.Add ("Axillary");
				ans.Add ("Radial");
				ans.Add ("Medial");
				ans.Add ("Ulnar Nerves");
			} else if (this.name.Equals ("Question3")){
				ans.Add ("Capitate");
				ans.Add ("Lunate");
				ans.Add ("Hamate");
				ans.Add ("Scaphoid");
				ans.Add ("Trapezoid");
			} else if (this.name.Equals ("Question4")){
				ans.Add ("Musculocutaneous");
				ans.Add ("Axillary");
				ans.Add ("Radial");
				ans.Add ("Medial");
				ans.Add ("Ulnar Nerves");
			} else if (this.name.Equals ("Question5")){
				ans.Add ("Axillary, musculocutaneous, long thoracic nerve, median, and ulnar nerves.");
				ans.Add ("Median, musculocutaneous, radial, axillary, and ulnar nerves.");
				ans.Add ("Musculocutaneous, axillary, radial, medial pectoral, and ulnar nerves.");
				ans.Add ("Radial, axillary, musculocutaneous, thoracodorsal, and ulnar nerves.");
				ans.Add ("Ulnar, thoracodorsal, radial, median, and axillary nerves.");
			} else if (this.name.Equals ("Question6")){
				ans.Add ("Deltoid tuberosity");
				ans.Add ("Greater tubercle");
				ans.Add ("Lesser tubercle");
				ans.Add ("Radial styloid");
				ans.Add ("Ulnar styloid");
			}
			break;

		//Bone Whole Arm
		case "Armbone1":
			ans.Add ("Humerus");
			ans.Add ("Ulna");
			ans.Add ("Radius");
			ans.Add ("Metacarpals");
			ans.Add ("Phalanges");
			ans.Add ("Scapula");
			ans.Add ("Carpals");
			break;

		//Bony Structure Arm
		case "Armbone2":
			ans.Add ("Spine of the Scapula");
			ans.Add ("Deltoid Tuberosity");
			ans.Add ("Radial Groove");
			ans.Add ("Acromion");
			ans.Add ("Olecranon Fossa");
			ans.Add ("Coracoid Process");
			ans.Add ("Supraglenoid Turbercle");
			ans.Add ("Acromion Process");
			ans.Add ("Radial Fossa");
			ans.Add ("Trochlea");
			ans.Add ("Capitulum");
			ans.Add ("Lateral Epicondyle");
			ans.Add ("Medial Epicondyle");
			ans.Add ("Coronoid Fossa");
			ans.Add ("Lesser Tubercle");
			ans.Add ("Greater Tubercle");
			break;


		//Bony Structure Forearm & Hand 
		case "Armbone3":
			if (this.name == "QF") {
				ans.Add ("Head of Radius");
				ans.Add ("Olecranon");
				ans.Add ("Styloid Process of Radius");
				ans.Add ("Styloid Process of Ulna");
				ans.Add ("Ulna Tuberosity");
				ans.Add ("Radial Tuberosity");
			}
			if (this.name == "QH") {
				ans.Add ("Hamate");
				ans.Add ("Triquetrum");
				ans.Add ("Trapezium");
				ans.Add ("Lunate");
				ans.Add ("Scaphoid");
				ans.Add ("Trapezoid");
				ans.Add ("Capitate");
				ans.Add ("Metacarpals"); 
				ans.Add ("Proximal Phalanges");
				ans.Add ("Middle Phalanges");
				ans.Add ("Distal Phalanges"); 
			}
			break;

		
		//Arm Muscles Baseline and Comprehension
		case "UpperArm0": case "Hand6A":
			if (this.name.Equals ("Question1")) {
				ans.Add ("Axillary, Median");
				ans.Add ("Median, Ulnar");
				ans.Add ("Radial, Axillary");
				ans.Add ("Ulnar, Axillary");
				ans.Add ("Ulnar, Radial");
			} else if (this.name.Equals ("Question2")){
				ans.Add ("Anterior surface of radius");
				ans.Add ("Anteromedial surface of ulna");
				ans.Add ("Medial epicondyle of humerus");
				ans.Add ("Oblique line of radius");
				ans.Add ("Posterior border of ulna");
			} else if (this.name.Equals ("Question3")){
				ans.Add ("Axillary");
				ans.Add ("Median");
				ans.Add ("Musculocutaneous");
				ans.Add ("Radial");
				ans.Add ("Ulnar");
			} else if (this.name.Equals ("Question4")){
				ans.Add ("Lateral epicondyle of humerus");
				ans.Add ("Lateral supracondylar ridge of humerus");
				ans.Add ("Posterior surface of distal third of ulna");
				ans.Add ("Posterior surface of middle third of ulna");
				ans.Add ("Posterior surface of proximal halves of ulna");
			} else if (this.name.Equals ("Question5")){
				ans.Add ("Adducts thumb; median nerve");
				ans.Add ("Adducts thumb; ulnar nerve");
				ans.Add ("Flexes thumb; median nerve");
				ans.Add ("Flexes thumb; ulnar nerve");
				ans.Add ("Opposes thumb; radial nerve");
			} else if (this.name.Equals ("Question6")){
				ans.Add ("Deep branch of radial nerve");
				ans.Add ("Deep branch of ulnar nerve");
				ans.Add ("Median nerve");
				ans.Add ("Recurrent branch of median nerve");
				ans.Add ("Ulnar nerve");
			}
			break;

		//Arm Naming
		case "UpperArm2":
			ans.Add ("Biceps Brachii");
			ans.Add ("Coracobrachialis");
			ans.Add ("Brachialis");
			ans.Add ("Triceps Brachii");
			ans.Add ("Anconeus");
			break;
		
		//Arm Attachment
		case "UpperArm3":
			if (this.name == "Distal") {
				ans.Add ("Radial tuberosity and bicipital aponeurosis");
				ans.Add ("Lateral surface of olecranon");
				ans.Add ("Olecranon process of ulna");
				ans.Add ("Coronoid process and tuberosity of ulna");
				ans.Add ("Medial surface of humerus");
				break;
			}
			else {
				ans.Add ("Coracoid process of scapula");
				ans.Add ("Supraglenoid tubercle of scapula");
				ans.Add ("Anterior surface of humerus");
				ans.Add ("Infraglenoid tubercle of scapula");
				ans.Add ("Superior to radial groove of humerus");
				ans.Add ("Inferior to radial groove of humerus");
				ans.Add ("Lateral epicondyle of humerus");
				break;
			}
		
		//Arm Innervation
		case "UpperArm4":
			ans.Add ("Axillary");
			ans.Add ("Median");
			ans.Add ("Musculocutaneous");
			ans.Add ("Radial");
			ans.Add ("Ulnar");
			break;


		
		//Forearm Anterior Naming
		case "Forearm2A":
			ans.Add ("Pronator Teres");
			ans.Add ("Palamaris Longus");
			ans.Add ("Flexor Carpi Ulnaris");
			ans.Add ("Flexor Carpi Radialis");
			ans.Add ("Flexor Digitorum Superficialis");
			ans.Add ("Flexor Pollicis Longus");
			ans.Add ("Pronator Quadratus");
			ans.Add ("Flexor Digitorum Profundus");
			break;
		
		//Forearm Posterior Naming
		case "Forearm2B":
			ans.Add ("Brachioradialis Extensor Carpi Radialus Longus");
			ans.Add ("Extensor Pollicis Brevis");
			ans.Add ("Extensor Pollicis Longus");
			ans.Add ("Extensor Indicis");
			ans.Add ("Abductor Pollicis Longus");
			ans.Add ("Supinator");
			ans.Add ("Extensor Carpi Radialis Brevis");
			ans.Add ("Extensor Carpi Ulnaris");
			ans.Add ("Extensor Digitorum carpi ulnaris");
			ans.Add ("Extensor Digiti Minimi");
			ans.Add ("Brachioradialis");
			break;
		
		
		
		//Forearm Anterior Attachments
		case "Forearm3A":
			if (this.name == "Proximal") {
				ans.Add ("Medial epicondyle of humerus");
				ans.Add ("Medial epicondyle, medial olecranon, posterior border of ulna");
				ans.Add ("Anteromedial surface of ulna, interosseous membrane");
				ans.Add ("Medial epicondyle, coronoid process, oblique line of radius");
				ans.Add ("Anterior surface of radius, interosseous membrane");
				ans.Add ("Anterior surface of distal ulna");
				ans.Add ("Medial epicondyle and coronoid process of ulna");
				break;
			}

			else {
				ans.Add ("Bases of second and third metacarpals");
				ans.Add ("Pisiform, hook of hamate, and base of fifth metacarpal");
				ans.Add ("Bases of distal phalanges of fingers (2-5)");
				ans.Add ("Shafts of middle phalanges of finger (2-5)");
				ans.Add ("Base of distal phalanx of thumb");
				ans.Add ("Flexor retinaculum, palmar aponeurosis");
				ans.Add ("Anterior surface of distal radius");
				ans.Add ("Middle of lateral surface of radius");
				break;
			}

		//Forearm Posterior Attachments
		case "Forearm3B":
			if (this.name == "Proximal") {
				ans.Add ("Lateral epicondyle of humerus");
				ans.Add ("Lateral epicondyle and posterior surface of ulna");
				ans.Add ("Posterior surface of distal third of ulna and interosseous membrane");
				ans.Add ("Posterior surface of middle third of ulna and interosseous membrane"); 
				ans.Add ("Lateral epicondyle of humerus, radial collateral and annular ligaments, supinator fossa and crest of ulna");
				ans.Add ("Lateral supracondylar ridge of humerus");
				ans.Add ("Posterior surface of proximal halves of ulna, radius, and interosseous membrane");
				break;
			}

			else {
				ans.Add ("Base of 1st metacarpal");
				ans.Add ("Dorsum of base of 3rd metacarpal");
				ans.Add ("Dorsum of base of 2nd metacarpal");
				ans.Add ("Dorsum of base of 5th metacarpal");
				ans.Add ("Extensor expansion of 5th digit");
				ans.Add ("Extensor expansions of 2-5 digits");
				ans.Add ("Extensor expansion 2nd digit");
				ans.Add ("Dorsum of base of proximal phalanx of thumb");
				ans.Add ("Dorsum of base of distal phalanx of thumb");
				ans.Add ("Proximal third of radius");
				ans.Add ("Base of radial styloid process");
				break;
			}
		
		//Forearm Anterior Innervation
		case "Forearm4A":
			ans.Add ("Median");
			ans.Add ("Median & Ulnar");
			ans.Add ("Median & Radial");
			ans.Add ("Radial");
			ans.Add ("Radial & Ulnar");
			ans.Add ("Ulnar");
			break;
		
		//Forearm Posterior Innervation (Identical to Arm Innervation)
		case "Forearm4B":
			ans.Add ("Axillary");
			ans.Add ("Median");
			ans.Add ("Musculocutaneous");
			ans.Add ("Radial");
			ans.Add ("Ulnar");
			break;

		//Forearm Anterior Action
		case "Forearm5A":
			ans.Add ("Pronates and flexes forearm");
			ans.Add ("Flexes and abducts hand at wrist");
			ans.Add ("Flexes hand (at wrist) and tenses palmar aponeurosis");
			ans.Add ("Flexes and adducts hand at wrist");
			ans.Add ("Flexes wrist and proximal interphalangeal joints (2-5)");
			ans.Add ("Flexes wrist and distal interphalangeal joints (2-5)");
			ans.Add ("Flexes metacarpophalangeal and interphalangeal joints thumb");
			ans.Add ("Pronates forearm");
			break;
		
		//Forearm Posterior Action
		case "Forearm5B":
			ans.Add ("Abducts thumb and extends its CMC joint");
			ans.Add ("Extends 2-5 digits");
			ans.Add ("Extends 2nd digit");
			ans.Add ("Extends 5th digit");
			ans.Add ("Extends and abducts hand");
			ans.Add ("Extends distal phalanx of thumb");
			ans.Add ("Extends proximal phalanx of thumb");
			ans.Add ("Flexes forearm");
			ans.Add ("Supinates forearm");
			break;

		//Hand Naming
		case "Hand2A":
			ans.Add ("Opponens Pollicis");
			ans.Add ("Abductor Digiti Minimi");
			ans.Add ("Abductor Pollicis Brevis");
			ans.Add ("Dorsal Interossei");
			ans.Add ("Flexor Pollicis Brevis");
			ans.Add ("Opponens Digiti Minimi");
			ans.Add ("Adductor Pollicis");
			ans.Add ("Flexor Digit Minimi Brevis");
			ans.Add ("Palmar Interosseous");
			ans.Add ("Lumbricals");
			break;

		//Hand Attachments
		case "Hand3A":
			if (this.name == "Proximal" || this.name == "P1-2" || this.name == "P3-4" || this.name == "Oblique" || this.name == "Transverse") {
				ans.Add ("Flexor retinaculum, scaphoid and trapezium");
				ans.Add ("Capitate, bases of 2nd and 3rd metacarpals");
				ans.Add ("Anterior surface of shaft of 3rd metacarpal");
				ans.Add ("Flexor retinaculum, scaphoid and trapezium");
				ans.Add ("Pisiform");
				ans.Add ("Flexor retinaculum, hook of hamate");
				ans.Add ("Lateral two tendons of flexor digitorum profundus");
				ans.Add ("Medial three tendons of flexor digitorum profundus");
				ans.Add ("Palmar surfaces of 2nd, 4th, and 5th metacarpals");
				ans.Add ("Adjacent sides of two metacarpals"); 
			} else {
				ans.Add ("Lateral side of 1st metacarpal");
				ans.Add ("Lateral side of base of proximal phalanx of thumb");
				ans.Add ("Skin of medial side of palm");
				ans.Add ("Medial side of base of proximal phalanx of 5th finger");
				ans.Add ("Medial border of 5th metacarpal");
				ans.Add ("Lateral aspect of extensor expansions");
				ans.Add ("Bases of proximal phalanges; extensor expansions of 2nd–4th fingers");
				ans.Add ("Bases of proximal phalanges; extensor expansions of 2nd, 4th, and 5th fingersBases of proximal phalanges; extensor expansions of 2nd, 4th, and 5th fingers");
			}
			break;

		//Hand Innervation
		case "Hand4A":
			ans.Add ("Deep branch of radial nerve");
			ans.Add ("Deep branch of ulnar nerve");
			ans.Add ("Median nerve");
			ans.Add ("Recurrent branch of median nerve");
			ans.Add ("Ulnar nerve");
			break;

		//Hand Action
		case "Hand5A":
			ans.Add ("Draws 1st metacarpal medially to center of palm and rotates it medially");
			ans.Add ("Abducts thumb");
			ans.Add ("Flexes thumb");
			ans.Add ("Adducts thumb");
			ans.Add ("Abducts 5th finger; assists in flexion of its proximal phalanx");
			ans.Add ("Flexes proximal phalanx of 5th finger");
			ans.Add ("Draws 5th metacarpal anterior and rotates it, bringing 5th finger into opposition with thumb");
			ans.Add ("Flex MP joints; extend IP joints of 2nd–5th fingers");
			ans.Add ("Abduct 2nd–4th fingers from axial line");
			ans.Add ("Adduct 2nd, 4th, and 5th fingers toward axial line");
			break;

		//Nervous System for Arm
		case "Nerves1":
			ans.Add ("Ulna Nerve");
			ans.Add ("Medial");
			ans.Add ("Axillary Nerve");
			ans.Add ("Musculocutaneous");
			//ans.Add ("Brachial Plexus");
			ans.Add ("Medial Cord of Brachial Plexus");
			ans.Add ("Posterior Cord of Brachial Plexus");
			ans.Add ("Lateral Cord of Brachial Plexus");
			ans.Add ("Medial Brachial Cutaneous");
			ans.Add ("Medial Antebrachial Cutaneous");
			ans.Add ("Radial Nerve");
			ans.Add ("Radial Nerve Posterior");
			ans.Add ("Axillary Nerve Posterior");
			break;

		//Nervous Skin for Arm
		case "Nerves2":
			ans.Add ("Ulna Nerve");
			ans.Add ("Medial");
			ans.Add ("Axillary Nerve");
			ans.Add ("Supraclavicular");
			ans.Add ("Clavicular");
			ans.Add ("Medial Brachial and Intercostal Brachial");
			ans.Add ("Ulnar");
			ans.Add ("Medial Antebrachial Cutaneous");
			ans.Add ("Radial Nerve");
			ans.Add ("Median");
			ans.Add ("Lateral Antebrachial Cutaneous");
			break;
		}


	}

	//Shuffles the list, ensuring that the correct answer is in the first five options.
	void sortList(){

		//For loop.
		for (int i = 1; i < ans.Count; i++) {
			string temp = ans[i]; //A temporary string to hold each list item.
			int randomIndex = UnityEngine.Random.Range(i, ans.Count); //Assign items to a new index.
			ans[i] = ans[randomIndex]; //assigns the index to its new index.
			ans[randomIndex] = temp; //assigns the string to the new index.

		}

		findAnswerValue (); //Finds the index value of the correct answer.

		//Check to see the the answer is within the choices 1-5 (exclude option 0)
		if (getValue () + 1 > MAXOPTIONS) {
			sortList (); //If the answer is outside of desired range, re-call the method.
		}


	}

	//Truncates the list to only 5 available answers.
	void cutExtra ()
	{
		//If there are more available answers than we want...
		if (ans.Count > MAXOPTIONS)
			ans.RemoveRange (MAXOPTIONS, ans.Count - MAXOPTIONS); //...Remove all of the extra answers!
		}

	//Place the list inside of the Drop Down Menu
	void setAnswerOptions(){
		drpDwn.AddOptions (ans); //Adds the list into the Drop Down Menu.
	}
		
	//Find the index value of the correct answer.
	void findAnswerValue(){
		//Look through each index value in the list.
		for (int n=1; n<=ans.Count; n++)  {
			//If the correct answer is found...
			if (ans [n].Equals (correctAnswer)) {
				ansVal = n; //...Set the int to its value.
				return; //And then we're done.
			} 
		}
		//Otherwise we're in trouble. Check that the Correct Answer and desired answer in the list match.
		Debug.LogWarning ("Correct answer not found! Check that the Correct Answer and desired answer in the list match.");
	}

	//A getter for the Correct Answer.
	public string getAnswer(){
		return correctAnswer;
	}

	//A getter for the Index number of the Correct Answer.
	public int getValue(){
		return ansVal;
	}

}
