using UnityEngine;
using System.Collections;

public class rpgWeapon : MonoBehaviour {
	
	public bool haveRpg = false;
	public int goalKillCount = 0;
	
	public GameObject rpgWeaponSpawn = null;
	
	public int minimumX = 1;
	public int maximumX = 10;
	
	public int minimumZ = 4;
	public int maximumZ = 20;
	
	private Transform _t = null;
	

	// Use this for initialization
	void Start () 
	{
		_t = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Checks the kill count to spawn rpg
		if(PlayerPhysics.killCount > goalKillCount && haveRpg == false)
		{
			SpawnWeapon();
		}
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
		Instantiate(rpgWeaponSpawn,_t.position,Quaternion.identity);
	}
}
