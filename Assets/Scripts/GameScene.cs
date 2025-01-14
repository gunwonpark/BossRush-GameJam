using UnityEngine;

public class GameScene : MonoBehaviour
{
    PlayerController Player { get; set; }
    BossController Boss { get; set; }

    public void Start()
    {
        Player = Manager.Instance.Object.Spawn<PlayerController>("Player");
        Boss = Manager.Instance.Object.Spawn<BossController>("Boss");

        Player.Init();
        Player.SetTarget(Boss.transform);
        Boss.Init();
    }
}
