using R3;
using UnityEngine;

namespace Projects.RayCastDetection.Core
{
    public interface IRayCastDetection
    {
        /// <summary>
        /// ヒットしたオブジェクト
        /// </summary>
        ReadOnlyReactiveProperty<RaycastHit> HitObject { get; }

        /// <summary>
        /// ヒットしたオブジェクトまでの距離
        /// </summary>
        ReadOnlyReactiveProperty<float> HitDistance { get; }
    }
}