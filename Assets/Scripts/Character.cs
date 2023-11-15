using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public int currentHealth;
    [SerializeField] int maxHealth;

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        print(gameObject.name + " has died");
        Destroy(this);
    }
}
