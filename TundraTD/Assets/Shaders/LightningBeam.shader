// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-2243-OUT,alpha-3094-OUT,clip-5421-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32084,y:32689,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.961242,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1515,x:32028,y:32867,ptovrint:False,ptlb:PatternTexture,ptin:_PatternTexture,varname:node_1515,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:37074acfb39e22148a7410e4bd1c82fc,ntxv:0,isnm:False|UVIN-6765-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2975,x:32279,y:32810,varname:node_2975,prsc:2|A-7241-RGB,B-1515-RGB;n:type:ShaderForge.SFN_TexCoord,id:731,x:31613,y:32776,varname:node_731,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:6765,x:31868,y:32867,varname:node_6765,prsc:2,spu:0,spv:0.05|UVIN-731-UVOUT,DIST-4713-OUT;n:type:ShaderForge.SFN_Time,id:4332,x:31455,y:33029,varname:node_4332,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4713,x:31666,y:33081,varname:node_4713,prsc:2|A-4332-T,B-9766-OUT;n:type:ShaderForge.SFN_Slider,id:9766,x:31298,y:33171,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_9766,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:7.728834,max:10;n:type:ShaderForge.SFN_Length,id:5421,x:32279,y:32986,varname:node_5421,prsc:2|IN-1515-RGB;n:type:ShaderForge.SFN_Slider,id:3094,x:30793,y:32966,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_3094,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Fresnel,id:9953,x:32184,y:32449,varname:node_9953,prsc:2|EXP-2338-OUT;n:type:ShaderForge.SFN_Lerp,id:2338,x:31964,y:32466,varname:node_2338,prsc:2|A-6521-OUT,B-2274-OUT,T-3040-OUT;n:type:ShaderForge.SFN_Sin,id:1610,x:31614,y:32606,varname:node_1610,prsc:2|IN-2916-T;n:type:ShaderForge.SFN_Abs,id:3040,x:31792,y:32606,varname:node_3040,prsc:2|IN-1610-OUT;n:type:ShaderForge.SFN_Vector1,id:2274,x:31729,y:32528,varname:node_2274,prsc:2,v1:0.99;n:type:ShaderForge.SFN_Vector1,id:6521,x:31729,y:32466,varname:node_6521,prsc:2,v1:0.7;n:type:ShaderForge.SFN_Time,id:2916,x:31430,y:32606,varname:node_2916,prsc:2;n:type:ShaderForge.SFN_Add,id:2243,x:32464,y:32789,varname:node_2243,prsc:2|A-9953-OUT,B-2975-OUT;proporder:7241-1515-9766-3094;pass:END;sub:END;*/

Shader "Custom/LightningBeam" {
    Properties {
        [HDR]_Color ("Color", Color) = (0,1,0.961242,1)
        _PatternTexture ("PatternTexture", 2D) = "white" {}
        _Speed ("Speed", Range(0, 10)) = 7.728834
        _Opacity ("Opacity", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 3.0
            uniform sampler2D _PatternTexture; uniform float4 _PatternTexture_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 node_4332 = _Time;
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float2 node_6765 = (i.uv0+(node_4332.g*_Speed_var)*float2(0,0.05));
                float4 _PatternTexture_var = tex2D(_PatternTexture,TRANSFORM_TEX(node_6765, _PatternTexture));
                clip(length(_PatternTexture_var.rgb) - 0.5);
////// Lighting:
////// Emissive:
                float4 node_2916 = _Time;
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float3 emissive = (pow(1.0-max(0,dot(normalDirection, viewDirection)),lerp(0.7,0.99,abs(sin(node_2916.g))))+(_Color_var.rgb*_PatternTexture_var.rgb));
                float3 finalColor = emissive;
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                return fixed4(finalColor,_Opacity_var);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            uniform sampler2D _PatternTexture; uniform float4 _PatternTexture_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 node_4332 = _Time;
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float2 node_6765 = (i.uv0+(node_4332.g*_Speed_var)*float2(0,0.05));
                float4 _PatternTexture_var = tex2D(_PatternTexture,TRANSFORM_TEX(node_6765, _PatternTexture));
                clip(length(_PatternTexture_var.rgb) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
