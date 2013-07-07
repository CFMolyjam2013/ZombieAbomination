using UnityEngine;
using System.Collections;

public class pistolAmmo : MonoBehaviour {
	
	//Starting ammo
	public static int curPistolAmmo = 0;
	int maxAmmo = 20;
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
			if(curPistolAmmo < maxAmmo && curPistolAmmo > 10)
			{
				//Adding Ammo
				curPistolAmmo = 20;
				//Destroy Item
				Destroy(gameObject);
			}
			else if(curPistolAmmo < maxAmmo && curPistolAmmo < 10)
			{
				curPistolAmmo += 10;
				Destroy(gameObject);
			}
			else 
			{
				Destroy(gameObject);	
			}
		}
	}
}
        
		

