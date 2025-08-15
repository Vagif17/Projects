using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices.JavaScript;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;

namespace CAR_SERVICE_EF_RIDER.Models;

public class Item
{

    [Key] public int Id { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public int Price { get; set; }
    [Required] public DateTime DateTime { get; set; }
    [Required] public byte[] Image { get; set; }
    
    [NotMapped]
    public BitmapImage BitmapImage
    {
// Для удобство в програмировании,что бы потом не приходилось писать конвертацию байтов в картинку в viewmodel,а что бы это сразу было в объекте класса Item     
//Были ошибки,исправил с помощью GPT
        get 
        {
            if (Image == null || Image.Length == 0) return null;

            var bmp = new BitmapImage();
            using (var ms = new MemoryStream(Image))
            {
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = ms;
                bmp.EndInit();
            }
            bmp.Freeze(); // Чтобы можно было использовать в UI
            return bmp;
        }
        set 
        {
            if (value == null)
            {
                Image = null;
                return;
            }

            using (var ms = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(value));
                encoder.Save(ms);
                Image = ms.ToArray();
            }
        }
    } 
    
    [ForeignKey("User")] 
    public int UserId { get; set; }

    public User User { get; set; }

    public Item()
    {
        
    }
    
}