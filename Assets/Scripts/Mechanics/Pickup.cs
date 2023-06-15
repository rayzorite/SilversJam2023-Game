using Player;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float attractorSpeed;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) AttractPickup(other);
    }

    private void AttractPickup(Component other)
    {
        transform.position =  Vector3.MoveTowards(transform.position, other.transform.position, attractorSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, other.transform.position) < 0.5f) Destroy(gameObject);

        if (ColorSwitcher.Instance.isRedColor || ColorSwitcher.Instance.isBlueColor)
            ColorSwitcher.Instance.colorSwitchParticle.Play();
    }
}
