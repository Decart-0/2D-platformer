using UnityEngine;

public class UserInput : MonoBehaviour
{
    [field: SerializeField] public KeyCode Jump { get; private set; }

    public string AxisHorizontal { get; private set; }

    private void Awake()
    {
        AxisHorizontal = "Horizontal";
    }
}