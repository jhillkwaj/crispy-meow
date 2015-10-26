using UnityEngine;
using System.Collections;

public class GenerateTrack : MonoBehaviour {

	int START_GEN = 10;
	int STRAIGHT_GEN = 3;
	GameObject lastEnd;
	public Queue track = new Queue();
	// Use this for initialization
	void Start () {
		lastEnd = this.gameObject;
		for (int i = 0; i < STRAIGHT_GEN; i++) {
			generateStraight();
		}
		for (int i = 0; i < START_GEN; i++) {
			generateNewTrack();
		}
	}
	
	public void generateNewTrack() {
		int random = Random.Range (1, 16);
		GameObject newTrack = null;
		if (random <= 10) {
			newTrack = Instantiate (Resources.Load ("StraightLine")) as GameObject;
			newTrack.transform.rotation = Quaternion.Euler(new Vector3(0 , lastEnd.transform.eulerAngles.y, lastEnd.transform.eulerAngles.z));
		} else if (random <= 14) {
			newTrack = Instantiate (Resources.Load ("Turn2")) as GameObject;
			newTrack.transform.rotation = Quaternion.Euler(new Vector3(0 , lastEnd.transform.eulerAngles.y, lastEnd.transform.eulerAngles.z - 90));
		} else if (random <= 15) {
			newTrack = Instantiate (Resources.Load ("Turn1")) as GameObject;
			newTrack.transform.rotation = Quaternion.Euler(new Vector3(0 , lastEnd.transform.eulerAngles.y, lastEnd.transform.eulerAngles.z));
		}// else {
			//newTrack = Instantiate (Resources.Load ("Turn1")) as GameObject;
		//}


		newTrack.transform.position = new Vector3(lastEnd.transform.position.x + (newTrack.transform.position.x - newTrack.GetComponent<Movement> ().start.transform.position.x),
		                                          lastEnd.transform.position.y + (newTrack.transform.position.y - newTrack.GetComponent<Movement> ().start.transform.position.y), 0 );
		lastEnd = newTrack.GetComponent<Movement> ().end;
		track.Enqueue (newTrack);
	}

	void generateStraight()
	{
		GameObject newTrack = Instantiate(Resources.Load("StraightLine")) as GameObject;
		newTrack.transform.rotation = lastEnd.transform.rotation;
		newTrack.transform.position = lastEnd.transform.position - newTrack.GetComponent<Movement> ().start.transform.localPosition;
		lastEnd = newTrack.GetComponent<Movement> ().end;
		track.Enqueue (newTrack);
	}

	public GameObject getNextTrack()
	{
		return (GameObject)track.Dequeue ();
	}
}
