using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public int Lives = 3;
    public bool Dead { get { return IsDead(); } }


    public void DecLive()
    {
        if (Lives > 0)
        {
            Lives--;
            Debug.Log("Lives=" + Lives);
        }
    }


    public bool IsDead()
    {
        return Lives < 1;
    }
}
