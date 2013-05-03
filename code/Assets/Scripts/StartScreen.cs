using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	
	public GameObject loader;
	public GameObject background;
	
	public GUIStyle playBtnStyle;
	public GUIStyle settingsBtnStyle;
	public AudioSource navSound;
	
	private Rect play;
	//private Rect settings;
	
	private Hashtable fadeIn;
	private Hashtable fadeOut;
	private Hashtable btnFade;
	
	private bool locked = false;
	private float alpha = 0F;
	
	// Use this for initialization
	void Start () {
		play = new Rect(0,420, 1280, 133);
		//settings = new Rect(0,560, 1280, 94);		
		
		fadeIn = new Hashtable();
		fadeIn.Add("alpha", 1);
		fadeIn.Add("time", 2);
		fadeIn.Add("delay", 0.1);
		fadeIn.Add("easeType", "easeOutBack");
		
		btnFade = new Hashtable();
		btnFade.Add("from", 0);
		btnFade.Add("to", 1);
		btnFade.Add("time", 1);
		btnFade.Add("delay", 1);
		btnFade.Add("onupdate", "onBtnFadeHandler");
		btnFade.Add("onupdatetarget", gameObject);
		btnFade.Add("easeType", "easeOutBack");
		
		fadeOut = new Hashtable();
		fadeOut.Add("alpha", 0);
		fadeOut.Add("time", 2);
		fadeOut.Add("easeType", "easeOutBack");
		
		iTween.ColorTo(background, new Color(0.5F, 0.5F, 0.5F, 0), 0);
		iTween.ColorTo(loader, new Color(0.5F, 0.5F, 0.5F, 0), 0);
		iTween.FadeTo(background, fadeIn);	
		iTween.ValueTo(gameObject, btnFade);
	}
		
	void OnGUI () {		
		GUI.color = new Color(1, 1, 1, alpha);
		if(GUI.Button(play, "", playBtnStyle)) {
			if(!locked) {
				//show loading screen, then load level				
				locked = true;
				
				btnFade.Remove("from");
				btnFade.Remove("to");
				btnFade.Add("from", 1);
				btnFade.Add("to", 0);
				btnFade.Add("oncomplete", "onBtnFadeCompleteHandler");
				btnFade.Add("oncompletetarget", gameObject);
				
				navSound.Play();
				iTween.ValueTo(gameObject, btnFade);
			}
		}		
		GUI.color = new Color(0.5F, 0.5F, 0.5F, 1);		
	}
	
	//------[Event Handlers]---------------------------
	
	void onBtnFadeHandler(float newAlpha) {	
		alpha = newAlpha;
	}
	
	void onBtnFadeCompleteHandler() {
		btnFade.Remove("oncomplete");
		btnFade.Remove("oncompletetarget");
		
		fadeIn.Remove("delay");
		fadeIn.Add("oncompletetarget", gameObject);
		fadeIn.Add("oncomplete", "onFadeInHandler");
		iTween.FadeTo(loader, fadeIn);
	}	
	
	void onFadeInHandler() {
		fadeIn.Remove("oncompletetarget");
		fadeIn.Remove("oncomplete");
		Application.LoadLevel(1);
	}
	
}
