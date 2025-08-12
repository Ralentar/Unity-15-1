using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private const string IsJumping = "isJumping";
    private const string IsRunning = "isRunning";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateJumpAnimation(bool isJumping)
    {
        _animator.SetBool(IsJumping, isJumping);
    }

    public void UpdateRunAnimation(bool isRunning)
    {
        _animator.SetBool(IsRunning, isRunning);
    }
}