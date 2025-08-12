using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Coin : MonoBehaviour
{
    public event Action<Vector2> Collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collected?.Invoke(transform.position);
        Destroy(gameObject);
    }
}