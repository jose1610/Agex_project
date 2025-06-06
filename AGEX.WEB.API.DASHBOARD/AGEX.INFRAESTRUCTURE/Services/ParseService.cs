using AGEX.CORE.Interfaces.Services;
using Newtonsoft.Json;

namespace AGEX.INFRAESTRUCTURE.Services
{
    public class ParseService : IParseService
    {
        public T Deserealize<T>(dynamic model)
        {
            return JsonConvert.DeserializeObject<T>(model);
        }

        public string Serialize(dynamic model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
