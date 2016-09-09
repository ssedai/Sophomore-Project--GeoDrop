using UnityEngine;
using System.Collections;

public class ReportSelf : MonoBehaviour {

	private StatTracking statTracking;
	private bool hintUsed = false;
	private int guesses;
	private int qGuesses;

	// Use this for initialization
	void Start () {
	
		statTracking = FindObjectOfType<StatTracking> ();
		guesses = 0; //Makes sure that guesses starts off equal to 0.
	}
	

	public void reportSelf(){
		if (statTracking.identifyLevel () == "Q0" || statTracking.identifyLevel () == "QC") {
			switch (this.name){
				case "Question1": case "Naming1": case "MuscNameArm1": case "Anterior Biceps Brachii": case "Spine of the Scapula": case "Pronator Teres": case "Brachioradialis Extensor Carpi Radialus Longus": case "Radius Posterior":
				case "Opponens Pollicis": case "Metacarpal": case "Ulna Nerve":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[0] = guesses.ToString () + "&Hint1=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[0] = guesses.ToString () + "&Hint1=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[0] = guesses.ToString () + "&Hint1=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[0] = guesses.ToString () + "&Hint1=N%2FU";
				}
				break;

				//Body Part 1
				case "Question2": case "Naming2": case "MuscNerveArm2": case "Posterior Anconeus": case "Deltoid Tuberosity": case "Flexor Carpi Radialis": case "Extensor Pollicis Brevis": case "Abductor Digiti Minimi": case "Olecranon": case "Hamate":
				case "Medial":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[1] = guesses.ToString () + "&Hint2=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[1] = guesses.ToString () + "&Hint2=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[1] = guesses.ToString () + "&Hint2=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[1] = guesses.ToString () + "&Hint2=N%2FU";
				}
				break;

				//Body Part 2
				case "Question3": case "Naming3":  case "MuscNameForePost3": case "Anterior Coracobrachialis": case "Radial Groove": case "Palamaris Longus": case "Extensor Pollicis Longus": case "Styloid Process of Radius":
				case "Abductor Pollicis brevis": case "Distal Phalanges": case "Axillary Nerve":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[2] = guesses.ToString () + "&Hint3=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[2] = guesses.ToString () + "&Hint3=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[2] = guesses.ToString () + "&Hint3=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[2] = guesses.ToString () + "&Hint3=N%2FU";
				}
				break;

				//Body Part 3
				case "Question4": case "Naming4": case "Posterior Triceps Brachii": case "Acromion": case "Flexor Carpi Ulnaris": case "Extensor Indicis": case "Dorsal Interossei": case "Styloid Process of Ulna": case "Triquetrum":
				case "Musculocutaneous": case "Supraclavicular":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[3] = guesses.ToString () + "&Hint4=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[3] = guesses.ToString () + "&Hint4=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[3] = guesses.ToString () + "&Hint4=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[3] = guesses.ToString () + "&Hint4=N%2FU";
				}
				break;

				//Body Part 4
				case "Question5": case "Nerve1": case "Anterior Brachialis": case "Olecranon Fossa": case "Flexor Digitorum Superficialis": case "Abductor Pollicis Longus": case "Ulna Tuberosity":
				case "Opponens Digiti Minimi": case "Trapezium": case "Axillary Nerve Posterior":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[4] = guesses.ToString () + "&Hint5=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[4] = guesses.ToString () + "&Hint5=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[4] = guesses.ToString () + "&Hint5=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[4] = guesses.ToString () + "&Hint5=N%2FU";
				}
				break;

				//Body Part 5
				case "Question6": case "Nerve2": case "Coracoid Process": case "Flexor Digitorum Profundus": case "Extensor Carpi Radialis Brevis": case "Radial Tuberosity": case "Adductor Pollicis": case "Lunate":
				case "Medial Cord of Brachial Plexus": case "Medial Brachail and Intercostal Brachial":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[5] = guesses.ToString () + "&Hint6=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[5] = guesses.ToString () + "&Hint6=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[5] = guesses.ToString () + "&Hint6=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[5] = guesses.ToString () + "&Hint6=N%2FU";
				}
				break;

				//Body Part 6
				case "Nerve3": case "Acromion Process": case "Flexor Pollicis Longus": case "Extensor Carpi Ulnaris": case "Flexor Pollicis Brevis": case "Scaphoid": case "Posterior Cord of Brachial Plexus":
				case "Medial Brachial and Intercostal Brachial Posterior":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[6] = guesses.ToString () + "&Hint7=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[6] = guesses.ToString () + "&Hint7=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[6] = guesses.ToString () + "&Hint7=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[6] = guesses.ToString () + "&Hint7=N%2FU";
				}
				break;

				//Body Part 7
				case "Question7": case "Nerve4": case "Radial Fossa": case "Pronator Quadratus": case "Extensor Digitorum carpi ulnaris": case "Flexor Digiti Minimi Brevis": case "Trapezoid": 
				case "Lateral Cord of Brachial Plexus": case "Ulnar":			
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[7] = guesses.ToString () + "&Hint8=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[7] = guesses.ToString () + "&Hint8=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[7] = guesses.ToString () + "&Hint8=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[7] = guesses.ToString () + "&Hint8=N%2FU";
				}
				break;

				//Body Part 8
				case "Question8": case "Trochlea": case "Extensor Digiti Minimi": case "Palmar Interosseous": case "Middle Phalanges": case "Medial Brachial Cutaneous":
				case "Medial Antebrachial Cutaneous Posterior":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[8] = guesses.ToString () + "&Hint9=Y";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[8] = guesses.ToString () + "&Hint9=N";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[8] = guesses.ToString () + "UA&Hint9=Y";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[8] = guesses.ToString () + "UA&Hint9=N";
				}
				break;

				//Body Part 9
				case "Question9": case "Capitulum": case "Supinator": case "Lumbricals": case "Capitate": case "Medial Antebrachial Cutaneous":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[9] = guesses.ToString () + "&Hint10=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[9] = guesses.ToString () + "&Hint10=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[9] = guesses.ToString () + "&Hint10=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[9] = guesses.ToString () + "&Hint10=N%2FU";
				}
				break;

				//Body Part 10
				case "Question10": case "Brachioradialis":  case "Lateral Epicondyle": case "Proximal Phalanges": case "Radial Nerve":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[10] = guesses.ToString () + "&Hint11=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[10] = guesses.ToString () + "&Hint11=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[10] = guesses.ToString () + "&Hint11=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[10] = guesses.ToString () + "&Hint11=N%2FU";
				}
				break;
			}
		}else{
			switch (transform.parent.name) {			
			//Body Part 0
			case "Naming1": case "Anterior Biceps Brachii": case "Spine of the Scapula": case "Pronator Teres": case "Brachioradialis Extensor Carpi Radialus Longus": case "Radius Posterior":
			case "Opponens Pollicis": case "Metacarpal": case "Ulna Nerve":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[0] = guesses.ToString () + "&Hint1=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[0] = guesses.ToString () + "&Hint1=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[0] = guesses.ToString () + "&Hint1=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[0] = guesses.ToString () + "&Hint1=N%2FU";
				}
				break;

				//Body Part 1
			case "Naming2": case "Posterior Anconeus": case "Deltoid Tuberosity": case "Flexor Carpi Radialis": case "Extensor Pollicis Brevis": case "Abductor Digiti Minimi": case "Olecranon": case "Hamate":
			case "Medial":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[1] = guesses.ToString () + "&Hint2=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[1] = guesses.ToString () + "&Hint2=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[1] = guesses.ToString () + "&Hint2=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[1] = guesses.ToString () + "&Hint2=N%2FU";
				}
				break;

				//Body Part 2
			case "Naming3": case "Anterior Coracobrachialis": case "Radial Groove": case "Palamaris Longus": case "Extensor Pollicis Longus": case "Styloid Process of Radius":
			case "Abductor Pollicis brevis": case "Distal Phalanges": case "Axillary Nerve":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[2] = guesses.ToString () + "&Hint3=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[2] = guesses.ToString () + "&Hint3=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[2] = guesses.ToString () + "&Hint3=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[2] = guesses.ToString () + "&Hint3=N%2FU";
				}
				break;

				//Body Part 3
			case "Naming4": case "Posterior Triceps Brachii": case "Acromion": case "Flexor Carpi Ulnaris": case "Extensor Indicis": case "Dorsal Interossei": case "Styloid Process of Ulna": case "Triquetrum":
			case "Musculocutaneous": case "Supraclavicular":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[3] = guesses.ToString () + "&Hint4=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[3] = guesses.ToString () + "&Hint4=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[3] = guesses.ToString () + "&Hint4=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[3] = guesses.ToString () + "&Hint4=N%2FU";
				}
				break;

				//Body Part 4
			case "Nerve1": case "Anterior Brachialis": case "Olecranon Fossa": case "Flexor Digitorum Superficialis": case "Abductor Pollicis Longus": case "Ulna Tuberosity":
			case "Opponens Digiti Minimi": case "Trapezium": case "Axillary Nerve Posterior":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[4] = guesses.ToString () + "&Hint5=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[4] = guesses.ToString () + "&Hint5=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[4] = guesses.ToString () + "&Hint5=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[4] = guesses.ToString () + "&Hint5=N%2FU";
				}
				break;

				//Body Part 5
			case "Nerve2": case "Coracoid Process": case "Flexor Digitorum Profundus": case "Extensor Carpi Radialis Brevis": case "Radial Tuberosity": case "Adductor Pollicis": case "Lunate":
			case "Medial Cord of Brachial Plexus": case "Medial Brachial and Intercostal Brachial":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[5] = guesses.ToString () + "&Hint6=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[5] = guesses.ToString () + "&Hint6=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[5] = guesses.ToString () + "&Hint6=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[5] = guesses.ToString () + "&Hint6=N%2FU";
				}
				break;

				//Body Part 6
			case "Nerve3": case "Acromion Process": case "Flexor Pollicis Longus": case "Extensor Carpi Ulnaris": case "Flexor Pollicis Brevis": case "Scaphoid": case "Posterior Cord of Brachial Plexus":
			case "Medial Brachial and Intercostal Brachial Posterior":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[6] = guesses.ToString () + "&Hint7=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[6] = guesses.ToString () + "&Hint7=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[6] = guesses.ToString () + "&Hint7=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[6] = guesses.ToString () + "&Hint7=N%2FU";
				}
				break;

				//Body Part 7
			case "Nerve4": case "Radial Fossa": case "Pronator Quadratus": case "Extensor Digitorum carpi ulnaris": case "Flexor Digiti Minimi Brevis": case "Trapezoid": 
			case "Lateral Cord of Brachial Plexus": case "Ulnar":			
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[7] = guesses.ToString () + "&Hint8=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[7] = guesses.ToString () + "&Hint8=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[7] = guesses.ToString () + "&Hint8=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[7] = guesses.ToString () + "&Hint8=N%2FU";
				}
				break;

				//Body Part 8
			case "Trochlea": case "Extensor Digiti Minimi": case "Palmar Interosseous": case "Middle Phalanges": case "Medial Brachial Cutaneous":
			case "Medial Antebrachial Cutaneous Posterior":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[8] = guesses.ToString () + "&Hint9=Y";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[8] = guesses.ToString () + "&Hint9=N";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[8] = guesses.ToString () + "UA&Hint9=Y";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[8] = guesses.ToString () + "UA&Hint9=N";
				}
				break;

				//Body Part 9
			case "Capitulum": case "Supinator": case "Lumbricals": case "Capitate": case "Medial Antebrachial Cutaneous":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[9] = guesses.ToString () + "&Hint10=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[9] = guesses.ToString () + "&Hint10=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[9] = guesses.ToString () + "&Hint10=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[9] = guesses.ToString () + "&Hint10=N%2FU";
				}
				break;

				//Body Part 10
			case "Brachioradialis":  case "Lateral Epicondyle": case "Proximal Phalanges": case "Radial Nerve":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[10] = guesses.ToString () + "&Hint11=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[10] = guesses.ToString () + "&Hint11=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[10] = guesses.ToString () + "&Hint11=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[10] = guesses.ToString () + "&Hint11=N%2FU";
				}
				break;

				//Body Part 11
			case "Medial Epicondyle": case "Radial Nerve Posterior":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[11] = guesses.ToString () + "&Hint12=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[11] = guesses.ToString () + "&Hint12=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[11] = guesses.ToString () + "&Hint12=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[11] = guesses.ToString () + "&Hint12=N%2FU";
				}
				break;

				//Body Part 12
			case "Coranoid Fossa": case "Clavicular":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[12] = guesses.ToString () + "&Hint13=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[12] = guesses.ToString () + "&Hint13=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[12] = guesses.ToString () + "&Hint13=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[12] = guesses.ToString () + "&Hint13=N%2FU";
				}
				break;

				//Body Part 13
			case "Lesser Tubercle": case "Median":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[13] = guesses.ToString () + "&Hint14=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[13] = guesses.ToString () + "&Hint14=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[13] = guesses.ToString () + "&Hint14=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[13] = guesses.ToString () + "&Hint14=N%2FU";
				}
				break;

				//Body Part 14
			case "Greater Tubercle": case "Radial Nerve (Thumb)":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[14] = guesses.ToString () + "&Hint15=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[14] = guesses.ToString () + "&Hint15=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[14] = guesses.ToString () + "&Hint15=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[14] = guesses.ToString () + "&Hint15=N%2FU";
				}
				break;

				//Body Part 15
			case "Supraglenoid Turbercle": case "Lateral Antebrachial Cutaneous":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[15] = guesses.ToString () + "&Hint16=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[15] = guesses.ToString () + "&Hint16=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[15] = guesses.ToString () + "&Hint16=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[15] = guesses.ToString () + "&Hint16=N%2FU";
				}
				break;

				//Body Part 16
			case "Lateral Antebrachial Cutaneous Posterior":
				if (hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[16] = guesses.ToString () + "&Hint17=Y%2FP";
				} else if (!hintUsed && transform.gameObject.tag == "Placed") {
					statTracking.iPart[16] = guesses.ToString () + "&Hint17=N%2FP";
				} else if (hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[16] = guesses.ToString () + "&Hint17=Y%2FU";
				} else if (!hintUsed && transform.gameObject.tag == "unPlaced") {
					statTracking.iPart[16] = guesses.ToString () + "&Hint17=N%2FU";
				}
				break;
			}
		}
	}

	public bool getHint(){
		return hintUsed;
	}

	public int getGuesses(){
		return guesses;
	}

	public void toggleHintTrue(){
		hintUsed = true;
	}

	public void toggleHintFalse(){
		hintUsed = false;
	}

	public void addGuess(){
		guesses++;
	}

	public void addQguess(){
		qGuesses++;
	}
	public int getQGuess(){
		return qGuesses;
	}
}
