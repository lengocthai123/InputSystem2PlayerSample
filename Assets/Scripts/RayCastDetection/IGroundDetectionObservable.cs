using R3;

namespace Projects.RayCastDetection
{
    /// <summary>
    ///  地面を検出する機能
    /// </summary>
    public interface IGroundDetectionObservable
    {
        public ReadOnlyReactiveProperty<bool> IsGround { get; }
        public ReadOnlyReactiveProperty<bool> IsInAir { get; }
    }
}