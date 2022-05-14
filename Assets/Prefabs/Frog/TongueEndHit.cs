using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueEndHit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Contains("Frog"))
            return;

        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        if (collision.GetContacts(contacts) > 0)
        {
            foreach(ContactPoint2D contact in contacts)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                transform.parent.GetComponent<FrogControllerForce>().ActivateTongueSpring(contact);
                transform.position = contact.point;
            }
        }
    }
}
