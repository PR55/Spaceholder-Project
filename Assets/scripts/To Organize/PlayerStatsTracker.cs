using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsTracker : MonoBehaviour
{
    public PlayerStats playerStats;

    public PlayerStats curPlayer()
    {
        return playerStats;
    }
}
