using System;
using UnityEngine;

public class DetectorEarth : MonoBehaviour
{
    private int _entryCount;
    private int _exitCount;

    public event Action GroundStatusChanged;

    public bool IsOnGround => _entryCount > _exitCount;

    public void OnGroundEnter()
    {
        _entryCount++;
        GroundStatusChanged?.Invoke();
    }

    public void OnGroundExit()
    {
        _exitCount++;
        GroundStatusChanged?.Invoke();
    }
}