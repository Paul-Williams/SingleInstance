using System.Reflection;

namespace SingleInstance {
  internal static class EntryAssemblyCustomAttributes {

    static public string AssemblyGuid() {

      var assembly = Assembly.GetEntryAssembly() ?? throw new Exception("'Assembly.GetEntryAssembly()' did not return a value.");

      
      var guidAttribute = assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false).FirstOrDefault()
        as System.Runtime.InteropServices.GuidAttribute
        ?? throw new Exception("Main assembly does not contain a GuidAttribute. Ensure it is defined in either project file or Program.cs ( e.g. [assembly: System.Runtime.InteropServices.Guid(...)])");


      return guidAttribute.Value;
    }

  }
}

