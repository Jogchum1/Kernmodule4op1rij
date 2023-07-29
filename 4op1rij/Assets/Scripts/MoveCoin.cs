using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoin : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    public Vector3 targetPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
