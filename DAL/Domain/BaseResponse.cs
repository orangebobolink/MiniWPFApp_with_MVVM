using DAL.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Domain
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T Data { get; set; }
        public required StatusCodeResult StatusCode { get; set; }
        public required string Description { get; set; }
    }
}
