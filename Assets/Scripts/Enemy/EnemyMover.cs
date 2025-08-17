using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class EnemyMover : MonoBehaviour
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

        Reflect();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 targetWaypoint = _points[_currentPointIndex].position;

        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, _speed * Time.deltaTime);

        if (transform.position == (Vector3)targetWaypoint)
            ChangeTarget(); 
    }

    private void ChangeTarget()
    {
        _currentPointIndex = ++_currentPointIndex % _points.Length;
        Reflect();
    }


    private void Reflect()
    {
        Vector2 direction = _points[_currentPointIndex].position - transform.position;
        _sprite.flipX = direction.x > 0;
    }
}