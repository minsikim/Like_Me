using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
	private float _targetTime;
    public float TargetTime { get { return _targetTime; } }
	private float _currentTime;
    public float CurrentTime { get { return _currentTime; } }

    public float RemainingTime { get { return _targetTime - _currentTime; } }

    public float Percent { get { return CurrentTime / TargetTime; } }

	public event TickDelegate OnTick;

	public Timer(float targetTime)
	{
		_targetTime = targetTime;
	}

	public int Tick(float amount)
	{
		_currentTime += amount;
		if(_currentTime >= _targetTime)
		{
			if(OnTick != null)
			{
				OnTick.Invoke();
			}
			int result = (int)(_currentTime / _targetTime);
			_currentTime -= TargetTime * result;
			return result;
		}
		return 0;
	}

	public void Reset()
	{
		_currentTime = 0;
	}

	public delegate void TickDelegate();

}