using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCoin : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    public Vector3 targetPosition;
    public Vector3 spawnPos;
    private RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.localPosition = new Vector3(spawnPos.x, spawnPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        rect.localPosition = Vector3.MoveTowards(rect.localPosition, targetPosition, step);
        //Debug.Log(transform.position);
    }
}
