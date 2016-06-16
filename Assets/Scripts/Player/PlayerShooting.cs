using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 1;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
	public float ShotForce = 10f;
	public GameObject Crosshair;
//	public GameObject Target;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
	Animator crosshairAnimator;
//	bool isFiring;


    void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Shootable");
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
//		isFiring = false;
		crosshairAnimator = Crosshair.GetComponent<Animator> ();
	}

    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetKeyDown ("f") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
			
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
		crosshairAnimator.SetTrigger ("isFiring");

        timer = 0f;
	
        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
            EnemyController enemyHealth = shootHit.collider.GetComponent <EnemyController> ();
            if(enemyHealth != null) {
//                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
				shootHit.rigidbody.AddForceAtPosition(Vector3.right * ShotForce, shootHit.point);
				enemyHealth.TakeDamage (damagePerShot);

            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }

    }
}
