using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebbkursProv.Areas.Identity.Data;
using WebbkursProv.Gateway;
using WebbkursProv.Models;

namespace WebbkursProv.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMaxGateway _gateway;

        [BindProperty(SupportsGet =true)]
        public string CookieValue { get; set; }

        #region User
        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        public WebbkursProvUser CurrentUser { get; set; }

        [BindProperty]
        public string RoleName { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public List<string> UserRoles { get; set; }

        public bool isNy { get; set; }
        public bool isSkribent { get; set; }
        public bool isAdmin { get; set; }

        public IQueryable<WebbkursProvUser> Users { get; set; }

        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManager<WebbkursProvUser> _userManager;
        public SignInManager<WebbkursProvUser> _signInManager;

        
        public IndexModel(RoleManager<IdentityRole> roleManager, UserManager<WebbkursProvUser> userManager, IMaxGateway gateway)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _gateway = gateway;
        }
        #endregion User

        #region Pages

        [BindProperty]
        public CreatedPage newCreatedPage { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<CreatedPage> cPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedPage { get; set; }

        [BindProperty(SupportsGet =true)]
        public CreatedPage CurrentPage { get; set; }

        [BindProperty(SupportsGet = true)]
        public long OrderID { get; set; }

        [BindProperty(SupportsGet =true)]
        public string PageDeleteID { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<imageClass> PageImages { get; set; }

        [BindProperty(SupportsGet =true)]
        public IFormFile UploadHeader { get; set; }
        [BindProperty(SupportsGet = true)]
        public string asd { get; set; }

        #endregion Pages

        #region Article

        [BindProperty]
        public Article NewArticle { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<Article> Articles { get; set; }

        [BindProperty(SupportsGet = true)]
        public long ArticleID { get; set; }

        [BindProperty (SupportsGet =true)]
        public Article SelectedArticle { get; set; }

        [BindProperty(SupportsGet =true)]
        public string backimg { get; set; }

        [BindProperty]
        public long DeleteArticleID { get; set; }

        #endregion Article

        #region Images

        [BindProperty(SupportsGet =true)]
        public List<Image> Images { get; set; }

        [BindProperty]
        public long ArticleImageID { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<imageClass> imgList { get; set; }

        [BindProperty]
        public long DeleteImageID { get; set; }

        [BindProperty(SupportsGet = true)]
        public Image SaveBackImage { get; set; }
        [BindProperty(SupportsGet = true)]
        public imageClass SaveBackImageHeader { get; set; }
        [BindProperty(SupportsGet = true)]
        public imageClass SaveBackImageLeft { get; set; }
        [BindProperty(SupportsGet = true)]
        public imageClass SaveBackImageMain { get; set; }
        [BindProperty(SupportsGet = true)]
        public imageClass SaveBackImageRight { get; set; }


        [BindProperty(SupportsGet =true)]
        public List<pdfDoc> pdfList { get; set; }

        [BindProperty]
        public long pdfID { get; set; }

        #endregion Images

        #region Links

        [BindProperty(SupportsGet =true)]
        public List<Link> LinkList { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<Link> LinkList2 { get; set; }

        [BindProperty]
        public Link createdLink { get; set; }

        [BindProperty]
        public long DeleteLinkID { get; set; }

        public HttpClient client = new HttpClient();

        [BindProperty(SupportsGet =true)]
        public List<apiClass> apiClassList { get; set; }

        #endregion Links

        #region OtherArticle

        [BindProperty]
        public OtherArticle newOtherArticle { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<OtherArticle> OtherArticleList { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<OtherArticle> OtherArticleList2 { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<imageClass> oaImageClassList { get; set; }

        [BindProperty]
        public long OaDeleteID { get; set; }

        #endregion OtherArticle

        public async Task<IActionResult> OnGetAsync()
        {
            // User

            //Roles = _roleManager.Roles.ToList();
            //Users = _userManager.Users;

            //CurrentUser = await _userManager.GetUserAsync(User);
            await UserTasksAsync();
            //if (CurrentUser != null)
            //{
            //    isNy = await _userManager.IsInRoleAsync(CurrentUser, "Ny");
            //    isSkribent = await _userManager.IsInRoleAsync(CurrentUser, "Skribent");
            //    isAdmin = await _userManager.IsInRoleAsync(CurrentUser, "Admin");
            //}

            //if (isSkribent)
            //{
            //    CookieOptions op = new CookieOptions();
            //    op.Expires = new DateTime(2022, 02, 02);
            //    op.MaxAge = TimeSpan.FromDays(365);
            //    Response.Cookies.Append("xxx", "Skribenten", op);
            //}

            //if (isAdmin)
            //{
            //    CookieOptions op = new CookieOptions();
            //    op.Expires = new DateTime(2022, 02, 02);
            //    op.MaxAge = TimeSpan.FromDays(365);
            //    Response.Cookies.Append("xxx", "Big Boss", op);
            //}

            //CookieValue = Request.Cookies.TryGetValue("xxx", out string cvalue ) ? cvalue : null;

            // Page

            //cPages = await _gateway.GetCreatedPages();
            //cPages = cPages.OrderBy(x => x.Order).ToList();
            await PageTasksAsync();
            //if (cPages.Count < 1)
            //{
            //    CreatedPage newpage = new CreatedPage();
            //    newpage.Title = "Home";
            //    newpage.TimeStamp = DateTime.Now;

            //    await _gateway.PostCreatedPage(newpage);
            //}

            //if (String.IsNullOrEmpty(SelectedPage))
            //{
            //    if (cPages.Count > 0)
            //    {
            //        CurrentPage = cPages.First();
            //        SelectedPage = CurrentPage.Title;
            //    }                
            //}
            //else
            //{
            //    foreach (var x in cPages)
            //    {
            //        if (x.Title == SelectedPage)
            //        {
            //            OrderID = x.Id;
            //            CurrentPage = cPages.FirstOrDefault(z => z.Id == x.Id);
            //        }
            //    }
            //    if (isAdmin == false && isSkribent == false && String.IsNullOrEmpty(SelectedPage) == false)
            //    {
            //        CurrentPage.Count++;
            //        await OnPostEditPageAsync();
            //    }
            //}



            // Article

            await ArticleTasksAsync();

            //var myart = await _gateway.GetArticles();
            //Articles = myart.Where(x => x.PageId == CurrentPage.Id).ToList();
            //Articles = Articles.OrderBy(x => x.Order).ToList();

            //if (ArticleID != 0)
            //{
            //    SelectedArticle = myart.FirstOrDefault(x => x.Id == ArticleID);
            //}


            // Image

            //Images = await _gateway.GetImages();
            await ImageTasksAsync();
            //foreach (var x in Images)
            //{
            //    if (x.Title.Contains(".jpg")|| x.Title.Contains(".png"))
            //    { 
            //        string imageBase64Data = Convert.ToBase64String(x.ImageData);
            //        string ImageUrl = string.Format($"data:image/jpg;base64, {imageBase64Data}");
            //        string imageName = imageBase64Data;
            //        imageClass aaa = new imageClass();
            //        aaa.Data = ImageUrl;
            //        aaa.ArticleID = x.ArticleId;
            //        aaa.imgID = x.Id;
            //        imgList.Add(aaa);
            //    }
            //    else
            //    {
            //        string imageBase64Data = Convert.ToBase64String(x.ImageData);
            //        string ImageUrl = string.Format($"data:image/jpg;base64, {imageBase64Data}");
            //        string imageName = imageBase64Data;
            //        pdfDoc aaa = new pdfDoc();
            //        aaa.Byte = ImageUrl;
            //        aaa.Title = x.Title;
            //        aaa.ArticleID = x.ArticleId;
            //        aaa.ID = x.Id;
            //        pdfList.Add(aaa);
            //    }                
            //}

            //imgList = imgList.OrderBy(x => x.ArticleID).ToList();
            //long ccc = -1;

            //foreach (var x in imgList)
            //{                
            //    if (ccc != x.ArticleID)
            //    {
            //        x.First = true;
            //    }
            //    ccc = x.ArticleID;
            //}

            //SaveBackImage = Images.Find(x => x.Location == "Mainbody" && x.PageId == CurrentPage.Id);
            //string imageBase64DataX = Convert.ToBase64String(SaveBackImage.ImageData);
            //string ImageUrlX = string.Format($"data:image/jpg;base64, {imageBase64DataX}");
            //string imageNameX = imageBase64DataX;
            //SaveBackImageHeader.Data = ImageUrlX;


            // Link
            await LinkTaskAsync();
            //List<Link> LList = await _gateway.GetLinks();
            //LinkList2 = await _gateway.GetLinks();
            //List<Link> apiList = new List<Link>();

            //foreach (var x in LList)
            //{
            //    if (x.LinkString.Contains("api") || x.LinkString.Contains("json"))
            //    {
            //        apiList.Add(x);
            //    }
            //    else
            //    {
            //        LinkList.Add(x);
            //    }
            //}

            //if (apiList != null)
            //{
            //    foreach (var x in apiList)
            //    {
            //        Task<string> apiTask = client.GetStringAsync(x.LinkString);
            //        string apiString = await apiTask;
            //        string[] stringArray = apiString.Split(',');
            //        apiClass aclass = new apiClass();
            //        aclass.ArticleID = x.ArticleId;

            //        for (int i = 0; i < stringArray.Length; i++)
            //        {
            //            if (!stringArray[i].Contains("null"))
            //            {
            //                if (stringArray[i].Contains(".png") || stringArray[i].Contains(".jpg"))
            //                {
            //                    string aString = "";
            //                    string[] aStringArray = new string[3];
            //                    aString = stringArray[i];
            //                    aStringArray = aString.Split('"');
            //                    aclass.imgString = aStringArray[3];
            //                }
            //                else
            //                {
            //                    string ddd = stringArray[i];
            //                    aclass.ApiStringList.Add(ddd);
            //                }
            //            }                        
            //        }
            //        apiClassList.Add(aclass);
            //    }
            //}

            // Other Article

            //OtherArticleList = await _gateway.GetOtherArticles();
            //OtherArticleList = OtherArticleList.Where(x => CurrentPage.Id == x.PageId).ToList();
            await OtherArticleTasksAsync();
            //foreach (var x in myart)
            //{
            //    if (x.ImgDataBack != null)
            //    {
            //        string imageBase64Data = Convert.ToBase64String(x.ImgDataBack);
            //        string ImageUrl = string.Format($"data:image/jpg;base64, {imageBase64Data}");
            //        string imageName = imageBase64Data;
            //        imageClass aaa = new imageClass();
            //        aaa.Data = ImageUrl;
            //        aaa.ArticleID = x.Id;
            //        oaImageClassList.Add(aaa);
            //    }
            //}

            return Page();
        }

        #region PostPage

        public void OnPostNewPage()
        {
            CreatedPage newPage = new CreatedPage();

            if (!String.IsNullOrEmpty(newCreatedPage.Title))
            {
                newPage = newCreatedPage;
                newPage.TimeStamp = DateTime.Now;

                _gateway.PostCreatedPage(newPage);
            }            
        }

        public async Task<IActionResult> OnPostEditPageAsync()
        {
            await _gateway.EditCreatedPage(CurrentPage.Id,CurrentPage);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeletePageAsync()
        {
            Articles = await _gateway.GetArticles();
            cPages = await _gateway.GetCreatedPages();
            Images = await _gateway.GetImages();
            LinkList = await _gateway.GetLinks();
            OtherArticleList = await _gateway.GetOtherArticles();

            CreatedPage deletePage = cPages.Find(x => x.Title == PageDeleteID);
            long deleteId = deletePage.Id;
            await _gateway.DeleteCreatedPage(deleteId);

            List<OtherArticle> oaDeleteList = OtherArticleList.Where(x => x.PageId == deleteId).ToList();

            foreach (var x in oaDeleteList)
            {
                await _gateway.DeleteOtherArticle(x.Id);
            }

            List<Article> DeleteArticles = Articles.Where(x => x.Id == deleteId).ToList();
            
            foreach (var x in DeleteArticles)
            {
                foreach (var y in Images)
                {
                    if (x.Id == y.ArticleId)
                    {
                        await _gateway.DeleteImage(y.Id);
                    }
                }

                foreach (var y in LinkList)
                {
                    if (x.Id == y.ArticleId)
                    {
                        await _gateway.DeleteLink(y.Id);
                    }
                }

                await _gateway.DeleteArticle(x.Id);
            }

            return RedirectToPage("./Index");
        }
        
        #endregion PostPage

        #region PostArticle

        public async Task<IActionResult> OnPostNewArticleAsync()
        {
            Articles = await _gateway.GetArticles();
            List<Article> xArticles = Articles.Where(x => x.Id == NewArticle.PageId).ToList();
            long ordernumb = 0;
            //if (xArticles != null)
            //{
            //    ordernumb = xArticles.Select(x => x.Order).Max();
                
            //}
            NewArticle.Order = ordernumb++;
            NewArticle.TimeStamp = DateTime.Now;
            await _gateway.PostArticle(NewArticle);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostEditArticleAsync()
        {
            
            //if (HttpContext.Request.Form.Files.Count > 0)
            //{
            //    var file = Request.Form.Files[0];
                
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        file.CopyTo(ms);
            //        SelectedArticle.ImgDataBack = ms.ToArray();
            //    }
            //}
            //else
            //{
            //    foreach (var x in oaImageClassList)
            //    {
            //        if (x.ArticleID == SelectedArticle.Id)
            //        {
                        
            //        }
            //    }
            //}

            await _gateway.EditArticle(SelectedArticle.Id, SelectedArticle);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteArticleAsync()
        {
            Articles = await _gateway.GetArticles();
            Images = await _gateway.GetImages();
            LinkList = await _gateway.GetLinks();

            Article DeleteArticle = Articles.Find(x => x.Id == DeleteArticleID);

            foreach (var x in Images)
            {
                if (DeleteArticle.Id == x.ArticleId)
                {
                    await _gateway.DeleteImage(x.Id);
                }
            }

            foreach (var x in LinkList)
            {
                if (DeleteArticle.Id == x.ArticleId)
                {
                    await _gateway.DeleteLink(x.Id);
                }
            }

            await _gateway.DeleteArticle(DeleteArticleID);

            return RedirectToPage("./Index");
        }

        #endregion PostArticle

        #region Image

        public async Task<IActionResult> OnPostPostImageAsync()
        {
            Image img = new Image();
            var files = Request.Form.Files;

            var file = files[0];
            img.Title = file.FileName;

            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                img.ImageData = ms.ToArray();
            }
            img.ArticleId = ArticleImageID;
            await _gateway.PostImage(img);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteImageAsync()
        {
            await _gateway.DeleteImage(DeleteImageID);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDownloadDocAsync()
        {
            var images = await _gateway.GetImages();
            var doc = images.FirstOrDefault(x => x.Id == pdfID);

            if (doc == null)
            {
                return NotFound();
            }

            if (doc.ImageData == null)
            {
                return Page();
            }
            else
            {
                byte[] fileBytes = doc.ImageData;
                string fileName = doc.Title;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

                //byte[] byteArr = doc.ImageData;
                //string mimeType = "application/pdf";
                //return new FileContentResult(byteArr, mimeType)
                //{
                //    FileDownloadName = $"Invoice {doc.ImageData}.pdf"
                //};
            }
        }

        public async Task<IActionResult> OnPostSaveBackgroundImageAsync()
        {
            
            var files = Request.Form.Files;
            var file = files[0];
            SaveBackImage.Title = file.FileName;

            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                SaveBackImage.ImageData = ms.ToArray();
            }

            await _gateway.PostImage(SaveBackImage);

            return RedirectToPage("./Index");
        }

        #endregion

        #region Links

        public async Task<IActionResult> OnPostAddLinkAsync()
        {
            if (!String.IsNullOrEmpty(createdLink.LinkString))
            {
                await _gateway.PostLink(createdLink);                
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteLinkAsync()
        {
            await _gateway.DeleteLink(DeleteLinkID);

            return RedirectToPage("./Index");
        }

        #endregion Links

        #region OtherArticle

        public async Task<IActionResult> OnPostNewOtherArticleAsync()
        {
            if (!String.IsNullOrEmpty(newOtherArticle.Link) && !String.IsNullOrEmpty(newOtherArticle.ImgLink))
            {
                await _gateway.PostOtherArticle(newOtherArticle);
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteOAAsync()
        {
            await _gateway.DeleteOtherArticle(OaDeleteID);

            return RedirectToPage("./Index");
        }

        #endregion OtherArticle

        public class SlugifyParameterTransformer : IOutboundParameterTransformer
        {
            public string TransformOutbound(object value)
            {
                if (value == null) { return null; }
                return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
            }
        }

        public async Task<IActionResult> UserTasksAsync()
        {
            Roles = _roleManager.Roles.ToList();
            Users = _userManager.Users;

            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser != null)
            {
                isNy = await _userManager.IsInRoleAsync(CurrentUser, "Ny");
                isSkribent = await _userManager.IsInRoleAsync(CurrentUser, "Skribent");
                isAdmin = await _userManager.IsInRoleAsync(CurrentUser, "Admin");
            }

            if (isSkribent)
            {
                CookieOptions op = new CookieOptions();
                op.Expires = new DateTime(2022, 02, 02);
                op.MaxAge = TimeSpan.FromDays(365);
                Response.Cookies.Append("xxx", "Skribenten", op);
            }

            if (isAdmin)
            {
                CookieOptions op = new CookieOptions();
                op.Expires = new DateTime(2022, 02, 02);
                op.MaxAge = TimeSpan.FromDays(365);
                Response.Cookies.Append("xxx", "Big Boss", op);
            }

            CookieValue = Request.Cookies.TryGetValue("xxx", out string cvalue) ? cvalue : null;

            return Page();
        }

        public async Task<IActionResult> ArticleTasksAsync()
        {
            var myart = await _gateway.GetArticles();
            Articles = myart.Where(x => x.PageId == CurrentPage.Id).ToList();
            Articles = Articles.OrderBy(x => x.Order).ToList();

            if (ArticleID != 0)
            {
                SelectedArticle = myart.FirstOrDefault(x => x.Id == ArticleID);
            }

            return Page();
        }

        public async Task<IActionResult> ImageTasksAsync()
        {
            Images = await _gateway.GetImages();

            foreach (var x in Images)
            {
                if (x.Title.Contains(".jpg") || x.Title.Contains(".png"))
                {
                    string imageBase64Data = Convert.ToBase64String(x.ImageData);
                    string ImageUrl = string.Format($"data:image/jpg;base64, {imageBase64Data}");
                    string imageName = imageBase64Data;
                    imageClass aaa = new imageClass();
                    aaa.Data = ImageUrl;
                    aaa.ArticleID = x.ArticleId;
                    aaa.imgID = x.Id;
                    imgList.Add(aaa);
                }
                else
                {
                    string imageBase64Data = Convert.ToBase64String(x.ImageData);
                    string ImageUrl = string.Format($"data:image/jpg;base64, {imageBase64Data}");
                    string imageName = imageBase64Data;
                    pdfDoc aaa = new pdfDoc();
                    aaa.Byte = ImageUrl;
                    aaa.Title = x.Title;
                    aaa.ArticleID = x.ArticleId;
                    aaa.ID = x.Id;
                    pdfList.Add(aaa);
                }
            }

            imgList = imgList.OrderBy(x => x.ArticleID).ToList();
            long ccc = -1;

            foreach (var x in imgList)
            {
                if (ccc != x.ArticleID)
                {
                    x.First = true;
                }
                ccc = x.ArticleID;
            }

            return Page();
        }

        public async Task<IActionResult> LinkTaskAsync()
        {
            List<Link> LList = await _gateway.GetLinks();
            LinkList2 = await _gateway.GetLinks();
            List<Link> apiList = new List<Link>();

            foreach (var x in LList)
            {
                if (x.LinkString.Contains("api") || x.LinkString.Contains("json"))
                {
                    apiList.Add(x);
                }
                else
                {
                    LinkList.Add(x);
                }
            }

            if (apiList != null)
            {
                foreach (var x in apiList)
                {
                    Task<string> apiTask = client.GetStringAsync(x.LinkString);
                    string apiString = await apiTask;
                    string[] stringArray = apiString.Split(',');
                    apiClass aclass = new apiClass();
                    aclass.ArticleID = x.ArticleId;

                    for (int i = 0; i < stringArray.Length; i++)
                    {
                        if (!stringArray[i].Contains("null"))
                        {
                            if (stringArray[i].Contains(".png") || stringArray[i].Contains(".jpg"))
                            {
                                string aString = "";
                                string[] aStringArray = new string[3];
                                aString = stringArray[i];
                                aStringArray = aString.Split('"');
                                aclass.imgString = aStringArray[3];
                            }
                            else
                            {
                                string ddd = stringArray[i];
                                aclass.ApiStringList.Add(ddd);
                            }
                        }
                    }
                    apiClassList.Add(aclass);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OtherArticleTasksAsync()
        {
            var myart = await _gateway.GetArticles();
            OtherArticleList = await _gateway.GetOtherArticles();
            OtherArticleList = OtherArticleList.Where(x => CurrentPage.Id == x.PageId).ToList();
            
            foreach (var x in myart)
            {
                if (x.ImgDataBack != null)
                {
                    string imageBase64Data = Convert.ToBase64String(x.ImgDataBack);
                    string ImageUrl = string.Format($"data:image/jpg;base64, {imageBase64Data}");
                    string imageName = imageBase64Data;
                    imageClass aaa = new imageClass();
                    aaa.Data = ImageUrl;
                    aaa.ArticleID = x.Id;
                    oaImageClassList.Add(aaa);
                }
            }
            return Page();
        }

        public async Task<IActionResult> PageTasksAsync()
        {
            cPages = await _gateway.GetCreatedPages();
            cPages = cPages.OrderBy(x => x.Order).ToList();

            if (cPages.Count < 1)
            {
                CreatedPage newpage = new CreatedPage();
                newpage.Title = "Home";
                newpage.TimeStamp = DateTime.Now;

                await _gateway.PostCreatedPage(newpage);
            }

            if (String.IsNullOrEmpty(SelectedPage))
            {
                if (cPages.Count > 0)
                {
                    CurrentPage = cPages.First();
                    SelectedPage = CurrentPage.Title;
                }
            }
            else
            {
                foreach (var x in cPages)
                {
                    if (x.Title == SelectedPage)
                    {
                        OrderID = x.Id;
                        CurrentPage = cPages.FirstOrDefault(z => z.Id == x.Id);
                    }
                }
                if (isAdmin == false && isSkribent == false && String.IsNullOrEmpty(SelectedPage) == false)
                {
                    CurrentPage.Count++;
                    await OnPostEditPageAsync();
                }
            }
            return Page();
        }
    }

    public class imageClass
    {
        public string Data { get; set; }
        public long ArticleID { get; set; }
        public bool First { get; set; }
        public long imgID { get; set; }
        public long OAId { get; set; }
    }

    public class pdfDoc
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Byte { get; set; }
        public long ArticleID { get; set; }
    }

    public class apiClass
    {
        public long ArticleID { get; set; }
        public List<string> ApiStringList = new List<string>();
        public string imgString { get; set; }
    }
}
