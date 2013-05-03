using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterMotor))]
[AddComponentMenu("Camera-Movement/Tilt Move")]
public class TiltMove : MonoBehaviour {
	
	public float moveSpeed = 4.75F;
	public float tiltOffset = 0.55F;	
	public AudioSource walking;	
	
	private CharacterMotor motor;
	
	// Use this for initialization
	void Start () {
		motor = GetComponent<CharacterMotor> ();
		if(walking.isPlaying) walking.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 directionVector = new Vector3((Input.acceleration.x * moveSpeed), 0, ((Input.acceleration.y + tiltOffset) * moveSpeed));		
		if (directionVector != Vector3.zero) {		
			if(!walking.isPlaying)walking.Play();
		
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			var directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
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
		motor.inputJump = Input.GetButton("Jump");
	}
}
