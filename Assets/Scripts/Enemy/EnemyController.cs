using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int maxHP = 20;
	public int hp;
	public float RotationSpeed = 3f;
	public float MoveSpeed = 3f;
	public int StopDistance = 2;
	public float Range1 = 10f;
	public float Range2 = 10f;
	public int damage = 10;
	public int KnockbackMaxTime = 60;
	public float KnockbackSpeed = 4f;
	public Color NormalColor = Color.white;
	public Color BlinkColor = Color.red;

	GameObject player;
	bool isDead;
	bool isAttacking;
	bool isDamaged;
	int knockbackTime;
	Transform target;
	float distance;

	void Awake () {
		hp = maxHP;
		isDead = false;
		isAttacking = false;
		isDamaged = false;
		knockbackTime = 0;
		player = GameObject.FindWithTag ("Player");
		target = player.transform;
	}

	void Update () {
//		print ("hp: " + hp);
		if (!isDead) {
			if (player != null)
				chase ();
			else
				print ("Player is null");
		} 
		else {
			Destroy (gameObject, 2f);
		}
	}
		
	void chase() {
		distance = Vector3.Distance(transform.position, target.position);

		//Original version: do not follow case
//		if (distance <= Range2 && distance >= Range1){
//			print ("1: not in range");
//			rotate ();
//		}

		if(distance > StopDistance && !isAttacking) {
			//move towards the player
			rotate();
			walk();
		}
		else {
			//2: attack the player
			rotate();
			//deduct player's HP
			if (!isAttacking) {
				isAttacking = true;
				PlayerHealth PlayerHealth = player.GetComponent<PlayerHealth> ();
				PlayerHealth.TakeDamage (damage);
				knockback ();
			} else {
				//knockback process
				if (knockbackTime < KnockbackMaxTime) {
//					print ("knocking back " + knockbackTime);
					knockback ();
				} else {
					//reset knockback time value and isAttacking
					knockbackTime = 0;
					isAttacking = false;
				}
			}
		}
	}

	public void TakeDamage(int amount) {
		hp -= amount;
		blink ();
		if (hp < 0 && !isDead)
			isDead = true;
	}

	void rotate() {
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.LookRotation(target.position - transform.position), RotationSpeed*Time.deltaTime);
	}

	void walk() {
		transform.position += transform.forward * MoveSpeed * Time.deltaTime;
	}

	void knockback() {
		transform.position += -transform.forward * KnockbackSpeed * Time.deltaTime;
		knockbackTime++;
	}

	IEnumerator blink() {
		GetComponent<MeshRenderer>().material.color = BlinkColor;
		yield return new WaitForSeconds(0.5f);
		GetComponent<MeshRenderer>().material.color = NormalColor;
	}
}
