Shader "Unlit/GameMapShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerPosition ("Player Position", Vector) = (0.5, 0.5, 0, 0)
        _PulseSpeed ("Pulse Speed", Float) = 50.0
        _PulseRange ("Pulse Range", Float) = 0.1
        _GradientWidth ("Gradient Width", Float) = 0.03
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
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _PlayerPosition;
            float _PulseSpeed;
            float _PulseRange;
            float _GradientWidth;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
                half2 playerPos = _PlayerPosition.xy;
                half2 uv = i.uv - playerPos;

                half pulse = sin(_Time * _PulseSpeed);
                half radius = ((pulse + 1.0f) * _PulseRange + 0.5)* 0.1f;
                half4 col = tex2D(_MainTex, i.uv);

                half len = length(uv);

                if (len <= radius)
                {
                    if (len <= radius - _GradientWidth)
                    {
                        col = half4(1, 0, 0, 0);
                    } else {
                        half gradient = smoothstep(radius - _GradientWidth, radius, len);
                        col *= gradient;
                        col = lerp(half4(1, 0.5, 0.5, 1), col, gradient);
                    }
                }

                return col;
            }
            ENDCG
        }
    }
}
