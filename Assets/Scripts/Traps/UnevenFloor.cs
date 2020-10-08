using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnevenFloor : MonoBehaviour
{
    bool inside = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Char"))
        {
            inside = true;
            Debug.Log("haha get floor trapped");
            Invoke("Drop", 1.0f);
            inside = false;
        }
        else
            inside = false;
    }

    void Drop()
    {
        Destroy(this.gameObject);
    }

}
