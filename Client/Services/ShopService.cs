using Client.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ShopService : IShopService
    {
        private readonly string _apiUrl;
        
        public ShopService(IConfiguration config)
        {
            _apiUrl = config["ApiUrl"];
        }

        public Pagination GetProducts(ShopParams shopParams)
        {
            var queryParams = new Dictionary<string, string>();
            if (shopParams.BrandId != 0)
            {
                queryParams.Add("brandId", shopParams.BrandId.ToString());
            }

            if (shopParams.TypeId != 0)
            {
                queryParams.Add("typeId", shopParams.TypeId.ToString());
            }

            if (!String.IsNullOrEmpty(shopParams.Sort)) {
                queryParams.Add("sort", shopParams.Sort);
            }

            if (!String.IsNullOrEmpty(shopParams.Search))
            {
                queryParams.Add("search", shopParams.Search);
            }
            if (shopParams.PageIndex!= 0)
            {
                queryParams.Add("pageindex", shopParams.PageIndex.ToString());
            }

            if (shopParams.PageSize != 0)
            {
                queryParams.Add("pagesize", shopParams.PageSize.ToString());
            }

            var url = QueryHelpers.AddQueryString("products", queryParams);

            Pagination pagination = new Pagination();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                var response = client.GetAsync(url);
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //Install-Package Microsoft.AspNet.WebApi.Client 

                    var readTask = result.Content.ReadAsAsync<Pagination>();
                    readTask.Wait();

                    pagination = readTask.Result;
                }
            }
            return pagination;
        }





        public IReadOnlyList<ProductBrand> GetProductBrands()
        {
            IReadOnlyList<ProductBrand> brands = new List<ProductBrand>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                var response = client.GetAsync("products/brands");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //Install-Package Microsoft.AspNet.WebApi.Client 

                    var readTask = result.Content.ReadAsAsync<IReadOnlyList<ProductBrand>>();
                    readTask.Wait();

                    brands = readTask.Result;
                }
            }
            return brands;
            
        }

        public IReadOnlyList<ProductType> GetProductTypes()
        {
            IReadOnlyList<ProductType> types = new List<ProductType>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                var response = client.GetAsync("products/types");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    //Install-Package Microsoft.AspNet.WebApi.Client 

                    var readTask = result.Content.ReadAsAsync<IReadOnlyList<ProductType>>();
                    readTask.Wait();

                    types = readTask.Result;
                }
            }
            return types;
        }
    }
}
