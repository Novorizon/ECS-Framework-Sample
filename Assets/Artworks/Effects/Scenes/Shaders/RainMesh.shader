Shader "Star/Effect/RainMesh" {
	Properties {
		_MainTex ("MainTex", 2D) = "white" { }
	}
	SubShader {
		Tags 
        { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent"
        }

		HLSLINCLUDE
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
 
		CBUFFER_START(UnityPerMaterial)
		float4 _MainTex_ST;
		CBUFFER_END
		ENDHLSL

		Pass {
			Blend SrcAlpha One

			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);

			Varyings Vert(Attributes IN)
			{
				Varyings OUT;

				OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
				OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
				return OUT;
			}

			half4 Frag(Varyings IN) : SV_Target
			{
				half4 baseMap = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
				return baseMap;
			}
			ENDHLSL
		}
	}
}