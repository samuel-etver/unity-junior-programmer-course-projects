using System;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    private static GlobalStorage _instance;
    public static GlobalStorage Instance { get { return _instance; } }


    [HideInInspector]
    [NonSerialized]
    public string PlayerName;


    [HideInInspector]
    [NonSerialized]
    public int BestScore = 0;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
