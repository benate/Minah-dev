using UnityEngine;
using System.Collections;

public class FloatRangeValue
{
	float max;
	float min;

	protected float m_value = float.MaxValue;
	public float value
	{
		get
		{
			if (m_value == float.MaxValue)
			{
				calculateValue();
			}
			return m_value;
		}
	}

	private FloatRangeValue() { }
	public FloatRangeValue(float min, float max)
	{
	    this.min = min;
	    this.max = max;
	}

	public FloatRangeValue(FloatRangeValue rhs)
	{
		this.min = rhs.min;
		this.max = rhs.max;

		this.m_value = rhs.value;
	}

	public void Recalculate()
	{
		calculateValue();
	}

	void calculateValue()
	{
		trim();

		m_value = Random.Range(min, max);
	}

	void trim()
	{
		if (min > max)
		{
			float temp = min;

			min = max;
			max = temp;
		}
	}
}


public class IntRangeValue
{
	int max;
	int min;

	protected int m_value = int.MaxValue;
	public int value
	{
		get
		{
			if (m_value == int.MaxValue)
			{
				calculateValue();
			}
			return m_value;
		}
	}

	private IntRangeValue() { }
	public IntRangeValue(int min, int max)
	{
	    this.min = min;
	    this.max = max;
	}

	void calculateValue()
	{
		trim();

		m_value = Random.Range(min, max);
	}

	void trim()
	{
		if (min > max)
		{
			int temp = min;

			min = max;
			max = temp;
		}
	}
}
