using CSharpPlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CSharpPlay.Domain
{
    public class ProductDoSomethingFinishedEvent : IDomainEvent
    {
        public DateTime DateTimeEventOccurred
        {
            get;
        } = DateTime.UtcNow;
    }
    public class ProductService
    {
        public int ProductId = (new Random()).Next();
        //private readonly DomainEvents _domainEvents;

        //public ProductService(DomainEvents domainEvents)
        //{
        //    this._domainEvents = domainEvents;
        //}

        public async Task DoSomething()
        {
            System.Diagnostics.Debug.WriteLine("From Service : " + ProductId);
            //await _domainEvents.RaiseAsync(new ProductDoSomethingFinishedEvent());
            await DomainEvents.RaiseAsync(new ProductDoSomethingFinishedEvent());
        }
    }
    public class ProductDoSomethingFinishedEventHandler : IDomainEventHandler<ProductDoSomethingFinishedEvent>
    {
        private readonly ProductService _svc;
        public ProductDoSomethingFinishedEventHandler(ProductService svc)
        {
            this._svc = svc;
        }
        public async Task HandleAsync(ProductDoSomethingFinishedEvent args)
        {
            System.Diagnostics.Debug.WriteLine("From Handler : " + _svc.ProductId);
            await Task.Delay(100);
        }
    }
}