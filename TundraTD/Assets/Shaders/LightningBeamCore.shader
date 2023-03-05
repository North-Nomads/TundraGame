// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-9149-OUT,alpha-2375-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31575,y:32530,ptovrint:False,ptlb:Fresnel Color,ptin:_FresnelColor,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.9171712,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:2375,x:32240,y:32969,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_2375,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8221287,max:1;n:type:ShaderForge.SFN_Fresnel,id:8500,x:31575,y:32686,varname:node_8500,prsc:2|EXP-1081-OUT;n:type:ShaderForge.SFN_Slider,id:1081,x:31157,y:32700,ptovrint:False,ptlb:Fresnel Exp,ptin:_FresnelExp,varname:node_1081,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.3574258,max:5;n:type:ShaderForge.SFN_Color,id:9796,x:31842,y:32848,ptovrint:False,ptlb:internalColor,ptin:_internalColor,varname:node_9796,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Blend,id:9149,x:32477,y:32698,varname:node_9149,prsc:2,blmd:8,clmp:True|SRC-7241-RGB,DST-8500-OUT;proporder:7241-2375-1081-9796;pass:END;sub:END;*/

Shader "Shader Forge/LightningBeamCore" {
    Properties {
        [HDR]_FresnelColor ("Fresnel Color", Color) = (0,0.9171712,1,1)
        _Opacity ("Opacity", Range(0, 1)) = 0.8221287
        _FresnelExp ("Fresnel Exp", Range(0.1, 5)) = 0.3574258
        [HDR]_internalColor ("internalColor", Color) = (1,1,1,1)
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
                UNITY_DEFINE_INSTANCED_PROP( float4, _FresnelColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelExp)
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
                float4 _FresnelColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelColor );
                float _FresnelExp_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelExp );
                float3 emissive = saturate((_FresnelColor_var.rgb+pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExp_var)));
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
