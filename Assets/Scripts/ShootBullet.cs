using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {

	public Rigidbody projectile;
	public float speed;

	// Use this for initialization
	void Start () {

		
	}


	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.B))  //B for Bullet
		{
			Rigidbody clone;
			clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
			clone.velocity = transform.TransformDirection(Vector3.forward * speed);
			Destroy(clone.gameObject,2);
		}
	}
}
