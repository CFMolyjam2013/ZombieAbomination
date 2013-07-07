using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour
{
    public static bool hasRpg = false;

    public static float currentBulletSpeed = 0.0f;
    public static int currDamage = 0;
    public static int killCount = 0;

    //speeds of player in zombie states
    public float fullHumanSpeed = 10.0f;
    public float partHumanSpeed = 20.0f;
    public float halfZombieSpeed = 30.0f;
    public float nearFullZombieSpeed = 40.0f;
    public float fullZombieSpeed = 60.0f;

    public float playerRotSpeed = 120.0f;

    //weapon fire rates
    public float pistolFireRate = 0.5f;
    public float shottyFireRate = 1.5f;
    public float rpgFireRate = 2.5f;

    //bullet speeds
    public float pistolBulletSpeed = 1.0f;
    public float shottyBulletSpeed = 0.5f;
    public float rpgRocketSpeed = 1.0f;

    public float health = 100;

    //bullet damage values
    public int pistolDamage = 20;
    public int rpgDamage = 100;
    public int shottyDamage = 10;
    
    //projectiles
    public Transform pistolBullet;
    public Transform shottyShell;
    public Transform rpgChainsawRocket;

    //gid sizes
    public int xGridSize = 4;
    public int yGridSize = 4;

    private int currentFrame = 0;

    //position of the grid tiles
    private float xGridPos = 0.0f;
    private float yGridPos = 0.0f;

    //frame times
    private float frameDur = 0.0f;
    private float nextTimeFrame = 0.0f;

    private float angle = 0.0f;
    private float counterActAngle = 90.0f;

    private Transform currentProjectile;

    private pistolAmmo pistolAmmoScript;
    private shotgunAmmo shottyAmmoScript;
    private rpgAmmo rpgAmmoScript;
    private Targetting targeting;
    
    private float shottyDamageRangeMod = 0.0f;
    
    public enum WeaponSelect
    {
        pistol,
        shotty,
        rpgChainsaw,
        melee
    }

    public WeaponSelect weaponSelected { get; set; }

    //zombie levels
    public enum ZombieState
    {
        fullHuman,
        partHuman,
        halfZombie,
        nearFullZombie,
        fullZombie
    }

    public ZombieState zombieStates { get; set; }

    void Awake()
    {
        targeting = GetComponent<Targetting>();
    }

    void Start()
    {
        zombieStates = ZombieState.fullHuman;

        //set default weapon stats
        currentProjectile = pistolBullet;
        currentBulletSpeed = pistolBulletSpeed;
        currDamage = pistolDamage;
        weaponSelected = WeaponSelect.pistol;
    }
    
    // Update is called once per frame
    void Update()
    {
        Aiming();
        ShottyDamageRange();
        WeaponSelection();
        Firing();
        GetMotion();
    }

    void Aiming()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);

        //get relative position from mouse position and player
        mousePos.x = mousePos.x - objPos.x;
        mousePos.y = mousePos.y - objPos.y;

        //get angle
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //get rotation
        Quaternion rot = Quaternion.Euler(new Vector3(0, -angle + counterActAngle, 0));
        //apply rotation
        transform.rotation = rot;
    }

    void ShottyDamageRange()
    {
        shottyDamageRangeMod = Mathf.Abs(targeting.shottyDist);
        shottyDamageRangeMod = Mathf.Clamp(shottyDamageRangeMod, 0, 30);
    }

    //select weapons and set their values
    void WeaponSelection()
    {
        //pistol select
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSelected = WeaponSelect.pistol;
            currentBulletSpeed = pistolBulletSpeed;
            currentProjectile = pistolBullet;
            currDamage = pistolDamage;
        }
        //shotty select
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponSelected = WeaponSelect.shotty;
            currentBulletSpeed = shottyBulletSpeed;
            currentProjectile = shottyShell;
            currDamage = (int)(shottyDamage * shottyDamageRangeMod);
        }
        //rpg select
        if (Input.GetKeyDown(KeyCode.Alpha3) && hasRpg)
        {
            weaponSelected = WeaponSelect.rpgChainsaw;
            currentBulletSpeed = rpgRocketSpeed;
            currentProjectile = rpgChainsawRocket;
        }
        //melee select
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponSelected = WeaponSelect.melee;
        }
    }

    float SetFireRate()
    {
        float fireRate = 0.0f;

        switch (weaponSelected)
        {
            case WeaponSelect.pistol:
                fireRate = pistolFireRate;
                break;
            case WeaponSelect.shotty:
                fireRate = shottyFireRate;
                break;
            case WeaponSelect.rpgChainsaw:
                fireRate = rpgFireRate;
                break;
        }

        return fireRate;
    }

    void Firing()
    {
        if (Input.GetMouseButton(0) && !IsInvoking("Fire"))
        {
            Invoke("Fire", SetFireRate());
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Fire");
        }
    }

    void Fire()
    {
        //switch (weaponSelected)
        //{
        //    case WeaponSelect.pistol:

        //}
        Instantiate(currentProjectile, transform.position, transform.rotation);
    }

    void GetMotion()
    {
        //get input
        float vInput = Input.GetAxisRaw("Vertical");
        float hInput = Input.GetAxisRaw("Horizontal");

        //set grid tiles
        xGridPos = 1.0f / xGridSize;
        yGridPos = 0.5f;

        //set scale
        renderer.material.mainTextureScale = new Vector2(vInput * xGridPos, yGridPos);

        if (Time.time - nextTimeFrame > frameDur)
        {
            nextTimeFrame = Time.time + frameDur;

            if (vInput > 0)
            {
                currentFrame++;
            }
            if (vInput < 0)
            {
                currentFrame--;
            }

            //loop frames
            if (currentFrame >= xGridSize || currentFrame <= -xGridSize)
            {
                currentFrame = 0;
            }

            //apply frames
            renderer.material.mainTextureOffset = new Vector2(vInput * ((currentFrame) % xGridSize + 1) * xGridPos, 1);
        }

        //rotate
        Quaternion rot = Quaternion.AngleAxis(hInput * playerRotSpeed * Time.deltaTime, transform.up) * transform.rotation;
        transform.rotation = rot;
        //move forward
        transform.position += transform.forward * vInput * MoveSpeed() * Time.deltaTime;
    }

    //set speed according to zombie state
    float MoveSpeed()
    {
        float moveSpeed = 0.0f;

        switch (zombieStates)
        {
            case ZombieState.fullHuman:
                moveSpeed = fullHumanSpeed;
                break;
            case ZombieState.partHuman:
                moveSpeed = partHumanSpeed;
                break;
            case ZombieState.halfZombie:
                moveSpeed = halfZombieSpeed;
                break;
            case ZombieState.nearFullZombie:
                moveSpeed = nearFullZombieSpeed;
                break;
            case ZombieState.fullZombie:
                moveSpeed = fullZombieSpeed;
                break;
        }

        return moveSpeed;
    }

    public void HealthControl(int dmg)
    {
        health += dmg;

        if (health <= 0)
        {
            zombieStates++;

            Targetting.instance.allTargets.Clear();

            if (zombieStates != ZombieState.fullHuman)
            {
                Targetting.instance.AddAllHumans();
            }
            else
            {
                Targetting.instance.AddAllZombies();
            }

            health = 100;
        }
    }
}