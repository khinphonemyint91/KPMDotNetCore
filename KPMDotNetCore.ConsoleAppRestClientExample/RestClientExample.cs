﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KPMDotNetCore.ConsoleAppRestClientExample
{
    internal class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7216"));
        private readonly string _blogEndpoint = "api/blog";
        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(100);
            //await CreateAsync("tilte rest", "kpm", "content");
            //await UpdateAsync(21,"tilte update2", "kpm2", "content2");
            //await EditAsync(21);
            //await PatchAsync(21, "tilte patch2", "kpm patch2", "content patch2");
            //await EditAsync(21);
            //await DeleteAsync(21);
            await EditAsync(21);

        }

        private async Task ReadAsync()
        {
            //RestRequest restRequest = new RestRequest(_blogEndpoint);
            //var response = await _client.GetAsync(restRequest);

            RestRequest restRequest =new RestRequest(_blogEndpoint,Method.Get);
            var response=await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach (var item in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine($"Title   =>{item.BlogTitle}");
                    Console.WriteLine($"Author  =>{item.BlogAuthor}");
                    Console.WriteLine($"Content =>{item.BlogContent}");
                }

            }
        }

        private async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                var item = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title   =>{item.BlogTitle}");
                Console.WriteLine($"Author  =>{item.BlogAuthor}");
                Console.WriteLine($"Content =>{item.BlogContent}");

            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }
        private async Task CreateAsync(string title, string author, string content)
        {
            BlogDto blogDto = new BlogDto();
            {
                blogDto.BlogTitle = title;
                blogDto.BlogAuthor = author;
                blogDto.BlogContent = content;
            }; //C# object

            RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Post);
            restRequest.AddJsonBody(blogDto);
            var response =await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }
        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogDto blogDto = new BlogDto();
            {
                blogDto.BlogTitle = title;
                blogDto.BlogAuthor = author;
                blogDto.BlogContent = content;
            }; //C# object

            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Post);
            restRequest.AddJsonBody(blogDto);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }
        private async Task PatchAsync(int id, string title, string author, string content)
        {
            BlogDto blogDto = new BlogDto();
            {
                blogDto.BlogTitle = title;
                blogDto.BlogAuthor = author;
                blogDto.BlogContent = content;
            }; //C# object

            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Patch);
            restRequest.AddJsonBody(blogDto);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }
        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                string message =  response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }
    }
}
