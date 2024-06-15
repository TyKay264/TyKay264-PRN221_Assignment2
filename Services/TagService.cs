using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository iTagRepository;
        private static int counter = 0;

        public TagService() {
            iTagRepository = new TagRepository();
        }

        public void DeleteTag(Tag tag)
        {
            iTagRepository.DeleteTag(tag);
        }

        public List<Tag> GetTags()
        {
            return iTagRepository.GetTags();
        }

        public void SaveTag(Tag tag)
        {
            int nextId = counter + 1;

            while (iTagRepository.GetTagById(nextId) != null)
            {
                nextId++;

                if (nextId == int.MaxValue)
                {
                    throw new InvalidOperationException("No available ID for Tag");
                }
            }

            tag.TagId = nextId;

            iTagRepository.SaveTag(tag);
        }

        public void UpdateTag(Tag tag)
        {
            iTagRepository.UpdateTag(tag);
        }
        
        public Tag GetTagById(int id)
        {
            return iTagRepository.GetTagById(id);
        }
    }
}
