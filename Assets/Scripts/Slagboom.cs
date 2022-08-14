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
            if (_timeToClose == 0f)
            {
                animator.SetTrigger("CloseSlagboom");
                OpenSlagboom();
            }
            else
            {
                _timeToClose = 0;
            }
            
        }
    }

    void OpenSlagboom()
    {
        StartCoroutine(OpenSlagboomAfterDelay());
    }

    IEnumerator OpenSlagboomAfterDelay()
    {
        while (_timeToClose < _closingDelay)
        {
            _timeToClose += Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger("OpenSlagboom");
        _timeToClose = 0;
    }
}
