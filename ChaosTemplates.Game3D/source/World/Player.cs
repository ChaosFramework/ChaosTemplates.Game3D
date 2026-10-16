using System.Drawing.Drawing2D;
using ChaosFramework.Components;
using ChaosFramework.Core;
using ChaosFramework.Graphics.OpenGl.Instancing;
using ChaosFramework.Math.Vectors;
using ChaosFramework.Physics;
using ChaosFramework.Shapes.Convex;
using static ChaosFramework.Math.Constants;

namespace ChaosTemplates.Game3D.World
{
    [DefaultInstancer(1, "models/cube.gmdl", null)]
    public class Player : Component<WorldScene>, Instancable
    {
        static readonly Vector3f VIEW_OFFSET = new Vector3f(0.5f, -2, 1);

        Physical physics;

        protected override void Create(CreateParameters cparams)
        {
            physics = new Physical(this, true);
            scene.physics.Add(physics);
            physics.shapes.Clear();

            using (Disposable x = new())
                physics.AddShape(new MeshShape([.. scene.game.meshes.Load("models/cube.gmdl", x).content.data.pos]));
            physics.shapes[0].bounce = 0;
            physics.state.contactFriction = 0;
            physics.state.position.y = 1;
            physics.state.velocity.z = -2.5f;

            scene.instancers.GetOrCreateManager(typeof(Player)).instancedThings.Add(this);
        }

        public override void SetUpdateCalls()
        {
            base.SetUpdateCalls();
            scene.updateLayers[(int)UpdateLayers.SetCamera].Add(UpdateCamera);
            scene.updateLayers[(int)UpdateLayers.Move].Add(Move);
        }

        void Move()
            => physics.state.Move(ftime);

        void UpdateCamera()
        {
            scene.view.Update(
                physics.state.position - VIEW_OFFSET,
                VIEW_OFFSET,
                new Vector3f(0, 1, 0),
                scene.view.nearClip,
                scene.view.farClip,
                PI_QUART,
                scene.view.screenRatio
                );
        }

        void Instancable.GiveMeInstances(InstancingAttribute[] instancers)
            => instancers[0].informer.AddInstance(physics.state.GetTransform());

        bool Instancable.NeedsInstancedDraw()
            => true;

        protected override void DoDispose()
        {
            scene.instancers[typeof(Player)].instancedThings.Remove(this);
            base.DoDispose();
        }
    }
}
