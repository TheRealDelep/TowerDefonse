using System.Linq;
using Godot;

public class Pool<T> where T : Node3D, IPoolable
{
    private T[] items;

    public Pool(string pathToRes, Node parent, int size) 
    {
        var packedScene = ResourceLoader.Load<PackedScene>(pathToRes);
        items = new T[size];
        for (int i = 0; i < size; i++)
        {  
            var item = packedScene.Instantiate<T>();
            parent.AddChild(item);
            item.IsActive = false;
            items[i] = item;
        }        
    }
    
    public T GetOne() 
    {
        var item = items.First(i => !i.IsActive);
        item.IsActive = true;
        return item;
    }
}

public interface IPoolable 
{
    bool IsActive { get; set; }
}