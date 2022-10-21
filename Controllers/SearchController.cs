using Microsoft.AspNetCore.Mvc;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using KavaaElasticSearch.Models;

namespace KavaaElasticSearch.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : Controller {
	private readonly ElasticsearchClient _client;

	public SearchController() {
        var credentials = new ApiKey("OXZCX19JTUJOUTBsSVZHM0hBTE86Y1FVNEZ3cjRTYVNHOXBrUi1RNjY0UQ=="); 
        var pool = new CloudNodePool("KavaaElasticSearch:dXMtY2VudHJhbDEuZ2NwLmNsb3VkLmVzLmlvJDRkMTc5YTFkODJiNjRmMGY5NmUyNGI4OWJjYTQ1ZTU2JGRhODVlZTdhNjY1MDQ1MGNhM2UwYTg0NmQ3ZDAyM2U0", credentials);
        var settings = new ElasticsearchClientSettings(pool)
            .DefaultMappingFor<Student>(i => i
            .IndexName("students_idx")
            .IdProperty(s => s.sid)
        ).DefaultMappingFor<Teacher>(i => i
            .IndexName("teachers_idx")
            .IdProperty(t => t.tid)
        )
        .EnableDebugMode()
        .PrettyJson()
        .RequestTimeout(TimeSpan.FromMinutes(2));
        _client = new ElasticsearchClient(settings);
    }

	[HttpGet]
	public JsonResult Search(string query) {
        var studentsResponse = _client.Search<Student>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.name)
                    .Query(query)                    
                )
            )
        );  
        var teachersResponse = _client.Search<Teacher>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.name)
                    .Query(query)                    
                )
            )
        ); 
        var students = studentsResponse.Documents.ToList(); 
        var teachers = teachersResponse.Documents.ToList();
        List<object> results =  students.Cast<object>().Concat(teachers).ToList();
		return Json(results); 
	}
}
