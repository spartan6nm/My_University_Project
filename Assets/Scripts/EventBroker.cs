using UnityEngine;
using System;

public class EventBroker
{
    //public static event Transform 

    public static event Action PlayerHited;

    public static event Action<Transform> SpawnPositionChange;
    public static void CallPlayerHitted()
    {
        if (PlayerHited != null)
        {
            PlayerHited();
        }
    }

    public static void CallSpawnPositionChange(Transform spawnPosition)
    {
        if(SpawnPositionChange != null)
        {
            SpawnPositionChange(spawnPosition);
        }
    }

}
