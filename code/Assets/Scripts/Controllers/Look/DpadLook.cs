using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Look/Dpad Look")]
public class DpadLook : MonoBehaviour {
	
	public float sensitivityX = 3.5F;
	public float sensitivityY = 3.5F;

	public float minimumY = -60F;
	public float maximumY = 60F;	
	public float padding = 10F;	
	
	//configurables
	public Vector2 controlPoint = new Vector2 (1066, 200);
	public float deadzone = 50F;
	public float radius = 200F;	
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("escape")){
			Application.LoadLevel(0);
		}
		
		if (Input.touchCount >= 1) {
			
			for (int i = 0; i < Input.touchCount; i++) {
				Touch cur = Input.touches[i];

				if (this.isNearRightDPad(cur.position)) {	
					//get touch deltas
					float theta = Mathf.Abs(Mathf.Atan((cur.position.y - controlPoint.y)/(cur.position.x - controlPoint.x)));
					float distance = Mathf.Sqrt(Mathf.Pow((cur.position.x - controlPoint.x), 2) + Mathf.Pow((cur.position.y - controlPoint.y), 2)) - deadzone;
					
					Vector2 delta = new Vector2((Mathf.Cos(theta) * distance), (Mathf.Sin(theta) * distance));
					
					float x = delta.y * (padding / 300F) * Mathf.Sign((controlPoint.y - cur.position.y));	// up, down					
					float y = delta.x * (padding / 300F) * Mathf.Sign((controlPoint.x - cur.position.x));	// left, right			
					
					x *= sensitivityX;
					y *= sensitivityY;
					
					Vector3 rotations = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
					
					//rotate around the right axis (X)
					float total = (transform.localEulerAngles.x + x);				
					if (total <= 60F) {					
						rotations.x = total;					
					} else if (total > 60F && total < (60F + padding)) { 
						rotations.x = 60F;
					} else if (total >= 300F) {	
						rotations.x = total;					
					} else if (total < 300F && total > (300F - padding)) { 
						rotations.x = 300F;
					}				
			
					//rotate around the up axis (Y)	
					rotations.y = (transform.localEulerAngles.y - y);				
					transform.localEulerAngles = rotations;
					
					return;
				}
			}
		}
	}	
	
	bool isNearRightDPad(Vector2 touch) {		
		float distance = Mathf.Sqrt(Mathf.Pow((touch.x - controlPoint.x), 2) + Mathf.Pow((touch.y - controlPoint.y), 2));
		return (deadzone < distance && distance < radius);
	}

}
