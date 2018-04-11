using UnityEngine;

namespace CustomMSVO.Internal
{
    static class ShaderIDs
    {
        internal static readonly int AOParams                        = Shader.PropertyToID("_AOParams");
        internal static readonly int AOColor                         = Shader.PropertyToID("_AOColor");
        internal static readonly int OcclusionTexture1               = Shader.PropertyToID("_OcclusionTexture1");
        internal static readonly int OcclusionTexture2               = Shader.PropertyToID("_OcclusionTexture2");
        internal static readonly int SAOcclusionTexture              = Shader.PropertyToID("_SAOcclusionTexture");
        internal static readonly int MSVOcclusionTexture             = Shader.PropertyToID("_MSVOcclusionTexture");
        internal static readonly int DepthCopy                       = Shader.PropertyToID("DepthCopy");
        internal static readonly int LinearDepth                     = Shader.PropertyToID("LinearDepth");
        internal static readonly int LowDepth1                       = Shader.PropertyToID("LowDepth1");
        internal static readonly int LowDepth2                       = Shader.PropertyToID("LowDepth2");
        internal static readonly int LowDepth3                       = Shader.PropertyToID("LowDepth3");
        internal static readonly int LowDepth4                       = Shader.PropertyToID("LowDepth4");
        internal static readonly int TiledDepth1                     = Shader.PropertyToID("TiledDepth1");
        internal static readonly int TiledDepth2                     = Shader.PropertyToID("TiledDepth2");
        internal static readonly int TiledDepth3                     = Shader.PropertyToID("TiledDepth3");
        internal static readonly int TiledDepth4                     = Shader.PropertyToID("TiledDepth4");
        internal static readonly int Occlusion1                      = Shader.PropertyToID("Occlusion1");
        internal static readonly int Occlusion2                      = Shader.PropertyToID("Occlusion2");
        internal static readonly int Occlusion3                      = Shader.PropertyToID("Occlusion3");
        internal static readonly int Occlusion4                      = Shader.PropertyToID("Occlusion4");
        internal static readonly int Combined1                       = Shader.PropertyToID("Combined1");
        internal static readonly int Combined2                       = Shader.PropertyToID("Combined2");
        internal static readonly int Combined3                       = Shader.PropertyToID("Combined3");
        
        internal static readonly int FogParams                       = Shader.PropertyToID("_FogParams");
    }
}