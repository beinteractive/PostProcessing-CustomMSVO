using System;
using CustomMSVO.Internal;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace CustomMSVO
{
    [Serializable]
    [PostProcess(typeof(MultiScaleVolumetricObscuranceRenderer), PostProcessEvent.BeforeTransparent, "Custom/Multi-scale Volumetric Obscurance")]
    public sealed class MultiScaleVolumetricObscurance : PostProcessEffectSettings
    {
        private static bool IsRenderTextureSpecSatisfiedResoved;
        private static bool IsRenderTextureSpecSatisfied;
        
        [Range(0f, 4f), Tooltip("Degree of darkness added by ambient occlusion.")]
        public FloatParameter intensity = new FloatParameter { value = 0f };

        [Range(1f, 10f), Tooltip("Modifies thickness of occluders. This increases dark areas but also introduces dark halo around objects.")]
        public FloatParameter thicknessModifier = new FloatParameter { value = 1f };

        [ColorUsage(false), Tooltip("Custom color to use for the ambient occlusion.")]
        public ColorParameter color = new ColorParameter { value = Color.black };

        [Range(0.1f, 1f)]
        public FloatParameter renderScale = new FloatParameter { value = 1f };

        public IntParameter downsampleThreshold = new IntParameter { value = 1100000 };

        [Range(0.1f, 1f)]
        public FloatParameter downsample = new FloatParameter { value = 1f };

        [Range(-8f, 0f)]
        public FloatParameter noiseFilterTolerance = new FloatParameter { value = 0f }; // Hidden

        [Range(-8f, -1f)]
        public FloatParameter blurTolerance = new FloatParameter { value = -4.6f }; // Hidden

        [Range(-12f, -1f)]
        public FloatParameter upsampleTolerance = new FloatParameter { value = -12f }; // Hidden
        
        // SRPs can call this method without a context set (see HDRP)
        // We need a better way to handle this than checking for a null context, context should
        // never be null.
        public override bool IsEnabledAndSupported(PostProcessRenderContext context)
        {
            bool state = enabled.value
                && intensity.value > 0f;

#if UNITY_2017_1_OR_NEWER
            if (context?.resources != null)
            {
                state &= context.resources.shaders.multiScaleAO
                      && context.resources.shaders.multiScaleAO.isSupported
                      && context.resources.computeShaders.multiScaleAODownsample1
                      && context.resources.computeShaders.multiScaleAODownsample2
                      && context.resources.computeShaders.multiScaleAORender
                      && context.resources.computeShaders.multiScaleAOUpsample;
            }

            if (!IsRenderTextureSpecSatisfiedResoved)
            {
                IsRenderTextureSpecSatisfiedResoved = true;
                IsRenderTextureSpecSatisfied =
                    SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RFloat) &&
                    SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RHalf) &&
                    SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8);
            }

            state &= SystemInfo.supportsComputeShaders
                  && !RuntimeUtilities.isAndroidOpenGL
                  && IsRenderTextureSpecSatisfied;
#else
            state = false;
#endif

            return state;
        }
    }

    public sealed class MultiScaleVolumetricObscuranceRenderer : PostProcessEffectRenderer<MultiScaleVolumetricObscurance>
    {
        private MSVO Method;

        public override void Init()
        {
            if (Method == null)
            {
                Method = new MSVO(settings);
            }
        }

        public override void Release()
        {
            Method?.Release();
        }
        
        public override void Render(PostProcessRenderContext context)
        {
            Method?.RenderAfterOpaque(context);
        }
    }
}