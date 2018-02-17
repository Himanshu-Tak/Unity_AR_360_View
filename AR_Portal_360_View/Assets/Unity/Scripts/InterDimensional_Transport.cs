using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AR_360View.Unity.Script 
{
    public class InterDimensional_Transport : MonoBehaviour
    {
        [SerializeField]
        private Material[] materials;

        void Start()
        {
            foreach (var mat in materials)
            {
                mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
            }
        }

        //Outside of other World
        private void OnTriggerStay(Collider other)
        {
            if (other.name != "Main Camera") 
                return;
            if (transform.position.z > other.transform.position.z)
            {
                Debug.Log("Outside of other World");
                foreach (var mat in materials)
                {
                    mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
                }
            }
            //Inside Other dimensional
            else
            {
                Debug.Log("Inside Other dimensional");
                foreach (var mat in materials)
                {
                    mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var mat in materials)
            {
                mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
            }
        }

        void Update()
        {

        }
    }   
}
