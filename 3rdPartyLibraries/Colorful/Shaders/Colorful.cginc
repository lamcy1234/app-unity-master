/* Utils */
fixed luminance(fixed3 color)
{
	return dot(color, fixed3(0.30, 0.59, 0.11));
}

fixed rot(fixed value, fixed low, fixed hi)
{
	if (value < low)		value += hi;
	else if (value > hi)	value -= hi;
	return value;
}

fixed rot10(fixed value)
{
	return rot(value, 0.0, 1.0);
}

fixed4 pixelate(sampler2D tex, fixed2 uv, float scale)
{
	float ds = 1.0 / scale;
	fixed2 coord = fixed2(ds * ceil(uv.x / ds), ds * ceil(uv.y / ds));
	return fixed4(tex2D(tex, coord).xyzw);
}

float simpleNoise(float x, float y, float seed, float phase)
{
	float n = x * y * phase * seed;
	return fmod(n, 13) * fmod(n, 123);
}

/* Color conversion */
fixed3 HSVtoRGB(fixed3 hsv)
{
	fixed H = hsv.x * 6.0;
	fixed R = abs(H - 3.0) - 1.0;
	fixed G = 2 - abs(H - 2.0);
	fixed B = 2 - abs(H - 4.0);
	fixed3 hue = saturate(fixed3(R, G, B));
	return ((hue - 1) * hsv.y + 1) * hsv.z;
}

fixed3 RGBtoHSV(fixed3 rgb)
{
	fixed3 hsv = fixed3(0.0, 0.0, 0.0);

	hsv.z = max(rgb.r, max(rgb.g, rgb.b));
	float cMin = min(rgb.r, min(rgb.g, rgb.b));
	float C = hsv.z - cMin;

	if (C != 0)
	{
		hsv.y = C / hsv.z;
		fixed3 delta = (hsv.z - rgb) / C;
		delta.rgb -= delta.brg;
		delta.rg += fixed2(2.0, 4.0);

		if (rgb.r >= hsv.z)			hsv.x = delta.b;
		else if (rgb.g >= hsv.z)	hsv.x = delta.r;
		else						hsv.x = delta.g;

		hsv.x = frac(hsv.x / 6);
	}

	return hsv;
}
