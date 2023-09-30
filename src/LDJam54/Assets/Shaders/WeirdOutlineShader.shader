Shader "URP/CustomOutline" {
    Properties {
        _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineWidth("Outline Width", Range(0.0, 10.0)) = 1.0
    }
    SubShader {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }
        Pass {
            HLSLPROGRAM
            // Requires a vertex and pixel function.
            #pragma vertex Vertex
            #pragma fragment Fragment
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float4 _OutlineColor;
            float _OutlineWidth;

            struct Attributes {
                float3 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
            };

            struct Varyings {
                float4 positionCS   : SV_POSITION;
            };

            Varyings Vertex(Attributes input) {
                Varyings output;
                float3 outlineOffset = normalize(input.normalOS) * (_OutlineWidth * 0.05);
                output.positionCS    = TransformObjectToHClip(input.positionOS + outlineOffset);
                return output;
            }

            half4 Fragment(Varyings input) : SV_TARGET {
                return _OutlineColor;
            }
            ENDHLSL
        }
        Pass {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float3 positionOS   : POSITION;
            };
            
            struct Varyings {
                float4 positionCS   : SV_POSITION;
            };

            Varyings Vert(Attributes input){
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS);
                return output;
            }

            half4 Frag(Varyings input) : SV_Target { 
                return half4 (1, 1, 1, 1);
            }
            ENDHLSL
        }
    }
}