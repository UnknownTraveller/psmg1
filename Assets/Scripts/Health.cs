using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int _InitialValue = 100;

    public Text _HealthDisplay;
    public string _HealthFormatString = "Health: {0}";

    private GameManager _Game;

    public void Start()
    {
        Value = _InitialValue;
        _Game = Camera.main.GetComponent<GameManager>();
    }

    private int _Value;
    public int Value
    {
        get { return _Value; }
        set
        {
            _Value = (value > 0 ? value : 0);
            if(_HealthDisplay != null)
            {
                _HealthDisplay.text = string.Format(_HealthFormatString, _Value);
            }

            if (HealthChanged != null)
            {
                HealthChanged(this, new HealthChangedEventArgs { newHealth = value });
            }
        }
    }


    public class HealthChangedEventArgs : EventArgs
    {
        public int newHealth;
    }
    public event EventHandler<HealthChangedEventArgs> HealthChanged;
}