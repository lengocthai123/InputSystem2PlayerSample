using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 移動機能
/// </summary>
public class MoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _movePower = 10f;
    [SerializeField] private float _maxSpeed = 8f;

    private InputAction _moveAction;
    private readonly ReactiveProperty<Vector2> _moveDirection = new();

    private void Start()
    {
        _moveDirection.AddTo(this);

        // 最大移動速度設定
        _rb.maxLinearVelocity = _maxSpeed;

        // FixedUpdate時にUpdateVelocityメソッドを実行
        this.FixedUpdateAsObservable()
            .Subscribe(_ => UpdateVelocity())
            .AddTo(this);
    }

    /// <summary>
    ///  移動入力状態更新
    /// </summary>
    /// <param name="value"></param>
    private void OnMove(InputValue value) =>
        _moveDirection.Value = value.Get<Vector2>();

    /// <summary>
    ///  移動処理
    /// </summary>
    private void UpdateVelocity()
    {
        var direction = new Vector3(_moveDirection.Value.x, 0, _moveDirection.Value.y);
        _rb.linearVelocity += Time.deltaTime * _movePower * direction;
    }
}