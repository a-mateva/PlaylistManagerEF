using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Models
{
    public class CustomException : Exception
    {
        public int? Id { get; set; }
        public string CustomMessage { get; set; }
        public string CustomStackTrace { get; set; }
        public string CustomInnerMessage { get; set; }
        public string CustomInnerStackTrace { get; set; }
        public DateTime DateCreated { get; set; }

        public CustomException()
        {

        }

        public CustomException(Exception ex, int? id = null)
        {
            Id = id;
            CustomMessage = ex.Message;
            CustomStackTrace = ex.StackTrace;
            CustomInnerMessage = ex.InnerException != null ? ex.InnerException.Message : String.Empty;
            CustomInnerStackTrace = ex.InnerException != null ? ex.InnerException.StackTrace : String.Empty;
            DateCreated = DateTime.Now;
        }
    }
}
