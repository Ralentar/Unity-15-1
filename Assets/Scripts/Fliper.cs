using UnityEngine;

public class Fliper : MonoBehaviour
{
    private InputHandler _inputHandler;

    private void Awake()
    {
        if (TryGetComponent(out InputHandler inputHandler))
            _inputHandler = inputHandler;
    }

    private void OnEnable()
    {
        if (_inputHandler != null)
            _inputHandler.Moving += HandleMoveInput;
    }

    private void OnDisable()
    {
        if (_inputHandler != null)
            _inputHandler.Moving -= HandleMoveInput;
    }

    public void HandleMoveInput(Vector2 moveInput)
    {
        Rotate(moveInput);
    }

    private void Rotate(Vector2 direction)
    {
        float rotationAngle = 180;

        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
    }
}