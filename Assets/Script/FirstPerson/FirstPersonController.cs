using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign.FirstPerson
{

    public class FirstPersonController : MonoBehaviour
    {
        float speed = 5f;
        float rotationSpeeed = 10f;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal") * rotationSpeeed * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(0, 0, vertical);
            transform.Rotate(0, horizontal, 0);

        }
    }
}
