using UnityEngine;

// the Explosion is shamelessly copied from our main project (all these are still by me).
public class Bomb : MonoBehaviour
{
    public int _Damage = 10;
    public float _ExplosionForce = 50f;
    public ParticleSystem _Explosion;

    private AudioSource _AudioSource;
    private Renderer _Renderer;
    private Collider _Collider;

    public void Start()
    {
        // _AudioSource = GetComponentInParent<AudioSource>();
        _AudioSource = GetComponent<AudioSource>();
        _Renderer = GetComponent<Renderer>();
        _Collider = GetComponent<Collider>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Value -= _Damage;

            // disable components so we don't render and cannot collide while we play the sound and the particle effect
            _Renderer.enabled = false;
            _Collider.enabled = false;

            // add explosion force if this is a rigidbody
            var rigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if(rigidbody)
            {
                rigidbody.AddExplosionForce(_ExplosionForce, gameObject.transform.position, 5f, 0f, ForceMode.Impulse);
            }

            // play sound and effect
            _AudioSource.Play();
            _Explosion.Play();
            Destroy(gameObject, Mathf.Max(_Explosion.duration, _AudioSource.clip.length));
        }
    }
}
