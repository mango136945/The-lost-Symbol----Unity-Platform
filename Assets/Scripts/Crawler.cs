using UnityEngine;
using System.Collections;

public class Crawler : MonoBehaviour ,IEnemyObserver{


	Transform player;
	Animator animator ;
	int DeadHash = Animator.StringToHash("Death");
	int EnableHash = Animator.StringToHash("Enabled");
	int AttackHash = Animator.StringToHash("Attack");
	NavMeshAgent navAgent;
	bool dead;
	
	int damage;
	int life;

	void Start () 
	{
		animator = GetComponent<Animator> ();
		dead = false;
		navAgent.enabled = false;
		PlayerControl.registerObserver (this);
		this.damage = 2;
		this.life = 20;
	}
	
	void Awake()
	{
		player=GameObject.FindGameObjectWithTag("Player").transform;
		navAgent = GetComponent<NavMeshAgent> ();
	}
	
	public void updateDrone(bool activationStatus)
	{
		navAgent.enabled = activationStatus;
		if(navAgent.enabled==true)
		{
			animator.SetTrigger(EnableHash);
		}
	}
		public void setLife (int damage)
	{
		this.life = this.life - damage;
	}

	void Update () 
	{
		
		if(dead==false && navAgent.enabled==true)
			navAgent.SetDestination (player.position);
		else if(dead==true)
		{
			GetComponent <NavMeshAgent> ().enabled = false;
			GetComponent <Rigidbody> ().isKinematic = true;
			Destroy (gameObject, 2);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.tag == "Player") 
		{
			//Attack Player
			animator.SetTrigger(AttackHash);
			PlayerControl.setLife(this.damage);
		}
		if (other.gameObject.tag == "Bullet") 
		{
			//Bullet damage is 10 units
			setLife(10);
			if(this.life<=0)
			{
				animator.SetTrigger(DeadHash);
				dead=true;
				PlayerControl.unregisterObserver(this);
			}

		}
		
	}
}

