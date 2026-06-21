Shader "Unlit/DrawShader"
{
    Properties
    {
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _SourceTex;
            float4 _Pos;

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

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                // 大范围的点
                // return length(uv-_Pos.xy);
                // 稍微大一些的点
                // return length(uv-_Pos.xy)/_Pos.z;
                // 很小的一点
                // return _Pos.z - length(uv -_Pos.xy)/_Pos.z;
                // 类似画线效果，有拿_SourceTex，所上一帧的改动还在，本身只是很小的一个点
                return max(_Pos.z - length(uv -_Pos.xy)/_Pos.z,0)+ tex2D(_SourceTex,uv).x;
            }
            ENDCG
        }
    }
}
