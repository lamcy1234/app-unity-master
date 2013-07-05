Shader "Hidden/CC_FastVignette"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_sharpness ("Sharpness", Range(-1.0, 1.0)) = 0.1
		_darkness ("Darkness", Range(0, 2.0)) = 0.3
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

				sampler2D _MainTex;
				fixed _sharpness;
				fixed _darkness;

				fixed4 frag(v2f_img i):COLOR
				{
					fixed4 color = tex2D(_MainTex, i.uv);

					float d = distance(i.uv, fixed2(0.5, 0.5));
					color.rgb *= smoothstep(0.8, _sharpness * 0.799, d * (_darkness + _sharpness));

					return color;
				}

			ENDCG
		}
	}

	FallBack off
}
