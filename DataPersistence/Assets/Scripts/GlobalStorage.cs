using System;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    private static GlobalStorage _instance;
    public static GlobalStorage Instance { get { return _instance; } }

    [HideInInspector]
    [NonSerialized]
    public string PlayerName;

    private void Awake()
    {
        _instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
