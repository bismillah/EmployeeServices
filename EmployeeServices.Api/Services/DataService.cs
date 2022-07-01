using EmployeeServices.Api.Interfaces;
using EmployeeServices.Api.Model;
using EmployeeServices.Domain.Constants;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeServices.Api.Providers
{
    public class DataService : IDataService
    {
        /// <summary>
        /// Read all employee records
        /// </summary>
        /// <returns></returns>
        public async Task<EmployeeList> ReadAll()
        {
            EmployeeList employeeModels = new EmployeeList();
            try
            {
                using (var client = this.CreateNewClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(String.Empty).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        employeeModels = JsonConvert.DeserializeObject<EmployeeList>(result, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Auto,
                            NullValueHandling = NullValueHandling.Ignore,
                        });

                        return employeeModels;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employeeModels;
        }

        /// <summary>
        /// add new employee record
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        public async Task<EmployeeModel> Add(EmployeeModel employeeModel)
        {
            EmployeeModel responseObj = new EmployeeModel();
            try
            {
                using (var client = this.CreateNewClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.PostAsJsonAsync(String.Empty, new { name = employeeModel.Name, email = employeeModel.Email, gender = employeeModel.Gender, status = employeeModel.Status }).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<EmployeeModel>(result);
                        response.Dispose();
                    }
                    else
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseObj;
        }

        /// <summary>
        /// Update employee based on id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        public async Task<EmployeeModel> Update(int id, EmployeeModel employeeModel)
        {
            EmployeeModel responseObj = new EmployeeModel();
            try
            {
                using (var client = this.CreateNewClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.PutAsJsonAsync(id.ToString(), new { name = employeeModel.Name, email = employeeModel.Email, gender = employeeModel.Gender, status = employeeModel.Status }).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<EmployeeModel>(result);
                        response.Dispose();
                    }
                    else
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseObj;
        }

        /// <summary>
        /// Delete employee based on employee id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var client = this.CreateNewClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.DeleteAsync(id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        response.Dispose();
                        return true;
                    }
                    else
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Read employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmployeeModel> Get(int id)
        {
            try
            {
                using (HttpClient client = this.CreateNewClient())
                {
                    HttpResponseMessage response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        EmployeeJsonData t = JsonConvert.DeserializeObject<EmployeeJsonData>(result);

                        return new EmployeeModel { Name = t.data.Name, Email = t.data.Email, Gender = t.data.Gender, Id = t.data.Id, Status = t.data.Status };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Search employee by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<EmployeeList> Search(string name)
        {
            EmployeeList employeeModels = new EmployeeList();
            try
            {
                using (HttpClient client = this.CreateNewClient())
                {
                    HttpResponseMessage response = await client.GetAsync("?name=" + name.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var employeeList = JsonConvert.DeserializeObject<EmployeeList>(result);

                        employeeModels = employeeList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employeeModels;
        }

        /// <summary>
        /// Search employee by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<EmployeeList> Search(int page)
        {
            EmployeeList employeeModels = new EmployeeList();
            try
            {
                using (HttpClient client = this.CreateNewClient())
                {
                    HttpResponseMessage response = await client.GetAsync("?page=" + page.ToString()).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var employeeList = JsonConvert.DeserializeObject<EmployeeList>(result);

                        employeeModels = employeeList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return employeeModels;
        }

        private HttpClient CreateNewClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ApiConstants.BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiConstants.ApplicationJson));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiConstants.Bearer, ApiConstants.Authorization);

            return client;
        }
    }
}
