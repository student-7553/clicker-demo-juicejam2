using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    [SerializeField]
    private bool _isActive;

    public bool isAlive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    private void Awake()
    {
        _isActive = false;
    }
}
