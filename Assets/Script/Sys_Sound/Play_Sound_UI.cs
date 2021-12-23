using UnityEngine;

public class Play_Sound_UI : MonoBehaviour
{
    [SerializeField] private Sound_UI _type;

    public void Play()
    {
        Manager_GAME.Get_Manager_Sound_Script().Play_Sound_UI(_type);
    }
}