Shader "UI/IconMakeMaskColor"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [HideInInspector] _Color ("Tint", Color) = (1,1,1,1)

        [HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
        [HideInInspector] _Stencil ("Stencil ID", Float) = 0
        [HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
        [HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
        [HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255

        [HideInInspector] _ColorMask ("Color Mask", Float) = 15

        _MaskColor ("MaskColor", Color) = (0.5,0.5,0.5,1)
        _Progress ("Progress", Range(0, 1)) = 0

        _BarTex ("Bar", 2D) = "white" {}
        _LightTex ("Light", 2D) = "white" {}
        [HideInInspector] _BarPosition ("BarPosition", Vector) = (5,10,-0.4,-0.95)
        [HideInInspector] _LightPosition ("LightPosition", Vector) = (1.2,10,-0.1,-0.95)
        _ScanSpeed ("ScanSpeed", Range(0, 10)) = 5.08
        _ScanScale ("ScanScale", Range(0, 5)) = 0.81

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        LOD 100

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask RGB

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _Progress;
            fixed4 _MaskColor;
            sampler2D _BarTex;
            sampler2D _LightTex;
            float4 _BarPosition;
            float4 _LightPosition;
            float4 _AtlasUV;
            float _ScanSpeed;
            float _ScanScale;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }

            float2 GetAtlasUV(float2 uv)
            {
                float u = (uv.x - _AtlasUV.y) * _AtlasUV.x;
                float v = (uv.y - _AtlasUV.w) * _AtlasUV.z;
                return float2(u, v);
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 uv = GetAtlasUV(IN.texcoord);
                //uv = IN.texcoord;

                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                float progress = _Progress * 1.08 - 0.04;
                float barX = sin(_Time.x * _ScanSpeed * 100) * _ScanScale * 0.1;
                half4 barColor = tex2D(_BarTex, (uv + _BarPosition.zw) * _BarPosition.xy + float2(barX, 1 - progress) * _BarPosition.xy);
                half4 lightColor = tex2D(_LightTex, (uv + _LightPosition.zw) * _LightPosition.xy + float2(0, 1 - progress) * _LightPosition.xy);

                float edge = step(progress, uv.y);
                
                color.rgb = lerp(color.rgb, _MaskColor.rgb, edge);

                barColor.a = step(0.7, barColor.a);

                color.rgb = lightColor.rgb * lightColor.a + barColor.rgb * barColor.a + color.rgb * (1 - barColor.a);
                color.a = max(max(barColor.a, color.a), lightColor.a);

                return color;
            }
        ENDCG
        }
    }
}