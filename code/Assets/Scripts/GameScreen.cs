using UnityEngine;
using System.Collections;

[AddComponentMenu ("GameScene/Main")]
public class GameScreen : MonoBehaviour {
	
	public GUIText debug;
	
	public int controlPreset = 0;
	
	public GameObject TiltDragOverlay;	
	public GameObject TwinStickOverlay;
	public GameObject TwinStickUI;
	
	private Hashtable fadeIn;
	private Hashtable overlayFadeIn;
	private Hashtable overlayFadeOut;
	
	private bool showingOverlay = false;
	
	// Use this for initialization
	void Start () {	
		GameState.SetControlPreset(controlPreset);
		GameState.DisableAllControls(gameObject);
		
		fadeIn = new Hashtable();
		fadeIn.Add("time", 1.0F);
		fadeIn.Add("amount", 1.0F);
		fadeIn.Add("delay", 2.0F);
		fadeIn.Add("easeType", "linear");
		fadeIn.Add("oncomplete", "onCameraFadeUpCompleteHandler");
		fadeIn.Add("oncompletetarget", gameObject);
		
		overlayFadeIn = new Hashtable();
		overlayFadeIn.Add("time", 2F);
		overlayFadeIn.Add("alpha", 0.65F);
		overlayFadeIn.Add("delay", 1F);
		overlayFadeIn.Add("easeType", "easeOutBack");
		overlayFadeIn.Add("oncomplete", "OnOverlayFadeInCompleteHandler");
		overlayFadeIn.Add("oncompletetarget", gameObject);
		
		overlayFadeOut = new Hashtable();
		overlayFadeOut.Add("time", 2F);
		overlayFadeOut.Add("alpha", 0F);
		overlayFadeOut.Add("easeType", "easeOutBack");
		overlayFadeOut.Add("oncomplete", "onOverlayFadeOutCompleteHandler");
		overlayFadeOut.Add("oncompletetarget", gameObject);		

		iTween.ColorTo(TiltDragOverlay, new Color(0.5F, 0.5F, 0.5F, 0), 0);
		iTween.ColorTo(TwinStickOverlay, new Color(0.5F, 0.5F, 0.5F, 0), 0);
		iTween.ColorTo(TwinStickUI, new Color(1, 1, 1, 0), 0);
		
		iTween.CameraFadeAdd();
		iTween.CameraFadeFrom(fadeIn);
	}
	
	void Update() {
	
		if(showingOverlay && Input.touchCount > 0 && Input.touchCount < 4) {					
			if(controlPreset == 1) iTween.FadeTo(TiltDragOverlay, overlayFadeOut);			
			else if(controlPreset == 2) iTween.FadeTo(TwinStickOverlay, overlayFadeOut);	
		}else if(!showingOverlay && Input.touchCount == 4) {
			showingOverlay = true;
			iTween.ColorTo(TwinStickUI, new Color(1, 1, 1, 0), 1);
			
			//debug.text = "preset: " + controlPreset + "\n";
			
			GameState.DisableControls(gameObject);
			GameState.IncrementControlPreset();
			controlPreset = GameState.GetControlPreset();
			
			//debug.text += "after increment: " + controlPreset;
			
			if(controlPreset == 1) iTween.FadeTo(TiltDragOverlay, overlayFadeIn);
			else if(controlPreset == 2) iTween.FadeTo(TwinStickOverlay, overlayFadeIn);			
		}
		
	}
		
	void onCameraFadeUpCompleteHandler() {
		showingOverlay = true;		
		
		if(controlPreset == 0) GameState.EnableControls(gameObject);
		else if(controlPreset == 1) iTween.FadeTo(TiltDragOverlay, overlayFadeIn);
		else if(controlPreset == 2) iTween.FadeTo(TwinStickOverlay, overlayFadeIn);
	}
	
	void OnOverlayFadeInCompleteHandler() {
		showingOverlay = true;	
	}
	
	void onOverlayFadeOutCompleteHandler() {
		showingOverlay = false;	
		
		if(controlPreset == 2)  iTween.ColorTo(TwinStickUI, new Color(0.5F, 0.5F, 0.5F, 0.5F), 2);
		GameState.EnableControls(gameObject);
	}
	
	
}
