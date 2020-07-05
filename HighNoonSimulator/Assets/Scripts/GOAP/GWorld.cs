using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * This class logs all resources the Enemies can use in the game
 * 
 */
public class ResourceQueue {

    public Queue<GameObject> que = new Queue<GameObject>();
    public string tag;
    public string modState;

    public ResourceQueue(string t, string ms, WorldStates w) {

        tag = t;
        modState = ms;
        if (tag != "") {

            GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject r in resources) {

                que.Enqueue(r);
            }
        }

        if (modState != "") {

            w.ModifyState(modState, que.Count);
        }
    }

    // Add the resource
    public void AddResource(GameObject r) {

        que.Enqueue(r);
    }


    // Remove the resource
    public GameObject RemoveResource() {

        if (que.Count == 0) return null;

        return que.Dequeue();
    }

    // Overloaded RemoveResource
    public void RemoveResource(GameObject r) {

        // Put everything in a new queue except 'r' and copy it back to que
        que = new Queue<GameObject>(que.Where(p => p != r));
    }
}

public sealed class GWorld {

    // Our GWorld instance
    private static readonly GWorld instance = new GWorld();
    // Our world states
    private static WorldStates world;
    // Queue of patients
    private static ResourceQueue patients;
    // Queue of cubicles
    private static ResourceQueue cubicles;
    // Queue of offices
    private static ResourceQueue offices;
    // Queue of toilets
    private static ResourceQueue toilets;
    // Queue for the puddles
    private static ResourceQueue puddles;

    // Queue for the puddles
    private static ResourceQueue covers;
    private static ResourceQueue Ammo;
    private static ResourceQueue weapons;

    // Storage for all
    private static Dictionary<string, ResourceQueue> resources = new Dictionary<string, ResourceQueue>();

    static GWorld() {

        // Create our world
        world = new WorldStates();

        //New Resources
        // Create Cover array
        covers = new ResourceQueue("Cover", "FreeCover", world);
        // Add to the resources Dictionary
        resources.Add("covers", covers);

        weapons = new ResourceQueue("Weapon", "FreeWeapon", world);
        // Add to the resources Dictionary
        resources.Add("weapons", weapons);
        Ammo = new ResourceQueue("Ammo", "FreeAmmo", world);
        // Add to the resources Dictionary
        resources.Add("Ammo", Ammo);
    }

    public ResourceQueue GetQueue(string type) {

        return resources[type];
    }

    private GWorld() {

    }

    public static GWorld Instance {

        get { return instance; }
    }

    public WorldStates GetWorld() {

        return world;
    }
}
