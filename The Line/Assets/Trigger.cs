using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trigger : MonoBehaviour {

	ArrayList colliders = new ArrayList();
	public float failTime;
	float remainingFailTime;
	
	public GameObject fadeToBlack;

	void Start(){
		remainingFailTime = failTime;
	}

	void OnTriggerStay2D(Collider2D other) {
		colliders.Add (other);
	}



	void FixedUpdate(){
		if (colliders.Count == 0) {
			remainingFailTime-= Time.fixedDeltaTime;
		} else {
			remainingFailTime = Mathf.Clamp(remainingFailTime+Time.fixedDeltaTime,0,failTime);
		}
	
		if (remainingFailTime <= 0) {
			Application.LoadLevel(0);
		}

		fadeToBlack.GetComponent<Image> ().color = new Color (0, 0, 0, 1-(remainingFailTime / failTime));
		colliders.Clear ();
	}
}
