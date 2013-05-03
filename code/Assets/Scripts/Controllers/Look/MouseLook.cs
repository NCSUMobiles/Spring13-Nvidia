using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Look/Mouse Look")]
public class MouseLook : MonoBehaviour {
	
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumY = -60F;
	public float maximumY = 60F;
	
	private float rotationY = 0F;
		
	void Update () {
		
		if(Input.GetKeyDown("escape")){
			Application.LoadLevel(0);
		}
		
		float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;			
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);	
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);		
	}
}
