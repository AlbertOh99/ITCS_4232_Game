using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;
    [SerializeField] Transform playerPosition;
    private Vector2 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, cameraPosition.position, 100);
        transform.position = new Vector2(transform.position.x, startPos.y);
    }
}
