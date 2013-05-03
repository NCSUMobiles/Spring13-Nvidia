using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
	
	private static int preset = 0;
	private static int maxPreset = 2;	
	
	public static void SetControlPreset(int i) { preset = i; }
	public static void IncrementControlPreset() {
		preset++;
		if(preset > maxPreset) preset = 1;
	}
	
	public static int GetControlPreset() { return preset; }
	
	public static void EnableControls(GameObject g) {
		if(preset == 0) EnableMouseAndKeyboard(g);
		else if(preset == 1) EnableTiltAndDrag(g);
		else if(preset == 2) EnableTwinSticks(g);
		
		g.GetComponent<CharacterMotor>().enabled = true;
	}
	
	public static void DisableControls(GameObject g) {
		if(preset == 0) DisableMouseAndKeyboard(g);
		else if(preset == 1) DisableTiltAndDrag(g);
		else if(preset == 2) DisableTwinSticks(g);
		
		g.GetComponent<CharacterMotor>().enabled = false;
	}
	
	public static void DisableAllControls(GameObject g) {
		DisableMouseAndKeyboard(g);
		DisableTiltAndDrag(g);
		DisableTwinSticks(g);
	}
	
	//---[Mouse and Keyboard]----------------------------------
	public static void EnableMouseAndKeyboard(GameObject g) {
		g.GetComponent<MouseLook>().enabled = true;
		g.GetComponent<KeyMove>().enabled = true;		
	}
	
	public static void DisableMouseAndKeyboard(GameObject g) {
		g.GetComponent<MouseLook>().enabled = false;
		g.GetComponent<KeyMove>().enabled = false;
	}
	
	//---[Tilt and Drag]---------------------------------------
	public static void EnableTiltAndDrag(GameObject g) {
		g.GetComponent<TouchLook>().enabled = true;
		g.GetComponent<TiltMove>().enabled = true;
	}
	
	public static void DisableTiltAndDrag(GameObject g) {
		g.GetComponent<TouchLook>().enabled = false;
		g.GetComponent<TiltMove>().enabled = false;
	}
	
	//---[Twin Sticks]---------------------------------------
	public static void EnableTwinSticks(GameObject g) {
		g.GetComponent<DpadLook>().enabled = true;
		g.GetComponent<DpadMove>().enabled = true;
	}
	
	public static void DisableTwinSticks(GameObject g) {
		g.GetComponent<DpadLook>().enabled = false;
		g.GetComponent<DpadMove>().enabled = false;
	}

}
