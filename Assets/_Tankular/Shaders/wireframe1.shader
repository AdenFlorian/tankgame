// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:Diffuse,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1419,x:32926,y:32899,varname:node_1419,prsc:2|emission-9052-OUT,alpha-4784-OUT;n:type:ShaderForge.SFN_Color,id:5448,x:32443,y:33059,ptovrint:False,ptlb:tint,ptin:_tint,varname:_tint,prsc:2,glob:False,c1:1,c2:0.475862,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:9052,x:32697,y:33030,varname:node_9052,prsc:2|A-4784-OUT,B-5448-RGB,C-6667-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6667,x:32449,y:33240,ptovrint:False,ptlb:glow,ptin:_glow,varname:_multiply,prsc:2,glob:False,v1:7;n:type:ShaderForge.SFN_TexCoord,id:8745,x:31942,y:32780,varname:node_8745,prsc:2,uv:0;n:type:ShaderForge.SFN_Code,id:4784,x:32232,y:32883,varname:node_4784,prsc:2,code:ZgBsAG8AYQB0ACAAcABvAHcASQBuAHQAIAA9ACAAMQAwADsACgBmAGwAbwBhAHQAIABhACAAPQAgAHAAbwB3ACgAYQBiAHMAKAAoAHUAIAAqACAAMgApACAALQAgADEAKQAsACAAcAApADsACgBmAGwAbwBhAHQAIABiACAAPQAgAHAAbwB3ACgAYQBiAHMAKAAoAHYAIAAqACAAMgApACAALQAgADEAKQAsACAAcAApADsACgByAGUAdAB1AHIAbgAgAG0AYQB4ACgAYQAsACAAYgApADsA,output:0,fname:Function_node_4784,width:319,height:128,input:0,input:0,input:0,input_1_label:u,input_2_label:v,input_3_label:p|A-8745-U,B-8745-V,C-5616-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5616,x:31988,y:32995,ptovrint:False,ptlb:thinness,ptin:_thinness,varname:node_5616,prsc:2,glob:False,v1:16;proporder:5448-6667-5616;pass:END;sub:END;*/

Shader "Aden/Wireframe1" {
    Properties {
        _tint ("tint", Color) = (1,0.475862,0,1)
        _glow ("glow", Float ) = 7
        _thinness ("thinness", Float ) = 16
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _tint;
            uniform float _glow;
            float Function_node_4784( float u , float v , float p ){
            float powInt = 10;
            float a = pow(abs((u * 2) - 1), p);
            float b = pow(abs((v * 2) - 1), p);
            return max(a, b);
            }
            
            uniform float _thinness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
////// Lighting:
////// Emissive:
                float node_4784 = Function_node_4784( i.uv0.r , i.uv0.g , _thinness );
                float3 emissive = (node_4784*_tint.rgb*_glow);
                float3 finalColor = emissive;
                return fixed4(finalColor,node_4784);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
