using Projects.RayCastDetection.Core;
using R3;
using UnityEngine;

namespace Projects.RayCastDetection
{
    /// <summary>
    /// 地面を検出する機能(2D)
    /// </summary>
    public class Ground2dDetection : LineCastDetection, IGroundDetectionObservable
    {
        [Header("地面までの距離がこの値以下なら地面と判定")]
        [SerializeField] private float _isGroundThreshold = 0.01f;

        public ReadOnlyReactiveProperty<bool> IsGround =>
            HitDistance.Select(v => v < _isGroundThreshold).ToReadOnlyReactiveProperty();
        
        public ReadOnlyReactiveProperty<bool> IsInAir =>
            HitDistance.Select(v => v > _isGroundThreshold).ToReadOnlyReactiveProperty();
    }
}