Shader "Hidden/CC_HueSaturationValue"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_hue ("Hue", Range(-1.0, 1.0)) = 0
		_saturation ("Saturation", Range(-1.0, 1.0)) = 0
		_value ("Value", Range(-1.0, 1.0)) = 0
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
				#pragma exclude_renderers flash
				#include "UnityCG.cginc"
				#include "Colorful.cginc"

				sampler2D _MainTex;
				fixed _hue;
				fixed _saturation;
				fixed _value;

				fixed4 frag(v2f_img i):COLOR
				{
					fixed4 color = tex2D(_MainTex, i.uv);

					fixed3 hsv = RGBtoHSV(color.rgb);
					hsv.x = rot10(hsv.x + _hue);
					hsv.y = saturate(hsv.y + _saturation);
					hsv.z = saturate(hsv.z + _value);
					fixed4 result = fixed4(HSVtoRGB(hsv), color.a);

					return result;
				}

			ENDCG
		}
	}

	FallBack off
}
