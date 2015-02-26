// Guids.cs
// MUST match guids.h
using System;

namespace Vectria.MeteorRide
{
    static class GuidList
    {
        public const string guidMeteorRidePkgString = "03911356-10c1-492c-bb0f-b5d3586ad3c9";
        public const string guidMeteorRideCmdSetString = "d4be4c0c-d7c4-435d-a6d1-020dedd1e52b";
        public const string guidShellWindowPersistanceString = "aab87e8f-55ef-410a-8ead-7db613435ff5";
		public const string guidExplorerWindowPersistanceString = "bbb87e8f-55ef-410a-8ead-7db613435ff5";

		public static readonly Guid guidMeteorRideCmdSet = new Guid(guidMeteorRideCmdSetString);
    };
}