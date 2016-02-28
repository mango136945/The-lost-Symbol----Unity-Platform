using UnityEngine;
using System.Collections;

public class DroneFactory :  IEnemyFactory
{

		// Use this for initialization
	public void createEnemies(GameObject drone,int numberOfDrones)
	{
		int i;
		for(i=1; i<=numberOfDrones; ++i)
		{
			Vector3 newPosition=new Vector3(Random.Range(-10.0F, 10.0F), 2.0F, Random.Range(-10.0F, 10.0F));
			GameObject cloneDrone = MonoBehaviour.Instantiate(drone.rigidbody, newPosition, Quaternion.identity ) as GameObject;
		}
	}
}

