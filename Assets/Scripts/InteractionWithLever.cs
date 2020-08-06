using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithLever : MonoBehaviour
{
    [SerializeField] GameObject lever;
    // Start is called before the first frame update
    private void Update()
    {
        if(lever && Input.GetKeyDown(KeyCode.E))
        {
            lever.GetComponent<Lever>().SwitchChange();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.CompareTag("Lever"))
        {
            Debug.Log("Triggered");
            lever = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Not Triggered");
        if (collision.gameObject.CompareTag("Lever"))
        {
            lever = null;
        }
    }
}
