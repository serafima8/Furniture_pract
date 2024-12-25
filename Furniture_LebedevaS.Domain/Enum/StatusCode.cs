using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furniture_LebedevaS.Domain.Enum
{
    public enum StatusCode
    {
        OK = 200, // успешно
        BadRequest = 400, // ошибка клиента
        NotFound = 404, // сервер не смог найти запрашиваемый ресурс
        InternalServerError = 500, // внутренняя ошибка сервера
        Error = 500,
    }
}
