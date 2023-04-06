namespace w_escolas.Shared;

public class PageDto<T>
{
    public int Count { get; set; }
    public IEnumerable<T>? Data { get; set; }
}
