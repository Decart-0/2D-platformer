using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DetectorPlayer))]
public class Ghost : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldownAttack;

    private DetectorPlayer _detectorPlayer;
    
    private void Awake()
    {
        _detectorPlayer = GetComponent<DetectorPlayer>();
    }

    private void OnEnable()
    {
        _detectorPlayer.OnAttacked += Attake;
    }

    private void Update()
    {
        if (_health <= 0)
            Destroy(gameObject);
    }

    private void OnDisable()
    {
        _detectorPlayer.OnAttacked -= Attake;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    private void Attake() 
    {
        StartCoroutine(WaitAttack());
    }

    private IEnumerator WaitAttack()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_cooldownAttack);

        while (_detectorPlayer.IsAttack)
        {
            _detectorPlayer.HealthPlayer.TakeDamage(_damage);

            yield return waitForSeconds;
        }
    }
}