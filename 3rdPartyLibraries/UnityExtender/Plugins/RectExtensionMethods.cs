using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class UnityRectExtensions
{
	
	/// <summary>
	/// Test if this rect intersects with another rect.
	/// </summary>
	/// <param name="rect">
	/// A <see cref="Rect"/>
	/// </param>
	/// <param name="other">
	/// A <see cref="Rect"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public static bool Intersects (this Rect rect, Rect other)
	{
		if (rect.xMax < other.xMin)
			return false;
		if (rect.xMin > other.xMax)
			return false;
		if (rect.yMax < other.yMin)
			return false;
		if (rect.yMin > other.yMax)
			return false;
		return true;
	}

	/// <summary>
	/// Test if this rect intersects with any rects in the list.
	/// </summary>
	/// <param name="rect">
	/// A <see cref="Rect"/>
	/// </param>
	/// <param name="rects">
	/// A <see cref="IEnumerable(Rect)"/> The list of rects to check against.
	/// </param>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public static bool Intersects (this Rect rect, IEnumerable<Rect> rects)
	{
		foreach (var i in rects) {
			if (rect.Intersects (i))
				return true;
		}
		return false;
	}

}
