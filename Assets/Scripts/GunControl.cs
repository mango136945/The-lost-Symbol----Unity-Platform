using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class GunControl : MonoBehaviour {

	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float timeBetweenBullets = 0.15f;        // The time between each shot.
	public float range ;                      // The distance the gun can fire.
	int score;
	public Text scoreText;
	public Text debug;
	public float torqueAmount;
	float timer;                                    // A timer to determine when to fire.
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	LineRenderer gunLine;                           // Reference to the line renderer.
	Light gunLight;                                 // Reference to the light component.
	float effectsDisplayTime = 0.2f;        


	// Use this for initialization

	void Start()
	{
		score = 0;
		scoreText.text = "Score:" + score.ToString ();
		debug.text = ""; 


	}

	void Awake ()
	{

		// Create a layer mask for the Shootable layer.
	
		gunLine = GetComponent <LineRenderer> ();
		gunLight = GetComponent<Light> ();
	}

	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
	
		// If the Fire1 button is being press and it's time to fire...
		if(Input.GetKeyDown(KeyCode.F) && timer >= timeBetweenBullets)
		{
			// ... shoot the gun.
			Shoot ();
		}
		
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			// ... disable the effects.
			DisableEffects ();
		}
	}
	
	public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
	
	void Shoot ()
	{
		// Reset the timer.
		timer = 0f;
		
		// Enable the light.
		gunLight.enabled = true;


		// Enable the line renderer and set it's first position to be the end of the gun.
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		
		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = transform.position;
 		
		//shootRay.direction = Vector3.up; //Shoots UP
		shootRay.direction = transform.TransformDirection (Vector3.up); // Shoots at Relative UP

		//Debug.DrawRay (transform.position, Vector3.up * 4);

		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (shootRay, out shootHit, range))//, shootableMask))
		{
			debug.text = "I hit !";

			if(shootHit.collider.gameObject.tag=="Drone")
			{
				score ++;
				scoreText.text = "Score:" + score.ToString ();
				debug.text = "I hit "+ shootHit.collider.gameObject.tag + " and Score!" ;
			}
			else
			{
				debug.text = "I hit "+ shootHit.collider.gameObject.tag + " but no score!";
			}
			// Set the second position of the line renderer to the point the raycast hit.
			gunLine.SetPosition (1, shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else
		{
			debug.text = "No hit!";
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}
