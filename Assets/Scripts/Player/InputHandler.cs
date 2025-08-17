using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const KeyCode MoveLeftKey = KeyCode.A;
    private const KeyCode MoveRightKey = KeyCode.D;
    private const KeyCode JumpKey = KeyCode.Space;

    public event Action<Vector2> Moving;
    public event Action Jumped;

    private void Update()
    {
        Vector2 moveInput = Vector2.zero;

        if (Input.GetKey(MoveLeftKey))
            moveInput.x = -1;

        if (Input.GetKey(MoveRightKey))
            moveInput.x = 1;

        if (Input.GetKey(MoveRightKey) && Input.GetKey(MoveLeftKey))
            moveInput.x = 0;

        Moving?.Invoke(moveInput);

        if (Input.GetKeyDown(JumpKey))
            Jumped?.Invoke();
    }
}