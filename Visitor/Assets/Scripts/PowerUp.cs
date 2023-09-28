using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//With this line of code we will be able to create a new power up
//  assets from the Asset menu. We will then be able to configure 
//  the parameters of each new powerup in Unity's inspector.
[CreateAssetMenu(fileName ="PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject, IVisitor
{
    public string powerUpName;
    public GameObject powerUpPrefab;
    public string powerUpDescription;

    [Tooltip("Fully heal shield")]
    public bool healShield;

    [Range(0.0f, 50f)]
    [Tooltip("Boost turbo settings up to increments of 50/mph")]
    public float turboBoost;

    [Range(0.0f, 25f)]
    [Tooltip("Boost weapon range in increments of up to 25 units")]
    public int weaponRange;

    [Range(0.0f, 50f)]
    [Tooltip("Boost weapon strength in increments of up to 50%")]
    public float weaponStrength; 

    //We have unique methods associated with each of the visitable 
    //  elements above. 
    //We are changing specific properties of the visited objects while
    //  taking into account the defined max values. Therefore, we 
    //  encapsulate the expected behavior of a powerup when it visits
    //  a specific visitable element inside the bike's structure inside
    //  individual Visit methods. 
    public void Visit(BikeShield bikeShield)
    {
        if(healShield)
        {
            bikeShield.health = 100f; 
        }
    }

    public void Visit(BikeWeapon bikeWeapon)
    {
        int range = bikeWeapon.range += weaponRange; 

        if(range >= bikeWeapon.maxRange)
        {
            bikeWeapon.range = bikeWeapon.maxRange;
        }
        else
        {
            bikeWeapon.range = range; 
        }

        float strength = bikeWeapon.strength += Mathf.Round(bikeWeapon.strength * weaponStrength / 100); 

        if(strength >= bikeWeapon.maxStrength)
        {
            bikeWeapon.strength = bikeWeapon.maxStrength; 
        }
        else
        {
            bikeWeapon.strength = strength; 
        }
    }

    public void Visit(BikeEngine bikeEngine)
    {
        float boost = bikeEngine.turboBoost += turboBoost; 

        if(boost < 0.0f)
        {
            bikeEngine.turboBoost = 0.0f; 
        }
        if(boost >= bikeEngine.maxTurboBoost)
        {
            bikeEngine.turboBoost = bikeEngine.maxTurboBoost; 
        }
    }
}
