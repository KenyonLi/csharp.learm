namespace Lifetime.Demo.IServices
{
    public interface ICounterService
    {
        int GetCount();
        //增量
        void Increment();
        Guid GetOperationId();
    }

    public class CounterService : ICounterService
    {
        private int _count = 0;
        private Guid _operationId;
        public CounterService()
        {
            _operationId = Guid.NewGuid();
        }
        public int GetCount()
        {
            return _count;
        }

        public void Increment()
        {
            _count++;
        }

        public Guid GetOperationId()
        {
            return _operationId;
        }
    }
}
