using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenRestLib
{
    public class OpenRestClient : HttpClient
    {
        /// <summary>
        /// Creates an instance for the client that receive as parameter the address of the service to consume
        /// Crea una instancia del cliente que recibe como parámetro la dirección del servicio a consumir
        /// </summary>
        /// <param name="serviceAddress"></param>
        public OpenRestClient(string serviceAddress)
        {
            //dirección del servicio rest
            BaseAddress = new Uri(serviceAddress);

            //limpia la cabecera Accept
            DefaultRequestHeaders.Accept.Clear();

            //Para aceptar recibir datos en formato json
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

    
    public static class RestClientExtension
    {
        /// <summary>
        /// Método de extensión que recibe un objeto restRequest y obtiene la información según sea el objeto T de Execute<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="restClient"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteAsync<T>(this OpenRestClient restClient, OpenRestRequest request) where T : new()
        {
            //SE crea una instancia del objeto generico
            var genericObject = new T();

            //se pregunta por el método http
            if (request.HttpMethod == HttpMethod.Get)
            {
                try
                {
                    //se obtiene los datos del endpoint del objeto request
                    var response = await restClient.GetAsync(request.EndPoint);

                    //si el status code es success deserealiza el contenido
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            //Obtiene los datos del contenido de la respuesta y los parsea al tipo de dato genérico que se pasó al método Execute<T>
                            genericObject = await response.Content.ReadAsAsync<T>();
                        }
                        catch (Exception e)
                        {
                            //Debug.WriteLine(e.Message);
                        }
                    }
                }
                catch (Exception e)
                {

                    //Debug.WriteLine(e.Message);
                }
            }

            return genericObject;
        }


        public static async Task<T> ExecuteJsonAsync<T>(this OpenRestClient restClient, OpenRestRequest request) where T : new()
        {
            //SE crea una instancia del objeto generico
            var genericObject = new T();

            //se pregunta por el método http
            if (request.HttpMethod == HttpMethod.Get)
            {
                try
                {
                    //se obtiene los datos del endpoint del objeto request
                    var response = await restClient.GetStringAsync(request.EndPoint);

                    var json = JsonConvert.DeserializeObject<T>(response);
                    //si el status code es success deserealiza el contenido
                    if (json!=null)
                    {
                        try
                        {
                            //Obtiene los datos del contenido de la respuesta y los parsea al tipo de dato genérico que se pasó al método Execute<T>
                            genericObject = json;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                        }
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }
            }

            return genericObject;
        }
        
    }
}
