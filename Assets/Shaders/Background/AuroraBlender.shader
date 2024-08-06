Shader "FX/AuroraBlender"
{
    Properties
    {
        _MainTex ("Frames", 2DArray) = "white" {}
        _FrameOut ("Frame Out", Integer) = 0
        _FrameIn ("Frame In", Integer) = 1
        _Progress ("Progress", Range(0.0, 1.0)) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            int _FrameOut;
            int _FrameIn;
            float _Progress;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 past = UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(i.uv, _FrameOut));
                fixed4 next = UNITY_SAMPLE_TEX2DARRAY(_MainTex, float3(i.uv, _FrameIn));
                next.rgb = _Progress * next.rgb;

                return (Luminance(past) > Luminance(next)) ? past : next;
            }
            ENDCG
        }
    }
}
