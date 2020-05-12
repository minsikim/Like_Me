using UnityEngine;
using UnityEngine.UI.ProceduralImage;
using UnityEngine.Events;

public class TimerUIController : MonoBehaviour
{
    public TimerType type;
    public float duration = 5f;

    [SerializeField]
    private GameObject _progression;

    public UnityEvent OnStart;
    public UnityEvent OnTimeUp;
    public UnityEvent OnReset;

    [SerializeField]
    [ReadOnly] private float _currentTime = 1f;


    void Start()
    {
        if(OnStart != null)
        {
            OnStart.Invoke();
        }
        Debug.Log("TimerUIController: " + transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime > 0)
            SetTime(_currentTime - Time.deltaTime / duration);
        else
        {
            TimeUp();
        }

        UpdateProgression();
    }

    public void ResetTime()
    {
        SetTime(1f);
    }

    private void SetTime(float t)
    {
        _currentTime = t;
    }

    private void TimeUp()
    {
        transform.parent.GetComponent<GameInfoAreaController>().DeleteTimerObject(gameObject);
        OnTimeUp.Invoke();
    }

    private void UpdateProgression()
    {
        _progression.GetComponent<ProceduralImage>().fillAmount = _currentTime;
    }
}