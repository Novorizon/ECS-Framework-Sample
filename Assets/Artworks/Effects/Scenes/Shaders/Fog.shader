Shader "Star/Effect/Fog" {
	Properties {
		
		_MainTex ("MainTex", 2D) = "white" {}
		_CloudTex2 ("CloudTex2", 2D) = "white" {}
		_MaskTex ("MaskTex", 2D) = "white" {}
		[Header(Color)]
		[Space(5)]
		_Color ("MainColor", Color) = (0, 1, 1, 1)
		_ColorOffset ("ColorOffset", Vector) = (1,1,1,1)
		[Header(Edge)]
		[Space(5)]
		_MaskEdgeA ("MaskEdgeA", Float) = 1
		_MaskEdgeB ("MaskEdgeB", Float) = 1
		_AlphaScale ("AlphaScale", Float) = 1
	}
	SubShader {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Stencil 
		{
			Ref 3
			Comp Always
			Pass Replace
		}

		HLSLINCLUDE
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
 
		CBUFFER_START(UnityPerMaterial)
		float4 _MainTex_ST;
		float4 _CloudTex2_ST;
		float4 _Color;
		float _MaskEdgeA;
		float _MaskEdgeB;
		float _AlphaScale;
		float4 _ColorOffset;
		CBUFFER_END
		ENDHLSL

		Pass {
			Tags { "LightMode" = "UniversalForward" }

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD01;
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float3 positionWS : TEXCOORD2;
			};

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);
			TEXTURE2D(_CloudTex2);
			SAMPLER(sampler_CloudTex2);

			TEXTURE2D(_MaskTex);
			SAMPLER(sampler_MaskTex);

			Varyings vert(Attributes IN)
			{
				Varyings OUT;

				OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
				OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
				OUT.uv2 = TRANSFORM_TEX(IN.uv, _CloudTex2);
				OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
				return OUT;
			}

			float Random (float2 st) {
				return frac(sin(dot(st.xy, float2(565656.233,123123.2033))) * 323434.34344);
			}

			half4 frag(Varyings IN) : SV_Target
			{
				half4 baseMap = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv + _MainTex_ST.zw * _Time.x);
				half4 cloudMap = SAMPLE_TEXTURE2D(_CloudTex2, sampler_CloudTex2, IN.uv2 + _CloudTex2_ST.zw * _Time.x);

				float3 finalColor = saturate(baseMap.rgb * cloudMap.rgb);

				half4 maskColor = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, IN.uv);

				//float dis = distance(IN.positionWS.xz, _Center.xz);
				//float r = Random(IN.uv);
				//float w = _Center.w;
				//dis = smoothstep(0.5 * w, w, dis);

				float a = smoothstep(_MaskEdgeA, _MaskEdgeB + (1 - _Color.a), min(maskColor.a, cloudMap.a) * _AlphaScale);
				a *= _Color.a;
				finalColor = baseMap.rgb * _ColorOffset.x + _ColorOffset.y;
				return float4(finalColor.rgb * _Color.rgb, a);
			}
			ENDHLSL
		}
	}
}