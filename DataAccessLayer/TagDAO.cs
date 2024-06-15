using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TagDAO
    {
        public static List<Tag> getTags()
        {
            List<Tag> tagsList = new List<Tag>();
            try
            {
                using var context = new FunewsManagementDbContext();
                tagsList = context.Tags.ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tagsList;
        }

        public static void SaveTag(Tag tag)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Tags.Add(tag);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateTag(Tag tag)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Entry<Tag>(tag).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteTag(Tag tag)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                var p1 = context.Tags.SingleOrDefault(c => c.TagId == tag.TagId);
                context.Tags.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Tag GetTagById(int id)
        {
            using var context = new FunewsManagementDbContext();
            return context.Tags.SingleOrDefault(c => c.TagId.Equals(id));
        }

    }
}
