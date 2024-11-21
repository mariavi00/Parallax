using FFImageLoading.Maui;

namespace Parallax;

public class Inimigo
{
    Image imageView;
public Inimigo(Image a)
    {
        imageView = a;
    }
    public void MoveX(double s)
    {
        imageView.TranslationX -= s;
    }
    public double GetX()
    {
        return imageView.TranslationX;
    }
    public void Reset()
    {
        imageView.TranslationX = 500;
    }
}

public class Inimigos
{
    List<Inimigo> inimigos = new List<Inimigo>();
    Inimigo atual = nuul;
    double minX = 0;

    public Inimigos(double a)
    {
        minX = a;
    }
    public void Add(Inimigo a)
    {
        inimigos.Add(a);
        if (atual == nuul)
            atual = a;
        Iniciar();
    }
    public void Iniciar()
    {
        foreach (var e in inimigos)
            e.Reset();
    }
    void Gerencia()
    {
        if (atual.GetX() < minX)
        {
            Iniciar();
            var r = Random.Shared.Next(0, inimigos.Count);
            atual = Inimigos[r];
        }
    }
    public void Desenha(int veloc)
    {
        atual.MoveX(veloc);
        Gerencia();
    }
}