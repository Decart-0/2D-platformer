using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    }
}

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _coin;

    private const string AxisHorizontal = "Horizontal";

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _isGrounded = true;
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Platform>()) 
        {
            _isGrounded = true;
        }       

        if (collision.GetComponent<Coin>())
        {
            _coin++;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Platform>()) 
        {
            _isGrounded = false;
        }  
    }

    public void GetCoin(int coin) 
    {
        _coin += coin;
    }

    private void Move()
    {
        float move = Input.GetAxis(AxisHorizontal);

        if (move != 0) 
        {
            _rigidbody.velocity = new Vector2(move * _speed, _rigidbody.velocity.y);
            float angle = move > 0 ? 0 : 180;
            Quaternion targetRotation = Quaternion.Euler(new Vector2(0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed);
        }
        
        Setup(Mathf.Abs(move), _isGrounded);
    }

    private void Jump() 
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void Setup(float speed, bool isGrounded)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, speed);
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
    }
}