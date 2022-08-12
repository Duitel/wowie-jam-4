using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSelect : MonoBehaviour
{
    [SerializeField] private GameObject _groundGameObject;
    [SerializeField] private int height = 12;
    [SerializeField] private int width = 16;


    private GameObject[,] _floorGameObject;
    private Ground[,] _floorGround;

    private int _xSelect = 8;
    private int _ySelect = 5;
    private int _xPreviousSelect = 0;
    private int _yPreviousSelect = 0;

    private float _elapsedTime;
    private float _timeStep = 0.02f;
    private bool _isMoving = false;
    private float _movingTime;
    private float _movingDelay = 0.2f;

    private enum State { };

    // Start is called before the first frame update
    void Start()
    {
        FillFloor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _elapsedTime+= Time.deltaTime;
        if( _elapsedTime > _timeStep)
        {
            if (!_isMoving)
            {
                _movingTime = 0f;
            }
            _movingTime += _elapsedTime;
            if (_movingTime<_movingDelay)
            {
                _elapsedTime = -2*_timeStep;
            }
            else
            {
                _elapsedTime = 0f;
            }
            _isMoving = UserInput();
        }
    }

    void FillFloor()
    {
        _floorGameObject = new GameObject[width, height];
        _floorGround = new Ground[width, height];
        //Instantiate(_groundGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                _floorGameObject[x, y] = (GameObject)Instantiate(_groundGameObject, new Vector3(x, 0, y), Quaternion.identity);
                _floorGround[x, y] = _floorGameObject[x,y].GetComponent<Ground>();
            }
        }
    }

    bool UserInput()
    {
        if (Input.GetKey("s") & _ySelect > 0)
        {
            _yPreviousSelect = _ySelect;
            _ySelect--;
            UnSelectRow();
            SelectRow();
            SelectColumn();
            return true;
        }
        if (Input.GetKey("w") & _ySelect < height - 1)
        {
            _yPreviousSelect = _ySelect;
            _ySelect++;
            UnSelectRow();
            SelectRow();
            SelectColumn();
            return true;
        }
        if (Input.GetKey("a") & _xSelect > 0)
        {
            _xPreviousSelect = _xSelect;
            _xSelect--;
            UnSelectColumn();
            SelectColumn();
            SelectRow();
            return true;
        }
        if (Input.GetKey("d") & _xSelect < width - 1)
        {
            _xPreviousSelect = _xSelect;
            _xSelect++;
            UnSelectColumn();
            SelectColumn();
            SelectRow();
            return true;
        }
        return false;
    }

    void SelectColumn()
    {
        for(int y = 0; y < height; y++)
        {
            if (y == _ySelect)
            {
                _floorGround[_xSelect, y].SwitchState(Ground.State.SELECTED, 0);
            }
            else
            {
                _floorGround[_xSelect, y].SwitchState(Ground.State.HIGHLIGHT, 0);
            }
        }
    }

    void UnSelectColumn()
    {
        for (int y = 0; y < height; y++)
        {
            if (y != _ySelect)
            {
                _floorGround[_xPreviousSelect, y].SwitchState(Ground.State.NORMAL, 0);
            }
            else
            {
                _floorGround[_xPreviousSelect, y].SwitchState(Ground.State.HIGHLIGHT, 0);
            }
            
        }
    }

    void SelectRow()
    {
        for (int x = 0; x <  width; x++)
        {
            if(x== _xSelect)
            {
                _floorGround[x, _ySelect].SwitchState(Ground.State.SELECTED, 0);
            }
            else
            {
                _floorGround[x, _ySelect].SwitchState(Ground.State.HIGHLIGHT, 0);
            }
            
        }
    }

    void UnSelectRow()
    {
        for (int x = 0; x < width; x++)
        {
            if (x != _xSelect)
            {
                _floorGround[x, _yPreviousSelect].SwitchState(Ground.State.NORMAL, 0);
            }
            else
            {
                _floorGround[x, _yPreviousSelect].SwitchState(Ground.State.HIGHLIGHT, 0);
            }
            
        }
    }
}
