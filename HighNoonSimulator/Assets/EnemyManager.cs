using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager current;
    // Start is called before the first frame update

    public delegate void onEnemyTriggerEnterDelegate();
    public static event onEnemyTriggerEnterDelegate OnEnemyTriggerEnter;
    public static void OnEnemyTrigger()
    {
        if (OnEnemyTriggerEnter != null)
        {
            OnEnemyTriggerEnter();
        }
    }
}
