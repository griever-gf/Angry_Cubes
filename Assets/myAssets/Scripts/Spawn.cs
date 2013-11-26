using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject player1;
	
	public GameObject player2;
	
	void OnServerInitialized()
	{
		SpawnPlayer(false);
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer(true);
	}
	
	public void SpawnPlayer(bool isEnemy)
	{
		GameObject instance;
		if (!isEnemy)
			instance = Network.Instantiate(player1, transform.position, transform.rotation, 0) as GameObject;
		else
			instance = Network.Instantiate(player2, transform.position+new Vector3(2, 0, 2), transform.rotation, 0) as GameObject;
	
		CameraMovement CamScript = Camera.main.GetComponent<CameraMovement>();
		CamScript.player = instance.transform;
		
		GameObject go = GameObject.Find("Sticks");
		StickController sc = go.GetComponent<StickController>();
		sc.player = instance;
	}
	
	void OnNetworkDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Network.RemoveRPCs(Network.player);
		Network.DestroyPlayerObjects(Network.player);
		Application.LoadLevel(Application.loadedLevel);
	}
}
