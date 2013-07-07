using UnityEngine;
using System.Collections;

public class rpgWeapon : MonoBehaviour {
	
	public bool haveRpg = false;
	public int goalKillCount = 20;
	private PlayerPhysics playerPhysics;

	// Use this for initialization
	void Start () 
	{
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Checks the kill count to spawn rpg
		//if(killCount > goalKillCount && haveRpg == false)
		//{
			//SpawnWeapon();
		//}
	}
	
	public void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "player")
		{
			//Enable player to select the weapon to use
			PlayerPhysics.hasRpg = true;
			//Make sure the weapon does not spawn again
			haveRpg = true;
			//Destroy object
			Destroy(gameObject);
		}
	}
	
	public void SpawnWeapon()
	{
		
	}
}
