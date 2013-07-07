using UnityEngine;
using System.Collections;


public class bar : MonoBehaviour {
	
	public int maxBar = 20;
	public int curBar = 20;
 
	public Texture2D barBg;
	public float barLength;
	
	public PlayerPhysics playerPhysics;
	
	// Use this for initialization
	void Start () 
	{
		barLength = Screen.width/4;
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(Input.GetKeyDown(KeyCode.A))
		{
			AddjustCurrentHunger(1);
		}
		if(Input.GetKeyDown(KeyCode.B))
		{
			//AddjustCurrentHunger(4);
		}
		if(Input.GetMouseButton(1))
		{
			//AddjustCurrentHunger(1);		
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			AddjustCurrentBar(19);
		}
		
	}
	
	void OnGUI () 
	{
		// Draw the background image
		GUI.DrawTexture( new Rect(20,20, barLength,30), barBg, ScaleMode.StretchToFill); 
	}
	 
	public void AddjustCurrentHunger(int adj)
	{ 
		curBar += adj;
		
		if(curBar <0)
		{
			curBar = 0;	
		}
		
		if(curBar > maxBar)
		{
			curBar = maxBar;	
		}
		
		if(maxBar <1)
		{
			maxBar = 1;
		}
		
		barLength = (Screen.width /4)* (curBar / (float)maxBar);
	}
	
	public void AddjustCurrentBar(int adj)
	{ 
		curBar -= adj;
		
		if(curBar <0)
		{
			curBar = 0;	
		}
		
		if(curBar > maxBar)
		{
			curBar = maxBar;	
		}
		
		if(maxBar <1)
		{
			maxBar = 1;
		}
		
		barLength = (Screen.width /4)* (curBar / (float)maxBar);
	}
}
