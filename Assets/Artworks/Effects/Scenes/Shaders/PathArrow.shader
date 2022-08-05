Shader "Star/Effect/PathArrow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", color) = (1,1,1,1)
        _Speed ("Speed", float) = 1
        _Blink ("Blink", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        ZWrite Off

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 velocity : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            
            CBUFFER_START(UnityPerMaterial)
            float4 _Color;
            float _Speed;
            float _Blink;
            float4 _MainTex_ST;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                //float2 angle = atan2(-v.velocity.z, v.velocity.x);
                //float2 pivot = float2(0.5,0.5);
                //float cosAngle = cos(angle);
                //float sinAngle = sin(angle);
                //float2x2 rot = float2x2(cosAngle,-sinAngle,sinAngle,cosAngle);
                //float2 uv = v.uv.xy - pivot;
                //o.uv = mul(rot, uv);
                //o.uv += pivot;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x = o.uv.x + _Time.x * _Speed;

                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv) * _Color;
                col.a *= cos(_Time.x * _Blink)* 0.5 + 0.6;
                return col;
            }
            ENDCG
        }
    }
}
