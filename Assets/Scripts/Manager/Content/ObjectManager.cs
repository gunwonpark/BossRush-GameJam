using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// object들의 소환을 담당하는 Manager입니다
/// </summary>
public class ObjectManager
{
    public PlayerController Player { get; private set; }
    public BossController Boss { get; private set; }

    public HashSet<MonsterController> Monsters { get; private set; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; private set; } = new HashSet<ProjectileController>();
    public T Spawn<T>(string name) where T : BaseController
    {
        System.Type type = typeof(T);

        GameObject go = Manager.Instance.Resource.Instantiate(name);
        if(go == null)
        {
            return null;
        }

        go.name = $"{go.name}{{{type.Name.Replace("Controller", " ")}}}";

        T controller = go.GetOrAddComponent<T>();

        if(controller is PlayerController)
        {
            Player = controller as PlayerController;
        }
        else if (controller is BossController)
        {
            Boss = controller as BossController;
        }
        else if (controller is MonsterController)
        {
            Monsters.Add(controller as MonsterController);
        }
        else if (controller is ProjectileController)
        {
            Projectiles.Add(controller as ProjectileController);
        }

        return controller;
    }

    public void Despawn(BaseController controller)
    {
        if (controller is PlayerController)
        {
            Player = null;
        }
        else if (controller is BossController)
        {
            Boss = null;
        }
        else if (controller is MonsterController)
        {
            Monsters.Remove(controller as MonsterController);
        }
        else if (controller is ProjectileController)
        {
            Projectiles.Remove(controller as ProjectileController);
        }
        Manager.Instance.Resource.Destroy(controller.gameObject);
    }
}
