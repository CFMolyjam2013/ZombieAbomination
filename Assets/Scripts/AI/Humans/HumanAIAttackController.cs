using UnityEngine;
using System.Collections;

public class HumanAIAttackController : MonoBehaviour
{
    //movement variables
    public float moveSpeed = 10.0f;
    public float rotateSpeed = 20.0f;

    //ranges
    public float minRange = 20.0f;
    public float attackRange = 60.0f;

    public float fireRate = 2.0f;

    public Transform bullet;

    private GameObject player;
    private PlayerPhysics playerPhysics;

    void Awake()
    {
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

        float dist = Vector3.Distance(player.transform.position, transform.position);

        //chase
        if (playerPhysics.zombieStates != PlayerPhysics.ZombieState.fullHuman)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), rotateSpeed);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            
            if (dist > minRange)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            //attack
            if (dist < attackRange && !IsInvoking("Shoot"))
            {
                Invoke("Shoot", fireRate);
            }
        }
    }

    //shoot
    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
