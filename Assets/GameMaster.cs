using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    public DropZone dropZone;
    public Hand hand;

	private void Awake ()
    {
        Instance = this;
        dropZone = FindObjectOfType<DropZone>();
        hand = FindObjectOfType<Hand>();
	}
}
