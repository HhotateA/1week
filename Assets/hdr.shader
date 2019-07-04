Shader "Unlit/hdr"
{
    Properties
    {
        _spect ("Texture", 2D) = "white" {}
        [HDR]_color ("color",color) = (0.,0.,0.,0.)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
            };

            struct v2f
            {
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _spect;
            float4 _spect_ST;
            float4 _color;

            v2f vert (appdata v)
            {
                v2f o;
                float val = tex2Dlod(_spect,float4(_spect_ST.x,0.5,0.0,0.0));
                //val = clamp(val,0.3,1.0);
                o.vertex = UnityObjectToClipPos(v.vertex*val);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = _color;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
