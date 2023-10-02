Shader "Custom/ViewSpaceNormals"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 viewSpaceNormal : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                // Convert object-space normal to view-space normal
                o.viewSpaceNormal = mul((float3x3)UNITY_MATRIX_V, mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Normalize the view space normals so they are in the range of [0,1]
                float3 normalizedNormals = normalize(i.viewSpaceNormal) * 0.5 + 0.5;
                return float4(normalizedNormals, 1.0);
            }
            ENDCG
        }
    }
}
