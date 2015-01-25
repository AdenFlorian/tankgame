// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1419,x:32831,y:32909,varname:node_1419,prsc:2|emission-9299-OUT;n:type:ShaderForge.SFN_Tex2d,id:557,x:31882,y:32971,ptovrint:False,ptlb:diffuseTex,ptin:_diffuseTex,varname:node_557,prsc:2,tex:a008a695bbac47944af0d2fa704236b6,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:5448,x:31962,y:33181,ptovrint:False,ptlb:tint,ptin:_tint,varname:node_5448,prsc:2,glob:False,c1:0.4896555,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:1514,x:32163,y:33047,varname:node_1514,prsc:2|A-557-RGB,B-5448-RGB,C-4795-RGB;n:type:ShaderForge.SFN_VertexColor,id:4795,x:32163,y:33249,varname:node_4795,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9299,x:32532,y:33040,varname:node_9299,prsc:2|A-9052-OUT,B-557-RGB;n:type:ShaderForge.SFN_Multiply,id:9052,x:32354,y:32873,varname:node_9052,prsc:2|A-4784-OUT,B-1514-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6667,x:32119,y:32738,ptovrint:False,ptlb:node_6667,ptin:_node_6667,varname:node_6667,prsc:2,glob:False,v1:7;n:type:ShaderForge.SFN_TexCoord,id:8745,x:31940,y:32547,varname:node_8745,prsc:2,uv:0;n:type:ShaderForge.SFN_Code,id:4784,x:32392,y:32647,varname:node_4784,prsc:2,code:cgBlAHQAdQByAG4AIABwAG8AdwAoAGEAYgBzACgAaQBuAHAAdQB0ACAAKgAgADIAKQAtADEALAAgADIAKQAgACoAIABtAHUAbAB0AGkAOwA=,output:0,fname:Function_node_4784,width:527,height:191,input:0,input:0,input_1_label:input,input_2_label:multi|A-1416-OUT,B-6667-OUT;n:type:ShaderForge.SFN_Subtract,id:1416,x:32149,y:32557,varname:node_1416,prsc:2|A-8745-U,B-8745-V;proporder:5448-557-6667;pass:END;sub:END;*/

Shader "Aden/UnlitTintedVertColorCornerGlow" {
    Properties {
        _tint ("tint", Color) = (0.4896555,0,1,1)
        _diffuseTex ("diffuseTex", 2D) = "white" {}
        _node_6667 ("node_6667", Float ) = 7
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
            uniform sampler2D _diffuseTex; uniform float4 _diffuseTex_ST;
            uniform float4 _tint;
            uniform float _node_6667;
            float Function_node_4784( float input , float multi ){
            return pow(abs(input * 2)-1, 2) * multi;
            }
            
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float4 _diffuseTex_var = tex2D(_diffuseTex,TRANSFORM_TEX(i.uv0, _diffuseTex));
                float3 emissive = ((Function_node_4784( (i.uv0.r-i.uv0.g) , _node_6667 )*(_diffuseTex_var.rgb*_tint.rgb*i.vertexColor.rgb))*_diffuseTex_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
