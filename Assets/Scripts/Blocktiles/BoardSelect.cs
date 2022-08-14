using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] _groundGameObject;
    private int height; // to the right
    private int width; // to the left

    // Level design
    // 0 = empty
    // 1 = earth tile
    // 2 = grass tile
    // 3 = sidewalk tile
    // 4 = bikepath tile
    // 5 = road tile
    // 6 = crossing of bikepath and sidewalk
    // 7 = crossing of bikepath and road
    // 8 = crossing of road and sidewalk
    private int[,] _floorplan =
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,1,5,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,1,5,5,5,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,1,5,5,5,5,1,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,1,5,5,5,5,1,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,1,5,5,5,5,1,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,1,5,5,5,5,1,2,2,2,2,3,3,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,1,5,5,5,5,1,2,2,2,2,3,3,2,2,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,1,5,5,5,5,1,2,2,2,2,3,3,2,2,2,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,1,5,5,5,5,1,2,2,2,2,3,3,2,2,2,2,2,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,8,8,8,8,3,3,3,3,3,3,3,2,2,2,2,2,2,2,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,8,8,8,8,3,3,3,3,3,3,3,2,2,2,2,2,2,2,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,1,5,5,5,5,1,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,1,5,5,5,5,1,2,6,6,2,2,2,2,2,2,2,2,2,2,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,1,5,5,5,5,1,2,6,6,2,2,2,2,2,2,2,2,2,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,1,5,5,5,5,5,5,1,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,5,5,5,5,5,5,5,5,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,5,5,5,5,5,5,5,5,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,5,5,5,5,5,5,1,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,4,4,4,4,4,4,4,4,4,4,4,2,2,2,2,1,5,5,5,5,1,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,4,4,4,4,4,4,4,4,4,4,4,4,4,2,2,2,1,5,5,5,5,1,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,2,2,2,1,5,5,5,5,1,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,2,2,2,2,2,2,2,2,2,2,4,4,4,4,2,2,2,1,5,5,5,5,1,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,2,2,2,2,2,2,2,2,2,2,2,2,4,4,4,2,2,2,1,5,5,5,5,1,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,2,2,2,2,2,2,2,2,2,2,2,2,4,4,4,2,2,1,5,5,5,5,5,1,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,1,1,5,5,5,5,5,5,1,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {5,5,5,5,5,5,5,5,5,5,5,5,5,7,7,7,5,5,5,5,5,5,5,1,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {5,5,5,5,5,5,5,5,5,5,5,5,5,7,7,7,5,5,5,5,5,5,5,1,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,5,5,5,5,5,5,5,5,5,5,5,7,7,7,5,5,5,5,5,5,1,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,5,5,5,5,5,5,5,5,5,7,7,7,5,5,5,5,1,1,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,1,1,1,1,1,1,1,1,4,4,4,1,1,1,1,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,2,2,2,2,2,4,4,4,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,2,2,2,2,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,2,2,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,2,2,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
    };
    //private int[,] _floorplan =
    //{
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {1,1,1,3,1,4,4,1,1,1 },
    //    {2,2,2,5,2,7,7,2,2,2 },
    //    {1,1,1,3,1,4,4,1,1,1 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {1,1,1,3,1,4,4,1,1,1 },
    //    {4,4,4,6,4,4,4,4,4,4 },
    //    {4,4,4,6,4,4,4,4,4,4 },
    //    {1,1,1,3,1,4,4,1,1,1 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //    {0,0,1,3,1,4,4,1,0,0 },
    //};

    // Arrays for the floor
    private GameObject[,] _floorGameObject;
    private Ground[,] _floorGround;

    // Selected tile position
    private int _xSelect = 1;
    private int _ySelect = 1;
    private int _xPreviousSelect = 0;
    private int _yPreviousSelect = 0;

    // Used for the animation of the block tiles
    private float _elapsedTime;
    private float _timeStep = 0.02f;
    private bool _isMoving = false;
    private float _movingTime;
    private float _movingDelay = 0.2f;

private enum State { };

    // Start the creation of the floor
    void Start()
    {
        FillFloor();
    }

    // FixedUpdate is called 100 times per second
    // Badly writen routine to control the speed of the selector 
    // make the first moments of movement 3 times as slow
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

    // create the different block tiles and create an array for it
    void FillFloor()
    {
        width = _floorplan.GetLength(0);
        height = _floorplan.GetLength(1);
        _floorGameObject = new GameObject[width, height];
        _floorGround = new Ground[width, height];
        //Instantiate(_groundGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                _floorGameObject[x, y] = (GameObject)Instantiate(_groundGameObject[_floorplan[x,y]], new Vector3(x, 0, y), Quaternion.identity);
                _floorGameObject[x, y].transform.SetParent(transform);
                _floorGround[x, y] = _floorGameObject[x,y].GetComponent<Ground>();
            }
        }
    }

    // Moving the selector on the ground, assigned to the WASD keys
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

    // Select a column and calling a function of each block in the column
    void SelectColumn()
    {
        for(int y = 0; y < height; y++)
        {
            if(_floorGameObject[_xSelect, y].name != "Empty(Clone)")
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
    }

    // Unselect a column and calling a function of each block in the column
    void UnSelectColumn()
    {
        for (int y = 0; y < height; y++)
        {
            if (_floorGameObject[_xPreviousSelect, y].name != "Empty(Clone)")
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
    }

    // Select a row and calling a function of each block in the row
    void SelectRow()
    {
        for (int x = 0; x <  width; x++)
        {
            if (_floorGameObject[x, _ySelect].name != "Empty(Clone)")
            {
                if (x == _xSelect)
                {
                    _floorGround[x, _ySelect].SwitchState(Ground.State.SELECTED, 0);
                }
                else
                {
                    _floorGround[x, _ySelect].SwitchState(Ground.State.HIGHLIGHT, 0);
                }
            }
               
            
        }
    }

    // Unselect a row and calling a function of each block in the row
    void UnSelectRow()
    {
        for (int x = 0; x < width; x++)
        {
            if (_floorGameObject[x, _yPreviousSelect].name != "Empty(Clone)")
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
}
