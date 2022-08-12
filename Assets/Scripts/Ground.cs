using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Material _normalMaterial;
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private Material _selectedMaterial;

    private Transform _thisTransform;
    private Vector3 _position;

    public enum State { NORMAL, HIGHLIGHT, SELECTED };

    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        SwitchState(State.NORMAL, Random.Range(0f, 1f));
        _thisTransform = this.GetComponent<Transform>();
        _position = _thisTransform.position;
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
