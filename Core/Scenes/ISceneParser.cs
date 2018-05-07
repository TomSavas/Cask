using System.IO;

namespace Core.Scenes
{
    public interface ISceneParser
    {
        IScene ParseFileToScene(string sceneFilePath);
        string ParseSceneToFile(IScene scene);
    }
}