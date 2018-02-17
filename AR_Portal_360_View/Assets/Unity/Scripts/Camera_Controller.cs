using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR_360View.Unity.Script
{
    public class Camera_Controller : MonoBehaviour
    {
        [SerializeField]
        private float rotation;
        [Range(0.00001f, 3)]
        float speed = 0.1f;

        void Update()
        {
            Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
            transform.Translate(velocity);
            if (Input.GetKey(KeyCode.Q))
            {
                rotation -= 1 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotation += 1 * Time.deltaTime;
            }
            transform.Rotate(0, rotation, 0);

        }
    }
}

