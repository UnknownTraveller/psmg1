using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Health _DeathTrigger;
    public CollisionEvent _WinningTrigger;
    public StopwatchScore _Score;

    public Text _GameEndDisplay;
    public AnimationCurve _GameEndDisplayAnimation = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1.6f, 1.2f), new Keyframe(2f, 1f));

    private float _GameEndDisplayAnimationPosition;

    public void Start()
    {
        _DeathTrigger.HealthChanged += new EventHandler<Health.HealthChangedEventArgs>(OnHealthChanged);
        _WinningTrigger.Enter += new EventHandler<CollisionEvent.CollisionEventArgs>(OnWinning);
    }

    public void OnHealthChanged(object sender, Health.HealthChangedEventArgs e)
    {
        if (e.newHealth <= 0)
        {
            GameEnd("Du hast leider keine Leben mehr :(");
        }
    }

    public void OnWinning(object sender, EventArgs e)
    {
        GameEnd("Du hast gewonnen!\nDeine Zeit war {1:00}:{2:00} Sekunden!");
    }

    public void GameEnd(string message)
    {
        _Score.Stop();
        var elapsed = _Score.Elapsed;

        message = string.Format(message, _DeathTrigger.Value, (int) elapsed.TotalSeconds, elapsed.Milliseconds / 100);

        _GameEndDisplay.gameObject.SetActive(true);
        _GameEndDisplay.text = message;
    }
	
    public void Update()
    {
        // this is mainly for debug purposes.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameEnd("Schon aufgegeben?");
        }

        if(_GameEndDisplay.gameObject.activeSelf)
        {
            if(_GameEndDisplayAnimationPosition < _GameEndDisplayAnimation.keys[_GameEndDisplayAnimation.length - 1].time)
            {
                _GameEndDisplay.transform.localScale = Vector3.one * _GameEndDisplayAnimation.Evaluate(_GameEndDisplayAnimationPosition);
                _GameEndDisplayAnimationPosition += Time.deltaTime;
            }
        }
    }

    public void OnRestart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
