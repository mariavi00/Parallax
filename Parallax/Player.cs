using FFImageLoading.Maui;

namespace Parallax;
public delegate void Callback();
public class Player:Animacao
{
    public Player(CachedImageView a):base(a)
    {
        for (int i = 1; i <=13;++i)
        animacao1.Add($"andar{i.ToString("D2")}.png");
        for (int i = 1; i <=25; ++i)
        animacao2.Add($"morrer{i.ToString("D2")}.png");
        SetanimacaoAtiva(1);
    }
    public void Die()
    {
        loop=false;
        SetanimacaoAtiva(2);
    }
    public void Run()
    {
        loop=true;
        SetanimacaoAtiva(1);
        Play();
    }
}