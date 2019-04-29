using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{

    public PlayerController player;

    public int limit = 100;

    protected int collected = 0;

    protected Text textCmp;

    void Awake() {
        textCmp = GetComponent<Text>();
        if (textCmp == null)
        {
            Debug.LogError("A Text component is required");
        }
    }

    void OnEnable() {
        if (player != null)
        {
            player.OnKillEnemy += Collect;  
        }
    }

    void OnDisable() {
        if (player != null)
        {
            player.OnKillEnemy -= Collect;  
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textCmp.text = string.Format("{0}/{1}", collected, limit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void Collect(int amount) {
        if (amount > 0)
        {
            collected += amount;
            textCmp.text = string.Format("{0}/{1}", collected, limit);
        }
    }
}
