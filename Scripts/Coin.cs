using UnityEngine;

public class Coin : MonoBehaviour 
{
    [SerializeField] private int _denomination;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>()) 
        { 
            collider.GetComponent<Player>().GetCoin(_denomination);
            Destroy(gameObject);
        } 
    }
}