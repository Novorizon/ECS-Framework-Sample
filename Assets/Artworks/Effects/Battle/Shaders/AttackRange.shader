Shader "Star/Effect/AttackRange"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width ("Width", Range(0, 1)) = 0.05
        _InnerColor ("InnerColor", Color) = (0.23, 0.67, 0.73, 1)
        _BorderColor ("BorderColor", Color) = (0.94, 0.90, 0.40, 1)
        _GlowSpeed ("GlowSpeed", Range(0, 10)) = 5
        _PatternScale ("PatternScale", Range(0, 1)) = 0.3
        _PatternGap ("PatternGap", Range(0, 1)) = 0.02
        _Alpha ("Alpha", Range(0, 1)) = 0.9
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent+1" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : TEXCOORD1;
            };

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float _Width;
            float _GlowSpeed;
            half4 _InnerColor;
            half4 _BorderColor;
            float _Alpha;
            float _PatternScale;
            float _PatternGap;
            CBUFFER_END

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 mainColor = tex2D(_MainTex, i.uv);
                half4 color = _InnerColor;
                half r = i.color.r;
                r = step(1 - r, _Width);
                float t = cos(_Time.x * _GlowSpeed * 20) * 0.5 + 0.5;
                half4 borderColor = _BorderColor * t * r;
                half y = step(1 - i.color.z,  1 - _PatternGap);
                half4 pc = (mainColor.a * _PatternScale + color) * y;
                pc.a = _Alpha;
                half4 finalColor = pc + borderColor;
                //finalColor.a = _Alpha;
                return finalColor;
            }
            ENDCG
        }
    }
}
