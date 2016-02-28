using UnityEngine;
using System.Collections;

public class MainClass : MonoBehaviour 
{
	public GameObject drone;
	public GameObject crawler;
	public int numberOfDrones;
	public int numberOfCrawlers;
	IEnemyFactory enemyFactory;

	void Start()
	{
		enemyFactory = new DroneFactory ();
		enemyFactory.createEnemies (drone,numberOfDrones);
		enemyFactory = new CrawlerFactory ();
		enemyFactory.createEnemies (crawler,numberOfCrawlers);

	}


}

