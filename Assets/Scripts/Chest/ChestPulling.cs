using System.Collections;
using UnityEngine;

public class ChestPulling : MonoBehaviour
{
    public GameObject[] Chests;

    private int ListLenght = 3;
    public int MaxPlayableChests = 3;

    void Start()
    {
        ListLenght = Chests.Length;

        TurnOffChests();

        ActivateChest();
    }

    private void StartCoroutine(IEnumerable enumerable)
    {
        throw new System.NotImplementedException();
    }

    void TurnOffChests()
    {
        for (int listIndex = 0; listIndex < ListLenght; listIndex++)
        {
            Chests[listIndex].SetActive(false);
        }
    }

    IEnumerable ActivateChest()
    {
        int activos = 0;

        while (activos < MaxPlayableChests)
        {
            activos = 0;

            foreach (var obj in Chests)
            {
                if (obj.activeSelf) activos++;
            }

            if (activos >= MaxPlayableChests)
                break;

            GameObject[] inactivos = System.Array.FindAll(Chests, InactiveChest => !InactiveChest.activeSelf);

            int randomIndex = Random.Range(0, inactivos.Length);
            inactivos[randomIndex].SetActive(true);
        }

        return null;
    }
}
