using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _coin;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Coin coin))
        {
            _coin += coin.Denomination;
            Destroy(collider.gameObject);
        }
    }
}