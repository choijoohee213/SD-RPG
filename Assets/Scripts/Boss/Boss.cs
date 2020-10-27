using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{    
    private Vector3 BossPos = new Vector3(-29.3f, 21.5f, 248.7f);

    public float MaxHealth;
    public float CurrentHealth;

    public bool IsDie { get => CurrentHealth <= 0; }

    public float Exp;

    private void Awake() {
        CurrentHealth = MaxHealth;
    }
}
