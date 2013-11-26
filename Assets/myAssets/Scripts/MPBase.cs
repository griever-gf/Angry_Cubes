using UnityEngine;
using System.Collections;
using System;

public class MPBase : MonoBehaviour {

	public string connectToIP = "127.0.0.1";
	public int connectPort = 5300;
	//public bool useNAT = false;
	public string ipAddress = "";
	public string port = "";
	
	string playerName = "<NAME ME>";
	
	void OnGUI()
	{	
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			if (GUILayout.Button("Connect"))
			{
				//if (playerName != "<NAME ME>")
				{
					NetworkConnectionError ne = Network.Connect(connectToIP, connectPort);
					PlayerPrefs.SetString("playerName", playerName);
				}
			}
			if (GUILayout.Button("Start Server"))
			{
				//if (playerName != "<NAME ME>")
				{
					Network.InitializeServer(1, connectPort, false);

					foreach(GameObject go in FindObjectsOfType(typeof(GameObject)))
					{
						go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
					}
					PlayerPrefs.SetString("playerName", playerName);
					//Debug.Log("Init network");
				}
			}
			playerName = GUILayout.TextField(playerName);
			connectToIP = GUILayout.TextField(connectToIP);
			connectPort = Convert.ToInt32(GUILayout.TextField(connectPort.ToString()));
		}
		else
		{
			if (Network.peerType == NetworkPeerType.Connecting)
				GUILayout.Label("Connect Status: Connecting");
			else if (Network.peerType == NetworkPeerType.Client) {
				GUILayout.Label("Connection Status: Client");
				GUILayout.Label("Ping to server: " + Network.GetAveragePing(Network.connections[0]));
			} else if (Network.peerType == NetworkPeerType.Server){
				GUILayout.Label("Connection Status: Server");
				GUILayout.Label("Connections: " + Network.connections.Length);
			}
			
			if (GUILayout.Button("Disconnect"))
				Network.Disconnect(200);
			ipAddress = Network.player.ipAddress;
			port = Network.player.port.ToString();
			GUILayout.Label("IP address: " + ipAddress + ":" + port);
		}
	}
	
	void OnConnectedToServer()
	{
		foreach(GameObject go in FindObjectsOfType(typeof(GameObject)))
		{
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
	}
}
