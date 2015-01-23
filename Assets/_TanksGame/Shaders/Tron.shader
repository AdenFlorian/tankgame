// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1419,x:32926,y:32899,varname:node_1419,prsc:2|emission-342-OUT;n:type:ShaderForge.SFN_Color,id:5448,x:32458,y:33061,ptovrint:False,ptlb:tint,ptin:_tint,varname:node_5448,prsc:2,glob:False,c1:0.4896555,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:9052,x:32646,y:32875,varname:node_9052,prsc:2|A-4784-OUT,B-5448-RGB;n:type:ShaderForge.SFN_ValueProperty,id:6667,x:32518,y:33250,ptovrint:False,ptlb:multiply,ptin:_multiply,varname:node_6667,prsc:2,glob:False,v1:7;n:type:ShaderForge.SFN_TexCoord,id:8745,x:31904,y:32643,varname:node_8745,prsc:2,uv:0;n:type:ShaderForge.SFN_Code,id:4784,x:32218,y:32619,varname:node_4784,prsc:2,code:ZgBsAG8AYQB0ACAAYQAgAD0AIABwAG8AdwAoACgAKAB1ACkAIAAqACAAMgApACAALQAgAHMAbABpAGQAZQByACwAIAAyACkAOwAKAGYAbABvAGEAdAAgAGIAIAA9ACAAcABvAHcAKAAoACgAdgApACAAKgAgADIAKQAgAC0AIABzAGwAaQBkAGUAcgAsACAAMgApADsACgByAGUAdAB1AHIAbgAgAG0AYQB4ACgAYQAsACAAYgApADsA,output:0,fname:Function_node_4784,width:527,height:191,input:8,input:0,input:0,input:0,input:0,input_1_label:exp,input_2_label:u,input_3_label:v,input_4_label:slider,input_5_label:multi|A-7999-OUT,B-8745-U,C-8745-V,D-7050-OUT,E-7438-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7999,x:31904,y:32912,ptovrint:False,ptlb:exponent,ptin:_exponent,varname:node_7999,prsc:2,glob:False,v1:8;n:type:ShaderForge.SFN_Multiply,id:342,x:32692,y:33072,varname:node_342,prsc:2|A-9052-OUT,B-6667-OUT;n:type:ShaderForge.SFN_Slider,id:9002,x:31904,y:33032,ptovrint:False,ptlb:node_9002,ptin:_node_9002,varname:node_9002,prsc:2,min:0,cur:0.5641026,max:9;n:type:ShaderForge.SFN_Slider,id:7438,x:31946,y:32490,ptovrint:False,ptlb:node_7438,ptin:_node_7438,varname:node_7438,prsc:2,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Code,id:4819,x:32670,y:32188,varname:node_4819,prsc:2,code:ZgBsAG8AYQB0ACAAYQAgAD0AIABwAG8AdwAoACgAKAAxAC0AdQApACAAKgAgAG0AdQBsAHQAaQApACAALQAgAHMAbABpAGQAZQByACwAIABlAHgAcAApADsACgBmAGwAbwBhAHQAIABiACAAPQAgAHAAbwB3ACgAKAAoADEALQB2ACkAIAAqACAAbQB1AGwAdABpACkAIAAtACAAcwBsAGkAZABlAHIALAAgAGUAeABwACkAOwAKAGYAbABvAGEAdAAgAGEAMQAgAD0AIABwAG8AdwAoACgAKAB1ACkAIAAqACAAbQB1AGwAdABpACkAIAAtACAAcwBsAGkAZABlAHIALAAgAGUAeABwACkAOwAKAGYAbABvAGEAdAAgAGIAMQAgAD0AIABwAG8AdwAoACgAKAB2ACkAIAAqACAAbQB1AGwAdABpACkAIAAtACAAcwBsAGkAZABlAHIALAAgAGUAeABwACkAOwAKAHIAZQB0AHUAcgBuACAAbQBhAHgAKABhACAAKwAgAGEAMQAsACAAYgAgACsAIABiADEAKQA7AA==,output:0,fname:Function_node_4784,width:527,height:191,input:0,input:0,input:0,input:0,input:0,input_1_label:exp,input_2_label:u,input_3_label:v,input_4_label:slider,input_5_label:multi;n:type:ShaderForge.SFN_ValueProperty,id:7050,x:32001,y:32307,ptovrint:False,ptlb:slider,ptin:_slider,varname:node_7050,prsc:2,glob:False,v1:0;proporder:5448-6667-7999-9002-7438-7050;pass:END;sub:END;*/

Shader "Aden/Tron" {
    Properties {
        _tint ("tint", Color) = (0.4896555,0,1,1)
        _multiply ("multiply", Float ) = 7
        _exponent ("exponent", Float ) = 8
        _node_9002 ("node_9002", Range(0, 9)) = 0.5641026
        _node_7438 ("node_7438", Range(0, 1)) = 1
        _slider ("slider", Float ) = 0
    }
    SubShader {
        Tags {
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
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
            uniform float _multiply;
            float Function_node_4784( fixed exp , float u , float v , float slider , float multi ){
            float a = pow(((u) * 2) - slider, 2);
            float b = pow(((v) * 2) - slider, 2);
            return max(a, b);
            }
            
            uniform float _exponent;
            uniform float _node_7438;
            uniform float _slider;
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
                float3 emissive = ((Function_node_4784( _exponent , i.uv0.r , i.uv0.g , _slider , _node_7438 )*_tint.rgb)*_multiply);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
