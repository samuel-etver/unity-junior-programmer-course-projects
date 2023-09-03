using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DetectCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Food>() != null)
        {
            Destroy(other.gameObject);
            Animal animal = GetComponent<Animal>();
            bool success = animal != null && animal.Feed();
            if (success && animal.GameManager != null)
            {
                if (animal.GameManager.TryGetComponent<GameScore>(out var score))
                {
                    score.IncScore();
                }
            }
        }

    }
}
