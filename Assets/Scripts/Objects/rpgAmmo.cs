using UnityEngine;
using System.Collections;

public class rpgAmmo : MonoBehaviour 
{
	//Starting ammo
	public static int curRpgAmmo = 0;
	int maxAmmo = 3;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(curRpgAmmo == 0)
		{
			//Reset the player's rpg so they have to find it again
			PlayerPhysics.hasRpg = false;
		}
	}	
}
