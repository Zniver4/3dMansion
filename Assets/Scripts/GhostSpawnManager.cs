using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostSpawnManager : MonoBehaviour
{
    public GameObject ghost;
    public GameObject ghostTarget;
    public GameObject player;

    public Transform[] gorundFloor;
    public Transform[] firstFoor;
    public Transform[] secondFoor;

    public Transform spawn;

    public float patrolUpdate;

    private void Update()
    {
        if (GhostBehaviour.chasing)
        {
            ghostTarget.transform.position = player.transform.position;
        }
        else
        {
            Patrol();
        }
    }

    private void Awake()
    {
        RoomChange.onDoor += SpawnGhost;
    }

    private void SpawnGhost(string name)
    {
        if(GameManager.lastRoom == GameManager.actualRoom)
        {
            StartCoroutine(CheckSpawn());
        }
    }

    private IEnumerator CheckSpawn()
    {
        yield return new WaitForSeconds(.2f);
        int probability = Random.Range(0, 2);
        if (probability == 1)
        {
            int room;

            if (GameManager.actualRoom == "GF")
            {
                room = Random.Range(0, gorundFloor.Length);
                spawn = gorundFloor[room];
                ghost.transform.position = spawn.position;
                ghost.SetActive(true);
            }
            if (GameManager.actualRoom == "1F")
            {
                room = Random.Range(0, firstFoor.Length);
                spawn = firstFoor[room];
                ghost.transform.position = spawn.position;
                ghost.SetActive(true);
            }
            if (GameManager.actualRoom == "2F")
            {
                room = Random.Range(0, secondFoor.Length);
                spawn = secondFoor[room];
                ghost.transform.position = spawn.position;
                ghost.SetActive(true);
            }
            ghostTarget.transform.position = spawn.position;
        }
    }

    private void Patrol()
    {
        int room;
        if (Time.time >= patrolUpdate & gameObject.activeSelf)
        {
            patrolUpdate = Time.time + 15;

            if (GameManager.actualRoom == "GF")
            {
                room = Random.Range(0, gorundFloor.Length);
                spawn = gorundFloor[room];
                ghostTarget.transform.position = spawn.position;
            }
            if (GameManager.actualRoom == "1F")
            {
                room = Random.Range(0, firstFoor.Length);
                spawn = firstFoor[room];
                ghostTarget.transform.position = spawn.position;
            }
            if (GameManager.actualRoom == "2F")
            {
                room = Random.Range(0, secondFoor.Length);
                spawn = secondFoor[room];
                ghostTarget.transform.position = spawn.position;
            }
        }
    }
}
