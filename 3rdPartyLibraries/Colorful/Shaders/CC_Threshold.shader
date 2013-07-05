Shader "Hidden/CC_Threshold"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_threshold ("Threshold", Range(0.0, 1.0)) = 0.5
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
				fixed _threshold;

				fixed4 frag(v2f_img i):COLOR
				{
					fixed4 color = tex2D(_MainTex, i.uv);
					color = luminance(color.rgb) >= _threshold ? fixed4(1.0, 1.0, 1.0, 1.0) : fixed4(0.0, 0.0, 0.0, 0.0);
					return color;
				}

			ENDCG
		}
	}

	FallBack off
}
