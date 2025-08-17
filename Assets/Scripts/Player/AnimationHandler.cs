using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    private readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));

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