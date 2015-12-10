using System;
using System.Net.Http;

namespace OpenRestLib
{
    public class OpenRestRequest
    {
        public HttpMethod HttpMethod { get; set; }
        public string EndPoint { get; set; }

        /// <summary>
        /// Permite crear la instancia de la petición
        /// </summary>
        /// <param name="httpMethod"></param>
        public OpenRestRequest(HttpMethod httpMethod)
        {
            HttpMethod = httpMethod;
        }

        /// <summary>
        /// Permite crear la instancia de la petición
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="httpMethod"></param>
        public OpenRestRequest(string endPoint, HttpMethod httpMethod)
        {
            EndPoint = endPoint;
            HttpMethod = httpMethod;
        }

        /// <summary>
        /// Agrega un parámetro al Endpoint que se apunta
        /// </summary>
        /// <param name="urlParameter"></param>
        /// <param name="newValue"></param>
        public void AddUrlParameter<T>(string urlParameter, T newValue)
        {
            if (!string.IsNullOrEmpty(EndPoint) || !string.IsNullOrWhiteSpace(EndPoint))
            {
                if (EndPoint.Contains(urlParameter))
                {
                    try
                    {
                        //Busca en la cadena del endpoint y reemplaza lo que se parezca a urlParameter por value
                        EndPoint = EndPoint.Replace("{"+urlParameter+"}",newValue.ToString());
                    }
                    catch (Exception e)
                    {
                        
                        throw;
                    }
                }
            }
        }
    }
}
