Shader "Unlit/RippleShader"
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

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _LastRT; // 上一帧
            sampler2D _CurrentRT;// 当前帧
            // xxx_TexelSize unityShader 的固定写法，用来获取xxx贴图的大小信息
            float4 _CurrentRT_TexelSize;// 贴图大小信息
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //最小单位偏移量
                float3 e = float3(_CurrentRT_TexelSize.xy,0);
                float2 uv = i.uv;
                // 获取当前帧的上下左右四个值
                // 上
                float pt = tex2D(_CurrentRT,uv + e.zy).x;
                // 下
                float pb = tex2D(_CurrentRT,uv - e.zy).x;
                // 左
                float pl = tex2D(_CurrentRT,uv - e.xz).x;
                // 右
                float pr = tex2D(_CurrentRT,uv + e.xz).x;
                // 上一帧的中心点
                float pc =  tex2D(_LastRT,uv).x;

                // 计算均值
                float pa = (pt + pb + pl + pr)/2 - pc;
                pa *= 0.99;
                return pa;
            }
            ENDCG
        }
    }
}
