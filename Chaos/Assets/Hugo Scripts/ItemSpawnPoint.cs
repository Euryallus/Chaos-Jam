using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    DirectionArrays itemList;
    private void Start()
    {
        itemList = GameObject.FindGameObjectWithTag("DirectionArrays").GetComponent<DirectionArrays>();
        int random = Random.Range(0, itemList.items.Count);

        GameObject newItem = Instantiate(itemList.items[random]);

        newItem.transform.position = transform.position;

        Destroy(gameObject);
    }
}
