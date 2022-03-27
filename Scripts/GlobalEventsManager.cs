using UnityEngine;
using UnityEngine.Events;

public static class GlobalEventsManager
{
    public static UnityEvent<DieType> OnPlayerDead = new UnityEvent<DieType>();

    public static void RemoveAllListeners()
    {
        OnPlayerDead.RemoveAllListeners();
    }
}