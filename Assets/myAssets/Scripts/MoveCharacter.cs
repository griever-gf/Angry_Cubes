using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class MoveCharacter : MonoBehaviour {
	
	public float Speed = 10f;
	
	void Start()
	{
		if (!networkView.isMine)
			enabled = false;
	}
	
	void Update () {
		float horiz = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");

		MoveCharacterToDirection(horiz, vert);
	}
	
	public void MoveCharacterToDirection(float horiz, float vert)
	{		
		if (networkView.isMine)
		{
			if ((horiz!=0)||(vert!=0))
			{
				Vector3 movementVector = new Vector3(horiz, vert, 0); //screen
				movementVector = Camera.main.transform.rotation * movementVector;
				var camToCharacterSpace = Quaternion.FromToRotation(-Camera.main.transform.forward, transform.up);
				movementVector = (camToCharacterSpace * movementVector);
				transform.Translate(movementVector*Speed*Time.deltaTime, Space.World);
				//Debug.Log(horiz.ToString() + "," + vert.ToString());
				//Debug.Log(Speed);
			}
		}
	}
	
	public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            // Выполняется у владельца networkview; 
            // Сервер рассылает позицию по сети

            Vector3 pos = transform.position;
            stream.Serialize(ref pos);//"Кодирование" и рассылка

        }
        else
        {
            // Выполняется у всех остальных; 
            // Клиенты получают позицию и устанавливают ее

            Vector3 posReceive = Vector3.zero;
            stream.Serialize(ref posReceive); //"Декодирование" и прием
            transform.position = posReceive;

        }
    }
}
