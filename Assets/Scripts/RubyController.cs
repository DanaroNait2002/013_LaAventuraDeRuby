using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    [Header("Health and related")]
    //
    public int maxHealth = 5;
    [SerializeField] private float timeInvincible = 2.0f;
    public int Health { get { return currentHealth; } }

    public int currentHealth;
    [SerializeField] private bool isInvincible;
    [SerializeField] private float invincibleTimer;
    //
    [Header("Movement")]
    [SerializeField] private Rigidbody2D rigidbody2d;
    public float speed = 4.0f;
    float horizontal;
    float vertical;

    [Header("Shooting")]
    public GameObject projectilePrefab;

    [Header("Sound")]
    public AudioClip throwSound;
    public AudioClip damagedSound;
    public AudioSource movingSound;
    public AudioSource audioSource;

    [Header("UI")]
    Vector2 lookDirection = new Vector2(1, 0);
    Animator animator;

    #region START, UPDATE Y FIXED UPDATE

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        AudioListener.pause = false;

    }
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
        {
            if (!movingSound.isPlaying)
            {
                movingSound.Play();
            }
        }
        else
        {
            if (movingSound.isPlaying)
            {
                movingSound.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
    

    public void ChangeHealth(int amount)
    {
            if (currentHealth == 0)
            {
                StartCoroutine(LoseCoroutine());
            }
            if (amount < 0)
            {
                if (isInvincible)
                    return;
                isInvincible = true;
                invincibleTimer = timeInvincible;

                animator.SetTrigger("Hit");
                audioSource.PlayOneShot(damagedSound);
            }
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

        void Launch()
        {
                GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f,
                    Quaternion.identity);
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.Launch(lookDirection, 300);
                animator.SetTrigger("Launch");
                audioSource.PlayOneShot(throwSound);
                StartCoroutine(ProjectileCooldownCoroutine());
        }

        IEnumerator ProjectileCooldownCoroutine()
        {
            yield return new WaitForSeconds(0.2f);
        }

        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("The scene has been reset");
        }

        public void ExitGame()
        {
            Application.Quit();
            Debug.Log("The game has been closed");
        }

        IEnumerator WinCoroutine()
        {
            yield return new WaitForSeconds(1);
            AudioListener.pause = true;
            Time.timeScale = 0;
        }

        IEnumerator LoseCoroutine()
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            yield return new WaitForSeconds(1);
        }
    }