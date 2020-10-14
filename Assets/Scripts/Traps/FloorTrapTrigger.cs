using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapTrigger : MonoBehaviour
{
    private UnevenFloor parentFloor;

    // Start is called before the first frame update
    void Start()
    {
        parentFloor = transform.parent.GetComponent<UnevenFloor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Char"))
        {
            parentFloor.ActivateFloorTrap();
        }
    }
}
