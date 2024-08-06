Shader "FX/AuroraBlenderV2"
{
    Properties
    {
        _MainTex ("Frames", 2DArray) = "white" {}
        _SecondStrength ("Second Frame Strength", Range(0.0, 1.0)) = 0.0
        _ThirdStrength ("Third Frame Strength", Range(0.0, 1.0)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        // No culling or depth
        Cull Off
        ZWrite Off
        ZTest Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma require 2darray
            #pragma target 4.0
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            UNITY_DECLARE_TEX2DARRAY(_MainTex);
            float _SecondStrength;
            float _ThirdStrength;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 first = UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(i.uv, 0));
                fixed4 next = UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(i.uv, 1));
                next.rgb = _SecondStrength * next.rgb;
                first = (Luminance(first) > Luminance(next)) ? first : next;
                next = UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(i.uv, 2));
                next.rgb = _ThirdStrength * next.rgb;
                first = (Luminance(first) > Luminance(next)) ? first : next;

                return first;
            }
            ENDCG
        }
    }
}
