using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using JobSocialPoster.Core.Models;


namespace JobSocialPoster.DataAccess.InMemory
{
    public class ProfileRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Profile> profiles;

        public ProfileRepository()
        {
            profiles = cache["profiles"] as List<Profile>;
            if (profiles == null)
            {
                profiles = new List<Profile>();
            }
        }

        public void Commit()
        {
            cache["profiles"] = profiles;
        }

        public void Insert(Profile p)
        {
            profiles.Add(p);
        }
        public void Update(Profile profile)
        {
            Profile profileToUpdate = profiles.Find(p => p.Id == profile.Id);

            if (profileToUpdate != null)
            {
                profileToUpdate = profile;
            }
            else
            {
                throw new Exception("Profil nieznaleziony");
            }

        }
        public Profile Find(string Id)
        {
            Profile profile = profiles.Find(p => p.Id == Id);

            if (profile != null)
            {
                return profile;
            }
            else
            {
                throw new Exception("Profil nieznaleziony");
            }
        }

        public IQueryable<Profile> Collection()
        {
            return profiles.AsQueryable();
        }

        public void Delete(string Id)
        {
            Profile profileToDelete = profiles.Find(p => p.Id == Id);

            if (profileToDelete != null)
            {
                profiles.Remove(profileToDelete);
            }
            else
            {
                throw new Exception("Profil nieznaleziony");
            }
        }
    }

}
