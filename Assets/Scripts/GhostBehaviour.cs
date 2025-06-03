using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public ParticleSystem ghostBody;
    public ParticleSystem.ColorOverLifetimeModule colors;
    public ParticleSystem.MinMaxGradient idle;
    public ParticleSystem.MinMaxGradient active;
    public WindZone wind;

    public delegate void OnHit();
    public static OnHit onHit;

    public bool chasing;
    private void Start()
    {
        colors = ghostBody.colorOverLifetime;
    }

    void Update()
    {
        if (chasing)
        {
            colors.color = active;
            wind.transform.localPosition = new Vector3(0, -1, 0);
            wind.windMain = 4;
        }else
        {
            colors.color = idle;
            wind.transform.localPosition = new Vector3(0, 1.5f, 0);
            wind.windMain = -4;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        onHit?.Invoke();
    }
}
