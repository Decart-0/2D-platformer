using System;
using UnityEngine;

public class DetectorPlayer : MonoBehaviour
{
    private int _numberEnters;

    public event Action OnAttacked;

    public Transform PositionPlayer { get; private set; }

    public HealthPlayer HealthPlayer { get; private set; }

    public bool IsVisiblePlayer { get; private set; }

    public bool IsAttack { get; private set; }

    private void Awake()
    {
        IsVisiblePlayer = false;
        IsAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {     
        if (collider.TryGetComponent(out Player player))
        {
            if (_numberEnters > 0) 
            {
                IsAttack = true;
                HealthPlayer = player.GetComponent<HealthPlayer>();
                OnAttacked?.Invoke();
                _numberEnters++;
            }
            else
            { 
                IsVisiblePlayer = true;
                PositionPlayer = player.transform;
                _numberEnters++;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>())
        {
            if (_numberEnters > 1)
            {
                IsAttack = false;
                OnAttacked?.Invoke();
                _numberEnters--;
            }
            else 
            {
                IsVisiblePlayer = false;
                _numberEnters--;
            }
        }
    }
}