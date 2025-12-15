using UnityEngine;

public class FootKick : MonoBehaviour
{
    [Header("Kuvvet ayarları (her iki prefab için aynı olacak)")]
    public float kickForce = 7f;
    public float upForce = 2f;

    private bool canKick = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Sadece topa çarptığında ve vurma hakkı varsa tek sefer çalışır
        if (canKick && collision.CompareTag("Ball"))
        {
            KickBall(collision);
        }
    }

    private void KickBall(Collider2D collision)
    {
        Debug.Log("Topa vuruldu!"); // test için log

        Rigidbody2D ballRb = collision.GetComponent<Rigidbody2D>();
        if (ballRb != null)
        {
            // Oyuncunun baktığı yöne göre sağa/sola vur
            float directionX = transform.root.localScale.x > 0 ? 1f : -1f;

            //Normalize edilmiş vektör: hem sağ hem sol aynı kuvvetle vurur
            Vector2 kickDirection = new Vector2(directionX, upForce).normalized;

            // Sabit kuvvet uygula
            ballRb.AddForce(kickDirection * kickForce, ForceMode2D.Impulse);
            
            Debug.Log("Topa vuruldu! Yön: " + directionX);
        }

        // Tek vuruşluk izin biter
        canKick = false;
    }

    // Animator eventlerinden çağırılacak
    public void EnableKick()
    {
        canKick = true;
    }

    public void DisableKick()
    {
        canKick = false;
    }
}
