using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public delegate void OnDoor(string name);
    public static OnDoor onDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onDoor?.Invoke(this.gameObject.name);
        }
    }
}
