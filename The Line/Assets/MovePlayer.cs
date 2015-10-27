using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour {

	ArrayList points = new ArrayList ();

	public GameObject generateTrackObj;
	GenerateTrack generateTrack;

	GameObject trackOn;

	public float speed;

	double score = 0;
	public GameObject scoreObject;

	int curve = 0;

	// Use this for initialization
	void Start () {
		generateTrack = generateTrackObj.GetComponent<GenerateTrack> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (trackOn == null || points.Count == 0) {
			getNextTrack();
		}


		float moveSpeed = Time.deltaTime * speed;

		int overCount = 100;
		while (moveSpeed > 0 && overCount > 0) {

			overCount--;

			if(curve == 0)
			{
				float distance = Vector3.Distance (this.transform.position, ((GameObject)points[0]).transform.position);

				this.transform.position = Vector3.MoveTowards (this.transform.position, ((GameObject)points [0]).transform.position, moveSpeed);
				this.transform.rotation = ((GameObject)points [0]).transform.rotation;

				moveSpeed -= distance;
				//Debug.Log(moveSpeed);

				if(moveSpeed>=0)
				{
					points.RemoveAt(0);
					if(points.Count<=0)
						getNextTrack();
				}
			}
			else {
				Vector3 center = ((GameObject)points [1]).transform.position;
				float radius = Vector3.Distance(center, ((GameObject)points [0]).transform.position);
				float moveAngle = moveSpeed  / radius;
				if(Vector3.Distance(this.transform.position, ((GameObject)points [2]).transform.position) < moveSpeed)
				{
					//move to the end, subtract distance, and gen new track
					this.transform.position = ((GameObject)points [2]).transform.position;

					points.RemoveAt(0);
					points.RemoveAt(0);
					points.RemoveAt(0);

					getNextTrack();

					moveSpeed -= Vector3.Distance(this.transform.position, ((GameObject)points [2]).transform.position);
				} else {
					if(curve == 1)
						this.transform.RotateAround(center,-this.transform.forward,Mathf.Rad2Deg * moveAngle);
					else
						this.transform.RotateAround(center,-this.transform.forward,Mathf.Rad2Deg * -moveAngle);
					moveSpeed = 0;
				}
			}

			

		}
		if (overCount == 0) {
			Application.Quit();
			Destroy(this.gameObject);
		}
	}

	void getNextTrack()
	{
		score++;
		scoreObject.GetComponent<Text> ().text = "Score " + score;
		if (score % 4 == 3) {
			speed+=.4f;
		}

		Destroy (trackOn);
		trackOn = generateTrack.getNextTrack();
		for(int i = 0; i < trackOn.GetComponent<Movement> ().movePoints.Length; i++)
			points.Add(trackOn.GetComponent<Movement> ().movePoints[i]);
		generateTrack.generateNewTrack ();
		curve = trackOn.GetComponent<Movement> ().curve;
	}
}
