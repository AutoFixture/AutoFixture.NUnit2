using System;
using NUnit.Core.Extensibility;

namespace AutoFixture.NUnit2.Addins
{
    /// <summary>
    /// The Addin class holds information about an addin.
    /// </summary>
    [NUnitAddin(Name = Constants.AutoDataExtension, Description = "Provides auto-generated data specimens generated by AutoFixture as an extention to NUnit TestCase attribute.", Type = ExtensionType.Core | ExtensionType.Gui | ExtensionType.Client)]
    public class Addin : IAddin
    {
        /// <summary>
        /// When called, the add-in installs itself into
        /// the host, if possible. Because NUnit uses separate
        /// hosts for the client and test domain environments,
        /// an add-in may be invited to istall itself more than
        /// once. The add-in is responsible for checking which
        /// extension points are supported by the host that is
        /// passed to it and taking the appropriate action.
        /// </summary>
        /// <param name="host">The host in which to install the add-in</param>
        /// <returns>True if the add-in was installed, otehrwise false</returns>
        public bool Install(IExtensionHost host)
        {
            if(host == null) throw new ArgumentNullException(nameof(host));
            
            var providers = host.GetExtensionPoint("TestCaseProviders");
            if (providers == null) 
                return false;

            providers.Install(new Builders.AutoDataProvider());

            return true;
        }
    }
}