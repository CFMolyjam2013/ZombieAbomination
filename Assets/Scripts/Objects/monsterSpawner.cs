using UnityEngine;
using System.Collections;

public class monsterSpawner : MonoBehaviour 
{
	private PlayerPhysics playerPhysics;
	
	//kill count and waves
	public int curKillCount = 0;
	public int waveComplete = 10;
	public int lastKillCount = 0;
	
	//spawning limits
	public int cap = 2;

	//current spawned 
	public int spawned = 0;
	
	//cap adjuster
	public int capAdjuster = 0;
	
	//gameobject for each
	public GameObject enemyToSpawn = null;
	
	public float spawnDelayMin = 5.0f;
	public float spawnDelayMax = 8.0f;
	
	public float _nextSpawnTime = 0.0f;
	
	public bool pause = false;
	public float pauseTime = 10.0f;

	private GameObject _spawnedEnemy = null;
	
	private Transform _t = null;
	
	// Use this for initialization
	void Start () 
	{
		playerPhysics = GetComponent<PlayerPhysics>();
		
		//gets starting kill count
		//curKillCount = playerPhysics.;
		
		//Caching the transform
		_t = transform;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//updates kill count
		curKillCount = curKillCount - lastKillCount;
		
		//Pauses the spawning inbetween waves
		if(curKillCount == waveComplete)
		{
			paused();
		}
		
		if( _spawnedEnemy != null)
		{	
			//Spawning the enemy
			if(spawned <= cap && Time.time > _nextSpawnTime && pause == false)
			{
				_nextSpawnTime = Time.time + Random.Range(spawnDelayMin,spawnDelayMax);
				Instantiate(enemyToSpawn,_t.position,Quaternion.identity);
				spawned++;
			}
		}
	}
	public void paused()
	{
		while(pauseTime <= 0.0f)
		{
			pause = true;
			DestroyObject(_spawnedEnemy);
			pauseTime -= Time.deltaTime;
			if(Input.GetMouseButtonDown(0))
			{
				pauseTime = 0.0f;	
			}
			waveComplete += 10;
			lastKillCount = curKillCount;
		}
		pause = false;
	}
}
