Shader "Hidden/CC_AnalogTV"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_phase ("Phase (time)", Float) = 0.01
		_grayscale ("Grayscale", Float) = 0.0
		_noiseIntensity ("Static noise intensity", Float) = 0.5
		_scanlinesIntensity ("Scanlines intensity", Float) = 2.0
		_scanlinesCount ("Scanlines count", Float) = 1024

		_distortion ("Distortion", Float) = 0.2
		_cubicDistortion ("Cubic Distortion", Float) = 0.6
		_scale ("Scale (Zoom)", Float) = 0.8
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

				float _phase;
				float _grayscale;
				float _noiseIntensity;
				float _scanlinesIntensity;
				float _scanlinesCount;

				float _distortion;
				float _cubicDistortion;
				float _scale;

				float2 barrelDistortion(float2 coord) 
				{
					// Inspired by SynthEyes lens distortion algorithm
					// See http://www.ssontech.com/content/lensalg.htm

					float2 h = coord.xy - float2(0.5, 0.5);
					float r2 = h.x * h.x + h.y * h.y;
					float f = 1.0 + r2 * (_distortion + _cubicDistortion * sqrt(r2));

					return f * _scale * h + 0.5;
				}

				fixed4 frag(v2f_img i):COLOR
				{
					float2 coord = barrelDistortion(i.uv);
					fixed4 color = tex2D(_MainTex, coord);

					float n = simpleNoise(coord.x, coord.y, 1234.0, _phase);
					float dx = fmod(n, 0.01);

					float3 result = color.rgb + color.rgb * saturate(0.1 + dx * 100.0);
					float2 sc = float2(sin(coord.y * _scanlinesCount), cos(coord.y * _scanlinesCount));
					result += color.rgb * sc.xyx * _scanlinesIntensity;
					result = color.rgb + saturate(_noiseIntensity) * (result - color.rgb);

					if(_grayscale != 0)
					{
						fixed lum = luminance(result);
						result = fixed3(lum, lum, lum);
					}

					return fixed4(result, color.a);
				}

			ENDCG
		}
	}

	FallBack off
}
