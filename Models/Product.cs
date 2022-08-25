namespace KavaaElasticSearch.Models;

public class Product
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? slug { get; set; }
    public string? short_description { get; set; }
    public string? description { get; set; }
    public double regular_price { get; set; }
    public double? sale_price { get; set; }
    public string? SKU { get; set; }
    public string? stock_status { get; set; }
    public bool featured { get; set; }
    public int quantity { get; set; }
    public string? image { get; set; }
    public string? images { get; set; }
    public int category_id { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}
