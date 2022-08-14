using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int _angle = 0;
    Animator animator;
    [SerializeField] private GameObject _topCollider;
    [SerializeField] private GameObject _rightCollider;
    [SerializeField] private GameObject _bottomCollider;
    [SerializeField] private GameObject _leftCollider;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetInteger("ArrowAngle", _angle);
        //foreach (GameObject eachChild in transform)
        //{
        //    if(eachChild.name == "Right")
        //    {
        //        _topCollider = eachChild;
        //    }else if(eachChild.name == "Bottom")
        //    {
        //        _bottomCollider = eachChild;
        //    }else if(eachChild.name == "Left")
        //    {
        //        _leftCollider = eachChild;
        //    }else if( eachChild.name == "Top")
        //    {
        //        _topCollider = eachChild;
        //    }
        //}
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
                    _topCollider.SetActive(true);
                    _rightCollider.SetActive(true);
                    _bottomCollider.SetActive(true);
                    _leftCollider.SetActive(false);
                    break;
                case 90:
                    _angle = 180;
                    animator.SetInteger("ArrowAngle", _angle);
                    _topCollider.SetActive(false);
                    _rightCollider.SetActive(true);
                    _bottomCollider.SetActive(true);
                    _leftCollider.SetActive(true);
                    break;
                case 180:
                    _angle = 270;
                    animator.SetInteger("ArrowAngle", _angle);
                    _topCollider.SetActive(true);
                    _rightCollider.SetActive(false);
                    _bottomCollider.SetActive(true);
                    _leftCollider.SetActive(true);
                    break;
                case 270:
                    _angle = 0;
                    animator.SetInteger("ArrowAngle", _angle);
                    _topCollider.SetActive(true);
                    _rightCollider.SetActive(true);
                    _bottomCollider.SetActive(false);
                    _leftCollider.SetActive(true);
                    break;
            }
        }
    }
}
