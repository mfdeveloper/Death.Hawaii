using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Attack")]
    public bool attackKill = true;

    protected HitBox attackHitBox;

    [FMODUnity.EventRef]
    public string idleSfx;
    
    [FMODUnity.EventRef]
    public string attackSfx;

    [FMODUnity.EventRef]
    public string enemySfx;
    public Animator animator;


    public delegate void RewardAction(int amount);
    public event RewardAction OnKillEnemy;

    protected int rewardsAmount = 0;


    protected FMOD.Studio.EventInstance idleSound;
    protected FMOD.Studio.EventInstance attackSound;

    protected FMOD.Studio.EventInstance enemySound;


    void Start() {
        
        if (attackSfx != null)
        {    
            attackSound = FMODUnity.RuntimeManager.CreateInstance(attackSfx);
        }

        if (enemySfx != null)
        {  
            enemySound = FMODUnity.RuntimeManager.CreateInstance(enemySfx);
        }

        if (idleSfx != null)
        {  
            idleSound = FMODUnity.RuntimeManager.CreateInstance(idleSfx);

            idleSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            idleSound.start();
        }

    }

    void OnDestroy()
    {
        idleSound.release();
        attackSound.release();
    }
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

        attackSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        attackSound.start();

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
                //Plays sound
            
                enemySound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(enemy.gameObject));
                enemySound.start();
                enemySound.release();
                

                enemy.gameObject.SetActive(false);
                if(OnKillEnemy != null)
                {
                    OnKillEnemy(1);
                }
                rewardsAmount += 1;
            } else
            {
                Debug.LogError("Only one attack kill is not implemented yet. Please, mark attackKill inspector property");
            }
        }
    }
}
