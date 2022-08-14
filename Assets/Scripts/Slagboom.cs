using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slagboom : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            animator.SetTrigger("CloseSlagboom");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            animator.SetTrigger("OpenSlagboom");
        }
    }
}
