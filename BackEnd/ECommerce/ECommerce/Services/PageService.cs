using AutoMapper;
using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class PageService : IPageService
    {
        private readonly IIndexPageRepository _indexPageRepository;
        private readonly IMapper _mapper;
        public PageService(IIndexPageRepository indexPageRepository, IMapper mapper)
        {
            _indexPageRepository = indexPageRepository;
            _mapper = mapper;
        }
        public async Task<List<PageDto>> GetPageInfoAsync()
        {
            var response = await _indexPageRepository.GetPageInfoAsync();
            if (response != null && response?.Count > 0)
            {
                var filtersResponse = await _indexPageRepository.GetAllFilters();
                foreach (var page in response)
                {
                    //page.Filters = new List<FilterDto>();
                    var filterBy = filtersResponse.Where(f => page.FilterById != null 
                                        && page.FilterById.Split(',').Select(id => int.Parse(id.Trim())).Contains(f.Id));
                    if (filterBy != null && filterBy?.Count() > 0)
                    {
                        var filterMap = _mapper.Map<IEnumerable<FilterDto>>(filterBy);
                        page.Filters.AddRange(filterMap);
                    }                    
                }                
            }
            return response;           
        }
    }
}
