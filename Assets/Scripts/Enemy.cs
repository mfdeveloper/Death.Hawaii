using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyVoices
{
    NONE,
    MALE,
    FEMALE
}


public class Enemy : MonoBehaviour
{

    public EnemyVoices voiceType = EnemyVoices.NONE;
}
