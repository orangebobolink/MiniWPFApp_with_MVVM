using Microsoft.AspNetCore.Mvc;

namespace DAL.Domain.Interfaces
{
    public interface IBaseResponse<T>
    {
        public T Data { get; set; }
        public StatusCodeResult StatusCode { get; set; }
        public string Description { get; set; }
    }
}
