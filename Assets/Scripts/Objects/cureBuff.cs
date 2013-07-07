using UnityEngine;
using System.Collections;

public class cureBuff : MonoBehaviour 
{
	public PlayerPhysics playerPhysics;
	public GameObject player;
	public bar bar;
	public GameObject barObject;
	
	//gid sizes
    public int xGridSize = 4;
    public int yGridSize = 4;

    public int currentFrame = 0;

    //position of the grid tiles
    private float xGridPos = 0.0f;
    private float yGridPos = 0.0f;

    //frame times
    private float frameDur = 0.0f;
    private float nextTimeFrame = 0.0f;
	
	//Tempory time variable
	private float timeVar = 0.0f;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("player");
		barObject = GameObject.FindWithTag ("barObject");
		playerPhysics = player.GetComponent<PlayerPhysics>();
		bar = barObject.GetComponent<bar>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//set grid tiles
        xGridPos = 1.0f / xGridSize;
        yGridPos = 1.0f;

        //set scale
        renderer.material.mainTextureScale = new Vector2(xGridPos, yGridPos);
        
		if((Mathf.Round(Time.time * 10)  % 2) == 0){
			
			nextTimeFrame = Time.time + frameDur;
        
			timeVar += 5 * Time.deltaTime;
			currentFrame = (int)timeVar;
			renderer.material.mainTextureOffset = new Vector2(((currentFrame) % xGridSize + 1) * xGridPos, 1);
			
        }
	}
		
	public void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "player")
		{
				//Change state
				playerPhysics.zombieStates -= 1;
				//Modify the bar to fill up
				bar.AddjustCurrentHunger(4);
				//Destroy item
				Destroy(gameObject);
		}
	}
}
