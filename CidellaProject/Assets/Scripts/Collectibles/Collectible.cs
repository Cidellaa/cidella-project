using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private bool isCookie;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isCookie) collision.GetComponent<Health>().AddHealth(1);
            else GameManager.Instance.GetPlayer().candyCount++;
            
            Destroy(gameObject);
        }
    }
}
