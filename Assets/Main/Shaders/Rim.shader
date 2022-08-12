Shader "Unlit/Rim"
{
    Properties
    {
        _MainColor ("MainColor", Color) = (0, 0, 0, 1)
        _RimColor ("RimColor", Color) = (0, 0, 0, 1)
        _RimPower ("RimPower", Range(0.0001, 5.0)) = 0.0001
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normalDir : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                UNITY_FOG_COORDS(1)
            };

            float4 _MainColor;
            half4 _RimColor;
            float _RimPower;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float rim = 1 - max(0, dot(viewReflectDirection, normalDirection));
                half3 rimColor = _RimColor * pow(rim, 1 / _RimPower);

                return fixed4(rimColor + _MainColor.rgb, 1);
            }
            ENDCG
        }
    }
}