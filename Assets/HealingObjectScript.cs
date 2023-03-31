using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObjectScript : MonoBehaviour

{
    private void Update()
    {
        transform.Rotate(50f * Time.deltaTime, 0f, 0f);
    }
}
