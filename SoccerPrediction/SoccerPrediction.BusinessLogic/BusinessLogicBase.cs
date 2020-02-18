using System;

namespace SoccerPrediction.BusinessLogic
{
    public class BusinessLogicBase : IDisposable
    {
        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
                if (disposing)
                { }
            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
