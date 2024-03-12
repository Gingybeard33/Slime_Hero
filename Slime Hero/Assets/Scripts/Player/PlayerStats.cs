using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;

    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentRecovery;
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentMight;
    [HideInInspector] public float currentProjectileMovementSpeed;

    // I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    InventoryManager inventory;
    public int abilityIndex;
    public int passiveItemIndex;

    public List<GameObject> startingAbilities = new List<GameObject>(5);
    public List<GameObject> startingPassiveItems = new List<GameObject>(3);

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        inventory = GetComponent<InventoryManager>();

        currentHealth = playerData.MaxHealth;
        currentRecovery = playerData.Recovery;
        currentMoveSpeed = playerData.MoveSpeed;
        currentMight = playerData.Might;
        currentProjectileMovementSpeed = playerData.ProjectileMovementSpeed;

        foreach(GameObject ability in startingAbilities)
        {
            if(ability != null)
            {
                SpawnAbility(ability);
            }
        }

        foreach(GameObject item in startingPassiveItems)
        {
            if(item != null)
            {
                SpawnPassiveItem(item);
            }
        }
    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        // Invincibility timer has reached 0
        else if(isInvincible)
        {
            isInvincible = false;
        }
    }

    public void TakeDamage(float dmg)
    {
        if(!isInvincible){
            if(currentHealth - dmg < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= dmg;
            }

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if(currentHealth == 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Debug.Log("PLAYER IS DEAD");
    }

    public void SpawnAbility(GameObject ability)
    {
        if(abilityIndex >= inventory.abilitySlots.Count - 1)
        {
            Debug.LogError("Ability Inventory already full!");
            return;
        }

        GameObject spawnedAbility = Instantiate(ability, transform.position, Quaternion.identity);
        spawnedAbility.transform.SetParent(transform);
        inventory.AddAbility(abilityIndex, spawnedAbility.GetComponent<AbilityController>());

        abilityIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {

        if(passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Passive Item Inventory already full!");
            return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }

}
