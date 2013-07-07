using UnityEngine;
using System.Collections.Generic;

public class Targetting : MonoBehaviour
{
    public static Targetting instance;

    public List<Transform> allTargets;

    public Transform currentTarget;

    [HideInInspector]
    public bool hasTurned = false;
    [HideInInspector]
    public bool isZombieAttacking = false;
    
    public float munchDuration = 1.0f;

    //ranges
    public float range = 3.0f;
    public float shottyRange = 20.0f;
    [HideInInspector]
    public float shottyDist = 0.0f;
    [HideInInspector]
    public float curDist;

    public int zombieDamage = 10;

    private PlayerPhysics playerPhysics;

    private float munchDur = 0.0f;

    private PlayerPhysics.ZombieState lastState;

    void Awake()
    {
        instance = this;

        playerPhysics = GetComponent<PlayerPhysics>();
    }

    // Use this for initialization
    void Start()
    {
        munchDur = munchDuration;

        allTargets = new List<Transform>();

        currentTarget = null;

        AddAllZombies();
    }

    public void AddAllHumans()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Human");

        foreach (GameObject target in go)
        {
            AddTarget(target.transform);
        }
    }

    public void AddAllZombies()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Zombie");

        foreach (GameObject target in go)
        {
            AddTarget(target.transform);
        }
    }

    void AddTarget(Transform target)
    {
        allTargets.Add(target);
    }

	// Update is called once per frame
	void Update ()
    {
        DistanceControl();
        ZombieAttack();
	}

    private void DistanceControl()
    {
        curDist = range;

        foreach (Transform target in allTargets)
        {
            if (allTargets.Count > 0)
            {
                float dist = Vector3.Distance(target.position, transform.position);

                HumanAICiviController humanCiviAI = target.GetComponent<HumanAICiviController>();
                HumanAIAttackController humanAttackai = target.GetComponent<HumanAIAttackController>();

                //calculate range for shotgun strength
                if (dist < shottyRange)
                {
                    shottyDist = (shottyDist / dist) + shottyRange;
                }

                //calculate distance to other targets
                if (dist < curDist)
                {
                    curDist = dist;

                    currentTarget = target;

                    if (Input.GetMouseButton(1) && playerPhysics.zombieStates != PlayerPhysics.ZombieState.fullHuman && (humanCiviAI.isDead || humanAttackai.isDead))
                    {
                        lastState = playerPhysics.zombieStates;
                        playerPhysics.zombieStates = PlayerPhysics.ZombieState.munching;

                        munchDur -= Time.deltaTime;
                    }
                    if (Input.GetMouseButtonUp(1))
                    {
                        playerPhysics.zombieStates = lastState;
                    }
                }

                if (munchDur <= 0)
                {
                    playerPhysics.zombieStates--;
                    PlayerPhysics.killCount++;

                    if (humanAttackai.isDead)
                    {
                        humanAttackai.Kill();
                    }
                    if (humanCiviAI.isDead)
                    {
                        humanCiviAI.Kill();
                    }

                    munchDur = munchDuration;
                }
            }
        }
    }

    void ZombieAttack()
    {
        if (isZombieAttacking)
        {
            HumanAICiviController humanCiviAI = currentTarget.GetComponent<HumanAICiviController>();
            HumanAIAttackController humanAttackai = currentTarget.GetComponent<HumanAIAttackController>();

            humanCiviAI.HealthControl(-zombieDamage);
            humanAttackai.HealthControl(-zombieDamage);
        }
    }
}
