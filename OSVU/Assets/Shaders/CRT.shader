Shader "Custom/CRT"
{
	Properties
	{
		_PixelsX("Pixels X", Float) = 64
		_PixelsY("Pixels Y", Float) = 64
		_GlitchPixelsX("Glitch Pixels X", Float) = 8
		_GlitchPixelsY("Glitch Pixels Y", Float) = 8
		_MainTexture("Main Texture", 2D) = "white" {}
		_LCDPixels("LCD Pixels", 2D) = "white" {}
		_MinDistance("Min Distance", Float) = 0.25
		_MaxDistance("Max Distance", Float) = 5
		_FarBrightness("Far Brightness", Float) = 1.2
		_CloseBrightness("Close Brightness", Float) = 1
		_PulseSpeed("Pulse Speed", Float) = 1.5
		_DimSpeed("Dim Speed", Float) = 0.25
		_DimFreq("Dim Freq", Float) = 1
		_PulseFreq("Pulse Freq", Float) = 1
		_PulseLead("Pulse Lead", Float) = 0.1
		_DimWidth("Dim Width", Float) = 0.01
		_PulseWidth("Pulse Width", Float) = 0.06
		_PulsePower("Pulse Power", Float) = 3
		_DimPower("Dim Power", Float) = 2
		_DimStrength("Dim Strength", Float) = 0.8
		_EdgeGlowColour("Edge Glow Colour", Color) = (0.1773662,0.1588643,0.3207547,1)
		_WobbleStrength("Wobble Strength", Float) = 0.01
		_WobbleSpeed("Wobble Speed", Float) = 1
		_WobbleFreq("Wobble Freq", Float) = -1
		_Brightness("Brightness", Float) = 1.8
		[HideInInspector] _texcoord("", 2D) = "white" {}
		[HideInInspector] __dirty("", Int) = 1
	}

		SubShader
		{
			Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
			Cull Back
			CGINCLUDE
			#include "UnityShaderVariables.cginc"
			#include "UnityPBSLighting.cginc"
			#include "Lighting.cginc"
			#pragma target 3.0
			struct Input
			{
				float3 worldPos;
				float2 uv_texcoord;
			};

			uniform float _CloseBrightness;
			uniform float _FarBrightness;
			uniform float _MinDistance;
			uniform float _MaxDistance;
			uniform sampler2D _LCDPixels;
			uniform float _PixelsX;
			uniform float _PixelsY;
			uniform sampler2D _MainTexture;
			uniform float _GlitchPixelsX;
			uniform float _GlitchPixelsY;
			uniform float _PulseLead;
			uniform float _PulseFreq;
			uniform float _PulseSpeed;
			uniform float _PulseWidth;
			uniform float _PulsePower;
			uniform float _WobbleStrength;
			uniform float _WobbleFreq;
			uniform float _WobbleSpeed;
			uniform float _DimFreq;
			uniform float _DimSpeed;
			uniform float _DimWidth;
			uniform float _DimPower;
			uniform float _DimStrength;
			uniform float4 _EdgeGlowColour;
			uniform float _Brightness;

			inline fixed4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
			{
				return fixed4(0, 0, 0, s.Alpha);
			}

			void surf(Input i , inout SurfaceOutput o)
			{
				float3 ase_worldPos = i.worldPos;
				float clampResult77 = clamp(distance(_WorldSpaceCameraPos , ase_worldPos) , _MinDistance , _MaxDistance);
				float lerpResult82 = lerp(_CloseBrightness , _FarBrightness , (0.0 + (clampResult77 - _MinDistance) * (1.0 - 0.0) / (_MaxDistance - _MinDistance)));
				float2 appendResult5 = (float2(_PixelsX , _PixelsY));
				float4 tex2DNode18 = tex2D(_LCDPixels, (appendResult5 * i.uv_texcoord));
				float2 appendResult68 = (float2(_GlitchPixelsX , _GlitchPixelsY));
				float2 temp_output_92_0 = ((floor((appendResult5 * i.uv_texcoord)) / appendResult5) + (float2(0.5,0.5) / appendResult5));
				float2 break100 = temp_output_92_0;
				float mulTime42 = _Time.y * _PulseSpeed;
				float temp_output_56_0 = (1.0 - _PulseWidth);
				float clampResult53 = clamp(sin(((_PulseLead * break100.x) + (_PulseFreq * break100.y) + mulTime42)) , temp_output_56_0 , 1.0);
				float2 lerpResult69 = lerp(appendResult5 , appendResult68 , pow((0.0 + (clampResult53 - temp_output_56_0) * (1.0 - 0.0) / (1.0 - temp_output_56_0)) , _PulsePower));
				float2 break106 = floor(log2(lerpResult69));
				float2 appendResult108 = (float2(pow(2.0 , break106.x) , pow(2.0 , break106.y)));
				float mulTime185 = _Time.y * _WobbleSpeed;
				float2 appendResult190 = (float2(0.0 , (_WobbleStrength * sin(((_WobbleFreq * i.uv_texcoord.x) + mulTime185)))));
				float4 tex2DNode14 = tex2D(_MainTexture, (((floor((i.uv_texcoord * appendResult108)) / appendResult108) + (float2(0.5,0.5) / appendResult108)) + appendResult190));
				float mulTime165 = _Time.y * _DimSpeed;
				float temp_output_170_0 = (1.0 - _DimWidth);
				float clampResult171 = clamp(sin(((_DimFreq * temp_output_92_0.y) + mulTime165)) , temp_output_170_0 , 1.0);
				float3 clampResult147 = clamp(((lerpResult82 * ((tex2DNode18).rgb * (tex2DNode14).rgb * (1.0 + (pow((0.0 + (clampResult171 - temp_output_170_0) * (1.0 - 0.0) / (1.0 - temp_output_170_0)) , _DimPower) - 0.0) * (_DimStrength - 1.0) / (1.0 - 0.0)))) + ((1.0 + (pow((0.0 + (clampResult171 - temp_output_170_0) * (1.0 - 0.0) / (1.0 - temp_output_170_0)) , _DimPower) - 0.0) * (_DimStrength - 1.0) / (1.0 - 0.0)) * ((1.414 * distance(i.uv_texcoord , float2(0.5,0.5))) * (_EdgeGlowColour).rgb))) , float3(0,0,0) , float3(1,1,1));
				o.Emission = (clampResult147 * _Brightness);
				o.Alpha = (tex2DNode18.a * tex2DNode14.a);
			}

			ENDCG
			CGPROGRAM
			#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows 

			ENDCG
			Pass
			{
				Name "ShadowCaster"
				Tags{ "LightMode" = "ShadowCaster" }
				ZWrite On
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#pragma multi_compile_shadowcaster
				#pragma multi_compile UNITY_PASS_SHADOWCASTER
				#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
				#include "HLSLSupport.cginc"
				#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
					#define CAN_SKIP_VPOS
				#endif
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				sampler3D _DitherMaskLOD;
				struct v2f
				{
					V2F_SHADOW_CASTER;
					float2 customPack1 : TEXCOORD1;
					float3 worldPos : TEXCOORD2;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};
				v2f vert(appdata_full v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_OUTPUT(v2f, o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					Input customInputData;
					float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
					o.customPack1.xy = customInputData.uv_texcoord;
					o.customPack1.xy = v.texcoord;
					o.worldPos = worldPos;
					TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
					return o;
				}
				fixed4 frag(v2f IN
				#if !defined( CAN_SKIP_VPOS )
				, UNITY_VPOS_TYPE vpos : VPOS
				#endif
				) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(IN);
					Input surfIN;
					UNITY_INITIALIZE_OUTPUT(Input, surfIN);
					surfIN.uv_texcoord = IN.customPack1.xy;
					float3 worldPos = IN.worldPos;
					fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
					surfIN.worldPos = worldPos;
					SurfaceOutput o;
					UNITY_INITIALIZE_OUTPUT(SurfaceOutput, o)
					surf(surfIN, o);
					#if defined( CAN_SKIP_VPOS )
					float2 vpos = IN.pos;
					#endif
					half alphaRef = tex3D(_DitherMaskLOD, float3(vpos.xy * 0.25, o.Alpha * 0.9375)).a;
					clip(alphaRef - 0.01);
					SHADOW_CASTER_FRAGMENT(IN)
				}
				ENDCG
			}
		}
	Fallback "Diffuse"
}
