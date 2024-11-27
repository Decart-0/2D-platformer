using UnityEngine;

public class Ground : MonoBehaviour
{
    private int _entryCount;
    private int _exitCount;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>()) 
        { 
            _entryCount++;
            collider.GetComponent<Player>().GetIsGround(IsOnGround());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>()) 
        {
            _exitCount++;
            collider.GetComponent<Player>().GetIsGround(IsOnGround());
        }     
    }

    private bool IsOnGround()
    {
        return _entryCount > _exitCount;
    }
}