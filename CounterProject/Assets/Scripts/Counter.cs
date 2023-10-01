using UnityEngine;


public class Counter : MonoBehaviour
{
    public GameManager GameManager;


    private void OnTriggerEnter(Collider other)
    {
        GameManager.InBox += 1;
    }
}
