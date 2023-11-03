using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Controls))]
public abstract class Animal : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _prefabDeathParticles;

    [SerializeField]
    private ParticleSystem _attackParticles;

    public int Experience;
    public AnimalSo AnimalSo;

    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected AnimalAttackTrigger _attackTrigger;
    public Animator Animator;

    public UnityEvent OnDeath = new();
    public UnityEvent OnExperience = new();

    protected Controls _controls;

    private SpawnManager _spawnManager;

    protected float _size = 1;
    protected Vector3 _attackParticleSize = Vector3.one;

    public bool IsGrounded;

    private float _attackCooldown;

    public AudioClip deathSound;
    public AudioClip killSound;


    void Start()
    {
        _size = transform.localScale.x;

        if (_prefabDeathParticles == null)
            _prefabDeathParticles = GlobalDataSo.Instance.PrefabDefaultDeathEffect;

        _spawnManager = SpawnManager.Instance;
        var controlComponents = GetComponents<Controls>();
        _controls = controlComponents.FirstOrDefault(x => x.enabled);
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attackTrigger = GetComponentInChildren<AnimalAttackTrigger>();
        TryGetComponent<Animator>(out Animator);
        _collider = GetComponent<Collider2D>();

        if (_attackParticles)
            _attackParticleSize = _attackParticles.transform.localScale;
    }

    void Update()
    {
        if (_attackCooldown > 0)
            _attackCooldown -= Time.deltaTime;
        else if (_controls.Attack)
        {
            _attackCooldown = 1f;
            Attack();
        }

        // Flip if Animal is heading the other way
        if (_rigidbody.velocity.x < -0.1f)
        {
            transform.localScale = Vector3.one * _size;
            if (_attackParticles)
                _attackParticles.transform.localScale = _attackParticleSize;
        }
        else if (_rigidbody.velocity.x > 0.1f)
        {
            transform.localScale = new Vector3(-_size, _size, _size);
            if (_attackParticles)
                _attackParticles.transform.localScale = 
                    _attackParticleSize + new Vector3(0, -_attackParticleSize.y*2, 0);
        }
    }

    public void SetControls(Controls controls)
    {
        this._controls = controls;
    }

    public void Attack()
    {
        var animal = _attackTrigger.TargetList.FirstOrDefault();
        if (animal != null)
        {
            if (AnimalSo.Level > animal.AnimalSo.Level)
            {
                AddExperience(animal.Experience);

                PlayAttackParticles();

                LeanTween.move(gameObject, animal.transform.position, 0.5f)
                    .setOnComplete(() =>
                    {
                        StopAttackParticles();

                        if (Experience >= AnimalSo.XpUntilLevelup)
                        {
                            EffectsManager.Instance.SpawnEffect(
                                GlobalDataSo.Instance.PrefabLevelUpEffect,
                                transform.position);
                            _spawnManager.LevelUp(this);
                        }
                    })
                    .setEaseInOutBounce();

                //SoundController.Instance.PlaySoundEffect(killSound);
                print($"{name} killed {animal.name}");
                animal.Kill();
            }
            else
            {
                print($"{animal.name} killed {name}");

                animal.PlayAttackParticles();
                LeanTween.move(animal.gameObject, transform.position, 0.5f)
                    .setOnComplete(() =>
                    {
                        animal.StopAttackParticles();
                    })
                    .setEaseInOutBounce();
                Kill();
            }
        }
    }

    public void PlayAttackParticles()
    {
        if (_attackParticles != null)
        {
            //_attackParticles.gameObject.SetActive(true);
            _attackParticles.Play();
            LeanTween.cancel(_attackParticles.gameObject);
            LeanTween.scale(_attackParticles.gameObject, _attackParticleSize, 0.2f)
                .setFrom(Vector3.zero)
                .setEaseOutBounce();
        }
    }

    public void StopAttackParticles()
    {
        if(_attackParticles != null)
        {
            //_attackParticles.gameObject.SetActive(false);
            LeanTween.cancel(_attackParticles.gameObject);
            LeanTween.scale(_attackParticles.gameObject, Vector3.zero, 0.2f)
                .setOnComplete(() => _attackParticles.Stop())
                .setEaseOutBounce();
        }
    }

    private void AddExperience(int xp)
    {
        Experience += xp;
        OnExperience.Invoke();
    }

    public void Kill()
    {
        OnDeath.Invoke();
        EffectsManager.Instance.SpawnEffect(_prefabDeathParticles ?? GlobalDataSo.Instance.PrefabDefaultDeathEffect, transform.position);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            IsGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            IsGrounded = false;
    }
}
