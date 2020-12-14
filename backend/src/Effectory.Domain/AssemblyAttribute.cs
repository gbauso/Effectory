using System.Runtime.CompilerServices;

// this is added because the operation classes which are internal are mocked, in order to Moq an internal class you need to provide the proxy assembly access to internal types
[assembly: InternalsVisibleTo("Effectory.Test")]