using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
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
            if (other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Artemis_Run"))
            {
                Debug.Log("haha get spike trapped");
                if (inside)
                    this.gameObject.GetComponent<Animator>().SetBool("pressed", true);
                Destroy(other.gameObject);
            }
            inside = false;
        }
        else
            inside = false;
    }

}