using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Logger
{
    public interface ILog
    {
        void LogCustomException(Exception ex, int? id);
        Task LogSendedEmailAsync();
    }
}
