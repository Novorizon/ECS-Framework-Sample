Shader "Star/UI/Glitch"
{
    Properties
    {
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}

        [Header(Glitch)]
        [Space] _Frequency ("Frequency", float) = 1
        _Amplitude ("Amplitude", float) = 1

        [Header(Glitch)]
        [Space] _JitterFrequency ("JitterFrequency", float) = 1
        _JitterAmplitude ("JitterAmplitude", float) = 1

        [Header(ScanLine)]
        [Space]_LineDensity ("LineDensity", float) = 1
        _LineWidth ("LineWidth", float) = 1
        _LineSpeed ("LineSpeed", float) = 1
        _LineColor ("LineColor", Color) = (1,1,1,1)
    }

    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        TEXTURE2D(_MainTex);
	    SAMPLER(sampler_MainTex);

        CBUFFER_START(UnityPerMaterial)
        float4 _AtlasUV = float4(1, 0, 1, 0);

        half _Indensity;
        half _Frequency;
        half _Amplitude;
        half _JitterFrequency;
        half _JitterAmplitude;

        half _LineWidth;
        half _LineDensity;
        half _LineSpeed;
        float4 _LineColor;
        CBUFFER_END

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

        Varyings Vert (Attributes IN)
        {
            Varyings OUT;
            OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
            OUT.uv = IN.uv;
            return OUT;
        }

        float RandomNoise(float x, float y)
	    {
		    return frac(sin(dot(float2(x, y), float2(12.9898, 78.233))) * 43758.5453);
	    }

        half4 Frag (Varyings IN) : SV_Target
        {
            float2 uv = IN.uv;
            half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

            float u = sin(6 * _Frequency * _Time.x);
            float t = 0.01 * (-_Time.x * 130.0);
            u += sin(u * _Frequency * 2.1 + t) * 3.6;
            u += sin(u * _Frequency * 0.8 + t * 1.11) * 4.6;
            u += sin(u * _Frequency * 2.6 + t * 0.8) * 5.0;
            u += sin(u * _Frequency * 4.5+ t * 3.49) * 2.8;
            u *= _Amplitude * 0.01;

            // jitter
            float jitter = RandomNoise(uv.y, _Time.x) * 2 - 1;
            jitter *= step(_JitterFrequency, abs(jitter)) * _JitterAmplitude;
            u = 0;
            jitter = 0;

            //float2 uv = float2((IN.uv.x - _AtlasUV.y) * _AtlasUV.x, (IN.uv.y - _AtlasUV.w) * _AtlasUV.z);
            // glitch color
            half r = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(uv.x + u + jitter, uv.y)).r;
            half g = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv).g;
            half b = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(uv.x - u - jitter, uv.y)).b;

            // scane line
            float y = step(_LineWidth, frac((uv.y + (_Time.x * _LineSpeed)) * _LineDensity));
            y *= pow(1 - uv.y, 2);

            half4 finalColor = half4(r, g, b, 1) + y * _LineColor;
            finalColor.a = col.a;
            return finalColor;
         }
    ENDHLSL

    SubShader
    {
        
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            Name "Glitch"

            HLSLPROGRAM
                #pragma vertex Vert
                #pragma fragment Frag
            ENDHLSL
        }
    }
}
