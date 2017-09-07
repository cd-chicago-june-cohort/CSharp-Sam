using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            var info = from artist in Artists
                        where (artist.Hometown == "Mount Vernon")
                        select new {artist.ArtistName, artist.RealName, artist.Age };
                        

            foreach (var item in info) {
                Console.WriteLine(item);
            }
            //Who is the youngest artist in our collection of artists?

            Artist youngster = Artists.OrderBy(artist => artist.Age).First();
            Console.WriteLine($"The youngest artist is {youngster.RealName}, age {youngster.Age}.");

            //Display all artists with 'William' somewhere in their real name

            var williams = from artist in Artists  
                                    where (artist.RealName.Contains("William"))
                                    select new {artist.RealName, artist.ArtistName};

            foreach(var william in williams) {
                Console.WriteLine($"Another William!? Name: {william.RealName} Artist: {william.ArtistName}");
            }

            //Display the 3 oldest artist from Atlanta
            List<Artist> OldAtlanta = Artists.Where(artist => artist.Hometown == "Atlanta").OrderByDescending(artist=> artist.Age).Take(3).ToList();

            foreach(var artist in OldAtlanta){
                Console.WriteLine($"{artist.RealName} is from {artist.Hometown}, is part of the group {artist.ArtistName}, and is {artist.Age} years old.");
            }
            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var notNY = Artists.Where(artist => artist.Hometown != "New York City").Join(Groups, 
                                                artist => artist.GroupId,
                                                group => group.Id,
                                                (artist, group) => {return group.GroupName;}).ToList().Distinct();

            foreach(var group in notNY) {
                Console.WriteLine(group);
            }
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            var wuTang = Groups.Where(group => group.GroupName == "Wu-Tang Clan").Join(
                Artists,
                group => group.Id,
                artist => artist.GroupId,
                (group, artist) => {return new {artist.ArtistName, artist.RealName};}).ToList();

            foreach (var member in wuTang) {
                Console.WriteLine($"Member Name: {member.RealName} || Goes By: {member.ArtistName}");
            }
        }
    }
}
