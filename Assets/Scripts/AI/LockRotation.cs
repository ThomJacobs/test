using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    //Attributes:
    [SerializeField] private Vector3 m_euler_angles;

    //Methods:
    private void Update() => transform.eulerAngles = m_euler_angles;
}
