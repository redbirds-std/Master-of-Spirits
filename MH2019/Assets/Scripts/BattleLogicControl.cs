using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleLogicControl : MonoBehaviour
{
    private int _tacts = 80; // Количество тактов в покое
    private int _delta = 40; // Максимальная дельта для определения линии

    public Transform Wand;

    
    private List<Vector2> _cast;
    private Vector3? _waitPosition;
    private float _tact;

    // Start is called before the first frame update
    void Start()
    {
        _waitPosition = new Vector3(Wand.position.x, Wand.position.y, Wand.position.z);
        _tact = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Cast();
        Wait();
    }

    private void Cast()
    {
        if (Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);

        var position = Camera.main.ScreenToWorldPoint(touch.position);
        position.z = 0f;
        Wand.position = position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                StartCast();
                break;
            case TouchPhase.Moved:
                _cast.Add(new Vector2(touch.position.x, touch.position.y));
                break;
            case TouchPhase.Ended:
                EndCast();
                break;
            case TouchPhase.Canceled:
                EndCast();
                break;
            default:
                break;
        }
    }

    private void StartCast()
    {
        _cast = new List<Vector2>();
        _waitPosition = null;
    }

    private void EndCast()
    {
        AnalisePath();
        _cast = null;
        _waitPosition = new Vector3(Wand.position.x, Wand.position.y, Wand.position.z);
        _tact = 0;
    }

    private void Wait()
    {
        if (!_waitPosition.HasValue)
            return;

        _tact++;

        var move = _tact > _tacts/2 ? _tacts - _tact : _tact;

        Wand.position = new Vector3(_waitPosition.Value.x + move * 2 / _tacts,
            _waitPosition.Value.y + move/_tacts/2,
            _waitPosition.Value.z);

        if (_tact > _tacts)
            _tact = 0;
    }

    private void AnalisePath()
    {
        Debug.Log($"IsVertical : {IsVertical()}");

        Debug.Log($"IsHorizontal : {IsHorizontal()}");
    }

    private bool IsVertical()
    {
        if (_cast.Count < 2)
            return false;

        var first = _cast.First();
        var last = _cast.Last();
        var deltaX = first.x - last.x;
        var deltaY = first.y - last.y;
        Debug.Log($"deltaX = {deltaX}     deltaY = {deltaY}");

        var bigValue = _delta * 10;

        if (-_delta > deltaX || deltaX > _delta)
            return false;

        if (-bigValue < deltaY && deltaY < bigValue)
            return false;

        var averange = first.x + deltaX / 2;

        var max = averange + _delta;
        var min = averange - _delta;

        foreach (var point in _cast)
        {
            if (min > point.x || point.x > max)
                return false;
        }

        return true;
    }

    private bool IsHorizontal()
    {
        if (_cast.Count < 2)
            return false;

        var first = _cast.First();
        var last = _cast.Last();
        var deltaX = first.x - last.x;
        var deltaY = first.y - last.y;
        Debug.Log($"deltaX = {deltaX}     deltaY = {deltaY}");

        var bigValue = _delta * 10;

        if (-_delta > deltaY || deltaY > _delta)
            return false;

        if (-bigValue < deltaX && deltaX < bigValue)
            return false;

        var averange = first.y + deltaY / 2;

        var max = averange + _delta;
        var min = averange - _delta;

        foreach (var point in _cast)
        {
            if (min > point.y || point.y > max)
                return false;
        }

        return true;
    }
}
