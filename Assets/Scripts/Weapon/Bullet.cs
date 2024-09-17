using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("target"))
        {
            print("hit" + objectWeHit.gameObject.name + "!");
            BulletImpactEffect(objectWeHit);
            Destroy(gameObject);

        }
        if (objectWeHit.gameObject.CompareTag("wall"))
        {
            print("hit a wall");
            BulletImpactEffect(objectWeHit);
            Destroy(gameObject);

        }
        if (objectWeHit.gameObject.CompareTag("Beer"))
        {
            print("hit a beer bottle");
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
        }

    }

    void BulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(GlobalReference.Instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        hole.transform.SetParent(objectWeHit.transform);
    }


}
