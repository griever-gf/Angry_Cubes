using UnityEngine;
using System.Collections;

public class HealthCounter : MonoBehaviour {
	
	public static int Health;
	private const int StartHealth = 100;
	private GUIStyle style;
	
	public AudioClip soundExplosion;
	public AudioClip soundPickup;
	public GameObject prefabPickup;
	
	private float lastTimeCollided = 0;
	private float lastTimeDied = 0;
	
	void Start()
	{
		style = new GUIStyle();
		style.fontSize = 30;
		style.normal.textColor = Color.white;
		Health = StartHealth;
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect( Screen.width/2, 20, 200, 50), "HEALTH: " + Health.ToString(), style);
	}
	
	void Update()
	{
		if (rigidbody.velocity.y < -5.0f) //check for falling
		{
			//Debug.Log("velocity " +rigidbody.velocity.y.ToString());
			if (Time.time > lastTimeDied +1.0f)
			{
				DestroyAndRespawn(false);
			}
		}
	}
	
	void DestroyAndRespawn(bool leftMedicine)
	{
			if (Time.time > lastTimeDied +1.0f)
			{
				AudioSource.PlayClipAtPoint(soundExplosion, transform.position);
				if (leftMedicine)
					Network.Instantiate(prefabPickup, transform.position, transform.rotation, 0);
				Network.Destroy(gameObject);
				GameObject go = GameObject.Find("GameState");
				GameCounter gc = go.GetComponent<GameCounter>();
				if (Network.peerType == NetworkPeerType.Client)
					gc.networkView.RPC("IncServer", RPCMode.All);
				if (Network.peerType == NetworkPeerType.Server)
					gc.networkView.RPC("IncClient", RPCMode.All);
			
				go = GameObject.Find("SpawnPoint");
				Spawn sp = go.GetComponent<Spawn>();
				//WaitTillRespawn();
				sp.SpawnPlayer((Network.peerType == NetworkPeerType.Client));
				lastTimeDied = Time.time;
		}
	}
	
	void OnCollisionEnter(Collision c)
	{
		if (networkView.isMine)
		{
			//Debug.Log("collision" + c.collider.name);
			if (c.collider.name.Contains("prefubBullet"))
			{
				Health -= 15;
				if (Health <= 0)
				{
					DestroyAndRespawn(true);
				}
			}
			if (c.collider.name.Contains("prefubMedicine"))
			{
				if (Time.time > lastTimeCollided +0.5f)
				{
					Health += 50;
					AudioSource.PlayClipAtPoint(soundPickup, transform.position);
					lastTimeCollided = Time.time;
				}
			}
		}

	}
	
	IEnumerator WaitTillRespawn()
	{
		yield return new WaitForSeconds(3);
	}
}
