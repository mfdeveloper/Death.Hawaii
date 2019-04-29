using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    protected Collider2D hitCollider;

    protected PlayerController player;

    /// <summary>
    /// Tracks if this attack instance has hit an enemy.
    /// </summary>
    protected bool hasHitCharacter = false;

    /// <summary>
    /// Returns true if the hit box has hit something since it was last enabled.
    /// </summary>
    virtual public bool HasHit
    {
        get
        {
            return hasHitCharacter;
        }
    }

    void Awake() {
        
        player = GetComponentInParent<PlayerController>();
        hitCollider = GetComponent<Collider2D>();
        
        if (hitCollider == null)
        {
            Debug.LogError("A HitBox for Player attack must be on the same GameObject as a Collider2D");
        }   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        var attackedEnemy = other.GetComponent<Enemy>();

        if (player == null) Debug.LogWarning("Tried to Damage() but no character has been set");

        if (attackedEnemy != null && !hasHitCharacter)
        {
            player.Damage(attackedEnemy);
            hasHitCharacter = true;
        }

    }

    virtual public void Enable()
    {
        hitCollider.enabled = true;
    }

    virtual public void Disable()
    {
        hasHitCharacter = false;
        hitCollider.enabled = false;
    }
}
