using UnityEngine;
using System.Collections;

public class accuracyBuff : MonoBehaviour 
{
	private PlayerPhysics playerPhysics;
	private GameObject player;
	
	public bool startPowerUp = false;
	public float timer = 10;
	
	//frame times
    private float frameDur = 0.0f;
    private float nextTimeFrame = 0.0f;
	
	//gid sizes
    public int xGridSize = 4;
    public int yGridSize = 4;

    private int frameDir = 0;
    private int currentFrame = 0;

    //position of the grid tiles
    private float xGridPos = 0.0f;
    private float yGridPos = 0.0f;
	
	//Tempory time variable
	private float timeVar = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("player");
		playerPhysics = player.GetComponent<PlayerPhysics>();
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
		//Starts the powerup
		if(startPowerUp == true)
		{
			timer -= Time.deltaTime;
		}
		buff();
	}
	
	public void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "player")
		{
			startPowerUp = true;
		    Destroy(gameObject);
		}
	}
	
	public void buff()
	{
		//Give the player the ability to instant kill zombies
		if(timer > 0 && startPowerUp == true)
		{
			playerPhysics.pistolDamage = 100;
			playerPhysics.shottyDamage = 100;
		}
		if(timer < 0 )
		{
			playerPhysics.pistolDamage = 20;
			playerPhysics.shottyDamage = 10;
			startPowerUp = false;
		}
	}
}
