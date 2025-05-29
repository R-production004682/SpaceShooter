Shader "Custom/CrackedWithFogAndWarning"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
        _CrackTex ("Crack Texture", 2D) = "black" {}
        _CrackIntensity ("Crack Intensity", Range(0, 1)) = 0
        _RedFogIntensity ("Red Fog Intensity", Range(0, 1)) = 0
        _WhiteFogIntensity ("White Fog Intensity", Range(0, 1)) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _CrackTex;
            float _CrackIntensity;
            float _RedFogIntensity;
            float _WhiteFogIntensity;
            float4 _FlashTime;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 base = tex2D(_MainTex, i.uv);
                fixed4 crack = tex2D(_CrackTex, i.uv);

                // 割れ演出（主役）
                float crackBlend = smoothstep(0.2, 0.8, _CrackIntensity);
                fixed4 cracked = lerp(base, crack, crack.a * crackBlend);

                // 赤い縁と角
                float edgeL = smoothstep(0.1, 0.0, i.uv.x);
                float edgeR = smoothstep(0.1, 0.0, 1.0 - i.uv.x);
                float edgeT = smoothstep(0.1, 0.0, 1.0 - i.uv.y);
                float edgeB = smoothstep(0.1, 0.0, i.uv.y);
                float edgeBlend = max(max(edgeL, edgeR), max(edgeT, edgeB));

                float corner = edgeL * edgeT + edgeL * edgeB + edgeR * edgeT + edgeR * edgeB;
                corner = saturate(corner * 2.0);

                float pulse = 0.5 + 0.5 * sin(_FlashTime.y * 2.0);
                float redAmount = (edgeBlend + corner) * _RedFogIntensity * pulse;

                fixed4 redGlow = fixed4(1, 0, 0, 1) * redAmount;

                // 白濁フィルタ（中央を覆う。ひび部分は除外）
                float fogAlpha = _WhiteFogIntensity * (1.0 - crack.a) * pulse;
                fixed4 fog = fixed4(1, 1, 1, 1) * fogAlpha;

                return cracked + fog + redGlow;
            }
            ENDCG
        }
    }
}
