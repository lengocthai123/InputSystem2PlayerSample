using R3;
using R3.Triggers;
using UnityEditor;
using UnityEngine;

namespace Projects.RayCastDetection.Core
{
    public interface IBoxCast2dDetection : IRayCast2dDetection
    {
    }

    /// <summary>
    /// BoxCastの当たり判定を行う機能
    /// </summary>
    public class BoxCast2dDetection : MonoBehaviour, IBoxCast2dDetection
    {
        [SerializeField] private Transform _rayPosition;
        [SerializeField] private Vector2 _raySize = new(1f, 0.1f);
        [SerializeField] private float _rayAngle;
        [SerializeField] private Vector2 _rayDirection = Vector2.down;
        [SerializeField] private float _maxRayDistance = 10;
        [SerializeField] private LayerMask _layerMask = int.MaxValue;

        [SerializeField] private bool _isShowGizmos = true;
        [SerializeField] private Vector3 _labelOffset = Vector3.right;

        private readonly ReactiveProperty<RaycastHit2D> _hitObject = new();
        public ReadOnlyReactiveProperty<RaycastHit2D> HitObject => _hitObject;

        private readonly ReactiveProperty<float> _hitDistance = new();
        public ReadOnlyReactiveProperty<float> HitDistance => _hitDistance;

        protected virtual void Awake()
        {
            _hitObject.AddTo(this);
            _hitDistance.AddTo(this);

            this.FixedUpdateAsObservable()
                .Subscribe(_ =>
                {
                    _hitObject.Value =
                        Physics2D.BoxCast(
                            _rayPosition.position,
                            _raySize,
                            _rayAngle,
                            _rayDirection,
                            _maxRayDistance,
                            _layerMask);

                    _hitDistance.Value =
                        _hitObject.Value
                            ? _hitObject.Value.distance
                            : _maxRayDistance;
                })
                .AddTo(this);
        }

#if UNITY_EDITOR
        /// <summary>
        /// BoxCastの当たり判定を描画
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!_isShowGizmos) return;

            var from = _rayPosition.position;
            var to = from + (Vector3)(_rayDirection * _hitDistance.Value);

            Gizmos.DrawWireCube(from, _raySize);
            Debug.DrawRay(from, _rayDirection * _hitDistance.CurrentValue, Color.red);
            Gizmos.DrawWireCube(to, _raySize);

            Handles.Label(from + _labelOffset, $"{_hitDistance.Value}\n{_hitObject.Value.collider?.gameObject}",
                GUI.skin.box);
        }
#endif
    }
}