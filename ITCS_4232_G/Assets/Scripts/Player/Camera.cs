using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    [SerializeField] private float yTransform;

    [Header("Camera Limit")]
    [SerializeField] public float leftLim;
    [SerializeField] public float rightLim;
    [SerializeField] public float bottomLim;
    [SerializeField] public float topLim;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, leftLim, rightLim), Mathf.Clamp(player.position.y + yTransform, bottomLim, topLim), transform.position.z);

        
    }
}
