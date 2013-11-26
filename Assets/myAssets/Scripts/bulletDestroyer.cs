using UnityEngine;
using System.Collections;

public class bulletDestroyer : MonoBehaviour {

	public float SecondToSelfDestroy = 5.0f;
	
	// Update is called once per frame
	void Update () {
		SecondToSelfDestroy -= Time.deltaTime;
		if (SecondToSelfDestroy <= 0)
			Destroy(gameObject);
	}
}
