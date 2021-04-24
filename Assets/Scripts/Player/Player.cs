using Unity.Collections;
using UnityEngine;

public class Player : Entity
{
    public float maxStamina = 100;
    [ReadOnly] public float stamina;
    public float minSprintStartStamina = 20;
    public float staminaUsagePerSec = 10;
    public float staminaRecoveryPerSec = 5;

    public bool previousSprint;

    public int numMoney;

    private void Start()
    {
        stamina = maxStamina;
        numMoney = 0;
        health = maxHealth;
    }

    public bool CalculateStaminaTick(bool isSprint)
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

    private bool CheckStamina()
    {
        return stamina > (previousSprint ? 0 : minSprintStartStamina);
    }

    public void CollectMoney(int money)
    {
        if (money > 0)
        {
            RandomizedSound.Play(transform, RandomizedSound.MONEY);
        }
        numMoney += money;
    }
}