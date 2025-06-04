using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviour : MonoBehaviour
{
    public ParticleSystem ghostBody;
    public ParticleSystem.ColorOverLifetimeModule colors;
    public ParticleSystem.MinMaxGradient idle;
    public ParticleSystem.MinMaxGradient active;
    public WindZone wind;

    public delegate void OnHit();
    public static OnHit onHit;

    public NavMeshAgent navAgent;
    public Transform target;
    public float pathUpdate;

    public bool chasing;
    private void Start()
    {
        colors = ghostBody.colorOverLifetime;
    }
    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
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

        if(GameManager.actualRoom == GameManager.lastRoom)
        {
            gameObject.SetActive(false);
        }

        if(target != null & !chasing)
        {
            bool inRange = Vector3.Distance(transform.position, target.forward)<=navAgent.stoppingDistance;
            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onHit?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
        if(Time.time >= pathUpdate & gameObject.activeSelf)
        {
            pathUpdate = Time.time + 0.2f;
            navAgent.SetDestination(target.position);
        }
    }
}
