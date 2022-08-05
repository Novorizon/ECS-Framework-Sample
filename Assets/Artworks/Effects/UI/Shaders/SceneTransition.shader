Shader "Star/Effect/SceneTransition"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _NoiseTexture1 ("NoiseTexture1", 2D) = "white" {}
        _NoiseTexture2 ("NoiseTexture2", 2D) = "white" {}
        _Progress ("Progress", Range(0, 1)) = 1
        _Width ("Width", Float) = 1
        _Radius ("Radius", Float) = 0.16
        _RadiusPower ("RadiusPower", Float) = 2.76
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTexture1;
            sampler2D _NoiseTexture2;
            float4 _MainTex_ST;
            float _Progress;
            float _RadiusPower;
            float _Radius;
            float _Width;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float2 Rotate(float2 UV, float Rotation)
            {
                Rotation = Rotation * (3.1415926f/180.0f);
                UV -= float2(0.5, 0.5);
                float s = sin(Rotation);
                float c = cos(Rotation);
                float2x2 rMatrix = float2x2(c, -s, s, c);
                rMatrix *= 0.5;
                rMatrix += 0.5;
                rMatrix = rMatrix * 2 - 1;
                UV.xy = mul(UV.xy, rMatrix);
                UV += float2(0.5, 0.5);
                return UV;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float4 noise1 = tex2D(_NoiseTexture1, i.uv);
                float4 noise2 = tex2D(_NoiseTexture2, Rotate(i.uv, 90));
                float a = max(noise1.r, noise2.r);

                float2 uv = i.uv;
                float dis = 1 - distance(uv, float2(0.5, 0.5));
                
                dis = smoothstep(dis, 0, _Radius);
                dis = pow(dis, _RadiusPower);
                float c = dis + _Progress * 1.3 - 0.75;
                clip(a - c);
                c += _Width;

                
                float edge = step(a, c);
                float4 edgeColor = edge * _Color;
                float4 finalColor = float4(col.rgb + edgeColor.rgb, a);
                return finalColor;
            }
            ENDCG
        }
    }
}
