using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootParticleEffect : MonoBehaviour
{
    public GameObject ParticleEffect = null;
    public float ZOffset = -0.2f;

    public void SpawnParticles()
    {
        Instantiate(ParticleEffect, transform.position + Vector3.forward * ZOffset, Quaternion.identity);
    }
}
