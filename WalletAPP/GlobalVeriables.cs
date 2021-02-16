using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletGlobal
{
    /// <summary>
    /// Global veriables, required for properly working application
    /// </summary>
    public static class GLOBALS
    {
        /// <summary>
        /// Current selected user name
        /// </summary>
        public static string CurrentUserName { get; set; } = "";
        /// <summary>
        /// Current selectet user ID
        /// </summary>
        public static long CurrentUserID { get; set; }
    }
}
