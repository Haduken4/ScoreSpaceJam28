using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    public float TimeUntilDestroyed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        TimeUntilDestroyed -= Time.deltaTime;
        if(TimeUntilDestroyed <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
