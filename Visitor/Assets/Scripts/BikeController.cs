using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour, IBikeElement
{
    private List<IBikeElement> _bikeElements = new List<IBikeElement>(); 

    void Start()
    {
        _bikeElements.Add(gameObject.AddComponent<BikeShield>());
        _bikeElements.Add(gameObject.AddComponent<BikeWeapon>());
        _bikeElements.Add(gameObject.AddComponent<BikeEngine>());
    }

    //This Accept() method is from the IBikeElement interface.
    //This will get called automatically when the bike collides with 
    //  a poerup item on the race track.
    //With this a powerup entity will be able to pass a visitor 
    //  object to the BikeController
    //The controller will forward the recieved visitor object to 
    //  each of its visitabe elements and they will get their 
    //  properties updated as configured in the instance of the 
    //  visitor object.
    public void Accept(IVisitor visitor)
    {
        foreach(IBikeElement element in _bikeElements)
        {
            element.Accept(visitor); 
        }
    }
}
