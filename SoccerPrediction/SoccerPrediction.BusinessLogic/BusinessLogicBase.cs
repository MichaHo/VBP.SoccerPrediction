using System;

namespace SoccerPrediction.BusinessLogic
{
    public class BusinessLogicBase : IDisposable
    {
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
                if (disposing)
                { }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
