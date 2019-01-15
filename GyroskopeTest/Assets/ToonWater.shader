﻿Shader "Roystan/Toon/Water"
{
    Properties
    {	
    _DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
    _DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
    _DepthMaxDistance("Depth Maximum Distance", Float) = 1
    }
    SubShader
    {
        Pass
        {
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                 float4 screenPosition : TEXCOORD2;
            };
               
               
               
            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;

            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;
            
            v2f vert (appdata v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Inside the vertex shader.
                o.screenPosition = ComputeScreenPos(o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
            float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
            float existingDepthLinear = LinearEyeDepth(existingDepth01);
				float depthDifference = existingDepthLinear - i.screenPosition.w;

              float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

return waterColor;
            }
            ENDCG
        }
    }
}