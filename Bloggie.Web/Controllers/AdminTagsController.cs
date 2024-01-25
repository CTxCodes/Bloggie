using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        
        private readonly ITagRepository tagRepository;

        //Constructor injection to use DbContext to save to database
        public AdminTagsController(ITagRepository tagRepository)
        {
            
            this.tagRepository = tagRepository;
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //creating the functionality to submit the information to create a new Tag

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            ValidateAddTagRequest(addTagRequest);
            if(ModelState.IsValid == false)
            {
                return View();
            }

            // Mapping AddTagRequest to Tag Domain model 
                var tag = new Tag
            {
                
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Use dbContext to read the tags from the dtabase
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

           
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {

                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);

            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
                Id = editTagRequest.Id
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if(updatedTag != null)
            {
                //Show success notificatoin
            } 
            else
            {
                //Show error notification
            }

            //Show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if(deletedTag != null)
            {
                //Show success notification
                return RedirectToAction("List");
            }

            //Show an error notification
            return RedirectToAction("Edit", new {id = editTagRequest.Id});


        }

        private void ValidateAddTagRequest(AddTagRequest request)
        {
            if(request?.Name is not null && request.DisplayName is not null )
            {
                if (request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }

       
    }
}
