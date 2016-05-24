using System;
using UnityEngine;
using UnityEngine.UI;

/**
    Store current health, update UI Display, call events.
**/
public class Health : MonoBehaviour
{
    public int _InitialValue = 100;

    public Text _HealthDisplay;
    public string _HealthFormatString = "Health: {0}";

    public void Start()
    {
        Value = _InitialValue;
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