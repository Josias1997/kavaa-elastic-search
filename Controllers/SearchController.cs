using Microsoft.AspNetCore.Mvc;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using KavaaElasticSearch.Models;

namespace KavaaElasticSearch.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : Controller {
	private readonly ElasticsearchClient client;

	public SearchController() {
        var credentials = new ApiKey("dUNlLTBJSUIxcHFwQTgwclB5ckM6MHl2TUswT0FSNENFNTdtRW1HSFB6dw=="); 
        var pool = new CloudNodePool("KavaaElasticSearch:dXMtY2VudHJhbDEuZ2NwLmNsb3VkLmVzLmlvJDg3ZWE3NzI4MDVlYTRjY2I5NWQ1NzVhOTM3YzA1N2QxJDY0ZjAyZWRjMjYzYjQ4YWM5YmZlNjQ5OWFhYWRhMDdl", credentials);
        var settings = new ElasticsearchClientSettings(pool)
            .DefaultMappingFor<Student>(i => i
            .IndexName("studies_idx")
            .IdProperty(s => s.sid)
        ).DefaultMappingFor<Teacher>(i => i
            .IndexName("teachers_idx")
            .IdProperty(t => t.tid)
        )
        .EnableDebugMode()
        .PrettyJson()
        .RequestTimeout(TimeSpan.FromMinutes(2));
        this.client = new ElasticsearchClient(settings);
    }

	[HttpGet]
	public JsonResult Search(string query) {
        var studentsResponse = this.client.Search<Student>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.name)
                    .Query(query)                    
                )
            )
        );  
        var teachersResponse = this.client.Search<Teacher>(s => s
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
