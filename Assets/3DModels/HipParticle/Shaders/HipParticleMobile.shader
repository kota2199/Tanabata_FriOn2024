﻿Shader "Custom/HipParticleMobile"
{
    Properties
    {
        [HDR] _Pos ("Pos", 2D) = "white" {}
        [HDR] _Col ("Col", 2D) = "white" {}
        [HDR] _Mag ("Mag", 2D) = "white" {}
        _Mag_param ("Star brightness", Range(0, 250)) = 100
        _Mag_min ("min brightness", Range(0, 1)) = 0.025
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue"="Transparent+1" }
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZWrite Off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma exclude_renderers gles
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
                float3 color : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _Pos;
            float4 _Pos_HDR;
            float4 _Pos_TexelSize;
            sampler2D _Col;
            float4 _Col_HDR;
            float4 _Col_TexelSize;
            sampler2D _Mag;
            float4 _Mag_HDR;
            float _Mag_param;
            float _Mag_min;
            
            //テクスチャからfloat値を取り出す
            float3 unpack(float2 uv) {
                float texWidth = _Pos_TexelSize.z;
                float2 e = float2(-1.0/texWidth/2, 1.0/texWidth/2);
                uint3 v0 = uint3(DecodeHDR(tex2Dlod(_Pos, float4(uv + e.xy,0,0)), _Pos_HDR).xyz * 255.) << 0;
                uint3 v1 = uint3(DecodeHDR(tex2Dlod(_Pos, float4(uv + e.yy,0,0)), _Pos_HDR).xyz * 255.) << 8;
                uint3 v2 = uint3(DecodeHDR(tex2Dlod(_Pos, float4(uv + e.xx,0,0)), _Pos_HDR).xyz * 255.) << 16;
                uint3 v3 = uint3(DecodeHDR(tex2Dlod(_Pos, float4(uv + e.yx,0,0)), _Pos_HDR).xyz * 255.) << 24;
                uint3 v = v0 + v1 + v2 + v3;
                return asfloat(v);
            }
            
            //テクスチャからint値を取り出す
            float3 unpack_int(float2 uv) {
                float v = 0;
                v *= 256.; v += DecodeHDR(tex2Dlod(_Mag, float4(uv,0,0)), _Mag_HDR).r * 255.;
                v *= 256.; v += DecodeHDR(tex2Dlod(_Mag, float4(uv,0,0)), _Mag_HDR).g * 255.;
                v *= 256.; v += DecodeHDR(tex2Dlod(_Mag, float4(uv,0,0)), _Mag_HDR).b * 255.;
                v /= 16777216;
                return v;
            }
            
            float3 ComputeAlignAxisX() 
            {
#if defined(USING_STEREO_MATRICES)
                return normalize(unity_StereoCameraToWorld[0]._m00_m10_m20 + unity_StereoCameraToWorld[1]._m00_m10_m20);
#else
                return unity_CameraToWorld._m00_m10_m20;
#endif
            }
            float3 ComputeAlignAxisY()
            {
#if defined(USING_STEREO_MATRICES)
                return normalize(unity_StereoCameraToWorld[0]._m01_m11_m21 + unity_StereoCameraToWorld[1]._m01_m11_m21);
#else
                return unity_CameraToWorld._m01_m11_m21;
#endif
            }
            
            v2f vert (appdata v)
            {
                v2f o;
                float texWidth = _Col_TexelSize.z;
                float2 uv = float2((floor(v.vertex.z / texWidth) + 0.5) / texWidth, (fmod(v.vertex.z, texWidth) + 0.5) / texWidth);

                float3 p = unpack(uv).xzy;
                float3 c = DecodeHDR(tex2Dlod(_Col, float4(uv,0,0)), _Col_HDR).xyz;

                float3 worldPos = mul(unity_ObjectToWorld, float4(p, 1));
                o.color = c;

                float sz = unpack_int(uv) * _Mag_param + _Mag_min;
                sz *= pow(determinant((float3x3)UNITY_MATRIX_M),1/3.0);
                if(length(c) < 0.1 && length(p) < 0.1) sz = 0;

                float3 xAxis = ComputeAlignAxisX();
                float3 yAxis = ComputeAlignAxisY();
                float2 offset;
                float3x2 triVert = float3x2(
                    float2(0, 1),
                    float2(-0.9, -0.5),
                    float2(0.9, -0.5));

                o.uv = triVert[round(v.vertex.y)];
                offset = o.uv * sz;
                o.vertex = UnityWorldToClipPos(worldPos + xAxis * offset.x + yAxis * offset.y);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float l = length(i.uv);
                clip(0.5-l);
                return float4(i.color, 1.0 - pow(max(0.0, abs(l) * 2.2 - 0.1), 0.2));
            }
            ENDCG
        }
    }
}