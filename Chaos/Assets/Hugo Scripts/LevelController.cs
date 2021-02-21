using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    GameObject centreRoom;

    [SerializeField]
    GameObject[] enemies;

    [SerializeField]
    private bool complete = false;

    public void Restart()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        Destroy(GameObject.FindGameObjectWithTag("SpawnRoom"));

        for (int i = 0; i < rooms.Length; i++)
        {
            Destroy(rooms[i]);
        }

        GameObject newRoom = Instantiate(centreRoom, position: new Vector3(0, 0, 0), rotation: new Quaternion(0,0,0,0));

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        complete = false;
    }

    public bool HasFinishedLevel()
    {
        return complete;
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length == 0)
        {
            complete = true;
        }
        else
        {
            complete = false;
        }
    }
}
