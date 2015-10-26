using UnityEngine;
using System.Collections;

public class Rotate_Player : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.rotation = Quaternion.Euler (0, 0, Input.gyro.attitude.eulerAngles.z);
	}
}
