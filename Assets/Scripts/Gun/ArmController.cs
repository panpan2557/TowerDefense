using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour {

	public GameObject Camera;

	float oldAngle;
	float newAngle;

	void Awake() {
		oldAngle = Camera.transform.eulerAngles.x;
	}

	void Update () {
		newAngle = Camera.transform.eulerAngles.x;
		transform.Rotate(new Vector3(newAngle - oldAngle, 0, 0));
		oldAngle = newAngle;
	}
}
