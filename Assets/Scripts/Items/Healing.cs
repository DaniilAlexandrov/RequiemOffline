using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private int healingAmount = 0;
    [SerializeField] private int numberOfUsages = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth targetHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (targetHealth != null && targetHealth.Health != targetHealth.MaxHealth)
            {
                targetHealth.GainHealth(healingAmount);
                numberOfUsages--;
                if (numberOfUsages == 0)
                {
                    Destroy(gameObject);
                }
            }

        }

    }
}
