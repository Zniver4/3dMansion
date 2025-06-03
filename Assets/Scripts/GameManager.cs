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
    private void OnEnable()
    {
        GhostBehaviour.onHit += HitHandler;
    }
    private void OnDisable()
    {
        GhostBehaviour.onHit -= HitHandler;
    }
    private void Update()
    {
        if(lives == 0)
        {
            print("Game Over");
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
}
