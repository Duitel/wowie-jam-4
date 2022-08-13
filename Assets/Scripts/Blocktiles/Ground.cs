using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // the 3 materials to show the state the block tile is in
    [SerializeField] private Material _normalMaterial;
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private Material _selectedMaterial;

    // used for vertical movemnt of the block tile
    private Vector3 _position;
    private float _startHeight = 5f;

    // the different states for the block tile
    public enum State { NORMAL, HIGHLIGHT, SELECTED };

    // needed to change the material of a GameObject
    private Renderer _renderer;

    // Get access to the Material and position of this GameObject
    // And begin the initiation animation of the block tiles
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        _position = transform.position;
        SwitchPosition(_position, Random.Range(0.05f, 0.5f) + 0.01f * _position.z * _position.x);
    }

    // start seperate thread for the movement of the block tile
    private void SwitchPosition(Vector3 _targetPosition, float _duration = 0)
    {
        StartCoroutine(SwitchMoving(_targetPosition, _duration));
    }

    // Linearly move the block from current position to target position using the time defined by _duration
    IEnumerator SwitchMoving(Vector3 _targetPosition, float _duration)
    {
        Vector3 _currentPosisition = transform.position;
        Vector3 _startPosition = _currentPosisition + Vector3.up * _startHeight;
        transform.position = _startPosition;
        float _timeElapsed = 0;
        while (_timeElapsed < _duration)
        {
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _timeElapsed / _duration);
            _timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = _targetPosition;
        SwitchState(State.NORMAL, 0.5f);
    }

    // Change the state of the block tile by calling a seperate thread for possible use of a delay
    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    // Changing the state of the block tile after a delay
    IEnumerator SwitchDelay(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        BeginState(newState);
    }

    // Change the block tile according to the state of the block tile
    private void BeginState(State newState)
    {
        switch (newState)
        {
            case State.NORMAL:
                _renderer.sharedMaterial = _normalMaterial;
                transform.position = _position;
                break;
            case State.HIGHLIGHT:
                _renderer.sharedMaterial = _highlightMaterial;
                transform.position = _position + Vector3.up * 0.05f;
                break;
            case State.SELECTED:
                _renderer.sharedMaterial = _selectedMaterial;

                transform.position = _position + Vector3.up * 0.2f;
                break;
        }
    }
}
