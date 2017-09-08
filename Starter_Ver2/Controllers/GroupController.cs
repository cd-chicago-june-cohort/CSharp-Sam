using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {
    public class GroupController : Controller {
        List<Group> allGroups {get; set;}
        private List<Artist> allArtists;
        public GroupController() {
            allGroups = JsonToFile<Group>.ReadJson();
            allArtists = JsonToFile<Artist>.ReadJson();
        }


        [Route("groups")]
        [HttpGet]
        public List<JsonData.Group> Show(){
            return allGroups;
        }

        [Route("groups/name/{name}")]
        [HttpGet]
        public List<JsonData.Group> GroupName(string name){
            List<JsonData.Group> allNames = allGroups.Where(thisName => name == thisName.GroupName).ToList();
            return allNames;
        }

        [Route("groups/name/{name}/members")]
        [HttpGet]
        public object ByNameWithMembers(string name){
            List<JsonData.Group> GroupWithName = allGroups.Where(thisGroup => name == thisGroup.GroupName).ToList();
            List<JsonData.Artist> Members = allArtists.Where(thisArtist => GroupWithName[0].Id == thisArtist.GroupId).ToList();
            var WithMembers = new {
                Groups = GroupWithName,
                Artists = Members
            };
            return Json(WithMembers);
        }

        [Route("groups/id/{id}")]
        [HttpGet]
        public List<JsonData.Group> ByID(int id){
            List<JsonData.Group> GroupWithId = allGroups.Where(thisGroup => id == thisGroup.Id).ToList();            
            return GroupWithId;
        }

        [Route("groups/id/{id}/members")]
        [HttpGet]
        public object ByIDWithMembers(int id){
            List<JsonData.Group> GroupWithId = allGroups.Where(thisGroup => id == thisGroup.Id).ToList();
            List<JsonData.Artist> Members = allArtists.Where(thisArtist => id == thisArtist.GroupId).ToList();
            var WithMembers = new {
                Groups = GroupWithId,
                Artists = Members
            };
            return Json(WithMembers);
        }

    }
}