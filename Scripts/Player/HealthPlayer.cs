using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out FirstAidKit firstAidKit)) 
        {
            if (_health < _maxHealth) 
            { 
                _health += firstAidKit.RestoresHealth;
                Destroy(collider.gameObject);

                if (_health > _maxHealth)
                {
                    _health = _maxHealth;
                }
            } 
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}