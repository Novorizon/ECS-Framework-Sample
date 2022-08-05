Shader "Star/Effect/Footprint"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent"
        }

        ZWrite Off

        Blend SrcAlpha OneMinusSrcAlpha

		HLSLINCLUDE

		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
 
		CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            half4 _Color;
		CBUFFER_END

        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);

        TEXTURE2D(_CameraDepthTexture);
        SAMPLER(sampler_CameraDepthTexture);

		ENDHLSL

        Pass
        {
            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment Frag

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                float4 viewRayOS : TEXCOORD1;
                float4 cameraPosOSAndFogFactor : TEXCOORD2;
            };

            Varyings Vert(Attributes IN)
            {
                Varyings o;
                VertexPositionInputs vertexPositionInput = GetVertexPositionInputs(IN.positionOS);
                o.positionCS = vertexPositionInput.positionCS;
                o.screenPos = ComputeScreenPos(o.positionCS);
                float3 viewRay = vertexPositionInput.positionVS;
                o.viewRayOS.w = viewRay.z;
                viewRay *= -1;
                float4x4 ViewToObjectMatrix = mul(UNITY_MATRIX_I_M, UNITY_MATRIX_I_V);
                o.viewRayOS.xyz = mul((float3x3)ViewToObjectMatrix, viewRay);
                o.cameraPosOSAndFogFactor.xyz = mul(ViewToObjectMatrix, float4(0,0,0,1)).xyz;
                return o;
            }

            half4 Frag (Varyings IN) : SV_Target
            {
                IN.viewRayOS.xyz /= IN.viewRayOS.w;
                float2 screenSpaceUV = IN.screenPos.xy / IN.screenPos.w;
                float sceneRawDepth = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, screenSpaceUV).r;
                float sceneDepthVS = LinearEyeDepth(sceneRawDepth, _ZBufferParams);

                float3 decalSpaceScenePos = IN.cameraPosOSAndFogFactor.xyz + IN.viewRayOS.xyz * sceneDepthVS;
                float2 decalSpaceUV = decalSpaceScenePos.xy + 0.5;
                clip(0.5 - abs(decalSpaceScenePos));
                float2 uv = decalSpaceUV.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                col *= _Color;
                return col;
            }
            ENDHLSL
        }
    }
}