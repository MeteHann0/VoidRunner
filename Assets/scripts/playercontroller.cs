using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 

public class playercontroller : MonoBehaviour
{
    public float thrustForce = 15f;
    public float maxSpeed = 10f;
    public GameObject boosterFlame;

    
    public TextMeshProUGUI scoreText;

    public int score = 0;

    Rigidbody2D rb;
    Vector2 targetDirection;
    bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
        UpdateScoreUI();
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            mousePos.z = 0f;

            if (Vector3.Distance(transform.position, mousePos) > 0.5f)
            {
                targetDirection = (mousePos - transform.position).normalized;
                isMoving = true;
            }
        }
        else
        {
            isMoving = false;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Unity 6 uyumlu yüksek performanslý fonksiyon ile GameManager'ý buluyoruz
        GameManager gm = Object.FindAnyObjectByType<GameManager>();

        if (gm != null)
        {
            gm.GameOver(); // GameManager'daki ekraný açma fonksiyonunu tetikle
        }

        Destroy(gameObject); // Roketi yok et
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Point"))
        {
            score += 10;

            
            UpdateScoreUI();

            Destroy(other.gameObject);
        }
    }

    
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * 15f);
            rb.MoveRotation(smoothAngle);

            if (rb.linearVelocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.up * thrustForce);
            }
        }
    }
}