using UnityEngine;
using System.Collections;
using System;

public class StickController : MonoBehaviour {

	public Texture2D MainCircleTexture;
	public Texture2D LeftCircleTexture;
	public Texture2D RightCircleTexture;
	
	public GameObject player;
	
	public int ShiftHorizontal = 50;
	public int ShiftVertical = 50;
	public int Diameter = 200;

	void OnGUI () {
		
		Rect LeftStick = new Rect(ShiftHorizontal,							Screen.height-ShiftVertical-Diameter,Diameter,Diameter);
		Rect RightStick = new Rect(Screen.width - ShiftHorizontal-Diameter,	Screen.height-ShiftVertical-Diameter,Diameter,Diameter);

		if( AlphaButton( LeftStick, MainCircleTexture ) )
	    {
			float x = (Event.current.mousePosition.x-LeftStick.center.x)/(LeftStick.width/2);
			float y = (-1)*(Event.current.mousePosition.y-LeftStick.center.y)/(LeftStick.height/2);
			//Debug.Log(x.ToString()+","+ y.ToString());
			
			//GameObject go = GameObject.Find("prefubPlayer1(clone)");
			//Debug.Log(go);
			if (player != null) {
				MoveCharacter MCCharacter = player.GetComponent<MoveCharacter>();
				MCCharacter.MoveCharacterToDirection(x, y);
			}

			GUI.DrawTexture(new Rect(Event.current.mousePosition.x-Diameter/4, Event.current.mousePosition.y-Diameter/4, Diameter/2, Diameter/2), LeftCircleTexture);
	    }
		
		if( AlphaButton( RightStick, MainCircleTexture ) )
	    {
			float x = (Event.current.mousePosition.x-RightStick.center.x)/(RightStick.width/2);
			float y = (-1)*(Event.current.mousePosition.y-RightStick.center.y)/(RightStick.height/2);
			
			//GameObject go = GameObject.Find("prefubPlayer2(clone)");
			if (player != null) {
				RotateAndShoot RaSCharacter = player.GetComponent<RotateAndShoot>();
				RaSCharacter.RotateCharacterAndShoot(x, y);
			}

			GUI.DrawTexture(new Rect(Event.current.mousePosition.x-Diameter/4, Event.current.mousePosition.y-Diameter/4, Diameter/2, Diameter/2), RightCircleTexture);
	    }
	}
	
	public static bool AlphaButton( Rect area, Texture2D icon )
	{
    	return AlphaButton( area, icon, 0.5f );
	}
	
	public static bool AlphaButton( Rect area, Texture2D icon, float alphaThreshold )
	{
		GUI.DrawTexture( area, icon );
		if( Input.GetMouseButton(0) && CheckRect( Event.current.mousePosition, area ) )
    	{
        	Vector2 relativeClick = new Vector2( (Event.current.mousePosition.x - area.x)*(icon.width/area.width),
												(Event.current.mousePosition.y - area.y)*(icon.height/area.height));
        	if( icon.GetPixel( (int)relativeClick.x, (int)relativeClick.y ).a > alphaThreshold )
			{            	
            	return true;
        	}
    	}
    	return false;
	}
	
	public static bool CheckRect( Vector2 point, Rect rect )
	{
    	return  point.x >= rect.x && point.x <= rect.x + rect.width && point.y >= rect.y && point.y <= rect.y + rect.height;
	}
}
