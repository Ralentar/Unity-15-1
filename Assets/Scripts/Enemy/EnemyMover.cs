using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed = 3;

    private Transform[] _points;
    private int _currentPointIndex;
    private Fliper _fliper;

    private void Awake()
    {
        _fliper = GetComponent<Fliper>();
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
        Vector2 targetWaypoint = _points[_currentPointIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, _speed * Time.deltaTime);

        if (transform.position == (Vector3)targetWaypoint)
            ChangeTarget();
    }

    private void ChangeTarget()
    {
        _currentPointIndex = ++_currentPointIndex % _points.Length;
        Vector2 direction = transform.position - _points[_currentPointIndex].position;
        _fliper.HandleMoveInput(direction);
    }
}