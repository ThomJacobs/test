using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    //Attributes:
    [SerializeField] private Transform m_target;
    [SerializeField] private Vector2 m_offset;

    //Methods:
    private void Update()
    {
        if (!m_target) return;

        transform.position = (Vector3) m_offset + m_target.transform.position;
    }
}
