using Microsoft.Xna.Framework;
using Pathoschild.Stardew.Automate.Framework;
using SObject = StardewValley.Object;

namespace Pathoschild.Stardew.Automate.Machines.Objects
{
    /// <summary>A loom that accepts input and provides output.</summary>
    internal class LoomMachine : GenericMachine
    {
        /*********
        ** Properties
        *********/
        /// <summary>The recipes to process.</summary>
        private readonly Recipe[] Recipes =
        {
            // wool => cloth
            new Recipe(
                input: 440,
                inputCount: 1,
                output: item => new SObject(Vector2.Zero, 428, null, false, true, false, false),
                minutes: 240
            )
        };


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="machine">The underlying machine.</param>
        public LoomMachine(SObject machine)
            : base(machine) { }

        /// <summary>Get the output item.</summary>
        public override ITrackedStack GetOutput()
        {
            SObject machine = this.Machine;
            return new TrackedItem(machine.heldObject, item =>
            {
                machine.heldObject = null;
                machine.readyForHarvest = false;
                machine.showNextIndex = false;
            });
        }

        /// <summary>Pull items from the connected pipes.</summary>
        /// <param name="pipes">The connected IO pipes.</param>
        /// <returns>Returns whether the machine started processing an item.</returns>
        public override bool Pull(IPipe[] pipes)
        {
            return this.GenericPullRecipe(pipes, this.Recipes);
        }
    }
}
