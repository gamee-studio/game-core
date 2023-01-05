using System;

namespace DG.Tweening
{
    public enum EEasingType
    {
        InBack,
        OutBack,
        InOutBack,

        InBounce,
        OutBounce,
        InOutBounce,

        InCirc,
        OutCirc,
        InOutCirc,

        InCubic,
        OutCubic,
        InOutCubic,

        InElastic,
        OutElastic,
        InOutElastic,

        InExpo,
        OutExpo,
        InOutExpo,

        Linear,

        InQuad,
        OutQuad,
        InOutQuad,

        InQuart,
        OutQuart,
        InOutQuart,

        InQuint,
        OutQuint,
        InOutQuint,

        InSine,
        OutSine,
        InOutSine,

        AnimationCurve,
    }


    public static class EasingType
    {
        public static Ease Interpolate(this EEasingType type)
        {
            switch (type)
            {
                case EEasingType.InBack:
                    return Ease.InBack;
                case EEasingType.OutBack:
                    return Ease.OutBack;
                case EEasingType.InOutBack:
                    return Ease.InOutBack;
                case EEasingType.InBounce:
                    return Ease.InBounce;
                case EEasingType.OutBounce:
                    return Ease.OutBounce;
                case EEasingType.InOutBounce:
                    return Ease.InOutBounce;
                case EEasingType.InCirc:
                    return Ease.InCirc;
                case EEasingType.OutCirc:
                    return Ease.OutCirc;
                case EEasingType.InOutCirc:
                    return Ease.InOutCirc;
                case EEasingType.InCubic:
                    return Ease.InCubic;
                case EEasingType.OutCubic:
                    return Ease.OutCubic;
                case EEasingType.InOutCubic:
                    return Ease.InOutCubic;
                case EEasingType.InElastic:
                    return Ease.InElastic;
                case EEasingType.OutElastic:
                    return Ease.OutElastic;
                case EEasingType.InOutElastic:
                    return Ease.InOutElastic;
                case EEasingType.InExpo:
                    return Ease.InExpo;
                case EEasingType.OutExpo:
                    return Ease.OutExpo;
                case EEasingType.InOutExpo:
                    return Ease.InOutExpo;
                case EEasingType.Linear:
                    return Ease.Linear;
                case EEasingType.InQuad:
                    return Ease.InQuad;
                case EEasingType.OutQuad:
                    return Ease.OutQuad;
                case EEasingType.InOutQuad:
                    return Ease.InOutQuad;
                case EEasingType.InQuart:
                    return Ease.InQuart;
                case EEasingType.OutQuart:
                    return Ease.OutQuart;
                case EEasingType.InOutQuart:
                    return Ease.InOutQuart;
                case EEasingType.InQuint:
                    return Ease.InQuint;
                case EEasingType.OutQuint:
                    return Ease.OutQuint;
                case EEasingType.InOutQuint:
                    return Ease.InOutQuint;
                case EEasingType.InSine:
                    return Ease.InSine;
                case EEasingType.OutSine:
                    return Ease.OutSine;
                case EEasingType.InOutSine:
                    return Ease.InOutSine;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}