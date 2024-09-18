using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeerBottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();//public --> can be accessed from outside of this script, this line sets up an empty list of Rigidbody objects. As the game runs, different Rigidbody components attached to parts of the game objects are added to this list for further manipulation

    public void Shatter()//public --> can be accessed from outside of this script
    {
        foreach (Rigidbody part in allParts) //This loop goes through each element in the allParts list or array (which is presumably a collection of different parts of the object that can shatter). Each element is expected to have a Rigidbody component, which controls physics interactions.
        {
            part.isKinematic = false;
            //For each part, the isKinematic property is set to false. When isKinematic is true, the object is not affected by physics (it won’t move or rotate due to forces). By setting isKinematic = false, the parts now respond to physics (e.g., gravity, collisions, etc.), allowing them to "shatter" or fall apart in a physically accurate way.
        }
    }

}
