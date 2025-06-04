using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnManager : MonoBehaviour
{
    public GameObject ghost;
    public GameObject ghostTarget;

    public Transform[] gorundFloor;
    public Transform[] firstFoor;
    public Transform[] secondFoor;

    public Transform spawn;

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
                room = Random.Range(0, gorundFloor.Length + 1);
                spawn = gorundFloor[room];
                ghost.transform.position = spawn.position;
                ghost.SetActive(true);
            }
            if (GameManager.actualRoom == "1F")
            {
                room = Random.Range(0, firstFoor.Length + 1);
                spawn = firstFoor[room];
                ghost.transform.position = spawn.position;
                ghost.SetActive(true);
            }
            if (GameManager.actualRoom == "2F")
            {
                room = Random.Range(0, secondFoor.Length + 1);
                spawn = secondFoor[room];
                ghost.transform.position = spawn.position;
                ghost.SetActive(true);
            }
            ghostTarget.transform.position = spawn.position;
        }
    }
}
