using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Material _normalMaterial;
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private Material _selectedMaterial;

    public enum State { NORMAL, HIGHLIGHT, SELECTED };

    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        SwitchState(State.NORMAL, Random.Range(0f, 1f));
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
                break;
            case State.HIGHLIGHT:
                _renderer.sharedMaterial = _highlightMaterial;
                break;
            case State.SELECTED:
                _renderer.sharedMaterial = _selectedMaterial;
                break;
        }
    }
}
