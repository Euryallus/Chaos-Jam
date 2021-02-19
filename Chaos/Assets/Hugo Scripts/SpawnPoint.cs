using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum directionSpawn
    {
        up,
        down,
        left,
        right,
        middle,
        centre
    }

    public directionSpawn direction;

    private DirectionArrays rooms;

    private int random;

    public bool spawned = false;

    private void Start()
    {
        rooms = GameObject.FindGameObjectWithTag("DirectionArrays").GetComponent<DirectionArrays>();
        random = Random.Range(0, 4);

        if(direction != directionSpawn.middle)
        {
            Invoke("Spawn", 0.5f);
        }
    }

    void Spawn()
    {
        Vector3 pos = transform.position;
        GameObject newRoom;

        if (!spawned)
        {
            switch (direction)
            {
                case directionSpawn.up:

                    newRoom = Instantiate(rooms.topDoorways[random]);
                    newRoom.transform.position = pos;

                    break;

                case directionSpawn.down:

                    newRoom = Instantiate(rooms.bottomDoorways[random]);
                    newRoom.transform.position = pos;

                    break;

                case directionSpawn.left:

                    newRoom = Instantiate(rooms.leftDoorways[random]);
                    newRoom.transform.position = pos;

                    break;

                case directionSpawn.right:

                    newRoom = Instantiate(rooms.rightDoorways[random]);
                    newRoom.transform.position = pos;

                    break;

            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if(direction == directionSpawn.centre)
        {
            Destroy(collision.gameObject);
            return;
        }

        if(collisionObj.GetComponent<SpawnPoint>().direction == directionSpawn.centre)
        {
            Destroy(gameObject);
            return;
        }

        if (collisionObj.CompareTag("SpawnPoint") && direction != directionSpawn.middle && direction != directionSpawn.centre)
        {

            Destroy(gameObject);
        }
    }

}
