using UnityEngine;
using System.Collections;

public class GameCounter : MonoBehaviour {

	public int CounterMax = 3;
	private int ServerCounter;
	private int ClientCounter;
	private GUIStyle style;
	
	public string EndgameScreenName;
	public bool GameResult;
		
	void Start()
	{
		DontDestroyOnLoad(this);
		ResetCounters();
		style = new GUIStyle();
		style.fontSize = 30;
	}
	
	public void ResetCounters(){
		ServerCounter = ClientCounter = 0;
	}
	
	void OnGUI()
	{
		if (Network.peerType != NetworkPeerType.Disconnected) {
			style.normal.textColor = Color.red;
			GUI.Label(new Rect( Screen.width/4, 20, 200, 50), "SERVER: " + ServerCounter.ToString(), style);
			style.normal.textColor = Color.blue;
			GUI.Label(new Rect( Screen.width*3/4, 20, 200, 50), "CLIENT: " + ClientCounter.ToString(), style);
		}
	}
	
	[RPC]
	public void IncServer()
	{
		ServerCounter++;
		if (ServerCounter >= CounterMax)
		{
			GameResult = (Network.peerType == NetworkPeerType.Server);
			Application.LoadLevel(EndgameScreenName);
		}
	}
	
	[RPC]
	public void IncClient()
	{
		ClientCounter++;
		if (ClientCounter >= CounterMax)
		{
			GameResult = (Network.peerType == NetworkPeerType.Client);
			Application.LoadLevel(EndgameScreenName);
		}
	}
}
