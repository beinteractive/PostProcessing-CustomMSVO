Shader "Hidden/PostProcessing/Custom/MSVO"
{
    HLSLINCLUDE

        #pragma exclude_renderers gles gles3 d3d11_9x
        #pragma target 4.5

        #include "PostProcessing/Shaders/StdLib.hlsl"
        #include "PostProcessing/Shaders/Builtins/Fog.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        TEXTURE2D_SAMPLER2D(_MSVOcclusionTexture, sampler_MSVOcclusionTexture);
        float3 _AOColor;

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        // Composite to the frame buffer
        Pass
        {
            HLSLPROGRAM

                #pragma multi_compile _ APPLY_FORWARD_FOG
                #pragma multi_compile _ FOG_LINEAR FOG_EXP FOG_EXP2
                #pragma multi_compile _ CUSTOM_COMPOSITION
                #pragma vertex VertDefault
                #pragma fragment Frag

                float4 Frag(VaryingsDefault i) : SV_Target
                {
                    float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
                    half ao = 1.0 - SAMPLE_TEXTURE2D(_MSVOcclusionTexture, sampler_MSVOcclusionTexture, i.texcoordStereo).r;
                    
                    // Apply fog when enabled (forward-only)
                #if (APPLY_FORWARD_FOG)
                    float d = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoordStereo));
                    d = ComputeFogDistance(d);
                    ao *= ComputeFog(d);
                #endif
                    
                    #ifdef CUSTOM_COMPOSITION
                    return float4((color.rgb + ((1.0).xxx - _AOColor) * ao) * (1.0 - ao), color.a);
                    #else
                    return float4(color.rgb * ((1.0).xxx - ao * _AOColor), color.a);
                    #endif
                }

            ENDHLSL
        }
    }
}