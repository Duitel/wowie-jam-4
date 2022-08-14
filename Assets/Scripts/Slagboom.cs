using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slagboom : MonoBehaviour
{
    [SerializeField] private float _closingDelay = 10f;
    private float _timeToClose = 0f;
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
            OpenSlagboom();
        }
    }

    void OpenSlagboom()
    {
        StartCoroutine(OpenSlagboomAfterDelay());
    }

    IEnumerator OpenSlagboomAfterDelay()
    {
        _timeToClose = 0;
        while (_timeToClose < _closingDelay)
        {
            _timeToClose += Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger("OpenSlagboom");
    }
}
