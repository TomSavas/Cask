using System.IO;

namespace Cask.Scenes
{
    public interface ISceneParser
    {
        IScene ParseFileToScene(string sceneFilePath);
        string ParseSceneToFile(IScene scene);
    }
}