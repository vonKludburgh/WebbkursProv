using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebbkursProv.Areas.Identity.Data;
using WebbkursProv.Gateway;
using WebbkursProv.Models;

namespace WebbkursProv.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMaxGateway _gateway;

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

        [BindProperty(SupportsGet =true)]
        public List<pdfDoc> pdfList { get; set; }

        [BindProperty]
        public long pdfID { get; set; }

        #endregion Images

        #region Links

        [BindProperty(SupportsGet =true)]
        public List<Link> LinkList { get; set; }

        [BindProperty]
        public Link createdLink { get; set; }

        [BindProperty]
        public long DeleteLinkID { get; set; }

        #endregion Links

        public async Task<IActionResult> OnGetAsync()
        {
            // User

            Roles = _roleManager.Roles.ToList();
            Users = _userManager.Users;

            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser != null)
            {
                isNy = await _userManager.IsInRoleAsync(CurrentUser, "Ny");
                isSkribent = await _userManager.IsInRoleAsync(CurrentUser, "Skribent");
                isAdmin = await _userManager.IsInRoleAsync(CurrentUser, "Admin");
            }

            // Page

            cPages = await _gateway.GetCreatedPages();

            if (String.IsNullOrEmpty(SelectedPage))
            {
                CurrentPage = cPages.First();
                SelectedPage = CurrentPage.Title;
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
            }

            // Article

            var myart = await _gateway.GetArticles();
            Articles = myart.Where(x => x.PageId == CurrentPage.Id).ToList();
            Articles = Articles.OrderBy(x => x.Order).ToList();

            if (ArticleID != 0)
            {
                SelectedArticle = myart.FirstOrDefault(x => x.Id == ArticleID);
            }

            // Image

            Images = await _gateway.GetImages();

            foreach (var x in Images)
            {
                if (x.Title.Contains(".pdf"))
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
                else
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

            // Link

            LinkList = await _gateway.GetLinks();

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
            cPages = await _gateway.GetCreatedPages();
            CreatedPage deletePage = cPages.Find(x => x.Title == PageDeleteID);
            long deleteId = deletePage.Id;
            await _gateway.DeleteCreatedPage(deleteId);
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
            await _gateway.EditArticle(SelectedArticle.Id, SelectedArticle);
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
                byte[] byteArr = doc.ImageData;
                string mimeType = "application/pdf";
                return new FileContentResult(byteArr, mimeType)
                {
                    FileDownloadName = $"Invoice {doc.ImageData}.pdf"
                };
            }
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
    }

    public class imageClass
    {
        public string Data { get; set; }
        public long ArticleID { get; set; }
        public bool First { get; set; }
        public long imgID { get; set; }
    }

    public class pdfDoc
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Byte { get; set; }
        public long ArticleID { get; set; }
    }
}
