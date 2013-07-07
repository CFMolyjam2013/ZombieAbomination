using UnityEngine;
using System.Collections;

public class ZombieAIController : MonoBehaviour
{
    public static ZombieAIController instance;

    public float rangeFromPlayer = 3.0f;

    //movement forces
    public float rotateSpeed = 50.0f;
    public float moveSpeed = 10.0f;

    

    public float convertWaitTime = 1.0f;

    [HideInInspector]
    public int curHealth = 100;

    public int damage = 10;
    
    private GameObject player;
    private PlayerPhysics playerPhysics;

    void Awake()
    {
        instance = this;

        player = GameObject.FindWithTag("player");
        playerPhysics = player.GetComponent<PlayerPhysics>();
    }
	
	void LateUpdate ()
    {
        ProcessMotion();
	}

    void ProcessMotion()
    {
        Vector3 targetDir = player.transform.position - transform.position;

        float dist = Vector3.Distance(transform.position, player.transform.position);

        //chase until fully converted
        if (playerPhysics.zombieStates != PlayerPhysics.ZombieState.fullZombie)
        {
            //only chase if out of range
            if (dist > rangeFromPlayer)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), rotateSpeed);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
                //start the convert process
            else if (!Targetting.instance.hasTurned)
            {
                //wait a bit before turning
                StartCoroutine("TurnAndWait", convertWaitTime);
                playerPhysics.HealthControl(-damage);
                //playerPhysics.zombieStates++;
            }
        }
    }

    //wait
    IEnumerator TurnAndWait(float waitTime)
    {
        Targetting.instance.hasTurned = true;

        yield return new WaitForSeconds(waitTime);

        //create the new list
        //Targetting.instance.allTargets.Clear();
        //Targetting.instance.AddAllHumans();

        Targetting.instance.hasTurned = false;
    }

    public void HealthControl(int adj)
    {
        curHealth += adj;

        if (curHealth <= 0)
        {
            PlayerPhysics.killCount++;
			Destroy (gameObject);
        }
    }
}
