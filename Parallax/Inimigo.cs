using FFImageLoading.Maui;

namespace Parallax;

public class Inimigo
{
    Image imageView;
    public Inimigo(Image a)
    {
        imageView=a;
    }
    public void MoveX(double s)
    {
        imageView.TranslationX-=s;
    }
    public double GetX()
    {
        return imageView.TranslationX;
    }
    public void Reset()
    {
        imageView.TranslationX=500;
    }
}

public class Inimigos
{
    List<Inimigo> inimigos= new List<Inimigo>();
    Inimigo atual=nuul;
    double minX=0;
    
    public Inimigos(double a)
    {
        minX=a;
    }
    public void Add(Inimigo a)
    {
    
    }
}