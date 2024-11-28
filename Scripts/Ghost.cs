using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _waitTime;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private int _currentPointIndex;

    private Transform[] _targetPoints;
    private bool _isWaiting;

    private void Awake()
    {
        _targetPoints = new Transform[_targetPoint.childCount];
        _isWaiting = false;

        for (int i = 0; i < _targetPoints.Length; i++)
        {
            _targetPoints[i] = _targetPoint.GetChild(i);
        }
    }

    private void Start()
    {      
        if (_targetPoints.Length > 0)
            transform.position = _targetPoints[_currentPointIndex].position;
    }

    private void Update()
    {
        if (_targetPoints.Length > 0)
            Move();
    }

    private void Move()
    {
        Transform targetPoint = _targetPoints[_currentPointIndex];
        float targetX = Mathf.MoveTowards(transform.position.x, targetPoint.position.x, _speed * Time.deltaTime);
        transform.position = new Vector2(targetX, transform.position.y);

        Vector2 direction = (targetPoint.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg > 0 ? 0 : 180;
        Quaternion rotation = Quaternion.Euler(new Vector2(0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _speedRotate);

        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.1f && !_isWaiting)
        {
            StartCoroutine(WaitAtPoint());
        }        
    }

    private IEnumerator WaitAtPoint()
    {
        _isWaiting = true;

        yield return new WaitForSeconds(_waitTime);

        _currentPointIndex = (_currentPointIndex + 1) % _targetPoints.Length;
        _isWaiting = false;
    }
}
