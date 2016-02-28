using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	public float speed;
	public static bool ActivateDrones;
	static ArrayList droneObserverList;
	static int life;
	public Text lifeText;


	public static void registerObserver(IEnemyObserver observer)
	{
		droneObserverList.Add (observer);
	}
	public static void unregisterObserver(IEnemyObserver observer)
	{
		droneObserverList.Remove(observer);
	}
	public static void notifyObservers()
	{
		foreach(IEnemyObserver observer in droneObserverList)
		{
			observer.updateDrone(ActivateDrones);
		}
	}

	void Start()
	{
		droneObserverList = new ArrayList ();
		ActivateDrones = false;
		notifyObservers();
		life = 100;
		lifeText.text = "Life : " + life.ToString ();

	}
	public static void setLife(int damage)
	{
		life = life - damage;
	}
	// Use this for initialization
	void FixedUpdate()
	{
		lifeText.text = "Life : " + life.ToString ();
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rigidbody.AddForce (movement * speed * Time.deltaTime);
	}
	void OnTriggerEnter(Collider other)
	{	
		if (other.gameObject.tag == "Cube") 
		{
			other.gameObject.SetActive(false);
			ActivateDrones=true;
			notifyObservers();
		}
	}
}
