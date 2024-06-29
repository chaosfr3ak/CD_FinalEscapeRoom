namespace libs;

public class GameObjectFactory : IGameObjectFactory
{
    public GameObject CreateGameObject(dynamic obj) {

        GameObject newObj;
        int type = (int)obj.Type;

        switch (type)
        {
            case (int)GameObjectType.Player:
                Player.Instance.PosX = obj.PosX;
                Player.Instance.PosY = obj.PosY;
                newObj = Player.Instance;
                break;
            case (int)GameObjectType.Obstacle:
                newObj = obj.ToObject<Obstacle>();
                break;
            case (int)GameObjectType.Box:
                string dialogFilePath = obj.DialogFilePath;
                newObj = new Box((int)obj.PosX, (int)obj.PosY, dialogFilePath);
                break;
            default:
                throw new Exception("Unknown game object type");
        }

        return newObj;
    }
    
    public GameObject LoadGameObject(dynamic obj)
    {
        int type = (int)obj.Type;

        switch (type)
        {
            case (int)GameObjectType.Player:
                Player.Instance.PosX = obj.PosX;
                Player.Instance.PosY = obj.PosY;
                return Player.Instance;
            case (int)GameObjectType.Obstacle:
                return obj.ToObject<Obstacle>();
            case (int)GameObjectType.Box:
                string dialogFilePath = obj.DialogFilePath;
                return new Box((int)obj.PosX, (int)obj.PosY, dialogFilePath);
            default:
                throw new Exception("Unknown game object type");
        }
    }
}