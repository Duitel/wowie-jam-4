using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Material _normalMaterial;
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private Material _selectedMaterial;

    private Transform _thisTransform;
    private Vector3 _position;
    private float _startHeight = 5f;

    public enum State { NORMAL, HIGHLIGHT, SELECTED };

    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _thisTransform = this.GetComponent<Transform>();
        _thisTransform.position = new Vector3(_thisTransform.position.x, -0.5f, _thisTransform.position.z);
        _position = _thisTransform.position;
        SwitchPosition(_position, Random.Range(0.05f, 0.5f) + 0.01f * _position.z * _position.x);
        

    }

    private void SwitchPosition(Vector3 _targetPosition, float _duration = 0)
    {
        StartCoroutine(SwitchMoving(_targetPosition, _duration));
    }

    IEnumerator SwitchMoving(Vector3 _targetPosition, float _duration)
    {
        Vector3 _currentPosisition = _thisTransform.position;
        Vector3 _startPosition = _currentPosisition + Vector3.up * _startHeight;
        _thisTransform.position = _startPosition;
        float _timeElapsed = 0;
        while (_timeElapsed < _duration)
        {
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _timeElapsed / _duration);
            _timeElapsed += Time.deltaTime;
            yield return null;
        }
        _thisTransform.position = _targetPosition;
        SwitchState(State.NORMAL, 0.5f);
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        BeginState(newState);
    }

    private void BeginState(State newState)
    {
        switch (newState)
        {
            case State.NORMAL:
                _renderer.sharedMaterial = _normalMaterial;
                _thisTransform.position = _position;
                break;
            case State.HIGHLIGHT:
                _renderer.sharedMaterial = _highlightMaterial;
                _thisTransform.position = _position + Vector3.up * 0.05f;
                break;
            case State.SELECTED:
                _renderer.sharedMaterial = _selectedMaterial;
                
                _thisTransform.position = _position + Vector3.up * 0.2f;
                break;
        }
    }
}
