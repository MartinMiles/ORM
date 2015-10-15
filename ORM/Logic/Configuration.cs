using System.Configuration;

namespace ORM
{
    internal static class Configuration
    {
        /// <summary>
        /// Gets connection string named "local"
        /// </summary>
        internal static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["local"].ConnectionString; }
        }
    }
}
