namespace GeneralElementsUI.Utils;

public class Filter<T>(Filter<T>.Modes mode)
{
    public List<FilterCategory<T>> Categories { get; set; } = new();
    
    public enum Modes
    {
        And,
        Or
    }

    public Modes Mode { get; set; } = mode;
    
    public bool GetAcceptByCategories(T obj)
    {
        var acceptedCount = 0;
        var enabledCategoriesCount = 0;
        
        foreach (var category in Categories)
        {
            if (category.IsEnable)
            {
                enabledCategoriesCount++;
                if (category.CheckAccept(obj))
                    acceptedCount++;
            }
        }
        
        if(Mode == Modes.And) return acceptedCount == enabledCategoriesCount;
        return acceptedCount > 0 || enabledCategoriesCount == 0;
    }
}

public abstract class FilterCategory<T>(string title, bool isEnable)
{
    public string Title { get; set; } = title;
    public bool IsEnable { get; set; } = isEnable;

    public abstract bool CheckAccept(T obj);
}