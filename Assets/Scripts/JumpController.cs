using Projects.RayCastDetection;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  ジャンプ機能
/// </summary>
public class JumpController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _jumpPower = 10f;
    [SerializeField] private GroundDetection _groundDetection;

    private InputAction _jumpAction;
    private bool _canJump;
    private readonly ReactiveProperty<bool> _tryJump = new();

    private void Start()
    {
        _tryJump.AddTo(this);

        // 一度でも地面に接地したらジャンプ可能
        _groundDetection.IsGround
            .Where(x => x)
            .Subscribe(_ => _canJump = true)
            .AddTo(this);

        // ジャンプが入力されたらJumpメソッドを実行
        _tryJump
            .Where(x => x && _canJump)
            .Subscribe(_ =>
            {
                _canJump = false;
                JumpStart();
            })
            .AddTo(this);
    }

    /// <summary>
    ///  ジャンプ入力状態更新
    /// </summary>
    /// <param name="value"></param>
    private void OnJump(InputValue value) =>
        _tryJump.Value = value.isPressed;


    /// <summary>
    ///  ジャンプ処理
    /// </summary>
    private void JumpStart()
        => _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
}