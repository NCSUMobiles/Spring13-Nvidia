using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Look/Touch Look")]
public class TouchLook : MonoBehaviour {
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumY = -60F;
	public float maximumY = 60F;
	
	public float padding = 10F;
	
	void Update ()	{
		
		if(Input.GetKeyDown("escape")){
			Application.LoadLevel(0);
		}
		
		if(Input.touchCount >= 1) {			
			//get touch deltas
			Vector2 delta = Input.touches[0].deltaPosition;				
			if(delta.x == 0 && delta.y == 0 && Input.touchCount == 2) delta = Input.touches[1].deltaPosition;
			
			//the 'x' variable is left/right, around the up (Y) axis
			float x = -delta.y * (padding / 5F);
			float y = -delta.x * (padding / 5F);			
					
			Vector3 rotations = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
			
			//rotate around the right axis (X)	
			float total = (transform.localEulerAngles.x + x);				
			if(total <= 60F) {					
				rotations.x = total;					
			}else if(total > 60F && total < (60F + padding)){ 
				rotations.x = 60F;
			}else if(total >= 300F) {	
				rotations.x = total;					
			}else if(total < 300F && total > (300F - padding)){ 
				rotations.x = 300F;
			}				
			
			//rotate around the up axis (Y)	
			rotations.y = (transform.localEulerAngles.y - y);				
			transform.localEulerAngles = rotations;
		}
	}	

}