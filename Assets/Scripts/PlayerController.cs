using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Attack")]
    public bool attackKill = true;

    public Animator animator;
    protected HitBox attackHitBox;

    void Awake() {
        attackHitBox = GetComponentInChildren<HitBox>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {  
            Attack();
        }

        // TODO: Refactor this to another method
        // Reference: https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("PLAYER_ATTACK"))
        {
            if (stateInfo.length > stateInfo.normalizedTime)
                attackHitBox.Disable();
        }
    }

    virtual public void Attack()
    {

        if (animator != null)
        {
            animator.Play("Base Layer.PLAYER_ATTACK");
        }

        if (attackHitBox != null)
        {
            attackHitBox.Enable();

        } else 
        {
            Debug.LogError("When perform attacks, a children gameObject with HitBox script is required");
        }        
    }
    
    virtual public void Damage(Enemy enemy)
    {
        if (enemy.gameObject.activeInHierarchy)
        {
            if (attackKill)
            {
                // TODO: FreeParallax use the enemy object reference.
                // Needs review this implementation
                
                // Destroy(enemy.gameObject);
                enemy.gameObject.SetActive(false);
            } else
            {
                Debug.LogError("Only one attack kill is not implemented yet. Please, mark attackKill inspector property");
            }
        }
    }
}
