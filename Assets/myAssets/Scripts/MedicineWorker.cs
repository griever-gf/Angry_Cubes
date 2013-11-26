using UnityEngine;
using System.Collections;

public class MedicineWorker : MonoBehaviour {

	void OnCollisionEnter(Collision c)
	{
		//Debug.Log("collision " + c.collider.name);
		//if (networkView.isMine)
		{
			if (c.collider.name.Contains("prefubPlayer"))
			{
				//Debug.Log("DESTROYER:" + c.collider.transform.gameObject.ToString());
				
				//HealthCounter  hc = c.collider.transform.gameObject.GetComponent<HealthCounter>();
				//hc.IncHealth(50);
				
				Network.Destroy(gameObject);
			}
		}
	}
}
