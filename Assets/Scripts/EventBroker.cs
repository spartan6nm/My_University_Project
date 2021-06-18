
using System;

public class EventBroker
{
    public static event Action PlayerHited;

    public static event Action PlayerDetected;

    public static void CallPlayerHitted()
    {
        if (PlayerHited != null)
        {
            PlayerHited();
        }
    }

    public static void CallPlayerDetected()
    {
        if(PlayerDetected != null)
        {
            PlayerDetected();
        }
    }
}
