using UnityEngine;
using System.Collections;

public class CrawlerFactory :  IEnemyFactory
{

		// Use this for initialization
		public void createEnemies(GameObject crawler,int numberOfCrawlers)
		{
			int i;
			for(i=1; i<=numberOfCrawlers; ++i)
			{
				Vector3 newPosition=new Vector3(Random.Range(-10.0F, 10.0F), 0.0F, Random.Range(-10.0F, 10.0F));
				GameObject cloneCrawler = MonoBehaviour.Instantiate(crawler.rigidbody, newPosition, Quaternion.identity ) as GameObject;
			}
		}

}