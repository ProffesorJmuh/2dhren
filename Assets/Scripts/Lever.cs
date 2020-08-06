using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] Moveable thingToMove;

    bool isSwitched;

    // Update is called once per frame
    void Update()
    {
        if (isSwitched)
        {
            thingToMove.MoveUp();
        }
        else thingToMove.MoveDown();
    }

    public void SwitchChange()
    {
        if (isSwitched) isSwitched = false;
        else isSwitched = true;
    }
}
