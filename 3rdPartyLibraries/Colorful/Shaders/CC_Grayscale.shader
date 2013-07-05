Shader "Hidden/CC_Grayscale"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_rLum ("Luminance (Red)", Range(0.0, 1.0)) = 0.30
		_gLum ("Luminance (Green)", Range(0.0, 1.0)) = 0.59
		_bLum ("Luminance (Blue)", Range(0.0, 1.0)) = 0.11
		_amount ("Amount", Range(0.0, 1.0)) = 1.0
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
				fixed _rLum;
				fixed _gLum;
				fixed _bLum;
				fixed _amount;

				fixed4 frag(v2f_img i):COLOR
				{
					fixed4 color = tex2D(_MainTex, i.uv);
					fixed lum = dot(color.rgb, fixed3(_rLum, _gLum, _bLum));
					fixed4 result = fixed4(lum, lum, lum, color.a);
					return lerp(color, result, _amount);
				}

			ENDCG
		}
	}

	FallBack off
}
