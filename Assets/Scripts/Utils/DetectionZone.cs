using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class DetectionZone : MonoBehaviour
{
    public UnityEvent<GameObject> OnDetection;

    List<GameObject> m_gameObjectsInArea = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        m_gameObjectsInArea.Add(other.gameObject);
        OnDetection.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_gameObjectsInArea.Contains(other.gameObject))
        {
            m_gameObjectsInArea.Remove(other.gameObject);
        }
    }

    public List<GameObject> GetObjectsInArea()
    {
        // Remove all destroyed game objects before returning the list
        int i = m_gameObjectsInArea.Count - 1;

        while (i >= 0)
        {
            if (m_gameObjectsInArea[i] == null)
            {
                m_gameObjectsInArea.RemoveAt(i);
            }
            i--;
        }

        return m_gameObjectsInArea;
    }
}
