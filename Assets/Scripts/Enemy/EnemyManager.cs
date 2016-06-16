using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public PlayerHealth playerHealth;       // Reference to the player's heatlh.
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public int spawnTimeReductionMultiplier = 10;
	public GameObject plane;
	public float spawnBoundaryOffset = 1f;

	Vector3 boundary;

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		boundary = plane.GetComponent<MeshRenderer> ().bounds.size / 2;
	}


	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f) {
			return;
		}

		float randomX = Random.Range (-boundary.x + spawnBoundaryOffset, boundary.x - spawnBoundaryOffset); 
//		float randomY = Random.Range (-boundary.y, boundary.y);
		float randomY = Random.Range (0.1f, 1f); //fixed because of flat plane
		float randomZ = Random.Range (-boundary.z + spawnBoundaryOffset, boundary.z - spawnBoundaryOffset);

		Vector3 spawnPoint = new Vector3(randomX, randomY, randomZ);
		print ("spawn: " + spawnPoint);

		Instantiate (enemy, spawnPoint, plane.transform.rotation);

		spawnTime -= 1/spawnTimeReductionMultiplier;
	}

	Vector3 RandomPointOnPlane(Vector3 position, Vector3 normal, float radius)
	{
		Vector3 randomPoint;

		do
		{
			randomPoint = Vector3.Cross(Random.insideUnitSphere, normal);
		} while (randomPoint == Vector3.zero);

		randomPoint.Normalize();
		randomPoint *= radius;
		randomPoint += position;

		return randomPoint;
	}
}

