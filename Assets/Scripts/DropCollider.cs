using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DropCollider : MonoBehaviour
{
    [SerializeField] string _id;

    public string ID => _id;
    public delegate void OnDropCallback(GameObject obj);
    public OnDropCallback OnDrop;
}
