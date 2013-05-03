using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterMotor))]
[AddComponentMenu("Camera-Movement/Dpad Move")]
public class DpadMove : MonoBehaviour {
	
	public float sensitivityX = 0.05F;
	public float sensitivityY = 0.05F;	
	public Vector2 controlPoint = new Vector2(225, 175);
	public float deadzone = 50F;
	public float radius = 200F;	
	
	public AudioSource walking;
	
	private CharacterMotor motor;
	
	void Start () {
		motor = GetComponent<CharacterMotor> ();
		if(walking.isPlaying) walking.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 directionVector = Vector3.zero;
		
		if(Input.touchCount >= 1) {
			
			for (int i = 0; i < Input.touchCount; i++) {
				Touch cur = Input.touches [i];			
				
				if(this.isNearLeftDPad(cur.position)) {
					float deltaX = cur.position.x - controlPoint.x;
					float deltaY = cur.position.y - controlPoint.y;
					
					float distance = Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2)) - deadzone; //0-150			
					distance /= 150F; 							//normalize
					distance = Mathf.Clamp(distance, 0, 1);		//clamp		
					
					float x = deltaX * Mathf.Sin(distance * Mathf.PI/2) * sensitivityX;
					float y = deltaY * Mathf.Sin(distance * Mathf.PI/2) * sensitivityY;
					
					directionVector = new Vector3(x, 0, y);					
					break;
				}
			}			
					
		}		
		
		// Get the input vector from keyboard or analog stick		
		if(directionVector != Vector3.zero) {			
			if(!walking.isPlaying)walking.Play();
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min (1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
		}else {
			if(walking.isPlaying) walking.Stop();		
		}		
		
		// Apply the direction to the CharacterMotor
		motor.inputMoveDirection = transform.rotation * directionVector;
		motor.inputJump = Input.GetButton ("Jump");	
	}
	
	bool isNearLeftDPad (Vector3 touch) {
		float distance = Mathf.Sqrt(Mathf.Pow((touch.x - controlPoint.x), 2) + Mathf.Pow((touch.y - controlPoint.y), 2));		
		return (deadzone < distance && distance < radius);
	}
}
