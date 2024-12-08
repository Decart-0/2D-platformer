using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DetectorPlayer))]
public class GhostAnimator : MonoBehaviour
{
    private Animator _animator;
    private DetectorPlayer _detectorPlayer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _detectorPlayer = GetComponent<DetectorPlayer>();
    }

    private void OnEnable()
    {
        _detectorPlayer.OnAttacked += Setup;
    }

    private void OnDisable()
    {
        _detectorPlayer.OnAttacked -= Setup;
    }

    private void Setup()
    {
        _animator.SetBool(AnimatorData.Params.IsAttacked, _detectorPlayer.IsAttack);
    }
}