using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using JsonData;
using Microsoft.AspNetCore.Http;

namespace MusicApi.Controllers {

    
    public class ArtistController : Controller {

        private List<Artist> allArtists;

        public ArtistController() {
            allArtists = JsonToFile<Artist>.ReadJson();
        }

        //This method is shown to the user navigating to the default API domain name
        //It just display some basic information on how this API functions
        [Route("")]
        [HttpGet]
        public string Index() {
            //String describing the API functionality
            string instructions = "Welcome to the Music API~~\n========================\n";
            instructions += "    Use the route /artists/ to get artist info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *RealName/{string}\n";
            instructions += "       *Hometown/{string}\n";
            instructions += "       *GroupId/{int}\n\n";
            instructions += "    Use the route /groups/ to get group info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *GroupId/{int}\n";
            instructions += "       *ListArtists=?(true/false)\n";
            return instructions;
        }

        [Route("artists")]
        [HttpGet]
        public List<JsonData.Artist> Show(){
            return allArtists;
        }

        [Route("artists/name/{name}")]
        [HttpGet]
        public List<JsonData.Artist> ArtistName(string name){
            List<JsonData.Artist> allNames = allArtists.Where(thisName => name == thisName.ArtistName).ToList();
            return allNames;
        }

        [Route("artists/realname/{name}")]
        [HttpGet]
        public List<JsonData.Artist> RealName(string name){
            List<JsonData.Artist> allNames = allArtists.Where(thisName => name == thisName.RealName).ToList();
            return allNames;
        }

        [Route("artists/hometown/{town}")]
        [HttpGet]
        public List<JsonData.Artist> Town(string town){
            List<JsonData.Artist> allTown = allArtists.Where(thisArtist => town == thisArtist.Hometown).ToList();
            return allTown;
        }

        [Route("artists/groupid/{id}")]
        [HttpGet]
        public List<JsonData.Artist> ByID(int id){
            List<JsonData.Artist> allWithGroup = allArtists.Where(thisArtist => id == thisArtist.GroupId).ToList();
            return allWithGroup;
        }

    }
}