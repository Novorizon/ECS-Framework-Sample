Shader "Star/Effect/Iron"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //[HDR] _EmissionColor ("EmissionColor", Color) = (1,1,1,1)
        _OutlineWidth ("OutlineWidth", Range(0.0, 10.0)) = 1
        _FresnelPower ("FresnelPower", Range(0.0, 10.0)) = 0.1
        [HDR] _OutlineColor ("OutlineColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Tags {
                "LightMode"="UniversalForward"
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float _OutlineWidth;
            float4 _OutlineColor;
            float _FresnelPower;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz += v.normal * _OutlineWidth * 0.001;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        Pass
        {
            Tags {
                "LightMode"="SRPDefaultUnlit"
            }
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float4 positionWS : TEXCOORD2;
            };

            sampler2D _MainTex;

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _EmissionColor;
            float4 _OutlineColor;
            float _FresnelPower;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.positionWS = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                float3 V = normalize(UnityWorldSpaceViewDir(i.positionWS.xyz));
                float3 N = normalize(i.normal);
                
                half rim = 1.0 - saturate(dot(V, N));
				float4 rimColor = _OutlineColor * pow(rim, _FresnelPower);
                return col + rimColor;
            }
            ENDCG
        }
    }
}
