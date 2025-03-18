using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioSource[] collisionSounds; 
    public float minCollisionForce = 1.0f; 
    public float pitchVariation = 0.3f;

    void OnCollisionEnter(Collision collision)
    {

        float collisionForce = collision.relativeVelocity.magnitude;

        
        if (collisionForce >= minCollisionForce && collisionSounds.Length > 0)
        {
            
            AudioSource availableSource = GetAvailableAudioSource();
            if (availableSource != null)
            {
                availableSource.pitch = 1.0f + Random.Range(-pitchVariation, pitchVariation);

                availableSource.Play();
            }
        }
    }

    AudioSource GetAvailableAudioSource()
    {
        foreach (var source in collisionSounds)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }
}
