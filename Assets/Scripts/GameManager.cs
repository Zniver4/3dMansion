using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform playerPos;
    public CharacterController playerController;
    public int lives = 3;
    public Slider healthBar;

    public GameObject gameOver;

    public static string lastRoom;
    public static string actualRoom;

    private void OnEnable()
    {
        GhostBehaviour.onHit += HitHandler;
        RoomChange.onDoor += RoomHandler;
    }
    private void OnDisable()
    {
        GhostBehaviour.onHit -= HitHandler;
        RoomChange.onDoor -= RoomHandler;
    }

    private void Start()
    {
        lastRoom = "GF";
        actualRoom = "";
    }
    private void Update()
    {
        if(lives == 0)
        {
            print("Game Over");
            gameOver.SetActive(true);
        }
        healthBar.value = lives;
    }
    public void HitHandler()
    {
        print("-1 life");
        lives--;
        playerController.enabled = false;
        playerPos.position = new Vector3(-31, 3, -18);
        playerController.enabled = true;
    }
    public void RoomHandler(string name)
    {
        if(lastRoom == actualRoom)
        {
            actualRoom = name;
        }
        else
        {
            lastRoom = actualRoom;
        }
    }
}
