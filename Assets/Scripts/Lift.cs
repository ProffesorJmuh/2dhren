using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : Moveable
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float upperBound;
    [SerializeField] float bottomBound;

    override
   public void MoveUp()
    {
        if(transform.position.y <= upperBound)
        {
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
        }
    }

    override
    public void MoveDown()
    {
        if (transform.position.y >= bottomBound)
        {
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
        }
    }
}
