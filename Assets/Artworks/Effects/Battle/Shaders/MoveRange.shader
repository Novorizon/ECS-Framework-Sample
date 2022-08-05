Shader "Star/Effect/MoveRange"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width ("Witdh", Range(0, 1)) = 1
        _InnerColor ("InnerColor", Color) = (0.03, 0.61, 0.92, 1)
        _OutterColor ("OutterColor", Color) = (0.03, 0.74, 0.93, 1)
        _BorderColor ("BorderColor", Color) = (0.67, 0.90, 0.98, 1)
        _Blend ("Blend", Range(0, 1)) = 0.67
        _BlendEdge ("BlendEdge", Range(0, 1)) = 0.32
        _Alpha ("Alpha", Range(0, 1)) = 0.9 

        _Segment ("Segment", float) = 1
        _Speed ("Speed", float) = 1
        _SegmentWidth ("SegmentWidth", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
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
            half4 _InnerColor;
            half4 _OutterColor;
            half4 _BorderColor;
            float _Blend;
            float _BlendEdge;
            float _Alpha;

            float _Segment;
            float _Speed;
            float _SegmentWidth;
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
                // sample the texture
                half4 color = _InnerColor;
                half r = i.color.r;
                half blend = smoothstep(_Blend - _BlendEdge, _Blend + _BlendEdge, r);
                half4 blendColor = lerp(_InnerColor, _OutterColor, blend);

                half2 uv = i.color.yz * 2 - 1;
                half bend = frac((uv.x + (_Speed * _Time.x)) * _Segment);
                bend = step(bend, _SegmentWidth);

                half4 borderColor = _BorderColor * bend * step(1 - r, _Width);
                half4 finalColor = borderColor + blendColor;
                finalColor.a = _Alpha;
                return finalColor;
            }
            ENDCG
        }
    }
}
