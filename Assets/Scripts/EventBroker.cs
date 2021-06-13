
using System;

public class EventBroker
{
    public static event Action PlayerHited;

    public static event Action EnemyHitted;

    public static void CallPlayerHitted()
    {
        if (PlayerHited != null)
        {
            PlayerHited();
        }
    }

    public static void CallEnemyHitted()
    {
        if(EnemyHitted != null)
        {
            EnemyHitted();
        }
    }
}
