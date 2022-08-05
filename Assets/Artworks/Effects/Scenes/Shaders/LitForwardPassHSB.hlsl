#ifndef UNIVERSAL_FORWARD_LIT_PASS_HUE_INCLUDED
#define UNIVERSAL_FORWARD_LIT_PASS_HUE_INCLUDED

// Used in Standard (Physically Based) shader
half4 LitPassFragmentHUE(Varyings input) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    SurfaceData surfaceData;
    InitializeStandardLitSurfaceData(input.uv, surfaceData);

    InputData inputData;
    InitializeInputData(input, surfaceData.normalTS, inputData);

    half4 color = UniversalFragmentPBR(inputData, surfaceData);

    color.rgb = MixFog(color.rgb, inputData.fogCoord);
    color.a = OutputAlpha(color.a, _Surface);

    half3 colorHSV;    
    colorHSV.rgb = RgbToHsv(color.rgb);
    colorHSV.r = (colorHSV.r + _Hue / 360) % 360;
    colorHSV.g *= _Saturation;
    colorHSV.b *= _Brightness; 
    color.rgb = HsvToRgb(colorHSV.rgb);

    return color;
}

#endif
