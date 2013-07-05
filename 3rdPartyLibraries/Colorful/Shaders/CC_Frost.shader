Shader "Hidden/CC_Frost"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_scale ("Scale", Float) = 1.2
		_sharpness ("Sharpness", Range(0, 1.0)) = 0.4
		_darkness ("Darkness", Range(0, 2.0)) = 0.35
	}

	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				#include "Colorful.cginc"

				sampler2D _MainTex;
				fixed _scale;
				fixed _enableVignette;
				fixed _sharpness;
				fixed _darkness;

				fixed4 frag(v2f_img i):COLOR
				{
					float2 uv = i.uv;
					fixed4 color = tex2D(_MainTex, uv);

					float n = simpleNoise(uv.x, uv.y, 1234, 1.0);

					float dx = -0.005 + fmod(n, 0.008);
					float dy = -0.006 + fmod(n, 0.01);

					fixed4 frosted = tex2D(_MainTex, uv + float2(dx, dy) * _scale);

					fixed4 vignette;
					if (_enableVignette != 0)
					{
						vignette = fixed4(1.0, 1.0, 1.0, 1.0);
						float d = distance(i.uv, fixed2(0.5, 0.5));
						vignette.rgb *= smoothstep(0.8, _sharpness * 0.799, d * (_darkness + _sharpness));
					}
					else vignette = fixed4(0.0, 0.0, 0.0, 0.0);

					return lerp(frosted, color, vignette);
				}

			ENDCG
		}
	}

	FallBack off
}
