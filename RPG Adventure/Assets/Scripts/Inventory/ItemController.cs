using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public Item item;

    private int range;

    private void Start()
    {
        range = item.range;
    }

    private void Update()
    {
        float distance = Vector3.Distance(PlayerManager.instance.playerObject.transform.position, transform.position);

        if (distance <= range)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerManager.instance.playerObject.transform.position, Time.deltaTime);
        }
    }
}
