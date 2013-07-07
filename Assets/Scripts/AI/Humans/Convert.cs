using UnityEngine;
using System.Collections;

public class Convert : MonoBehaviour
{
    private HumanAIAttackController humanAttackai;

    void Awake()
    {
        humanAttackai = GetComponent<HumanAIAttackController>();
    }
    
    public void ConvertToZombie()
    {
        gameObject.AddComponent("ZombieAIController");
        Destroy(humanAttackai);
        transform.tag = "Zombie";
    }
}
