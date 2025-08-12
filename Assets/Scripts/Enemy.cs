using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed = 3;

    private Transform[] _points;
    private int _currentPointIndex;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distance = 0.1f;
        Transform target = _points[_currentPointIndex];

        Reflect();

        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < distance)
            _currentPointIndex = ++_currentPointIndex % _points.Length;
    }

    private void Reflect()
    {
        Vector2 direction = _points[_currentPointIndex].position - transform.position;
        _sprite.flipX = direction.x > 0;
    }
}