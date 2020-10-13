using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private float damageThreshold = 10.0f;
    [SerializeField]
    private PlayerHealthText healthText;

    private Rigidbody2D rigidBodyComponent;
    private bool damageWhenStop = false;

    public static PlayerHealth Instance { get; private set; }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = Mathf.Max(value, 0);
            healthText.Health = health;
        }
    }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        Instance = this;

        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rigidBodyComponent.velocity.sqrMagnitude > damageThreshold * damageThreshold)
        {
            damageWhenStop = true;
        }
        else if (damageWhenStop && PlayerController.Instance.Grounded)
        {
            damageWhenStop = false;
            if (--Health == 0)
            {
                PlayerController.Instance.Die(DeathType.Fall);
            }
        }
    }

    public void ApplySpikeDamage(int damage)
    {
        Health -= damage;
        if (health == 0)
        {
            PlayerController.Instance.Die(DeathType.Trap);
        }
    }
}
