using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;

    public bool IsDie { get => CurrentHealth <= 0; }

    public float Exp;

    private void Awake() {
        CurrentHealth = MaxHealth;
    }
}
