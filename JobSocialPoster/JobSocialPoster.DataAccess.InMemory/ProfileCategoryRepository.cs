using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using JobSocialPoster.Core.Models;

namespace JobSocialPoster.DataAccess.InMemory
{
    public class ProfileCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProfileCategory> profileCategories;

        public ProfileCategoryRepository()
        {
            profileCategories = cache["profileCategories"] as List<ProfileCategory>;
            if (profileCategories == null)
            {
                profileCategories = new List<ProfileCategory>();
            }
        }

        public void Commit()
        {
            cache["profileCategories"] = profileCategories;
        }

        public void Insert(ProfileCategory p)
        {
            profileCategories.Add(p);
        }
        public void Update(ProfileCategory profileCategory)
        {
            ProfileCategory profileCategoryToUpdate = profileCategories.Find(p => p.Id == profileCategory.Id);

            if (profileCategoryToUpdate != null)
            {
                profileCategoryToUpdate = profileCategory;
            }
            else
            {
                throw new Exception("Profil nieznaleziony");
            }

        }
        public ProfileCategory Find(string Id)
        {
            ProfileCategory profileCategory = profileCategories.Find(p => p.Id == Id);

            if (profileCategory != null)
            {
                return profileCategory;
            }
            else
            {
                throw new Exception("Profil nieznaleziony");
            }
        }

        public IQueryable<ProfileCategory> Collection()
        {
            return profileCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProfileCategory profileToDelete = profileCategories.Find(p => p.Id == Id);

            if (profileToDelete != null)
            {
                profileCategories.Remove(profileToDelete);
            }
            else
            {
                throw new Exception("Kategoria profilu nieznaleziona");
            }

        }
    }
}