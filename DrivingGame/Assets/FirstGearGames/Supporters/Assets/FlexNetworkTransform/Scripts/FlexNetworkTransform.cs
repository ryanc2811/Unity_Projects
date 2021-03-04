using UnityEngine;

namespace FirstGearGames.Mirrors.Assets.FlexNetworkTransforms
{

    [DisallowMultipleComponent]
    public class FlexNetworkTransform : FlexNetworkTransformBase
    {
        protected override Transform TargetTransform => base.transform;
    }
}

