using ChaosFramework.Graphics.Colors;
using ChaosFramework.Graphics.OpenGl.Lights;
using ChaosFramework.Math.Vectors;

namespace ChaosTemplates.Game3D.World
{
    public class TemplateWorld : WorldScene
    {
        public readonly DirectionalLight sun;

        public TemplateWorld(Game game)
            : base (game)
        {
            sun = new DirectionalLight(
                deferredShader,
                new Vector3f(0.5f, -1, 1),
                new Rgba(0.9f, 0.9f, 0.9f, 1) * Confix.instance.brightness
                );
            sun.ambient = new Rgba(0.1f, 0.1f, 0.1f, 1) * Confix.instance.brightness;
            lights.Add(sun);

            AddComponent<World>();
        }
    }
}
