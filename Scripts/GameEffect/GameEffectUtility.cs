using UnityEngine;
using System.Collections;

public class GameEffectUtility
{
	public static float Bezierat(float a, float b, float c, float d, float t)
	{
		return (Mathf.Pow(1-t,3) * a +
				3 * t * (Mathf.Pow(1 - t, 2)) * b +
				3 * Mathf.Pow(t, 2) * (1 - t) * c +
				Mathf.Pow(t, 3) * d);
	}

	// CatmullRom Spline formula:
	public Vector2 CardinalSplineAt(ref Vector2 p0, ref Vector2 p1, ref Vector2 p2, ref Vector2 p3, float tension, float t)
	{
		float t2 = t * t;
		float t3 = t2 * t;
    
		/*
		 * Formula: s(-ttt + 2tt - t)P1 + s(-ttt + tt)P2 + (2ttt - 3tt + 1)P2 + s(ttt - 2tt + t)P3 + (-2ttt + 3tt)P3 + s(ttt - tt)P4
		 */
		float s = (1 - tension) / 2;
	
		float b1 = s * ((-t3 + (2 * t2)) - t);                      // s(-t3 + 2 t2 - t)P1
		float b2 = s * (-t3 + t2) + (2 * t3 - 3 * t2 + 1);          // s(-t3 + t2)P2 + (2 t3 - 3 t2 + 1)P2
		float b3 = s * (t3 - 2 * t2 + t) + (-2 * t3 + 3 * t2);      // s(t3 - 2 t2 + t)P3 + (-2 t3 + 3 t2)P3
		float b4 = s * (t3 - t2);                                   // s(t3 - t2)P4
    
		float x = (p0.x*b1 + p1.x*b2 + p2.x*b3 + p3.x*b4);
		float y = (p0.y*b1 + p1.y*b2 + p2.y*b3 + p3.y*b4);
	
		return new Vector2(x,y);
	}
}
