using PlaylistManager.DataAccess;
using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.Logger
{
    public sealed class Logger : ILog
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());

        public static Logger GetInstance
        {
            get { return instance.Value; }
        }

        public void LogCustomException(Exception ex, int? id = null)
        {
            CustomException exception = new CustomException(ex, id);
            unitOfWork.CustomExceptionsRepository.Add(exception);
            unitOfWork.Save();
        }

        public Task LogSendedEmailAsync()
        {
            throw new NotImplementedException();
        }
    }
}
