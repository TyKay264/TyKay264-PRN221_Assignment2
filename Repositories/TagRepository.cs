using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TagRepository : ITagRepository
    {
        public void DeleteTag(Tag tag)
        {
            TagDAO.DeleteTag(tag);
        }

        public List<Tag> GetTags()
        {
            return TagDAO.getTags();
        }

        public void SaveTag(Tag tag)
        {
            TagDAO.SaveTag(tag);
        }

        public void UpdateTag(Tag tag)
        {
            TagDAO.UpdateTag(tag);
        }
        public Tag GetTagById(int id)
        {
            return TagDAO.GetTagById(id);
        }
    }
}
