using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace csharp.learm.csharp8
{
    public class ExampleAsyncDisposable : IAsyncDisposable, IDisposable
    {

        private Utf8JsonWriter _jsonWriter = new(new MemoryStream());

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _jsonWriter?.Dispose();
                _jsonWriter = null;
            }
        }
      
        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_jsonWriter is not null)
            {
                await _jsonWriter.DisposeAsync().ConfigureAwait(false);
            }
            _jsonWriter = null;
        }
    }
}
