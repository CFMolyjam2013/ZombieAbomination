using UnityEngine;
using System.Collections;

public class shotgunAmmo : MonoBehaviour 
{
	//Starting ammo
	public static int curShotgunAmmo = 0;
	int maxAmmo = 10;
	
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
			if(curShotgunAmmo < maxAmmo && curShotgunAmmo > 5)
			{
				//Adding Ammo
				curShotgunAmmo = 10;
				//Destroy Item
				Destroy(gameObject);
			}
			else if(curShotgunAmmo < maxAmmo && curShotgunAmmo < 5)
			{
				curShotgunAmmo += 5;
				Destroy(gameObject);
			}
			else 
			{
				Destroy(gameObject);	
			}
		}
	}
}
