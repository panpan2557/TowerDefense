using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public int Damage;
	public float Range = 100f;

	bool isHitting;
//	int shootableMask;

	// Use this for initialization
	void Awake () {
		isHitting = false;
//		shootableMask = LayerMask.GetMask ("Shootable");
	}

	void FixedUpdate () {
		//if hit with the enemy
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Range)) {
//			print ("hit " + hit.transform.gameObject.name);
			if (hit.transform.tag == "Enemy" && !isHitting) {
				isHitting = true;
				print ("hit enemy!");
				EnemyController e = hit.collider.GetComponent<EnemyController> ();
				e.TakeDamage (Damage);
			}
			Destroy (gameObject, 1.5f);
		}
	}
}
