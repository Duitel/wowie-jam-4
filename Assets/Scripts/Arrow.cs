using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int _angle = 0;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetInteger("ArrowAngle", _angle);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            switch (_angle)
            {
                case 0:
                    _angle = 90;
                    animator.SetInteger("ArrowAngle", _angle);
                    break;
                case 90:
                    _angle = 180;
                    animator.SetInteger("ArrowAngle", _angle);
                    break;
                case 180:
                    _angle = 270;
                    animator.SetInteger("ArrowAngle", _angle);
                    break;
                case 270:
                    _angle = 0;
                    animator.SetInteger("ArrowAngle", _angle);
                    break;
            }
        }
    }
}
