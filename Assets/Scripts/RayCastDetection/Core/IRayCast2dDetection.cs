using R3;
using UnityEngine;

namespace Projects.RayCastDetection.Core
{
    public interface IRayCast2dDetection
    {
        /// <summary>
        /// ヒットしたオブジェクト
        /// </summary>
        ReadOnlyReactiveProperty<RaycastHit2D> HitObject { get; }

        /// <summary>
        /// ヒットしたオブジェクトまでの距離
        /// </summary>
        ReadOnlyReactiveProperty<float> HitDistance { get; }
    }
}