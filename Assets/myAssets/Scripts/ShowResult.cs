using UnityEngine;
using System.Collections;

public class ShowResult : MonoBehaviour {
	
	private bool result;
	private GUIStyle style;
	
	public string StartLevelName;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("GameState");
		GameCounter gc = go.GetComponent<GameCounter>();
		result = gc.GameResult;
		//Debug.Log(go);
		style = new GUIStyle();
		style.fontSize = 60;
		style.normal.textColor = Color.white;
	}
	
	void OnGUI()
	{
		if (result)
			GUI.Label(new Rect( Screen.width/2-150, Screen.height/2-50, 300, 100), "YOU WIN", style);
		else
			GUI.Label(new Rect( Screen.width/2-150, Screen.height/2-50, 300, 100), "YOU LOSE", style);
	}
	
	void Update () {
		if (Input.anyKeyDown)
		{
			GameObject go = GameObject.Find("GameState");
			GameCounter gc = go.GetComponent<GameCounter>();
			gc.ResetCounters();
			//Application.LoadLevel(StartLevelName);
			Application.Quit();
		}
	}
}
