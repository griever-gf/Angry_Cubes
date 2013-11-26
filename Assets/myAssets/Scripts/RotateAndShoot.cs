using UnityEngine;
using System.Collections;

public class RotateAndShoot : MonoBehaviour {
	
	public GameObject bulletPrefub;
	
	public int RotationSpeed = 5;
	
	public int BulletSpeed = 30;
	
	public float BulletDelay = 0.1f;
	
	private float lastShotTime = 0;
	
	public AudioClip soundShoot;
	
	// Update is called once per frame
	void Update () {
		float horizFire = Input.GetAxis("TurnAndFireHorizontal");
		float vertFire = Input.GetAxis("TurnAndFireVertical");
		//Debug.Log(horizFire.ToString("0.000")+","+vertFire.ToString("0.000"));
		RotateCharacterAndShoot(horizFire, vertFire);
	}
	
	public void RotateCharacterAndShoot(float horizFire, float vertFire)
	{
		if (networkView.isMine)
		{
			if ((horizFire!=0)||(vertFire!=0))
			{
				Vector3 rotationVector = new Vector3(horizFire, vertFire, 0); //screen
				rotationVector = Camera.main.transform.rotation * rotationVector;
				var camToCharacterSpace = Quaternion.FromToRotation(-Camera.main.transform.forward, transform.up);
				rotationVector = (camToCharacterSpace * rotationVector);
				//Debug.Log(rotationVector.x.ToString()+" "+ rotationVector.y.ToString()+" "+ rotationVector.z.ToString());
				Quaternion _lookRotation = Quaternion.LookRotation(rotationVector);
		  		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
				
				if (Time.time > lastShotTime + BulletDelay)
				{
					//GameObject bullet = Instantiate(bulletPrefub, transform.position+new Vector3(horizFire, 0, vertFire), transform.rotation) as GameObject;
					GameObject bullet = Network.Instantiate(bulletPrefub, transform.position, transform.rotation, 0) as GameObject;
					bullet.transform.eulerAngles = new Vector3 (bullet.transform.eulerAngles.x+90, bullet.transform.eulerAngles.y, bullet.transform.eulerAngles.z);
					//bullet.rigidbody.AddForce(transform.up*Time.deltaTime, ForceMode.Acceleration);
					Physics.IgnoreCollision(gameObject.collider, bullet.collider);
					//bullet.rigidbody.AddForce(transform.forward*Time.deltaTime*BulletSpeed, ForceMode.VelocityChange);
					bullet.rigidbody.AddForce(transform.forward*BulletSpeed, ForceMode.VelocityChange);
					//bullet.audio.clip = soundShoot;
					bullet.audio.Play();
					lastShotTime = Time.time;
					//Debug.Log(Time.deltaTime);
				}
			}
		}
	}
}

