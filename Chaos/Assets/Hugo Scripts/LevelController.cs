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

    [SerializeField]
    private GameObject playerCharacter;

    [SerializeField]
    private CameraMovement cam;

    [SerializeField]
    bool checkRestart = false;

    private void Start()
    {
        StartCoroutine("startCheck");
    }

    public void Restart()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        Destroy(GameObject.FindGameObjectWithTag("SpawnRoom"));

        for (int i = 0; i < rooms.Length; i++)
        {
            Destroy(rooms[i]);
        }

        GameObject newRoom = Instantiate(centreRoom, position: new Vector3(0, 0, 0), rotation: new Quaternion(0,0,0,0));

        cam.focus = newRoom;

        GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject boomerang = GameObject.FindGameObjectWithTag("Boomerang");

        if(boomerang != null)
        {
            boomerang.SetActive(false);
        }

        complete = false;
        checkRestart = false;


        StartCoroutine("startCheck");
    }

    public bool HasFinishedLevel()
    {
        return complete;
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length == 0 && checkRestart)
        {
            complete = true;
            Restart();
        }
        else
        {
            complete = false;
        }
    }

    IEnumerator startCheck()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("CHECK MNOW");
        checkRestart = true;
    }
}
