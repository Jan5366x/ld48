using System;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float maxStamina = 100;
    [ReadOnly] public static float stamina;
    public static float minSprintStartStamina = 20;
    public static float staminaUsagePerSec = 10;
    public static float staminaRecoveryPerSec = 5;

    public static float timeLoad;

    public static bool previousSprint;

    public static int numMoney;

    public Transform playerDieTriggerPrefab;

    // Restore non static health after level transition
    public static EntityData entity = new EntityData();
    public static bool initializeOnLoad = true;

    private void Start()
    {
        if (initializeOnLoad)
        {
            ResetPlayerData();
            entity.onDeathPrefab = playerDieTriggerPrefab;
            timeLoad = Time.time;
            initializeOnLoad = false;
        }
    }

    public static void ResetPlayerData()
    {
        stamina = maxStamina;
        numMoney = 0;
        entity.maxHealth = 100;
        entity.health = entity.maxHealth;
    }

    public static bool CalculateStaminaTick(bool isSprint)
    {
        isSprint = isSprint && CheckStamina();

        if (isSprint)
        {
            stamina = Mathf.Max(0, stamina - staminaUsagePerSec * Time.deltaTime);
            previousSprint = true;
        }
        else
        {
            stamina = Mathf.Min(maxStamina, stamina + staminaRecoveryPerSec * Time.deltaTime);
            previousSprint = false;
        }

        return isSprint;
    }

    private static bool CheckStamina()
    {
        return stamina > (previousSprint ? 0 : minSprintStartStamina);
    }

    public static void CollectMoney(Transform transform, int money)
    {
        if (money > 0)
        {
            RandomizedSound.Play(transform, RandomizedSound.MONEY);
        }

        numMoney += money;
    }

    public static bool UseMoney(int amount)
    {
        if (numMoney < amount)
        {
            return false;
        }

        numMoney -= amount;
        return true;
    }
}