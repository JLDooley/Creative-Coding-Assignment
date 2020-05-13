using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIDCITY
{
    public class SlotScanner : MonoBehaviour
    {
        private GridCityManager cityManager;
        int x, y, z;

        void Awake()
        {
            x = (int)transform.position.x + 20;
            y = (int)transform.position.y;
            z = (int)transform.position.z + 20;
            cityManager = GridCityManager.Instance;
        }
        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Collision Detected");
            cityManager.SetSlot(x, y, z, true);
            Destroy(this.gameObject);
        }

        private void LateUpdate()
        {
            Destroy(this.gameObject);
        }
    }
}


