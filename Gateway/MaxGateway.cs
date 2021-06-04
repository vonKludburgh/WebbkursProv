using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebbkursProv.Models;

namespace WebbkursProv.Gateway
{
    public class MaxGateway : IMaxGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MaxGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        //Page

        public async Task<List<CreatedPage>> GetCreatedPages()
        {
            var response = await _httpClient.GetAsync(_configuration["WPPage"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CreatedPage>>(apiResponse);
        }

        public async Task<CreatedPage> PostCreatedPage(CreatedPage createdPage)
        {
            createdPage.BgColor = "#ffffff";
            //createdPage.ColorFooter = "#ffffff";
            //createdPage.ColorHeader = "#ffffff";
            //createdPage.ColorLeftbar = "#ffffff";
            //createdPage.ColorRightbar = "#ffffff";

            var response = await _httpClient.PostAsJsonAsync(_configuration["WPPage"], createdPage);
            CreatedPage returnValue = await response.Content.ReadFromJsonAsync<CreatedPage>();

            return returnValue;
        }
        public async Task<CreatedPage> DeleteCreatedPage(long deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["WPPage"] + "/" + deleteId);
            CreatedPage returnValue = await response.Content.ReadFromJsonAsync<CreatedPage>();

            return returnValue;
        }

        public async Task EditCreatedPage(long editId, CreatedPage createdPage)
        {
            var response = await _httpClient.PutAsJsonAsync(_configuration["WPPage"] + "/" + editId, createdPage);            
        }

        // Article

        public async Task<List<Article>> GetArticles()
        {
            var response = await _httpClient.GetAsync(_configuration["WPArticle"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Article>>(apiResponse);
        }

        public async Task<Article> PostArticle(Article createdArticle)
        {
            createdArticle.BgColor = "#ffffff";
            createdArticle.ImgWidth = "25";
            var response = await _httpClient.PostAsJsonAsync(_configuration["WPArticle"], createdArticle);
            Article returnValue = await response.Content.ReadFromJsonAsync<Article>();

            return returnValue;
        }
        public async Task<Article> DeleteArticle(long deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["WPArticle"] + "/" + deleteId);
            Article returnValue = await response.Content.ReadFromJsonAsync<Article>();

            return returnValue;
        }

        public async Task EditArticle(long editId, Article article)
        {
            var response = await _httpClient.PutAsJsonAsync(_configuration["WPArticle"] + "/" + editId, article);
        }

        // Images

        public async Task<List<Image>> GetImages()
        {
            var response = await _httpClient.GetAsync(_configuration["WPImage"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Image>>(apiResponse);
        }

        public async Task<Image> PostImage(Image createdImage)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["WPImage"], createdImage);
            Image returnValue = await response.Content.ReadFromJsonAsync<Image>();

            return returnValue;
        }
        public async Task<Image> DeleteImage(long deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["WPImage"] + "/" + deleteId);
            Image returnValue = await response.Content.ReadFromJsonAsync<Image>();

            return returnValue;
        }

        public async Task EditImage(long editId, Image image)
        {
            var response = await _httpClient.PutAsJsonAsync(_configuration["WPImage"] + "/" + editId, image);
        }

        // Documents

        public async Task<List<Document>> GetDocuments()
        {
            var response = await _httpClient.GetAsync(_configuration["WPDocument"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Document>>(apiResponse);
        }

        public async Task<Document> PostDocument(Document document)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["WPDocument"], document);
            Document returnValue = await response.Content.ReadFromJsonAsync<Document>();

            return returnValue;
        }
        public async Task<Document> DeleteDocument(long deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["WPDocument"] + "/" + deleteId);
            Document returnValue = await response.Content.ReadFromJsonAsync<Document>();

            return returnValue;
        }

        public async Task EditDocument(long editId, Document document)
        {
            var response = await _httpClient.PutAsJsonAsync(_configuration["WPDocument"] + "/" + editId, document);
        }

        // Links

        public async Task<List<Link>> GetLinks()
        {
            var response = await _httpClient.GetAsync(_configuration["WPLink"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Link>>(apiResponse);
        }

        public async Task<Link> PostLink(Link link)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["WPLink"], link);
            Link returnValue = await response.Content.ReadFromJsonAsync<Link>();

            return returnValue;
        }
        public async Task<Link> DeleteLink(long deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["WPLink"] + "/" + deleteId);
            Link returnValue = await response.Content.ReadFromJsonAsync<Link>();

            return returnValue;
        }

        public async Task EditLink(long editId, Link link)
        {
            var response = await _httpClient.PutAsJsonAsync(_configuration["WPLink"] + "/" + editId, link);
        }

        // Other Article

        public async Task<List<OtherArticle>> GetOtherArticles()
        {
            var response = await _httpClient.GetAsync(_configuration["WPOtherArticle"]);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<OtherArticle>>(apiResponse);
        }

        public async Task<OtherArticle> PostOtherArticle(OtherArticle otherarticle)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["WPOtherArticle"], otherarticle);
            OtherArticle returnValue = await response.Content.ReadFromJsonAsync<OtherArticle>();

            return returnValue;
        }
        public async Task<OtherArticle> DeleteOtherArticle(long deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["WPOtherArticle"] + "/" + deleteId);
            OtherArticle returnValue = await response.Content.ReadFromJsonAsync<OtherArticle>();

            return returnValue;
        }

        public async Task EditOtherArticle(long editId, OtherArticle otherarticle)
        {
            var response = await _httpClient.PutAsJsonAsync(_configuration["WPOtherArticle"] + "/" + editId, otherarticle);
        }
    }
}
