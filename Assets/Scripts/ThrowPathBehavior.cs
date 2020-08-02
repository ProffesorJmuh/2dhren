using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPathBehavior : MonoBehaviour
{

    private int layerGround;

    void OnCollisionEnter2D(Collision2D coll)
    {
        // если касаемся земли, то поднимаем флаг

        if (coll.gameObject.layer == layerGround)
        {
            Destroy(this.gameObject);
        }
    }
    private void Awake()
    {
        layerGround = LayerMask.NameToLayer("Ground");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
