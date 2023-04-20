using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using ProtoBuf;
using ReactiveUI;

namespace PromtStorage.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private Bitmap _bitmap;
    
    public Bitmap Bitmap
    {
        get => _bitmap;
        set => this.RaiseAndSetIfChanged(ref _bitmap, value);
    }
    public async Task Check()
    {
        var formatsAsync = await Application.Current.Clipboard.GetFormatsAsync();
        var obj = (await Application.Current.Clipboard.GetDataAsync("image/jpeg"));
        
        var ms = new MemoryStream();
        Serializer.Serialize(ms, obj);
        byte[] data = ms.ToArray().Skip(4).ToArray();
        ms = new MemoryStream(data);
        try
        {
            File.WriteAllBytes("test.jpg", ms.ToArray());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        try
        {
            Bitmap bmp = new Bitmap(ms);
            Bitmap = bmp;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
      
    }
}