namespace GeneralElementsUI.Utils;

public class Filter<T>(Filter<T>.Modes mode)
{
    public List<IFilterCategory<T>> Categories { get; set; } = new();
    
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

public interface IFilterCategory<T>
{
    public string Title { get; set; }
    public bool IsEnable { get; set; }

    public abstract bool CheckAccept(T obj);
}