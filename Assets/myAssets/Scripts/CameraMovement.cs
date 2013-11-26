/*using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public GameObject player;
	
	public float maximumDistance = 2;
	//The velocity of your player, used to determine que speed of the camera
	public float playerVelocity = 10;
	
	// Update is called once per frame
	void Update () {
		float horiz = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		
		
		if ((horiz!=0)||(vert!=0))
		{
			Debug.Log(horiz.ToString("0.000")+","+vert.ToString("0.000"));
			Vector3 vectorMovement = new Vector3(horiz, vert, 0); //screen
			vectorMovement = Camera.main.transform.rotation * vectorMovement;
			Debug.Log(vectorMovement.x.ToString()+" "+ vectorMovement.y.ToString()+" "+ vectorMovement.z.ToString());
			var camToCharacterSpace = Quaternion.FromToRotation(-Camera.main.transform.forward, transform.up);
			vectorMovement  = (camToCharacterSpace * vectorMovement );
			transform.Translate(vectorMovement*Time.deltaTime*10);
			
	        //float movementX = ((player.transform.position.x + 0 - this.transform.position.x))/maximumDistance;
        	//float movementZ = ((player.transform.position.z + 0 - this.transform.position.z))/maximumDistance;
        	//this.transform.position += new Vector3((movementX * playerVelocity * Time.deltaTime), 0, (movementZ * playerVelocity * Time.deltaTime));
		}
	}
}
*/
 
using UnityEngine;

using System.Collections;

public class CameraMovement : MonoBehaviour

{

    public Transform player;

    public float offsetX;

    public float offsetZ;

    

    public float maxDistance = 12f;

    public float playerVelocity = 10f;

    

    private float movementX;

    private float movementZ;

    

    void LateUpdate()

    {
		if (player != null)
		{
        	movementX = ((player.transform.position.x + offsetX - this.transform.position.x)) / maxDistance;
        	movementZ = ((player.transform.position.z + offsetZ - this.transform.position.z)) / maxDistance;
        	//this.transform.position += new Vector3((movementX * playerVelocity * Time.deltaTime), 0, (movementZ * playerVelocity * Time.deltaTime));
			this.transform.position += new Vector3((movementX * playerVelocity), 0, (movementZ * playerVelocity));
		}

    }

}