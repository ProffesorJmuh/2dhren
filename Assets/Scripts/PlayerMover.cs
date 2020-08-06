using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("adksfkbskbvkb");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * horizontal * moveSpeed);
        
    }
}
