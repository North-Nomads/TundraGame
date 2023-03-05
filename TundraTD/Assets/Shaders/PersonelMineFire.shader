// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32822,y:32647,varname:node_3138,prsc:2|emission-8901-OUT,alpha-2263-OUT;n:type:ShaderForge.SFN_Fresnel,id:9246,x:32273,y:32537,varname:node_9246,prsc:2|EXP-1489-OUT;n:type:ShaderForge.SFN_Time,id:3185,x:31466,y:32614,varname:node_3185,prsc:2;n:type:ShaderForge.SFN_Abs,id:6943,x:31857,y:32635,varname:node_6943,prsc:2|IN-1943-OUT;n:type:ShaderForge.SFN_Sin,id:1943,x:31694,y:32635,varname:node_1943,prsc:2|IN-3185-T;n:type:ShaderForge.SFN_Lerp,id:1489,x:32104,y:32556,varname:node_1489,prsc:2|A-2841-OUT,B-5709-OUT,T-6943-OUT;n:type:ShaderForge.SFN_Vector1,id:5709,x:31864,y:32590,varname:node_5709,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:2841,x:31864,y:32541,varname:node_2841,prsc:2,v1:12;n:type:ShaderForge.SFN_Color,id:7434,x:32273,y:32691,ptovrint:False,ptlb:OuterCollor,ptin:_OuterCollor,varname:node_7434,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.003921569,c3:0.003921569,c4:1;n:type:ShaderForge.SFN_Multiply,id:8901,x:32446,y:32537,varname:node_8901,prsc:2|A-9246-OUT,B-7434-RGB;n:type:ShaderForge.SFN_Slider,id:2263,x:32240,y:32906,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_2263,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6173924,max:1;proporder:7434-2263;pass:END;sub:END;*/

Shader "Shader Forge/PersonelMineFire" {
    Properties {
        _OuterCollor ("OuterCollor", Color) = (1,0.003921569,0.003921569,1)
        _Opacity ("Opacity", Range(0, 1)) = 0.6173924
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _OuterCollor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_3185 = _Time;
                float4 _OuterCollor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OuterCollor );
                float3 node_8901 = (pow(1.0-max(0,dot(normalDirection, viewDirection)),lerp(12.0,1.0,abs(sin(node_3185.g))))*_OuterCollor_var.rgb);
                float3 emissive = node_8901;
                float3 finalColor = emissive;
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                return fixed4(finalColor,_Opacity_var);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
