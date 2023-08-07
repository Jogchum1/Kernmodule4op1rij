using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : NetworkedBehaviour
{
    public uint col;
    public Vector3 spawnLocation;
    public Vector3 targetLocation;

    public void UpdateTargetLocation()
    {
        targetLocation = new Vector3(targetLocation.x, targetLocation.y + 38, targetLocation.z);
        Debug.Log("updated location");
    }

}
