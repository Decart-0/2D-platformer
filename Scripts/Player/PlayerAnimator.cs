using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DetectorGround))]
[RequireComponent(typeof(PlayerMotionController))]
[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private DetectorGround _detectorGround;
    private PlayerMotionController _motionController;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _detectorGround = GetComponent<DetectorGround>();
        _motionController = GetComponent<PlayerMotionController>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _detectorGround.GroundStatusChanged += Setup;
        _motionController.OnMoved += Setup;
        _player.OnAttacked += SetupAttack;
    }

    private void OnDisable()
    {
        _detectorGround.GroundStatusChanged -= Setup;
        _motionController.OnMoved -= Setup;
        _player.OnAttacked -= SetupAttack;
    }

    private void Setup()
    {
        _animator.SetFloat(AnimatorData.Params.Speed, Mathf.Abs(_motionController.CurrentMove));
        _animator.SetBool(AnimatorData.Params.IsGrounded, _detectorGround.IsOnGround);
    }

    private void SetupAttack()
    {
        if (_player.IsAttack)
        {
            _animator.SetTrigger(AnimatorData.Params.Attack);
        }
    }
}