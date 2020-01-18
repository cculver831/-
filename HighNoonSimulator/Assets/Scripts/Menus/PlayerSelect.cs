using UnityEngine;
using UnityEngine.UI;
public class PlayerSelect : MonoBehaviour
{
    // ArrayLsit of Players
    public GameObject[] PlayersIdol;
    private int count = 0;
    public GameObject obj;
    public GameObject Prev;
    public GameObject Next;
    public Transform spawn;
    private void Start()
    {
        obj = Instantiate(PlayersIdol[count], spawn.position, Quaternion.Euler(0, 180, 0));
    }
    private void Update()
    {
        if(count == 0)
        {
            Prev.SetActive(false);
        }
        else
        {
            Prev.SetActive(true);
        }
        if (count == 6)
        {
            Next.SetActive(false);
        }
        else
        {
            Next.SetActive(true);
        }
    }
    public void Left()
    {
       
        if(count > 0)
        {
            Destroy(obj);
            count--;
            obj = Instantiate(PlayersIdol[count], spawn.position, Quaternion.Euler(0, 180, 0));
        }



    }
    public void Right()
    {

        if (count < 6)
        {
            Destroy(obj);
            count++;
            obj = Instantiate(PlayersIdol[count], spawn.position, Quaternion.Euler(0, 180, 0));
        }

       
    }
    public void Select()
    {
        // Save Player Choice for load up
        GameManager.count = count;
        Debug.Log("Model Saved");
    }
}
