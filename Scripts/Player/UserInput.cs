using UnityEngine;

public class UserInput : MonoBehaviour
{
    [field: SerializeField] public KeyCode Jump { get; private set; }

    [field: SerializeField] public KeyCode Attack { get; private set; }

    public string AxisHorizontal { get; private set; }

    private void Awake()
    {
        AxisHorizontal = "Horizontal";
    }
}