// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:False,igpj:True,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:1,fgcr:0.7941176,fgcg:0.7941176,fgcb:0.7941176,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1419,x:32926,y:32899,varname:node_1419,prsc:2|emission-9052-OUT,alpha-5940-OUT;n:type:ShaderForge.SFN_Color,id:5448,x:32481,y:32894,ptovrint:False,ptlb:tint,ptin:_tint,varname:_tint,prsc:2,glob:False,c1:1,c2:0.475862,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:9052,x:32724,y:32998,varname:node_9052,prsc:2|A-6667-OUT,B-5448-RGB,C-7605-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6667,x:32486,y:32816,ptovrint:False,ptlb:glow,ptin:_glow,varname:_glow,prsc:2,glob:False,v1:7;n:type:ShaderForge.SFN_TexCoord,id:8745,x:32042,y:33013,varname:node_8745,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:5616,x:31996,y:33184,ptovrint:False,ptlb:thinness,ptin:_thinness,varname:_thinness,prsc:2,glob:False,v1:16;n:type:ShaderForge.SFN_Add,id:5940,x:32722,y:33157,varname:node_5940,prsc:2|A-7605-OUT,B-3871-OUT;n:type:ShaderForge.SFN_Slider,id:3871,x:32334,y:33241,ptovrint:False,ptlb:faceOpacity,ptin:_faceOpacity,varname:_faceOpacity,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Code,id:7605,x:32228,y:33063,varname:node_7605,prsc:2,code:dQB2ACAAPQAgAHAAbwB3ACgAYQBiAHMAKABjAG8AcwAoAHUAdgAgACoAIABzAGMAYQBsAGUALQAzAC4AMQA0AC8AMgApACkALAAgAHAAKQA7AAoAcgBlAHQAdQByAG4AIABtAGEAeAAoAHUAdgAuAHgALAAgAHUAdgAuAHkAKQA7AA==,output:0,fname:curveFunction,width:439,height:128,input:1,input:0,input:0,input_1_label:uv,input_2_label:p,input_3_label:scale|A-8745-UVOUT,B-5616-OUT,C-994-OUT;n:type:ShaderForge.SFN_ValueProperty,id:994,x:32035,y:33255,ptovrint:False,ptlb:scale,ptin:_scale,varname:node_994,prsc:2,glob:False,v1:0;proporder:5448-6667-5616-3871-994;pass:END;sub:END;*/

Shader "Aden/TronGrid" {
    Properties {
        _tint ("tint", Color) = (1,0.475862,0,1)
        _glow ("glow", Float ) = 7
        _thinness ("thinness", Float ) = 16
        _faceOpacity ("faceOpacity", Range(0, 1)) = 0
        _scale ("scale", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
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
            uniform float _thinness;
            uniform float _faceOpacity;
            float curveFunction( float2 uv , float p , float scale ){
            uv = pow(abs(cos(uv * scale-3.14/2)), p);
            return max(uv.x, uv.y);
            }
            
            uniform float _scale;
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
                float node_7605 = curveFunction( i.uv0 , _thinness , _scale );
                float3 emissive = (_glow*_tint.rgb*node_7605);
                float3 finalColor = emissive;
                return fixed4(finalColor,(node_7605+_faceOpacity));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
