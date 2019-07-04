Shader "Dance/Water/Simulation"
{

Properties
{
    _InputTex ("InputTexture", 2D) = "white" {}
    _Attenuation ("Attenuation",float) = 0.01
}

CGINCLUDE

#include "UnityCustomRenderTexture.cginc"

sampler2D _InputTex;
float _Attenuation;

float4 frag(v2f_customrendertexture i) : SV_Target
{
    float2 uv = i.globalTexcoord;
    float inputVal = tex2D(_InputTex,uv).r;
    float attenuationVal = tex2D(_SelfTexture2D,uv).r - _Attenuation*unity_DeltaTime.y;
    float4 newCol = float4(max(inputVal,attenuationVal),0.0,0.0,1.0);

    return newCol;
}

ENDCG

SubShader
{
    Cull Off ZWrite Off ZTest Always
    Pass
    {
        Name "Update"
        CGPROGRAM
        #pragma target 3.0
        #pragma vertex CustomRenderTextureVertexShader
        #pragma fragment frag
        ENDCG
    }
}

}