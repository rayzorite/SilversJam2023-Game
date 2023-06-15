using System;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] redBlocks;
    [SerializeField] private GameObject[] blueBlocks;

    private Renderer[] _redBlockRenderers;
    private Collider[] _redBlockColliders;
    private Renderer[] _blueBlockRenderers;
    private Collider[] _blueBlockColliders;

    private void Start()
    {
        _redBlockRenderers = new Renderer[redBlocks.Length];
        _redBlockColliders = new Collider[redBlocks.Length];
        _blueBlockRenderers = new Renderer[blueBlocks.Length];
        _blueBlockColliders = new Collider[blueBlocks.Length];

        for (int i = 0; i < redBlocks.Length; i++)
        {
            _redBlockRenderers[i] = redBlocks[i].GetComponent<Renderer>();
            _redBlockColliders[i] = redBlocks[i].GetComponent<Collider>();
            
            _redBlockColliders[i].enabled = false;

            var tempColor = _redBlockRenderers[i].material.color;
            tempColor.a = 0;
            _redBlockRenderers[i].material.color = tempColor;
        }

        for (int i = 0; i < blueBlocks.Length; i++)
        {
            _blueBlockRenderers[i] = blueBlocks[i].GetComponent<Renderer>();
            _blueBlockColliders[i] = blueBlocks[i].GetComponent<Collider>();
            
            _blueBlockColliders[i].enabled = false;

            var tempColor = _blueBlockRenderers[i].material.color;
            tempColor.a = 0f;
            _blueBlockRenderers[i].material.color = tempColor;
        }
    }

    private void Update()
    {
        if (ColorSwitcher.Instance.isRedColor)
        {
            for (int i = 0; i < redBlocks.Length; i++)
            {
                _redBlockColliders[i].enabled = true;
                _redBlockRenderers[i].material.color = Color.white;
            }

            for (int i = 0; i < blueBlocks.Length; i++)
            {
                _blueBlockColliders[i].enabled = false;

                var tempColor = _blueBlockRenderers[i].material.color;
                tempColor.a = 0f;
                _blueBlockRenderers[i].material.color = tempColor;
            }
        }

        if (ColorSwitcher.Instance.isBlueColor)
        {
            for (int i = 0; i < blueBlocks.Length; i++)
            {
                _blueBlockColliders[i].enabled = true;
                _blueBlockRenderers[i].material.color = Color.white;
            }

            for (int i = 0; i < redBlocks.Length; i++)
            {
                _redBlockColliders[i].enabled = false;

                var tempColor = _redBlockRenderers[i].material.color;
                tempColor.a = 0f;
                _redBlockRenderers[i].material.color = tempColor;
            }
        }
    }
}
