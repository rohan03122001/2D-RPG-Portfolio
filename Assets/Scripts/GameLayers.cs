using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjects;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask Player;
    [SerializeField] LayerMask Portals;
    [SerializeField] LayerMask Triggerable;
    [SerializeField] LayerMask fovLayer;

    private void Awake()
    {
        i = this;
    }
    public static GameLayers i { get; set; }
    public LayerMask SolidLayer
    {
        get => solidObjects;
    }

    public LayerMask InteractableLayer
    {
        get => interactableLayer;
    }

    public LayerMask PlayerLayer
    {
        get => Player;
    }

    public LayerMask PortalLayer
    {
        get => Portals;
    }

    public LayerMask TriggerableLayers
    {
        get => Portals;
    }

    public LayerMask FovLayer
    {
        get => fovLayer;
    }
}
