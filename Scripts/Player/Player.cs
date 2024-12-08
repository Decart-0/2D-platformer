using System;
using System.Collections;
using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(InputScheme))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldownAttack;
    [SerializeField] private float _attackRadius;
    [SerializeField] private Transform _attackPosition;

    private InputScheme _inputScheme;
    private bool _isCooldown;

    public event Action OnAttacked;

    public bool IsAttack { get; private set; }

    private void Awake()
    {
        _inputScheme = GetComponent<InputScheme>();
        _isCooldown = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_inputScheme.Attack) && _isCooldown == false) 
        {
            StartAttack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRadius);
    }

    private void Attack() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRadius);

        foreach (Collider2D collider in colliders) 
        {
            if (collider.TryGetComponent(out Ghost ghost)) 
            {
                ghost.TakeDamage(_damage);
            }  
        }
    }

    private void StartAttack()
    {   
        _isCooldown = true;
        IsAttack = true;
        OnAttacked?.Invoke();
        Attack();
        StartCoroutine(WaitAttack());
    }

    private IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(_cooldownAttack);
        
        _isCooldown = false;
        IsAttack = false;
        OnAttacked?.Invoke();  
    }
}