using UnityEngine;
using System.Collections;

public class monsterSpawner : MonoBehaviour 
{
	public int killCount = 0;
	public int waveComplete = 5;
	public GameObject enemyToSpawn = null;
	
	public float spawnDelayMin = 5.0f;
	public float spawnDelayMax = 8.0f;
	
	public bool pause = true;
	public float pauseTime = 10.0f;
	
	private float _nextSpawnTime = 0.0f;
	private GameObject _spawnedEnemy = null;
	private GameObject _spawnedEnemy1 = null;
	private GameObject _spawnedEnemy2 = null;
	private GameObject _spawnedEnemy3 = null;
	private GameObject _spawnedEnemy4 = null;
	private GameObject _spawnedEnemy5 = null;
	private GameObject _spawnedEnemy6 = null;
	private GameObject _spawnedEnemy7 = null;
	private GameObject _spawnedEnemy8 = null;
	private GameObject _spawnedEnemy9 = null;
	
	
	private Transform _t = null;
	
	// Use this for initialization
	void Start () 
	{
		//Caching the transform
		_t = transform;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Pauses the spawning inbetween waves
		paused();
		
		if( _spawnedEnemy == null && Time.time > _nextSpawnTime)
		{
			//Setting the time for the next spawn
			_nextSpawnTime = Time.time +Random.Range(spawnDelayMin,spawnDelayMax);
			
			//Spawning the enemy
			if(killCount >= 0 && killCount <= 5 && pause == false)
			{
			_spawnedEnemy = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			_spawnedEnemy1 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			}
			else if(killCount >= 5 && killCount <= 10 && pause == false)
			{
			_spawnedEnemy2 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			_spawnedEnemy3 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			}
			else if(killCount >= 10 && killCount <= 15 && pause == false)
			{
			_spawnedEnemy4 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			_spawnedEnemy5 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			} else if(killCount >=15 && killCount <= 20 && pause == false)
			{
			_spawnedEnemy6 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			_spawnedEnemy7 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			}else if(killCount >=20 && killCount <= 25 && pause == false)
			{
			_spawnedEnemy8 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			_spawnedEnemy9 = Instantiate(enemyToSpawn,_t.position,Quaternion.identity) as GameObject;
			}
		}
	}
	public void paused()
	{
		while(killCount == waveComplete && pauseTime <= 0.0f)
		{
			pause = true;
			DestroyObject(_spawnedEnemy);
			DestroyObject(_spawnedEnemy1);
			DestroyObject(_spawnedEnemy2);
			DestroyObject(_spawnedEnemy3);
			DestroyObject(_spawnedEnemy4);
			DestroyObject(_spawnedEnemy5);
			DestroyObject(_spawnedEnemy6);
			DestroyObject(_spawnedEnemy7);
			DestroyObject(_spawnedEnemy8);
			DestroyObject(_spawnedEnemy9);
			pauseTime -= Time.deltaTime;
			if(Input.GetMouseButtonDown(0))
			{
				pauseTime = 0.0f;	
			}
		}
		pause = false;
	}
}
