Shader "Custom/GradientSkyBox"
{
    Properties
    {
        Color1("Color 1", Color) = (1, 1, 1, 0)
        Color2("Color 2", Color) = (1, 1, 1, 0)
        UpVector("Up Vector", Vector) = (0, 1, 0, 0)
        Intensity("Intensity", Float) = 1.0
        Exponent("Exponent", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Background" "Queue"="Background" }

        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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


            float4 Color1;
            float4 Color2;
            float4 UpVector;
            float Intensity;
            float Exponent;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float d = dot(normalize(i.uv), UpVector) * 0.5f + 0.5f;
                return lerp(Color1, Color2, pow(d, Exponent)) * Intensity;
            }
            ENDCG
        }
    }
    CustomEditor "GradientSkyboxInspector"
}
