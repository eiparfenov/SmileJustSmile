using System;
using Player;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponentInParent<PlayerManager>())
            GlobalEventsManager.OnLevelFinished.Invoke();
    }
}