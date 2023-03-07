using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDirectory : MonoBehaviour
{
    public EnemyHealth enemyHealth;

    public EnemyHealth healthLocation()
    {
        return enemyHealth;
    }
}
