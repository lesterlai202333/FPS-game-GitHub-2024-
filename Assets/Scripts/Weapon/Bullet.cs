using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)//OnCollisionEnter method handles different collision events based on the tag of the object that was hit: The Collision parameter objectWeHit contains information about the collision, including the object that was hit.
    {
        if (objectWeHit.gameObject.CompareTag("target")) //if a 'target' is hit (compares the tag, as we can assign the gameobject with a tag in the unity editor)
        {
            print("hit" + objectWeHit.gameObject.name + "!"); //prints it out in the console so makes it easy to debug
            BulletImpactEffect(objectWeHit); //calls the bullet impact function so instantiates a bullet hole
            Destroy(gameObject); //destroys the bullet

        }
        if (objectWeHit.gameObject.CompareTag("wall"))
        {
            print("hit a wall");// //prints it out in the console so makes it easy to debug
            BulletImpactEffect(objectWeHit); ////calls the bullet impact function so instantiates a bullet hole
            Destroy(gameObject); ////destroys the bullet

        }
        if (objectWeHit.gameObject.CompareTag("Beer"))
        {
            print("hit a beer bottle");// //prints it out in the console so makes it easy to debug
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter(); //calls the shatter function in the BeerBottle script which causes the beer bottle to shatter into pieces
        }

    }

    void BulletImpactEffect(Collision objectWeHit) //This is a method that takes in a Collision object(we gave it the)
    {
        ContactPoint contact = objectWeHit.contacts[0]; //objectWeHit.contacts is an array that stores the first point(as its 0) of contact where the collision occurred. The ContactPoint contains details about the exact position and direction of the impact.
        //It uses the globalreference script to instantiate the bulletimpacteffect : GlobalReference.Instance.bulletImpactEffectPrefab at the contact point, and the Quaternion.LookRotation orients the effect to match the surface's angle at the collision point, so the bullet hole or effect appears aligned with the surface normal.
        GameObject hole = Instantiate(GlobalReference.Instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal)); // It uses the GlobalReference.Instance.bulletImpactEffectPrefab to instantiate the hole. 
        hole.transform.SetParent(objectWeHit.transform);
        // It gets the contact point of the collision, instantiates a prefab for the bullet impact effect at that point, and aligns it with the surface's normal direction(so makes sure it seems to be 'on' that object). This effect is then parented to the hit object so it moves with the object if it gets displaced.
    }


}
